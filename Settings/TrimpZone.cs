// <copyright file="TSSZone.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using System.Drawing;

    /// <summary>
    /// Single HR zone with the addition of an intensity factor.
    /// </summary>
    class TrimpZone : INamedLowHighZone
    {
        #region Private Fields

        private static bool modified;
        private float factor;
        private float high;
        private float low;
        private string name;
        private Color color;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TrimpZone class.  TrimpZone is an INamedLowHighZone with the addition of a Trimp factor
        /// </summary>
        /// <param name="zone">INamedLowHighZone</param>
        internal TrimpZone(INamedLowHighZone zone)
        {
            this.Add(zone);
        }

        /// <summary>
        /// Initializes a new instance of the TrimpZone class.  TrimpZone is an INamedLowHighZone with the addition of a Trimp factor
        /// </summary>
        /// <param name="zone">INamedLowHighZone</param>
        /// <param name="factor">Trimp Factor</param>
        internal TrimpZone(INamedLowHighZone zone, float factor)
        {
            this.Add(zone, factor);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets TRIMP Zone factor
        /// </summary>
        public float Factor
        {
            get
            {
                if (float.IsNaN(this.factor))
                    return 0;

                return this.factor;
            }

            set
            {
                modified = true;
                this.factor = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether any settings have been modified.
        /// </summary>
        internal bool Modified
        {
            get { return modified; }
            set { modified = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculate Factor for zone based on HR algorithms including HRR and HRMax, and the HR zone high/low range.
        /// </summary>
        /// <param name="zone">Zone used to calculate factor</param>
        /// <param name="date">Date on which to find defualt factor.  
        /// Collects Max/Rest HR data from athlete on this date to calculate default factor.</param>
        /// <returns>Returns TRIMP zone factor, or 0 if below 0.2</returns>
        internal static float GetDefaultZoneFactor(float avgZoneHR, float restAthleteHR, float maxAthleteHR)
        {
            double factor, zonePercentHR;
            ILogbook logbook = PluginMain.GetLogbook();

            zonePercentHR = (avgZoneHR - restAthleteHR) / (maxAthleteHR - restAthleteHR); // % of Max HR
            zonePercentHR = Math.Min(zonePercentHR, 100); // Cap HR at 100% for calculation below

            // Mario's equation variables
            double factorA, factorB;
            /* From 'ModeloEntrenamiento total de por vida' spreadsheet:
                        * 
                        * factor = % of Max HR * factorA * EXP(factorB * % of Max HR)
                        * factorA & B are male/female dependant constants:
                        *   Male  : A - 0.64   B - 1.92
                        *   Female: A - 0.86   B - 1.67
                        *  ('Female' constants return factor of ~0.22 higher through all zones)
                        *  
                        *  NOTE: factorA is adjusted (multiplied) by 1.0827 to compensate for Mario 
                        *        using upper HR zone boundary in his calc, and I prefer zone average.
                        */

            // Employ gender specific settings.  Assume 'male' if not specified
            if (logbook.Athlete.Sex == AthleteSex.Female)
            {
                factorA = 0.86 * 1.0827;
                factorB = 1.67;
            }
            else
            {
                factorA = 0.64 * 1.0827;
                factorB = 1.92;
            }

            factor = zonePercentHR * factorA * Math.Exp(factorB * zonePercentHR);

            // Return factor.  Cutoff below 0.2 (typically should clip first HR zones 0 - x)
            if (factor < 0.2)
            {
                factor = 0;
            }

            return (float)factor;
        }

        /// <summary>
        /// Calculate Factor for zone based on HR algorithms including HRR and HRMax, and the HR zone high/low range.
        /// </summary>
        /// <param name="zone">Zone used to calculate factor</param>
        /// <param name="date">Date on which to find defualt factor.  
        /// Collects Max/Rest HR data from athlete on this date to calculate default factor.</param>
        /// <returns>Returns TRIMP zone factor, or 0 if below 0.2</returns>
        internal static float GetDefaultZoneFactor(INamedLowHighZone zone, DateTime date)
        {
            float avgZoneHR, maxAthleteHR, restAthleteHR;
            ILogbook logbook = PluginMain.GetLogbook();

            // Athlete Max and Resting HR
            maxAthleteHR = logbook.Athlete.InfoEntries.LastEntryAsOfDate(date).MaximumHeartRatePerMinute; // Max HR
            if (float.IsNaN(maxAthleteHR))
            {
                maxAthleteHR = 190;
            }

            restAthleteHR = logbook.Athlete.InfoEntries.LastEntryAsOfDate(date).RestingHeartRatePerMinute; // Max HR
            if (float.IsNaN(restAthleteHR))
            {
                restAthleteHR = 63;
            }

            // Average zone HR for this zone
            if (!double.IsInfinity(zone.High))
            {
                avgZoneHR = (zone.High + zone.Low) / 2;
            }
            else
            {
                avgZoneHR = (maxAthleteHR + zone.Low) / 2;
            }

            //factor = Math.Round(factor, 1);
            return GetDefaultZoneFactor(avgZoneHR, restAthleteHR, maxAthleteHR);
        }

        /// <summary>
        /// Gets the lowest avg HR zone value that will return a factor > 0.  
        /// All HR values less than this are considered inactive and will have a 0 factor.
        /// </summary>
        /// <param name="restAthleteHR"></param>
        /// <param name="maxAthleteHR"></param>
        /// <returns>First HR value that produces an active Factor (>0).  Note that this is the avg. 
        /// zone value, which is relevant because Factors are typically calculated using a hi/low range of values.</returns>
        internal static float GetLowActiveZoneHR(float restAthleteHR, float maxAthleteHR)
        {
            float factor;
            for (int hr = (int)restAthleteHR; hr < maxAthleteHR; hr++)
            {
                factor = GetDefaultZoneFactor(hr, restAthleteHR, maxAthleteHR);
                if (0 < factor)
                {
                    return hr;
                }
            }

            return restAthleteHR;
        }

        #endregion

        #region INamedLowHighZone Members

        /// <summary>
        /// Gets or sets High HR value for this zone.
        /// </summary>
        public float High
        {
            get { return this.high; }
            set { this.high = value; }
        }

        /// <summary>
        /// Gets or sets Low HR value for this zone.
        /// </summary>
        public float Low
        {
            get { return this.low; }
            set { this.low = value; }
        }

        /// <summary>
        /// Gets or sets Name of this zone.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or 
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        #endregion

        /// <summary>
        /// Add a SportTracks zone to this Trimp Zone Category.
        /// </summary>
        /// <param name="zone">INamedLowHighZone to add.</param>
        internal void Add(INamedLowHighZone zone)
        {
            this.high = zone.High;
            this.low = zone.Low;
            this.name = zone.Name;
        }

        /// <summary>
        /// Add a SportTracks zone to this Trimp Zone Category.
        /// </summary>
        /// <param name="zone">INamedLowHighZone to add.</param>
        /// <param name="factor">Trimp factor associated with this zone.</param>
        internal void Add(INamedLowHighZone zone, float factor)
        {
            modified = true;
            this.high = zone.High;
            this.low = zone.Low;
            this.name = zone.Name;
            this.factor = factor;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
