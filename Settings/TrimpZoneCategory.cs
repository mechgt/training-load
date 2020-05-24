using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace TrainingLoad.Settings
{
    /// <summary>
    /// Zone category containing several HR zone definitions, with the addition of an intensity factor.
    /// </summary>
    class TrimpZoneCategory : IZoneCategory
    {
        #region Private Fields

        private static bool modified;
        private string id;
        private SortedList<int, TrimpZone> trimpZones;
        float maxHR, restHR;
        private static TrimpZoneCategory autoZone;

        #endregion

        /// <summary>
        /// Initializes a new instance of the TrimpZoneCategory class.  
        /// This is a zone category containing several HR zone definitions, with the addition of an intensity factor.
        /// </summary>
        public TrimpZoneCategory(string refId)
        {
            if (this.trimpZones == null)
            {
                this.trimpZones = new SortedList<int, TrimpZone>();
            }

            this.ReferenceId = refId;
        }

        /// <summary>
        /// Initialize a new default instance of the TrimpZoneCategory class.
        /// This will create an instance with the default HR intnesity factors and 'numZones' equally spaced HR zones.
        /// </summary>
        /// <param name="zones"></param>
        /// <param name="date"></param>
        public TrimpZoneCategory(int numZones, DateTime date)
        {
            if (this.trimpZones == null)
            {
                this.trimpZones = new SortedList<int, TrimpZone>();
            }

            // Set Min/Max HR range
            IAthleteInfoEntry info = PluginMain.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(date);
            this.maxHR = info.MaximumHeartRatePerMinute;
            this.restHR = info.RestingHeartRatePerMinute;

            // Create the default zones
            float low, high, first;
            first = TrimpZone.GetLowActiveZoneHR(restHR, maxHR) - 1;

            // Zero zone
            this.trimpZones.Add(0, new TrimpZone(new NamedLowHighZone("0", 0, first)));

            for (int i = 1; i <= numZones; i++)
            {
                low = ((this.maxHR - first) / numZones * i) + first;
                high = ((this.maxHR - first) / numZones * (i + 1)) + first;
                TrimpZone zone = new TrimpZone(new NamedLowHighZone(i.ToString(), low, high));
                zone.Factor = TrimpZone.GetDefaultZoneFactor(zone, DateTime.Now);

                this.trimpZones.Add(i, zone);
            }

            // Update 0 zone high value
            if (1 < trimpZones.Count)
                trimpZones[0].High = trimpZones[1].Low;
        }

        /// <summary>
        /// Update Intensity Factors in this zone from Athlete logbook data.  Automatically calculates 
        /// all Intensity Factors based on athlete entered max/rest HR data for a particular date.  Since
        /// max/rest HR changes over time, the factors will change over time as well.
        /// If max/rest is detected as unchanged on this date, updates will be skipped.
        /// </summary>
        /// <remarks>Skipping updates is intended to be more efficient since HRrest/HRmax is not 
        /// frequently changed.  This would cause a problem if Factor was manually set elsewhere</remarks>
        /// <param name="date"></param>
        public void UpdateFactors(DateTime date)
        {
            UpdateFactors(date, false);
        }

        /// <summary>
        /// Update Intensity Factors in this zone from Athlete logbook data.  Automatically calculates 
        /// all Intensity Factors based on athlete entered max/rest HR data for a particular date.  Since
        /// max/rest HR changes over time, the factors will change over time as well.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="force">Force update even if max/rest is unchanged</param>
        public void UpdateFactors(DateTime date, bool force)
        {
            // Set Min/Max HR range
            IAthleteInfoEntry info = PluginMain.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(date);
            float max = info.MaximumHeartRatePerMinute;
            float rest = info.RestingHeartRatePerMinute;

            if (!force && this.maxHR == max && this.restHR == rest)
            {
                // Nothing to do.
                return;
            }

            // Update factors for all zones
            for (int i = 0; i < trimpZones.Count; i++)
            {
                trimpZones[i].Factor = TrimpZone.GetDefaultZoneFactor(trimpZones[i], DateTime.Now);
            }
        }

        /// <summary>
        /// Gets or sets a sorted list of INamedLowHighZone with the addition of a Trimp factor.
        /// </summary>
        public SortedList<int, TrimpZone> TrimpZones
        {
            get { return this.trimpZones; }
            set { this.trimpZones = value; }
        }

        /// <summary>
        /// Gets or sets ReferenceID for linking logbook HR zone to TRIMP factor
        /// </summary>
        public string ReferenceId
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether any settings have been modified.
        /// </summary>
        public bool Modified
        {
            get
            {
                foreach (TrimpZone zone in this.trimpZones.Values)
                {
                    if (zone.Modified == true)
                    {
                        modified = true;
                    }
                }

                return modified;
            }

            set
            {
                if (value == false)
                {
                    modified = false;
                    foreach (TrimpZone zone in this.trimpZones.Values)
                    {
                        zone.Modified = false;
                    }
                }
                else
                {
                    modified = value;
                }
            }
        }

        /// <summary>
        /// Get the default automatic zone category.  Used for fully automatic TRIMP calculations.  No user defined category required.
        /// </summary>
        public static TrimpZoneCategory AutoZoneCat
        {
            get
            {
                if (autoZone == null)
                {
                    autoZone = new TrimpZoneCategory(25, DateTime.Now);
                }

                return autoZone;
            }
        }

        #region IZoneCategory Members

        public string Name
        {
            get
            {
                return "DefaultTrimpZone";
            }
            set
            {
                // Not implemented
            }
        }

        /// <summary>
        /// Gets a list of named low high zones associated with this category.
        /// NOTE: This is READ-ONLY, 'Set' is not implemented.
        /// </summary>
        public IList<INamedLowHighZone> Zones
        {
            get
            {
                List<INamedLowHighZone> zones = new List<INamedLowHighZone>();

                foreach (TrimpZone zone in TrimpZones.Values)
                {
                    zones.Add(zone);
                }

                return zones;
            }
            set
            {
                // Not implemented
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
