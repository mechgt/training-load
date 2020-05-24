// <copyright file="UserData.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.Settings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using TrainingLoad.Data;

    /// <summary>
    /// Training Load User settings.
    /// </summary>
    [Serializable()]    // Set this attribute to all the classes that want to serialize
    class UserData : ISerializable
    {
        #region Private Fields
        private const string defaultCharts = "SCORE;CTL;ATL;TSB";

        private static int tca; // ATL Time Constant
        private static int tcc; // CTL Time Constant
        private static float kc; // Weighting factor for CTL
        private static float ka; // Weighting factor for ATL
        private static int initatl; // Initial ATL
        private static int initctl; // Initial CTL
        private static bool enableFTP;
        private static bool enableNormPwr;
        private static bool enableTRIMP;
        private static bool enableTSS;
        private static bool multizone;
        private static bool forecast;
        private static bool dynamicZones;
        private static bool filterCharts;
        private static string zoneId; // Single Zone to use for all activities
        private static SortedList<string, TrimpZoneCategory> trimpZones; // Link Zones to Factors
        private static AthleteField ftpField;
        private static int smoothing; // Main chart smoothing
        private static bool modified; // Modification status
        private static string userCharts; // Default user chart settings
        private static string userColumns; // User Activity list column settings
        private static bool loaded; // Indicates if data has been loaded from the logbook
        private static DateTime perfDate; // ATL Time Constant
        private static int movingSumDays1;
        private static int movingSumDays2;
        private static int movingSumCat1;
        private static int movingSumCat2;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the UserData class.  This contains all user plugin settings.
        /// </summary>
        internal UserData()
        {
            LoadSettings();
        }

        #endregion

        #region Enumerations

        internal enum AthleteField
        {
            BPHigh,
            BPLow,
            Skinfold,
            BodyFat
        }

        internal enum SumCat
        {
            Trimp = 0, Display = 1, Time = 2
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ATL (Acute Training Load) time constant as a number of days.
        /// </summary>
        internal static int TCa
        {
            get
            {
                LoadSettings();
                return tca;
            }

            set
            {
                if (tca != value)
                {
                    tca = value;
                    Modified = true;
                    RaisePropertyChanged("TCa");
                }
            }
        }

        /// <summary>
        /// Gets or sets the CTL (Chronic Training Load) time constant as a number of days.
        /// </summary>
        internal static int TCc
        {
            get
            {
                LoadSettings();
                return tcc;
            }

            set
            {
                if (tcc != value)
                {
                    tcc = value;
                    Modified = true;
                    RaisePropertyChanged("TCc");
                }
            }
        }

        /// <summary>
        /// Gets or sets the weighting factor for CTL
        /// </summary>
        internal static float Kc
        {
            get
            {
                LoadSettings();
                if (!float.IsNaN(kc) && kc != 0)
                {
                    return kc;
                }
                else
                {
                    // Default value
                    return 1;
                }
            }

            set
            {
                if (kc != value)
                {
                    kc = value;
                    Modified = true;
                    RaisePropertyChanged("Kc");
                }
            }
        }

        /// <summary>
        /// Gets or sets the weighting factor for ATL
        /// </summary>
        internal static float Ka
        {
            get
            {
                LoadSettings();
                if (!float.IsNaN(ka) && ka != 0)
                {
                    return ka;
                }
                else
                {
                    // Default value
                    return 2;
                }
            }

            set
            {
                if (kc != value)
                {
                    ka = value;
                    Modified = true;
                    RaisePropertyChanged("Ka");
                }
            }
        }

        /// <summary>
        /// Gets or sets Initial ATL setting.
        /// </summary>
        internal static int InitialATL
        {
            get
            {
                LoadSettings();
                return initatl;
            }

            set
            {
                LoadSettings();

                if (initatl != value)
                {
                    initatl = value;
                    Modified = true;
                    RaisePropertyChanged("InitialATL");
                }
            }
        }

        /// <summary>
        /// Gets or sets Initial CTL setting.
        /// </summary>
        internal static int InitialCTL
        {
            get
            {
                LoadSettings();
                return initctl;
            }

            set
            {
                if (initctl != value)
                {
                    initctl = value;
                    Modified = true;
                    RaisePropertyChanged("InitialCTL");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the SportTracks HR zones assigned to an activity, or 
        /// single zone should be used when calculating TRIMP scores.
        /// </summary>
        internal static bool Multizone
        {
            get
            {
                LoadSettings();
                return multizone;
            }

            set
            {
                if (multizone != value)
                {
                    multizone = value;
                    Modified = true;
                    RaisePropertyChanged("MultiZone");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to forecast CTL, ATL, and TSB into the future 7 days after TSB peak.
        /// </summary>
        // TODO: (LOW)Remove forecast option.  Not really necessary.
        internal static bool Forecast
        {
            get
            {
                LoadSettings();
                return forecast;
            }
            
            set
            {
                if (forecast != value)
                {
                    forecast = value;
                    Modified = true;
                    RaisePropertyChanged("Forecast");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use dynamic zone factor calculation or not.  
        /// Auto Trimp uses daily HRmax and HRrest to calculate the TRIMP factor.  
        /// NOTE: This was previously termed 'DynamicZones'.  It was renamed to reflect the further
        /// automatic nature of no longer requiring a zone to be created.
        /// </summary>
        internal static bool AutoTrimp
        {
            get
            {
                LoadSettings();
                return dynamicZones;
            }

            set
            {
                if (dynamicZones != value)
                {
                    dynamicZones = value;
                    Modified = true;
                    RaisePropertyChanged("AutoTrimp");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to filter CTL, ATL, and TSB charts with the category filter.
        /// </summary>
        internal static bool FilterCharts
        {
            get
            {
                LoadSettings();
                return filterCharts;
            }

            set
            {
                if (filterCharts != value)
                {
                    filterCharts = value;
                    Modified = true;
                    RaisePropertyChanged("FilterCharts");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable the FTP custom parameter
        /// </summary>
        internal static bool EnableFTP
        {
            get
            {
                LoadSettings();
                return enableFTP;
            }

            set
            {
                if (enableFTP != value)
                {
                    enableFTP = value;
                    Modified = true;
                    RaisePropertyChanged("EnableFTP");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable the NormPwr custom parameter
        /// </summary>
        internal static bool EnableNormPwr
        {
            get
            {
                LoadSettings();
                return enableNormPwr;
            }

            set
            {
                if (enableNormPwr != value)
                {
                    enableNormPwr = value;
                    Modified = true;
                    RaisePropertyChanged("EnableNormPwr");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable the TRIMP custom parameter
        /// </summary>
        internal static bool EnableTRIMP
        {
            get
            {
                LoadSettings();
                return enableTRIMP;
            }

            set
            {
                if (enableTRIMP != value)
                {
                    enableTRIMP = value;
                    Modified = true;
                    RaisePropertyChanged("EnableTRIMP");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable the TSS custom parameter
        /// </summary>
        internal static bool EnableTSS
        {
            get
            {
                LoadSettings();
                return enableTSS;
            }

            set
            {
                if (enableTSS != value)
                {
                    enableTSS = value;
                    Modified = true;
                    RaisePropertyChanged("EnableTSS");
                }
            }
        }

        /// <summary>
        /// Gets or sets ReferenceID of the selected HR zone category for all TRIMP calcs (if multizone is false).
        /// </summary>
        internal static string ZoneID
        {
            get
            {
                LoadSettings();

                if (!trimpZones.ContainsKey(zoneId))
                {
                    // This might occur during migration from ST2 to ST3.
                    zoneId = trimpZones.Values[0].ReferenceId;
                }

                return zoneId;
            }

            set
            {
                if (zoneId != value)
                {
                    zoneId = value;
                    Modified = true;
                    RaisePropertyChanged("ZoneID");
                }
            }
        }

        /// <summary>
        /// DEPRECATED.  LEFT FOR ST2 MIGRATION.
        /// Gets or sets a value indicating which athlete field is re-purposed for FTP data.
        /// </summary>
        internal static AthleteField FTPfield
        {
            get
            {
                LoadSettings();
                return ftpField;
            }

            set
            {
                ftpField = value;
            }
        }

        /// <summary>
        /// Gets the HR zone category used for all TRIMP calculations and also the HR distribution chart.
        /// </summary>
        internal static IZoneCategory SingleZoneCategory
        {
            get
            {
                LoadSettings();

                // Set params for single TRIMP zone
                // TODO: (LOW/MED)Trying to implement a single zone setup of x zones.  Create zone here rather than in ST.
                //  - Decided to wait and implement this later.  It works now and there are more important things to work on.
                //TrimpZoneCategory singleCat = new TrimpZoneCategory(12, DateTime.Now);
                //return singleCat;

                foreach (IZoneCategory zoneCat in PluginMain.GetLogbook().HeartRateZones)
                {
                    if (zoneCat.ReferenceId == zoneId)
                    {
                        return zoneCat;
                    }
                }

                if (PluginMain.GetLogbook().HeartRateZones.Count > 0)
                {
                    // Return a defaul HR zone
                    return PluginMain.GetLogbook().HeartRateZones[0];
                }
                else
                {
                    // No Heartrate zones exist
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the list associating TRIMP zone factors with HR zones.  Key should be the ReferenceId.
        /// </summary>
        internal static SortedList<string, TrimpZoneCategory> TrimpZones
        {
            // Stores the custom zones, common to all activities
            get
            {
                LoadSettings();

                if (trimpZones == null)
                {
                    trimpZones = new SortedList<string, TrimpZoneCategory>();
                }

                return trimpZones;
            }

            set
            {
                trimpZones = value;
                StoreSettings();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether any settings have been modified.
        /// </summary>
        internal static bool Modified
        {
            get
            {
                foreach (TrimpZoneCategory zoneCat in trimpZones.Values)
                {
                    if (zoneCat.Modified == true)
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
                    foreach (TrimpZoneCategory zoneCat in trimpZones.Values)
                    {
                        zoneCat.Modified = false;
                    }
                }
                else
                {
                    StoreSettings();
                    ChartData.IsCalculated = false;
                    modified = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets charts stored in user profile
        /// </summary>
        internal static string UserCharts
        {
            get
            {
                LoadSettings();

                if (userCharts.Contains("Training") || string.IsNullOrEmpty(userCharts))
                    userCharts = defaultCharts;

                return userCharts;
            }

            set
            {
                if (userCharts != value)
                {
                    userCharts = value;
                    Modified = true;
                    RaisePropertyChanged("UserCharts");
                }
            }
        }

        /// <summary>
        /// Gets or sets list columns stored in user profile.  Columns are stored as a single string in a format like:
        /// "ColumnId1;ColumnId2;StartTime;NormalizedPower;TRIMP"
        /// </summary>
        internal static string UserColumns
        {
            get
            {
                LoadSettings();

                // The "Display" check below is because the names changed in v1.8.7
                if (string.IsNullOrEmpty(userColumns) || userColumns.Contains("Display"))
                {
                    return "StartTime;TRIMP;TSBPre;NormalizedPower;Category;Distance;TotalTime;AvgPace;AvgSpeed;AvgHR";
                }
                else
                {
                    return userColumns.Trim(';');
                }
            }

            set
            {
                if (userColumns != value)
                {
                    userColumns = value;
                    Modified = true;
                    RaisePropertyChanged("UserColumns");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the settings have been loaded from the logbook.
        /// </summary>
        internal static bool Loaded
        {
            get
            {
                return loaded;
            }

            set
            {
                loaded = value;
            }
        }

        /// <summary>
        /// Gets or sets Performance Date from which to calculate Influence curve.
        /// </summary>
        internal static DateTime PerfDate
        {
            get
            {
                LoadSettings();

                if (perfDate.Year == 1)
                {
                    perfDate = DateTime.Now;
                }

                return perfDate;
            }

            set
            {
                if (perfDate != value)
                {
                    perfDate = value;
                    Modified = true;
                    RaisePropertyChanged("PerfDate");
                }
            }
        }

        /// <summary>
        /// Gets or sets the constant for the Moving Avg. chart 1.
        /// </summary>
        internal static int MovingSumDays1
        {
            get
            {
                LoadSettings();

                if (movingSumDays1 <= 0)
                {
                    movingSumDays1 = 7;
                }

                return movingSumDays1;
            }

            set
            {
                if (movingSumDays1 != value)
                {
                    movingSumDays1 = value;
                    Modified = true;
                    RaisePropertyChanged("MovingSumDays1");
                }
            }
        }

        /// <summary>
        /// Gets or sets the constant for the Moving Avg. chart 2.
        /// </summary>
        internal static int MovingSumDays2
        {
            get
            {
                LoadSettings();

                if (movingSumDays2 <= 0)
                {
                    movingSumDays2 = 28;
                }

                return movingSumDays2;
            }

            set
            {
                if (movingSumDays2 != value)
                {
                    movingSumDays2 = value;
                    Modified = true;
                    RaisePropertyChanged("MovingSumDays2");
                }
            }
        }

        /// <summary>
        /// Gets or sets the data type displayed in Moving Sum chart 1
        /// </summary>
        internal static int MovingSumCat1
        {
            get
            {
                LoadSettings();
                return movingSumCat1;
            }

            set
            {
                if (movingSumCat1 != value)
                {
                    movingSumCat1 = value;
                    Modified = true;
                    RaisePropertyChanged("MovingSumCat1");
                }
            }
        }

        /// <summary>
        /// Gets or sets the data type displayed in Moving Sum chart 2
        /// </summary>
        internal static int MovingSumCat2
        {
            get
            {
                LoadSettings();
                return movingSumCat2;
            }

            set
            {
                if (movingSumCat2 != value)
                {
                    movingSumCat2 = value;
                    Modified = true;
                    RaisePropertyChanged("MovingSumCat2");
                }
            }
        }

        #endregion

        #region Public Utilities

        /// <summary>
        /// Returns the index of the selected Zone within the Category.
        /// </summary>
        /// <param name="category">Category to search for zone in</param>
        /// <param name="lowHighZone">Zone to search for</param>
        /// <returns>Returns zone index</returns>
        internal static int GetZoneIndex(IZoneCategory category, INamedLowHighZone lowHighZone)
        {
            int i = 0;
            foreach (INamedLowHighZone zone in category.Zones)
            {
                if (zone.Name == lowHighZone.Name)
                {
                    return i;
                }

                i++;
            }

            return i;
        }

        /// <summary>
        /// Load user settings from logbook
        /// </summary>
        internal static void LoadSettings()
        {
            if (Loaded)
            {
                return;
            }

            ILogbook logbook = PluginMain.GetLogbook();
            Loaded = true;

            if (logbook != null)
            {
                // Initialize trimpZones if null for deserialization
                // Pre-load with logbook data, then add Trimp factors during deserialization
                InitTrimpZone();

                byte[] byteUserData = logbook.GetExtensionData(new PluginMain().Id);

                if (byteUserData.Length > 0)
                {
                    // Deserialize settings data from logbook
                    try
                    {
                        MemoryStream stream = new MemoryStream(byteUserData);
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Binder = new Binder(); // Help Deserializer resolve my class
                        stream.Position = 0;
                        formatter.Deserialize(stream);
                    }
                    catch
                    { }
                }
                else
                {
                    LoadDefaultSettings();
                }
            }
        }

        /// <summary>
        /// Initialize all Training Load settings to default values.
        /// </summary>
        internal static void LoadDefaultSettings()
        {
            ILogbook logbook = PluginMain.GetLogbook();

            tca = 11;
            tcc = 45;
            initatl = 0;
            initctl = 0;
            ka = 2;
            kc = 1;
            multizone = true;
            forecast = false;
            zoneId = string.Empty;
            movingSumDays1 = 7;
            movingSumDays2 = 28;
            enableFTP = true;
            enableNormPwr = true;
            enableTRIMP = true;
            enableTSS = true;

            InitTrimpZone();

            foreach (IZoneCategory zoneCat in logbook.HeartRateZones)
            {
                ResetCategory(zoneCat);
            }

            modified = true;
            userCharts = defaultCharts;
            userColumns = string.Empty;
            smoothing = 10;
            loaded = true;
            dynamicZones = false;
            filterCharts = true;
        }

        /// <summary>
        /// Reset TrimpZoneCategory values to default.
        /// </summary>
        /// <param name="category"></param>
        internal static void ResetCategory(IZoneCategory category)
        {
            // Initialize variables
            float factor;

            if (!trimpZones.ContainsKey(category.ReferenceId))
            {
                InitTrimpZone(category);
            }

            // Reset Each zone factor within the selected category
            foreach (INamedLowHighZone zone in category.Zones)
            {
                factor = TrimpZone.GetDefaultZoneFactor(zone, DateTime.Now);
                trimpZones[category.ReferenceId].TrimpZones[GetZoneIndex(category, zone)].Factor = factor;
            }
        }

        /// <summary>
        /// Save all user settings, and mark logbook as Modified.
        /// </summary>
        internal static void StoreSettings()
        {
            ILogbook logbook = PluginMain.GetLogbook();

            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, new UserData());

            logbook.SetExtensionData(new PluginMain().Id, stream.ToArray());
            logbook.Modified = true;
        }

        #endregion

        #region Private Methods

        private static void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(null,new PropertyChangedEventArgs(property));
            }
        }

        /// <summary>
        /// Initialize all HR categories in the logbook
        /// </summary>
        private static void InitTrimpZone()
        {
            ILogbook logbook = PluginMain.GetLogbook();
            trimpZones = new SortedList<string, TrimpZoneCategory>();

            foreach (IZoneCategory category in logbook.HeartRateZones)
            {
                InitTrimpZone(category);
            }
        }

        /// <summary>
        /// Initialize a single HR Category for use.  Factors will be all 0.
        /// </summary>
        /// <param name="category">The HR Category to initialize.  Factors will default to 0.</param>
        private static void InitTrimpZone(IZoneCategory category)
        {
            int izone = 0;
            if (trimpZones == null)
            {
                // Should not hit here
                trimpZones = new SortedList<string, TrimpZoneCategory>();
            }

            if (!trimpZones.ContainsKey(category.ReferenceId))
            {
                // Add the new zone only if it does not exist
                trimpZones.Add(category.ReferenceId, new TrimpZoneCategory(category.ReferenceId));
            }

            trimpZones[category.ReferenceId].ReferenceId = category.ReferenceId;
            trimpZones[category.ReferenceId].TrimpZones.Clear();
            foreach (INamedLowHighZone zone in category.Zones)
            {
                trimpZones[category.ReferenceId].TrimpZones.Add(izone, new TrimpZone(zone));
                izone++;
            }
        }

        #endregion

        #region ISerializable Members

        /// <summary>
        /// Automatically called during Serialization
        /// </summary>
        /// <param name="info">Object to store data to.</param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) // Serialize   
        {
            // Store data during serialization
            info.SetType(typeof(UserData));
            info.AddValue("TCa", tca);
            info.AddValue("TCc", tcc);
            info.AddValue("Ka", ka);
            info.AddValue("Kc", kc);
            info.AddValue("InitATL", initatl);
            info.AddValue("InitCTL", initctl);
            info.AddValue("Multizone", multizone);
            info.AddValue("Forecast", forecast);
            info.AddValue("ZoneID", zoneId);
            info.AddValue("Smoothing", smoothing);
            info.AddValue("UserCharts", userCharts);
            info.AddValue("UserColumns", userColumns);
            info.AddValue("PerfDate", perfDate);
            info.AddValue("DynamicZones", dynamicZones);
            info.AddValue("FilterCharts", filterCharts);
            info.AddValue("TSSCatCount", trimpZones.Count);
            info.AddValue("MovingSumDays1", movingSumDays1);
            info.AddValue("MovingSumDays2", movingSumDays2);
            info.AddValue("MovingSumCat1", movingSumCat1);
            info.AddValue("MovingSumCat2", movingSumCat2);
            info.AddValue("EnableFTP", enableFTP);
            info.AddValue("EnableNormPwr", enableNormPwr);
            info.AddValue("EnableTRIMP", enableTRIMP);
            info.AddValue("EnableTSS", enableTSS);

            int icat = 0;
            int izone = 0;

            foreach (TrimpZoneCategory zoneCat in trimpZones.Values)
            {
                info.AddValue("RefID_" + icat, zoneCat.ReferenceId);
                info.AddValue("TSSZonesCount_" + icat, zoneCat.TrimpZones.Count);
                foreach (TrimpZone zone in zoneCat.TrimpZones.Values)
                {
                    info.AddValue("Factor_" + icat + "_" + izone, zone.Factor);
                    izone++;
                }

                izone = 0;
                icat++;
            }
        }

        /// <summary>
        /// Initializes a new instance of the UserData class.  UserData stores and retrieves user settings from the logbook.  
        /// This constructor is used for deserialization.
        /// </summary>
        /// <param name="info">Serialization object containing the (de)serializad data.</param>
        /// <param name="context"></param>
        protected UserData(SerializationInfo info, StreamingContext context)
        {
            ILogbook logbook = PluginMain.GetLogbook();
            string refID;
            float factor;

            // Load all default settings in case settings do not previously exist.
            LoadDefaultSettings();

            // Restore data during deserialization
            // TODO: (LOW)use info.GetEnumerator to run through the saved settings.
            tca = info.GetInt32("TCa");
            tcc = info.GetInt32("TCc");
            multizone = info.GetBoolean("Multizone");
            zoneId = info.GetString("ZoneID");

            int trimpCatCount = info.GetInt32("TSSCatCount");

            // Deserialize each category
            for (int icat = 0; icat < trimpCatCount; icat++)
            {
                refID = info.GetString("RefID_" + icat);
                if (trimpZones.ContainsKey(refID))
                {
                    // Do not add zones that are not in the logbook.  All zones have been initialized by this time.
                    //trimpZones.Add(refID, new TrimpZoneCategory(refID));
                    //}

                    // Deserialize each zone within each category
                    for (int izone = 0; izone < info.GetInt32("TSSZonesCount_" + icat); izone++)
                    {
                        // Get factor
                        factor = (float)info.GetDouble("Factor_" + icat + "_" + izone);
                        if (trimpZones[refID].TrimpZones.ContainsKey(izone))
                        {
                            // Update existing zone
                            trimpZones[refID].TrimpZones[izone].Factor = factor;
                            trimpZones[refID].TrimpZones[izone].Modified = false;
                        }
                        else
                        {
                            // Create new zone
                            foreach (IZoneCategory zoneCat in logbook.HeartRateZones)
                            {
                                // Use logbook data for High/Low/Name info
                                if (zoneCat.ReferenceId == refID)
                                {
                                    trimpZones[refID].TrimpZones.Add(izone, new TrimpZone(zoneCat.Zones[izone], factor));
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            userCharts = info.GetString("UserCharts");
            initatl = info.GetInt32("InitATL");
            initctl = info.GetInt32("InitCTL");
            ka = info.GetSingle("Ka");
            kc = info.GetSingle("Kc");
            forecast = info.GetBoolean("Forecast");
            perfDate = info.GetDateTime("PerfDate");
            userColumns = info.GetString("UserColumns");
            dynamicZones = info.GetBoolean("DynamicZones");
            filterCharts = info.GetBoolean("FilterCharts");
            movingSumDays1 = info.GetInt32("MovingSumDays1");
            movingSumDays2 = info.GetInt32("MovingSumDays2");
            movingSumCat1 = info.GetInt32("MovingSumCat1");
            movingSumCat2 = info.GetInt32("MovingSumCat2");

            try
            {
                ftpField = (AthleteField)info.GetInt32("ftpField");
                Util.MigrationHelper.MigrateFTP();
            }
            catch { }

            enableFTP = info.GetBoolean("EnableFTP");
            enableNormPwr = info.GetBoolean("EnableNormPwr");
            enableTRIMP = info.GetBoolean("EnableTRIMP");
            enableTSS = info.GetBoolean("EnableTSS");
        }

        /// <summary>
        /// Serialization binder necessary for serializing (storing) data to logbook.
        /// </summary>
        private class Binder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Type tyType = null;
                string shortName = assemblyName.Split(',')[0];
                System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

                foreach (System.Reflection.Assembly assembly in assemblies)
                {
                    if (shortName == assembly.FullName.Split(',')[0])
                    {
                        tyType = assembly.GetType(typeName);
                        break;
                    }
                }

                return tyType;
            }
        }

        #endregion

        #region Events

        internal static event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
