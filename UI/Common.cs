namespace TrainingLoad.UI
{
    using System.Drawing;
    using TrainingLoad.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;

    public class Common
    {
        public static readonly Color ColorCadence = Color.FromArgb(78, 154, 6);
        public static readonly Color ColorElevation = Color.FromArgb(143, 89, 2);
        public static readonly Color ColorGrade = Color.FromArgb(193, 125, 17);
        public static readonly Color ColorHR = Color.FromArgb(204, 0, 0);
        public static readonly Color ColorPower = Color.FromArgb(92, 53, 102);
        public static readonly Color ColorSpeed = Color.FromArgb(32, 74, 135);

        private static ActivityCollection activities;

        public enum ChartBasis
        {
            Power, HR, Daniels
        }

        public static ActivityCollection Activities
        {
            get
            {
                if (activities == null)
                {
                    activities = new ActivityCollection();
                    activities.AddRange(PluginMain.GetApplication().Logbook.Activities);
                    activities.Sort();
                }

                return activities;
            }
        }

        public static void ClearActivityList()
        {
            activities = null;
        }
    }
}
