// <copyright file="TSSActivity.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Diagnostics;
    using TrainingLoad.Settings;
    using TrainingLoad.Data;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Data.Fitness.CustomData;
    using ZoneFiveSoftware.Common.Data.GPS;
    using TrainingLoad.UI;
    using TrainingLoad.UI.View;

    class TrimpActivity : IComparable, IActivity
    {
        #region Fields

        private const double defaultFTP = 230;

        private IActivity activity;
        private int dayIndex;
        private float atl;
        private float ctl;
        private float rollingSum1;
        private float rollingSum2;
        private float tsbPre;

        private static bool localEdit;

        #endregion

        #region Constructors

        public TrimpActivity(IActivity activity)
        {
            this.activity = activity;

            // Add listener for monitoring if activity data has changed (data tracks for instance)
            activity.PropertyChanged += activity_PropertyChanged;

            // Create cached normalized power.  This seems to be the value that takes the longest to calculate.
            double cache = NormalizedPower;
            cache = TRIMP;
            cache = TSS;
        }

        #endregion

        #region Activity Properties

        /// <summary>
        /// DEPRECATED.  DO NOT USE.
        /// Associated root activity
        /// </summary>
        public IActivity Activity
        {
            // TODO: (LOW) Get rid of Activity property since TrimpActivity now is an IActivity itself.
            get { return this.activity; }
        }

        /// <summary>
        /// Gets or sets the ATL for the activity.  ATL is based on surrounding activities, and selected algorithm.
        /// It is not absolute for an activity
        /// </summary>
        public float ATL
        {
            get { return this.atl; }
            set { this.atl = value; }
        }

        /// <summary>
        /// Gets the avg. cadence for the activity
        /// </summary>
        public float AvgCadence
        {
            get { return Info.AverageCadence; }
        }

        /// <summary>
        /// Gets the avg grade for the activity
        /// </summary>
        public float AvgGrade
        {
            get { return Info.AverageGrade; }
        }

        /// <summary>
        /// Gets the average HR for the activity
        /// </summary>
        public float AvgHR
        {
            get { return Info.AverageHeartRate; }
        }

        /// <summary>
        /// Gets the average pace for the activity
        /// </summary>
        public TimeSpan AvgPace
        {
            get
            {
                if (this.AvgSpeed != 0 && !float.IsNaN(this.AvgSpeed))
                {
                    int seconds = (int)(60 * 60 / this.AvgSpeed);
                    return new TimeSpan(0, 0, 0, seconds);
                }
                else
                {
                    return new TimeSpan(0);
                }
            }
        }

        /// <summary>
        /// Gets the average power for the activity
        /// </summary>
        public float AvgPower
        {
            get
            {
                if (this.activity.PowerWattsTrack != null)
                {
                    return Info.AveragePower;
                }
                else
                {
                    return this.activity.AveragePowerWattsEntered;
                }
            }
        }

        /// <summary>
        /// Gets the average speed of the activity in the user's selected speed units
        /// </summary>
        public float AvgSpeed
        {
            get
            {
                double value = Info.AverageSpeedMetersPerSecond; // meter/sec
                value = Length.Convert(value, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits); // (ST Units)/sec
                value = value * 60 * 60; // Convert sec. to hours (mph or km/hr)
                return (float)value;
            }
        }

        /// <summary>
        /// Exponentially weighted Power based activity score
        /// </summary>
        private float BikeScore
        {
            get
            {
                // TODO: (LOW)Calculate BikeScore
                return 0;
            }
        }

        /// <summary>
        /// Gets the total calories burned for the activity as reported by ST.
        /// </summary>
        public int Calories
        {
            get { return (int)this.activity.TotalCalories; }
        }

        /// <summary>
        /// Gets the assigned activity category
        /// </summary>
        public IActivityCategory Category
        {
            get { return this.activity.Category; }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets or sets the CTL for the activity.  CTL is based on surrounding activities, and selected algorithm.
        /// It is not absolute for an activity
        /// </summary>
        public float CTL
        {
            get { return this.ctl; }
            set { this.ctl = value; }
        }

        /// <summary>
        /// Gets the associated day of week for an activity
        /// </summary>
        public DayOfWeek Day
        {
            get { return this.activity.StartTime.DayOfWeek; }
        }

        /// <summary>
        /// Gets the day index for the activity.  This is the number of days since the 'first' activity.
        /// </summary>
        public int DayIndex
        {
            get { return this.dayIndex; }
            set { this.dayIndex = value; }
        }

        /// <summary>
        /// Gets the distance traveled for an activity in the users specified units.
        /// </summary>
        public float Distance
        {
            get
            {
                return (float)Length.Convert(Info.DistanceMeters, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits);
            }
        }

        /// <summary>
        /// Gets the fastest pace for an activity
        /// </summary>
        public TimeSpan FastestPace
        {
            get
            {
                if (FastestSpeed != 0 && !float.IsNaN(FastestSpeed))
                {
                    int seconds = (int)(60 * 60 / FastestSpeed);
                    return new TimeSpan(0, 0, 0, seconds);
                }
                else
                {
                    return new TimeSpan(0);
                }
            }
        }

        /// <summary>
        /// Gets the fastest speed for an activity
        /// </summary>
        public float FastestSpeed
        {
            get
            {
                double value = Info.FastestSpeedMetersPerSecond; // sec/meter
                value = Length.Convert(value, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits); // sec/(ST Units)
                value = value * 60 * 60; // Convert sec. to hours (mph or km/hr)
                return (float)value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not this activity is filtered by category selection.
        /// True = Filtered (hide activity when applicable), False = Show/Include activity
        /// </summary>
        internal bool FilterActivity
        {
            get
            {
                IActivityCategory category = PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter;
                IActivityCategory activityCategory = Info.Activity.Category;

                while (true)
                {
                    if (activityCategory == category)
                    {
                        // Show Activity
                        return false;
                    }
                    else if (activityCategory.Parent != null)
                    {
                        // Keep searching
                        activityCategory = activityCategory.Parent;
                    }
                    else
                    {
                        // Hide Activity
                        return true;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the FTP associated with this activity's date
        /// </summary>
        internal float FTP
        {
            get { return (float)GetFTP(); }
        }

        public SortedList<int, TimeSpan> HRTime
        {
            get
            {
                SortedList<int, TimeSpan> hrTime = new SortedList<int, TimeSpan>();
                int i = 0;

                ZoneCategoryInfo zoneCatInfo;
                if (Settings.UserData.Multizone)
                {
                    zoneCatInfo = Info.HeartRateZoneInfo(activity.Category.HeartRateZone); // Zone category (Standard, Running, Cycling, TrainingHR) info
                }
                else
                {
                    zoneCatInfo = Info.HeartRateZoneInfo(Settings.UserData.SingleZoneCategory);
                }

                foreach (ZoneInfo zoneInfo in zoneCatInfo.Zones)
                {
                    // Find Time in each HR Zone within an activity
                    hrTime.Add(i, zoneInfo.TotalTime);
                    i++;
                }

                return hrTime;
            }
        }

        public ActivityInfo Info
        {
            get
            {
                return ActivityInfoCache.Instance.GetInfo(this.activity);
            }
        }

        /// <summary>
        /// Gets the power based Intensity Factor for an activity.  
        /// Intensity Factor = NormalizedPower / FTP
        /// </summary>
        public float IntensityFactor
        {
            get
            {
                float intensityFactor = (float)(NormalizedPower / FTP);

                if (float.IsNaN(intensityFactor))
                {
                    return 0;
                }

                return intensityFactor;
            }
        }

        public int Intensity
        {
            get { return this.activity.Intensity; }
            set { throw new NotImplementedException(); }
        }

        public string Location
        {
            get { return this.activity.Location; }
            set { throw new NotImplementedException(); }
        }

        public float MaxHR
        {
            get { return Info.MaximumHeartRate; }
        }

        public float MaxPower
        {
            get { return Info.MaximumPower; }
        }

        public string Name
        {
            get { return this.activity.Name; }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the Normalized Power from the power data track for the activity.
        /// </summary>
        public float NormalizedPower
        {
            get
            {
                double? normalizedPower = null;

                ICustomDataFieldDefinition field = NormPwrField;
                if (field == null) return this.AvgPower;

                // 1 - Read from logbook
                if (!Recalculate)
                {
                    normalizedPower = activity.GetCustomDataValue(field) as double?;
                }

                // 2 - Calculate
                INumericTimeDataSeries activityPowerTrack = activity.PowerWattsTrack;

                // TODO: (LOW)Need better way to filter for 'GOVSS' or 'Running' activities
                //if (activity.Category.Name == "Running")
                //{
                //    //activityPowerTrack = GOVSSPowerTrack;
                //}
                //else
                //{
                //    activityPowerTrack = activity.PowerWattsTrack;
                //}

                if ((normalizedPower == null || Recalculate) && activityPowerTrack != null)
                {
                    // Calculate only if necessary
                    float max, min;
                    INumericTimeDataSeries powerTrack;
                    INumericTimeDataSeries calcTrack = new NumericTimeDataSeries();

                    // Smooth the existing Power Track
                    // NOTE: Smoothing is total # of seconds... not on each side seconds (different from ST Smooth method which is # sec on each side)
                    // TODO: (LOW)RemovePausedTimes is returning crazy data (empty tracks)
                    if (PluginMain.GetApplication().SystemPreferences.AnalysisSettings.IncludePaused)
                    {
                        powerTrack = Utilities.Smooth(activityPowerTrack, 30, out min, out max);
                    }
                    else
                    {
                        powerTrack = Utilities.Smooth(Utilities.RemovePausedTimesInTrack(activityPowerTrack, activity), 30, out min, out max);
                    }

                    // Raise smoothed values to 4th power (Training Load NP)
                    for (int sec = 0; sec < powerTrack.TotalElapsedSeconds; sec++)
                    {
                        normalizedPower = Math.Pow(powerTrack.GetInterpolatedValue(powerTrack.StartTime.AddSeconds(sec)).Value, 4);
                        calcTrack.Add(powerTrack.StartTime.AddSeconds(sec), (float)normalizedPower);
                    }

                    // Take the 4th root of the avg value found above (Final step)
                    normalizedPower = Math.Pow(calcTrack.Avg, .25);

                    // Store data in custom field
                    SetCustomDataValue(field, normalizedPower);
                }

                // 3 - Default value (returns 0 if avg power is blank)
                if (normalizedPower == null)
                {
                    normalizedPower = this.Info.AveragePower;
                }

                return (float)normalizedPower;
            }
        }

        /// <summary>
        /// Activity Notes formatted to remove line breaks
        /// </summary>
        public string Notes
        {
            get
            {
                // Replace all line feeds with a space, to make the full notes show in the table.
                return this.activity.Notes.Replace("\r\n", " ");
            }
            set { throw new NotImplementedException(); }
        }

        public string ReferenceId
        {
            get { return this.activity.ReferenceId; }
        }

        public float RollingSum1
        {
            get { return this.rollingSum1; }
            set { this.rollingSum1 = value; }
        }

        public float RollingSum2
        {
            get { return this.rollingSum2; }
            set { this.rollingSum2 = value; }
        }

        /// <summary>
        /// Gets the 'score' based on the current chart type (TSS or TRIMP)
        /// </summary>
        public float Score
        {
            get
            {
                float score = 0;

                switch (ChartData.TLChartBasis)
                {
                    case Common.ChartBasis.Power:
                        score = this.TSS;
                        break;
                    case Common.ChartBasis.HR:
                        score = this.TRIMP;
                        break;
                }

                return score;
            }
        }

        /// <summary>
        /// Start time (local time to when/where the activity occurred)
        /// </summary>
        public DateTime StartTime
        {
            get { return this.activity.StartTime.Add(activity.TimeZoneUtcOffset); }
            set { throw new NotImplementedException(); }
        }

        public TimeSpan TotalTime
        {
            get { return Info.Time; }
        }

        public float TRIMP
        {
            get
            {
                double? trimp = null;

                // #1 - Read from logbook
                if (!Recalculate)
                {
                    trimp = activity.GetCustomDataValue(TrimpField) as double?;
                }

                // #2 - If still no trimp, calculate it
                if (trimp == null || Recalculate || double.IsNaN((double)trimp))
                {
                    trimp = GetTrimp(activity);

                    // Store value if available
                    SetCustomDataValue(TrimpField, trimp);
                }

                // Return trimp or 0
                if (trimp != null && !double.IsNaN((double)trimp) && !double.IsInfinity((double)trimp))
                {
                    return (float)trimp;
                }
                else
                {
                    return 0;
                }
            }
        }

        public float TSBPost
        {
            get { return this.ctl - this.atl; }
        }

        public float TSBPre
        {
            get { return this.tsbPre; }
            set { this.tsbPre = value; }
        }

        public float TSS
        {
            get
            {
                double? tss = null;

                ICustomDataFieldDefinition field = TSSField;
                if (field == null) return 0;

                // #1 - Read from logbook
                if (!Recalculate)
                {
                    tss = activity.GetCustomDataValue(field) as double?;

                    if (tss != null)
                    {
                        return (float)tss;
                    }
                }

                // 2 - Calculate TSS entry
                tss = GetTSS();

                // If FTP is not defined, it returns NaN.  TSS cannot be NaN.
                if (tss != null && tss != double.NaN && tss != 0)
                {
                    SetCustomDataValue(field, tss);
                    return (float)tss;
                }
                else
                {
                    return 0;
                }
            }
        }

        public float TotalAscend
        {
            get
            {
                float totalAscend;

                // Elevation Data
                totalAscend = (float)Info.TotalAscendingMeters(PluginMain.GetApplication().Logbook.ClimbZones[0]);

                if (totalAscend == 0 || float.IsNaN(totalAscend))
                {
                    totalAscend = this.activity.TotalAscendMetersEntered;
                }

                return (float)Length.Convert(totalAscend, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.ElevationUnits);
            }
        }

        public float TotalDescend
        {
            get
            {
                float totalDescend;

                // Elevation Data
                totalDescend = (float)Info.TotalDescendingMeters(PluginMain.GetApplication().Logbook.ClimbZones[0]);

                if (totalDescend == 0 || float.IsNaN(totalDescend))
                {
                    totalDescend = this.activity.TotalDescendMetersEntered;
                }

                return (float)Length.Convert(totalDescend, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.ElevationUnits);
            }
        }

        public float VariabilityIndex
        {
            get
            {
                float np = (float)NormalizedPower;
                float ap = AvgPower;
                if (ap == 0)
                {
                    return 0;
                }
                else
                {
                    return np / ap;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating work: Work (in kJ) = Avg Power * duration (seconds) / 1000
        /// </summary>
        public float Work
        {
            get
            {
                return this.AvgPower * (float)this.TotalTime.TotalSeconds / 1000f;
            }
        }


        #endregion

        #region Utility Properties

        public static bool Recalculate { get; set; }

        /// <summary>
        /// Boolean indicating Training Load is changing some data and not to continually refresh screen
        /// </summary>
        internal static bool LocalEdit
        {
            get { return localEdit; }
            set { localEdit = value; }
        }

        #endregion

        #region CustomField Properties

        internal static ICustomDataFieldDefinition DanielsPointsField
        {
            get
            {
                return CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.DanielsPoints, false);
            }
        }

        internal static ICustomDataFieldDefinition TrimpField
        {
            get
            {
                return CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.Trimp, UserData.EnableTRIMP);
            }
        }

        internal static ICustomDataFieldDefinition TSSField
        {
            get
            {
                return CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.TSS, UserData.EnableTSS);
            }
        }

        internal static ICustomDataFieldDefinition NormPwrField
        {
            get
            {
                return CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.NormPwr, UserData.EnableNormPwr);
            }
        }

        internal static ICustomDataFieldDefinition FTPCycleField
        {
            get
            {
                return CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.FTPcycle, UserData.EnableFTP);
            }
        }

        internal static ICustomDataFieldDefinition FTPRunningField
        {
            get
            {
                return CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.FTPrun, UserData.EnableFTP & PowerRunnerPlugin.IsInstalled);
            }
        }

        internal static ICustomDataFieldDefinition VDOTField
        {
            get
            {
                // TODO: (LOW)VDOT field settings
                return CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.VDOT, false);
            }
        }

        #endregion

        #region Public/Private Methods

        /// <summary>
        /// Gets the custom formatted text with proper decimal places, etc.
        /// </summary>
        /// <param name="column">Column defines the property value to get</param>
        /// <returns>Returns custom formatted string ready for display to user.</returns>
        public string GetFormattedText(string Id)
        {
            return GetFormattedText(this, Id);
        }

        /// <summary>
        /// Gets the custom formatted text with proper decimal places, etc.
        /// </summary>
        /// <param name="activity">Activity containing value</param>
        /// <param name="column">Column defines the property value to get</param>
        /// <returns>Returns custom formatted string ready for display to user.</returns>
        public static string GetFormattedText(TrimpActivity activity, string Id)
        {
            Type activityType = typeof(TrimpActivity);  // Used to collect property value from activity
            string text = null;                         // text to display in cell (if defined)

            // Create custom display text
            if (Id == "AvgCadence" ||
                Id == "FastestSpeed" ||
                Id == "AvgSpeed")
            {
                // 320.0, -320.0, 0.0
                float value = (float)activityType.GetProperty(Id).GetValue(activity, null);

                if (float.IsNaN(value) || value == 0)
                {
                    text = string.Empty;
                }
                else
                {
                    text = value.ToString("0.0", CultureInfo.CurrentCulture);
                }
            }
            else if (Id == "ATL" ||
                Id == "CTL" ||
                Id == "TRIMP" ||
                Id == "TSS" ||
                Id == "TSBPre" ||
                Id == "TSBPost" ||
                Id == "Work")
            {
                // 320, -320, 0
                float value = (float)activityType.GetProperty(Id).GetValue(activity, null);

                if (float.IsNaN(value))
                {
                    text = string.Empty;
                }
                else
                {
                    text = value.ToString("0.", CultureInfo.CurrentCulture);
                }
            }
            else if (Id == "IntensityFactor" ||
                Id == "Distance")
            {
                // 3.20, -3.20, 0.00
                float value = (float)activityType.GetProperty(Id).GetValue(activity, null);

                if (value == 0 || float.IsNaN(value))
                {
                    text = string.Empty;
                }
                else
                {
                    text = value.ToString("0.00", CultureInfo.CurrentCulture);
                }
            }
            else if (Id == "AvgPower" ||
                Id == "NormalizedPower" ||
                Id == "MaxHR" ||
                Id == "MaxPower")
            {
                // 320, -320
                float value = (float)activityType.GetProperty(Id).GetValue(activity, null);

                if (float.IsNaN(value))
                {
                    text = string.Empty;
                }
                else
                {
                    text = value.ToString("#.", CultureInfo.CurrentCulture);
                }
            }
            else if (Id == "TotalAscend" ||
                Id == "TotalDescend")
            {
                // +320, -320
                float value = (float)activityType.GetProperty(Id).GetValue(activity, null);

                if (value > 0)
                {
                    text = value.ToString("+#.", CultureInfo.CurrentCulture);
                }
                else
                {
                    text = value.ToString("#.", CultureInfo.CurrentCulture);
                }
            }
            else if (Id == "FastestPace" ||
                Id == "TotalTime" ||
                Id == "AvgPace")
            {
                // 1:23:09
                TimeSpan value = (TimeSpan)activityType.GetProperty(Id).GetValue(activity, null);

                if (value.TotalSeconds == 0)
                {
                    text = string.Empty;
                }
                else
                {
                    text = Utilities.ToTimeString(value);
                }
            }
            else if (Id == "RollingSum1")
            {
                // (various)
                float value = (float)activityType.GetProperty(Id).GetValue(activity, null);

                if (TrainingLoad.Settings.UserData.MovingSumCat1 == (int)Settings.UserData.SumCat.Time)
                {
                    text = Utilities.ToTimeString(new TimeSpan(0, 0, (int)value));
                }
                else
                {
                    text = value.ToString("0.", CultureInfo.CurrentCulture);
                }
            }
            else if (Id == "RollingSum2")
            {
                // (various)
                float value = (float)activityType.GetProperty(Id).GetValue(activity, null);

                if (TrainingLoad.Settings.UserData.MovingSumCat2 == (int)Settings.UserData.SumCat.Time)
                {
                    text = Utilities.ToTimeString(new TimeSpan(0, 0, (int)value));
                }
                else
                {
                    text = value.ToString("0.", CultureInfo.CurrentCulture);
                }
            }
            else if (Id == "Category")
            {
                // Cycling: Road
                ZoneFiveSoftware.Common.Data.Fitness.IActivityCategory category = activity.Category;

                string catString = category.Name;
                if (category.Parent != null)
                {
                    while (category.Parent.Parent != null)
                    {
                        category = category.Parent;
                        catString = category.Name + ": " + catString;
                    }
                }

                text = catString;
            }
            else if (Id == "AvgHR")
            {
                // 123 (57 %)
                DateTime date = activity.StartTime.Date;
                float maxHR = float.NaN, HRR = float.NaN;

                do
                {
                    IAthleteInfoEntry info = PluginMain.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(date);
                    if (float.IsNaN(maxHR))
                        maxHR = info.MaximumHeartRatePerMinute;
                    if (float.IsNaN(HRR))
                        HRR = info.RestingHeartRatePerMinute;

                    date = date.AddDays(1);
                }
                while ((float.IsNaN(maxHR) || float.IsNaN(HRR)) && date < DateTime.Now.Date);

                // Calculate HR percent portion of text: (nn%)
                float percent;
                switch (PluginMain.GetApplication().SystemPreferences.AnalysisSettings.HeartRatePercentType)
                {
                    case HeartRate.PercentTypes.PercentOfMax:
                        percent = activity.AvgHR / maxHR;
                        break;

                    case HeartRate.PercentTypes.PercentOfReserve:
                        percent = (activity.AvgHR - HRR) / (maxHR - HRR);
                        break;

                    default:
                        percent = 0;
                        break;
                }

                if (activity.AvgHR == 0 || float.IsNaN(activity.AvgHR))
                {
                    text = string.Empty;
                }
                else if (float.IsNaN(percent) || float.IsInfinity(percent))
                {
                    percent = 0;
                    text = activity.AvgHR.ToString("#", CultureInfo.CurrentCulture) + " (" + percent.ToString("0%", CultureInfo.CurrentCulture) + ")";
                }
                else
                {
                    text = activity.AvgHR.ToString("#", CultureInfo.CurrentCulture) + " (" + percent.ToString("0%", CultureInfo.CurrentCulture) + ")";
                }
            }
            else if (Id == "AvgGrade")
            {
                // 3.2%, -3.2%, 0.0.%
                float value = (float)activityType.GetProperty(Id).GetValue(activity, null);
                if (float.IsNaN(value))
                {
                    text = string.Empty;
                }
                else
                {
                    text = value.ToString("0.0%", CultureInfo.CurrentCulture);
                }
            }
            else
            {
                // Default
                object value = activityType.GetProperty(Id).GetValue(activity, null);
                text = value.ToString();
            }

            return text;
        }

        /// <summary>
        /// Returns an integer number of days since FirstActivity.
        /// </summary>
        /// <param name="firstActivity">The date of the activity from which to comapre.</param>
        /// <returns>The number of days from the FirstActivity</returns>
        internal int GetDayIndex(DateTime firstActivity)
        {
            return (this.activity.StartTime.ToLocalTime().Date - firstActivity).Days;
        }

        /// <summary>
        /// Get the TRIMP score of an activity according to the Training Load settings
        /// </summary>
        /// <param name="activity">Activity to score</param>
        /// <returns>TRIMP score for an activity</returns>
        private static float GetTrimp(IActivity activity)
        {
            try
            {
                // Declare variables
                int zoneIndex;                                      // Zone (HR zone in this case: Recovery, Tempo, Power, etc.)
                int zoneCount = 0;                                  // Number of TRIMP HR zones
                ILogbook logbook = PluginMain.GetLogbook();         // Loaded logbook
                ZoneInfo zoneInfo;                                  // Info object containing 'time in zone' info
                float trimp = 0;                                    // TRIMP score
                //List<float> iFactors = new List<float>();           // TRIMP scaling factors

                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity); // Current activity
                ZoneCategoryInfo zoneCatInfo = info.HeartRateZoneInfo(activity.Category.HeartRateZone); // Zone category (Standard, Running, Cycling, TrainingHR) info
                TrimpZoneCategory zoneCat;

                // TRIMP priority
                // 1) Manual entry in Notes field.  This is so that user can override bad activities if desired.
                // 2) HR Track.  This is the typical method.
                // 3) Manually entered AvgHR and activity time.  A guesstimate is better than nothing at all.

                /***************************************************************
                 *  Return TRIMP from manually entered notes field
                 ****************************************************************/
                trimp = GetTrimp(activity.Notes);

                /***************************************************************
                 * Calculate TRIMP from HR track.  This is the typical method.
                 *  NOTE:  'if...else' is restarted here and valid is checked
                 *         in case of invalid entry above
                 ***************************************************************/
                if (activity.HeartRatePerMinuteTrack != null && float.IsNaN(trimp))
                {
                    // Select Category to calculate from
                    if (UserData.AutoTrimp)
                    {
                        // Automatic zone
                        zoneCat = TrimpZoneCategory.AutoZoneCat;
                        zoneCount = TrimpZoneCategory.AutoZoneCat.Zones.Count;
                        zoneCatInfo = ZoneCategoryInfo.Calculate(info, TrimpZoneCategory.AutoZoneCat, info.SmoothedHeartRateTrack, 1f);
                        zoneCat.UpdateFactors(activity.StartTime);
                    }
                    else if (UserData.Multizone)
                    {
                        // Set params for activity associated HR zone
                        zoneCat = UserData.TrimpZones[activity.Category.HeartRateZone.ReferenceId];
                        zoneCount = activity.Category.HeartRateZone.Zones.Count;
                        zoneCatInfo = info.HeartRateZoneInfo(activity.Category.HeartRateZone);
                    }
                    else
                    {
                        // Single static TRIMP zone used
                        zoneCat = UserData.TrimpZones[UserData.ZoneID];
                        zoneCount = zoneCat.TrimpZones.Values.Count;
                        // NOTE: This step seems to take the longest for whatever reason...
                        zoneCatInfo = info.HeartRateZoneInfo(UserData.SingleZoneCategory);
                    }

                    // Calculate TRIMP - Cumulative zone total starting at zone 1
                    trimp = 0;
                    for (zoneIndex = 0; zoneIndex < zoneCount; zoneIndex++)
                    {
                        // Find Time in each HR Zone within an activity
                        zoneInfo = zoneCatInfo.Zones[zoneIndex];
                        trimp += (float)(zoneInfo.TotalTime.TotalSeconds * zoneCat.TrimpZones[zoneIndex].Factor / 60);
                    }
                }
                else if (info.Time != null && info.AverageHeartRate > 0 && float.IsNaN(trimp))
                {
                    /***************************************************************
                     * Calculate from manually entered avg HR and activity time
                     ***************************************************************/
                    float factor = 0;
                    if (UserData.AutoTrimp)
                    {
                        zoneCat = TrimpZoneCategory.AutoZoneCat;
                    }
                    else if (UserData.Multizone)
                    {
                        // Set params for activity associated HR zone
                        zoneCat = UserData.TrimpZones[activity.Category.HeartRateZone.ReferenceId];
                    }
                    else
                    {
                        // Set params for single TRIMP zone
                        zoneCat = UserData.TrimpZones[UserData.ZoneID];
                    }

                    // Find AvgHR zone factor
                    foreach (TrainingLoad.Settings.TrimpZone zone in zoneCat.TrimpZones.Values)
                    {
                        if (zone.Low <= info.AverageHeartRate && info.AverageHeartRate < zone.High)
                        {
                            factor = zone.Factor;
                            break;
                        }
                    }

                    // Calculate TRIMP - Activity time * factor for AvgHR Zone factor
                    trimp = (float)(info.Time.TotalSeconds * factor / 60);
                }

                // Do not return NaN
                if (float.IsNaN(trimp))
                    trimp = 0;

                return trimp;
            }
            catch (System.Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// Get TRIMP from a given notes string
        /// </summary>
        /// <param name="notes"></param>
        /// <returns></returns>
        public static float GetTrimp(string notes)
        {
            if (notes.Contains("TRIMP="))
            {
                if (notes.Length < notes.IndexOf("TRIMP=", StringComparison.InvariantCulture) + 7)
                {
                    // Nothing following "TRIMP=".  Invalid entry.
                    return float.NaN;
                }
                else
                {
                    // Pull in max 4 chars following TRIMP=
                    string strTrimp = notes.Substring(notes.IndexOf("TRIMP=", StringComparison.InvariantCulture) + 6, Math.Min(4, notes.Length - (notes.IndexOf("TRIMP=", StringComparison.InvariantCulture) + 6)));

                    // Find last digit
                    int i = 0;
                    while (i < strTrimp.Length)
                    {
                        if (!char.IsDigit(strTrimp, i))
                        {
                            if (i == 0)
                            {
                                // First character is not a digit.  Invalid entry.
                                break;
                            }
                            else
                            {
                                // End of valid entry found
                                strTrimp = strTrimp.Substring(0, i);
                                break;
                            }
                        }

                        i++;
                    }

                    float trimp;
                    if (float.TryParse(strTrimp, NumberStyles.Number, CultureInfo.CurrentCulture, out trimp))
                    {
                        return trimp;
                    }
                }
            }

            return float.NaN;
        }

        /// <summary>
        /// Get the TRIMP score of an activity according to the Training Load settings
        /// </summary>
        /// <param name="activity">Activity to score</param>
        /// <returns>TRIMP score for an activity</returns>
        private float GetTSS()
        {
            float localTss;

            // #1 - Try to read from manual notes entry
            if (activity.Notes.Contains("TSS="))
            {
                if (activity.Notes.Length >= activity.Notes.IndexOf("TSS=", StringComparison.InvariantCulture) + 5)
                {
                    // Pull in max 4 chars following TSS=
                    string strTss = activity.Notes.Substring(activity.Notes.IndexOf("TSS=", StringComparison.InvariantCulture) + 4, Math.Min(4, activity.Notes.Length - (activity.Notes.IndexOf("TSS=", StringComparison.InvariantCulture) + 4)));

                    int i = Math.Min(4, strTss.Length);
                    while (i > 0)
                    {
                        // Read TSS from Notes field
                        float tssNotes;
                        if (float.TryParse(strTss, out tssNotes))
                        {
                            localTss = tssNotes;
                            return localTss;
                        }

                        i--;
                        strTss = strTss.Substring(0, i);
                    }
                }
            }

            // 3 - Calculate TSS
            localTss = (float)(this.IntensityFactor * this.IntensityFactor * this.TotalTime.TotalHours * 100d);
            return localTss;
        }

        internal Shared.Sports Sport
        {
            get
            {
                if (PowerRunnerPlugin.IsRunningCategory(Category))
                    return Shared.Sports.Running;
                else
                    return Shared.Sports.Cycling;
            }
        }

        internal double GetFTP()
        {
            return GetFTP(this.StartTime);
        }

        internal double GetFTP(DateTime date)
        {
            return GetFTP(date, Sport);
        }

        internal static double GetFTP(DateTime date, Shared.Sports sport)
        {
            ICustomDataFieldDefinition ftpDef;
            double? ftp = null;

            switch (sport)
            {
                case Shared.Sports.Running:
                    ftpDef = FTPRunningField;
                    break;

                default:
                case Shared.Sports.Cycling:
                    ftpDef = FTPCycleField;
                    break;
            }

            if (ftpDef != null)
            {
                // NOTE: SportTracks bug here where valid dates aren't returning FTP values (returning NULL) - Reproduce by adding other Athlete value 
                //  and notice that a null FTP value is stored.  TL workaround done several lines below.
                ftp = PluginMain.GetLogbook().Athlete.InfoEntries.LastEntryAsOfDate(date).GetCustomDataValue(ftpDef) as double?;
            }

            if (ftp != null)
            {
                return (double)ftp;
            }
            else
            {
                // Work around ST bug where custom values sometimes return null.
                return GetFTP(date, ftpDef);
            }
        }

        /// <summary>
        /// Used to work around ST's broken custom field issues
        /// </summary>
        /// <param name="date"></param>
        /// <param name="ftpDef"></param>
        /// <returns></returns>
        private static double GetFTP(DateTime date, ICustomDataFieldDefinition ftpDef)
        {
            if (ftpDef == null)
                return defaultFTP;

            TimeSpan closest = TimeSpan.MaxValue;
            double? ftp = null;

            // TODO: Code around broken ST (FTP entries)
            foreach (DateTime current in ChartData.AthleteEntryDates)
            {
                if (current < date && date - current < closest)
                {
                    PluginMain.GetLogbook().Athlete.InfoEntries.EntryForDate(current);
                    double? value = PluginMain.GetLogbook().Athlete.InfoEntries.LastEntryAsOfDate(current).GetCustomDataValue(ftpDef) as double?;
                    if (value != null)
                    {
                        closest = date - current;
                        ftp = value;
                    }
                }
            }

            if (ftp != null)
                return (double)ftp;
            else
                return defaultFTP;
        }

        /// <summary>
        /// Enable or disable monitoring on this specific activity
        /// </summary>
        /// <param name="enable"></param>
        internal void SetActivityMonitoring(bool enable)
        {
            if (enable)
            {
                activity.PropertyChanged += activity_PropertyChanged;
            }
            else
            {
                activity.PropertyChanged -= activity_PropertyChanged;
            }
        }

        #endregion

        #region Event Handlers

        void activity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CustomDataValue" && !LocalEdit)
            {
                ChartData.IsCalculated = false;
            }
            else if (e.PropertyName == "CustomDataValue" ||
                e.PropertyName == "Name" ||
                e.PropertyName == "EquipmentUsed" ||
                e.PropertyName == "Weather" ||
                e.PropertyName == "TotalCalories" ||
                e.PropertyName == "TotalAscendMetersEntered" ||
                e.PropertyName == "TotalDescendMetersEntered")
            {
                // Exclude these changes from causing a recalculation
            }
            else if (false)
            {
                // Fully process these changes
                ChartData.QueueActivityUpdate(activity.ReferenceId);
                ChartData.ProcessActivityUpdateQueue();
            }
            else
            {
                // Partially process these changes (everything else)
                ChartData.QueueActivityUpdate(activity.ReferenceId);
            }

            if (PluginMain.GetApplication().ActiveView.Id == GUIDs.PluginMain && !LocalEdit)
            {
                ViewTrainingLoadPage.Instance.RefreshPage();
            }
        }

        #endregion

        #region IComparable Members

        // Default Sort(): Start date (newest first)
        public int CompareTo(object obj)
        {
            TrimpActivity a = (TrimpActivity)obj;
            return DateTime.Compare(a.activity.StartTime, this.activity.StartTime);
        }

        // Column specific sort
        public int CompareTo(TrimpActivity a2, TrimpActivityComparer.ComparisonType comparisonMethod, TrimpActivityComparer.Order sortOrder)
        {
            int result = 0;

            switch (comparisonMethod)
            {
                // Define all different sort methods
                case TrimpActivityComparer.ComparisonType.AvgPower:
                    if (this.AvgPower != a2.AvgPower)
                    {
                        result = this.AvgPower.CompareTo(a2.AvgPower);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.Category:
                    if (this.Category != a2.Category)
                    {
                        result = this.Category.ToString().CompareTo(a2.Category.ToString());
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.Distance:
                    if (this.Distance != a2.Distance)
                    {
                        result = this.Distance.CompareTo(a2.Distance);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.Location:
                    if (this.Location != a2.Location)
                    {
                        result = this.Location.CompareTo(a2.Location);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.NormalizedPower:
                    if (this.NormalizedPower != a2.NormalizedPower)
                    {
                        result = this.NormalizedPower.CompareTo(a2.NormalizedPower);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.TotalTime:
                    if (this.TotalTime != a2.TotalTime)
                    {
                        result = this.TotalTime.CompareTo(a2.TotalTime);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.TRIMP:
                    if (this.TRIMP != a2.TRIMP)
                    {
                        result = this.TRIMP.CompareTo(a2.TRIMP);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.IntensityFactor:
                    if (this.IntensityFactor != a2.IntensityFactor)
                    {
                        result = this.IntensityFactor.CompareTo(a2.IntensityFactor);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.TSS:
                    if (this.TSS != a2.TSS)
                    {
                        result = this.TSS.CompareTo(a2.TSS);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.TotalAscend:
                    if (this.TotalAscend != a2.TotalAscend)
                    {
                        result = this.TotalAscend.CompareTo(a2.TotalAscend);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.TotalDescend:
                    if (this.TotalDescend != a2.TotalDescend)
                    {
                        result = this.TotalDescend.CompareTo(a2.TotalDescend);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.ATL:
                    if (this.ATL != a2.ATL)
                    {
                        result = this.ATL.CompareTo(a2.ATL);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.CTL:
                    if (this.CTL != a2.CTL)
                    {
                        result = this.CTL.CompareTo(a2.CTL);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.RollingSum1:
                    if (this.rollingSum1 != a2.rollingSum1)
                    {
                        result = this.rollingSum1.CompareTo(a2.rollingSum1);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.RollingSum2:
                    if (this.rollingSum2 != a2.rollingSum2)
                    {
                        result = this.rollingSum2.CompareTo(a2.rollingSum2);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.TSBPre:
                    if (this.TSBPre != a2.TSBPre)
                    {
                        result = this.TSBPre.CompareTo(a2.TSBPre);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.TSBPost:
                    if (this.TSBPost != a2.TSBPost)
                    {
                        result = this.TSBPost.CompareTo(a2.TSBPost);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.AvgPace:
                    if (this.AvgPace != a2.AvgPace)
                    {
                        result = this.AvgPace.CompareTo(a2.AvgPace);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.AvgSpeed:
                    if (this.AvgSpeed != a2.AvgSpeed)
                    {
                        result = this.AvgSpeed.CompareTo(a2.AvgSpeed);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.AvgHR:
                    if (this.AvgHR != a2.AvgHR)
                    {
                        result = this.AvgHR.CompareTo(a2.AvgHR);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.Calories:
                    if (this.Calories != a2.Calories)
                    {
                        result = this.Calories.CompareTo(a2.Calories);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.AvgCadence:
                    if (this.AvgCadence != a2.AvgCadence)
                    {
                        result = this.AvgCadence.CompareTo(a2.AvgCadence);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.AvgGrade:
                    if (this.AvgGrade != a2.AvgGrade)
                    {
                        result = this.AvgGrade.CompareTo(a2.AvgGrade);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.Day:
                    if (this.Day != a2.Day)
                    {
                        result = this.Day.CompareTo(a2.Day);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.FastestPace:
                    if (this.FastestPace != a2.FastestPace)
                    {
                        result = this.FastestPace.CompareTo(a2.FastestPace);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.FastestSpeed:
                    if (this.FastestSpeed != a2.FastestSpeed)
                    {
                        result = this.FastestSpeed.CompareTo(a2.FastestSpeed);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.MaxHR:
                    if (this.MaxHR != a2.MaxHR)
                    {
                        result = this.MaxHR.CompareTo(a2.MaxHR);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.MaxPower:
                    if (this.MaxPower != a2.MaxPower)
                    {
                        result = this.MaxPower.CompareTo(a2.MaxPower);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.Name:
                    if (this.Name != a2.Name)
                    {
                        result = this.Name.CompareTo(a2.Name);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.Notes:
                    if (this.Notes != a2.Notes)
                    {
                        result = this.Notes.CompareTo(a2.Notes);
                    }
                    else
                    {
                        result = StartTime.CompareTo(a2.StartTime);
                    }

                    break;

                case TrimpActivityComparer.ComparisonType.StartTime:
                default:
                    result = StartTime.CompareTo(a2.StartTime);
                    break;
            }

            if (sortOrder == TrimpActivityComparer.Order.Descending)
            {
                return result * -1;
            }
            else
            {
                return result;
            }
        }

        #endregion

        #region TrimpActivityComparer

        internal class TrimpActivityComparer : IComparer<TrimpActivity>
        {
            private ComparisonType comparisonType;
            private Order sortOrder;

            internal enum ComparisonType
            {
                StartTime, TRIMP, NormalizedPower, AvgPower, Location,
                Category, Distance, TotalTime, TotalAscend, TotalDescend,
                TSBPre, TSBPost, ATL, CTL, AvgPace,
                AvgSpeed, AvgHR, Calories, AvgCadence, AvgGrade,
                Day, FastestPace, FastestSpeed, MaxHR, MaxPower, Name, Notes,
                RollingSum1, RollingSum2, IntensityFactor, TSS
            }

            internal enum Order
            {
                Ascending = 1, Descending = 2
            }

            internal ComparisonType ComparisonMethod
            {
                set { this.comparisonType = value; }
            }

            internal Order SortOrder
            {
                set { this.sortOrder = value; }
            }

            #region IComparer<TrimpActivity> Members

            public int Compare(TrimpActivity x, TrimpActivity y)
            {
                Recalculate = false;
                return x.CompareTo(y, this.comparisonType, this.sortOrder);
            }

            #endregion
        }

        #endregion

        #region IActivity Members

        public float AverageCadencePerMinuteEntered
        {
            get { return activity.AverageCadencePerMinuteEntered; }
            set { throw new NotImplementedException(); }
        }

        public float AverageHeartRatePerMinuteEntered
        {
            get { return activity.AverageHeartRatePerMinuteEntered; }
            set { throw new NotImplementedException(); }
        }

        public float AveragePowerWattsEntered
        {
            get { return activity.AveragePowerWattsEntered; }
            set { throw new NotImplementedException(); }
        }

        public INumericTimeDataSeries CadencePerMinuteTrack
        {
            get { return activity.CadencePerMinuteTrack; }
            set { throw new NotImplementedException(); }
        }

        public bool CalcSpeedFromDistanceTrack
        {
            get { return activity.CalcSpeedFromDistanceTrack; }
            set { throw new NotImplementedException(); }
        }

        public IList<float> DistanceMarkersMeters
        {
            get { return activity.DistanceMarkersMeters; }
        }

        public IDistanceDataTrack DistanceMetersTrack
        {
            get { return activity.DistanceMetersTrack; }
            set { throw new NotImplementedException(); }
        }

        public INumericTimeDataSeries ElevationMetersTrack
        {
            get { return activity.ElevationMetersTrack; }
            set { throw new NotImplementedException(); }
        }

        public IList<IEquipmentItem> EquipmentUsed
        {
            get { return activity.EquipmentUsed; }
        }

        public bool HasStartTime
        {
            get { return activity.HasStartTime; }
            set { throw new NotImplementedException(); }
        }

        public INumericTimeDataSeries HeartRatePerMinuteTrack
        {
            get { return activity.HeartRatePerMinuteTrack; }
            set { throw new NotImplementedException(); }
        }

        public IActivityLaps Laps
        {
            get { return activity.Laps; }
        }

        public float MaximumCadencePerMinuteEntered
        {
            get { return activity.MaximumCadencePerMinuteEntered; }
            set { throw new NotImplementedException(); }
        }

        public float MaximumHeartRatePerMinuteEntered
        {
            get { return activity.MaximumHeartRatePerMinuteEntered; }
            set { throw new NotImplementedException(); }
        }

        public float MaximumPowerWattsEntered
        {
            get { return activity.MaximumPowerWattsEntered; }
            set { throw new NotImplementedException(); }
        }

        public INumericTimeDataSeries PowerWattsTrack
        {
            get { return activity.CadencePerMinuteTrack; }
            set { throw new NotImplementedException(); }
        }

        public TimeSpan TimeZoneUtcOffset
        {
            get { return activity.TimeZoneUtcOffset; }
            set { throw new NotImplementedException(); }
        }

        public IValueRangeSeries<DateTime> TimerPauses
        {
            get { throw new NotImplementedException(); }
        }

        public float TotalAscendMetersEntered
        {
            get { return activity.TotalAscendMetersEntered; }
            set { throw new NotImplementedException(); }
        }

        public float TotalCalories
        {
            get { return activity.TotalCalories; }
            set { throw new NotImplementedException(); }
        }

        public float TotalDescendMetersEntered
        {
            get { return activity.TotalDescendMetersEntered; }
            set { throw new NotImplementedException(); }
        }

        public float TotalDistanceMetersEntered
        {
            get { return activity.TotalDistanceMetersEntered; }
            set { throw new NotImplementedException(); }
        }

        public TimeSpan TotalTimeEntered
        {
            get { return activity.TotalTimeEntered; }
            set { throw new NotImplementedException(); }
        }

        public bool UseEnteredData
        {
            get { return activity.UseEnteredData; }
            set { throw new NotImplementedException(); }
        }

        public IWeatherInfo Weather
        {
            get { return activity.Weather; }
        }

        #endregion

        #region IGPSRouteItem Members

        public IGPSRoute GPSRoute
        {
            get { return activity.GPSRoute; }
            set { throw new NotImplementedException(); }
        }

        public IItemMetadata Metadata
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IExtensionData Members

        public byte[] GetExtensionData(Guid id)
        {
            return activity.GetExtensionData(id);
        }

        public string GetExtensionText(Guid id)
        {
            return activity.GetExtensionText(id);
        }

        public void SetExtensionData(Guid id, byte[] data)
        {
            activity.SetExtensionData(id, data);
        }

        public void SetExtensionText(Guid id, string text)
        {
            activity.SetExtensionText(id, text);
        }

        #endregion

        #region ICustomDataValueCollection Members

        public object GetCustomDataValue(ICustomDataFieldDefinition fieldDefinition)
        {
            return GetCustomDataValue(fieldDefinition);
        }

        /// <summary>
        /// Stores a custom value in an activity.  This method makes Training Load aware that it is 
        /// modifying the activity custom value as opposed to an outside source by setting localEdit to true
        /// while the modification is happening.
        /// </summary>
        /// <param name="fieldDefinition">Custom field to set</param>
        /// <param name="value">Value to store</param>
        public void SetCustomDataValue(ICustomDataFieldDefinition fieldDefinition, object value)
        {
            LocalEdit = true;

            if (fieldDefinition != null)
            {
                activity.SetCustomDataValue(fieldDefinition, value);
            }

            LocalEdit = false;
        }

        #endregion

        public override string ToString()
        {
            return StartTime.ToShortDateString();
        }

        #region IActivity Members from newer ST Builds
        
        public INumericTimeDataSeries GroundContactTimeMillisecondsTrack
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public INumericTimeDataSeries LeftPowerPercentTrack
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public INumericTimeDataSeries TemperatureCelsiusTrack
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public INumericTimeDataSeries VerticalOscillationMillimetersTrack
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public INumericTimeDataSeries SaturatedHemoglobinPercentTrack
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public INumericTimeDataSeries TotalHemoglobinConcentrationTrack
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

    }
}
