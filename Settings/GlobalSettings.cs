namespace TrainingLoad.Settings
{
    using System;
    using System.Xml.Serialization;
    using TrainingLoad.UI;
    using System.ComponentModel;

    /// <summary>
    /// Global settings
    /// </summary>
    [XmlRootAttribute(ElementName = "TrainingLoad", IsNullable = false)]
    public class GlobalSettings
    {
        public static event PropertyChangedEventHandler PropertyChanged;

        private static int pastDays = 120;
        private static int futureDays = 0;
        private static GlobalSettings instance;
        private static bool ctlStacked = true;
        private static bool enableMultisport = false;
        // TODO: (High) Ensure show fit plan default is loaded properly into display control
        private static bool showFitPlanWorkouts = true;

        public Common.ChartBasis ChartBasis
        {
            get { return TrainingLoad.Data.ChartData.TLChartBasis; }
            set { TrainingLoad.Data.ChartData.TLChartBasis = (Common.ChartBasis)Enum.Parse(typeof(Common.ChartBasis), value.ToString()); }
        }

        public int PastDays
        {
            get { return pastDays; }
            set { pastDays = value; }
        }

        public int FutureDays
        {
            get { return futureDays; }
            set { futureDays = value; }
        }

        public bool CTLstacked
        {
            get { return ctlStacked; }
            set
            {
                if (ctlStacked != value)
                {
                    ctlStacked = value;
                    RaisePropertyChanged("CTLstacked");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that will show/hide FitPlan workouts on the user interface.
        /// </summary>
        public bool ShowFitPlanWorkouts
        {
            get { return showFitPlanWorkouts; }
            set
            {
                if (showFitPlanWorkouts == value)
                    return;

                showFitPlanWorkouts = value;
                RaisePropertyChanged("ShowFitPlanWorkouts");
            }
        }

        public bool EnableMultisport
        {
            get { return enableMultisport; }
            set
            {
                if (enableMultisport != value)
                {
                    enableMultisport = value;
                    RaisePropertyChanged("EnableMultisport");
                }
            }
        }

        public float GraceCTL
        {
            get { return 0.5f; }
        }

        public float GraceTSB
        {
            get { return 0.70f; }
        }

        public float GraceINF
        {
            get { return 0.75f; }
        }



        internal static GlobalSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalSettings();
                }

                return instance;
            }
        }

        private static void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(null, new PropertyChangedEventArgs(property));
        }
    }
}
