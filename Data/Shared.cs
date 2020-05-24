using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using TrainingLoad.UI.View;
using ZoneFiveSoftware.Common.Visuals.Chart;

namespace TrainingLoad.Data
{
    /// <summary>
    /// Data class for other plugins to access Training Load Data
    /// </summary>
    public static class Shared
    {
        public enum Sports
        {
            Cycling,
            Running
        }

        public enum CalculationType
        {
            Trimp,
            TSS
        }

        /// <summary>
        /// Gets the TSS score for an activity
        /// </summary>
        /// <param name="activity">Activity to find score for</param>
        /// <returns>Power based TSS score, or 0 if not found</returns>
        public static double GetTSS(IActivity activity)
        {
            TrimpActivity trimpactivity = FindTrimpActivity(activity);

            if (trimpactivity != null)
            {
                return trimpactivity.TSS;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the Trimp score for an activity
        /// </summary>
        /// <param name="activity">Activity to find score for</param>
        /// <returns>HR based Trimp score, or 0 if not found</returns>
        public static double GetTrimp(IActivity activity)
        {
            TrimpActivity trimpactivity = FindTrimpActivity(activity);

            if (trimpactivity != null)
            {
                return trimpactivity.TRIMP;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the Normalized Power for an activity
        /// </summary>
        /// <param name="activity">Activity to find score for</param>
        /// <returns>Returns Normalized Power, or 0 if not found</returns>
        public static double GetNormalizedPower(IActivity activity)
        {
            TrimpActivity trimpactivity = FindTrimpActivity(activity);

            if (trimpactivity != null)
            {
                return trimpactivity.NormalizedPower;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the total work (in kJ) for an activity
        /// </summary>
        /// <param name="activity">Activity to find work for</param>
        /// <returns>Returns total work (in kJ), or 0 if not found</returns>
        public static float GetWork(IActivity activity)
        {
            TrimpActivity trimpactivity = FindTrimpActivity(activity);

            if (trimpactivity != null)
            {
                return trimpactivity.Work;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the user recorded FTP for a particular date.
        /// </summary>
        /// <param name="date">Date of interest</param>
        /// <returns>Returns user specified FTP for a particular date, or 0 if not found</returns>
        public static float GetFTP(DateTime date)
        {
            return GetFTP(date, Shared.Sports.Cycling);
        }

        public static float GetFTP(DateTime date, Shared.Sports sport)
        {
            double ftp = TrimpActivity.GetFTP(date, sport);

            // Return 0 if not found
            if (double.IsNaN(ftp))
            {
                ftp = 0;
            }

            return (float)ftp;
        }

        /// <summary>
        /// Get the CTL for a particular date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static float GetCTL(DateTime date, int mode)
        {
            // Provide CTL
            ChartData.TLChartBasis = (TrainingLoad.UI.Common.ChartBasis)mode;
            ChartData.CalculateDataSeries();

            return GetValue(ChartData.CTL, date);
        }

        /// <summary>
        /// Get the ATL for a particular date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static float GetATL(DateTime date, int mode)
        {
            // Provide CTL
            ChartData.TLChartBasis = (TrainingLoad.UI.Common.ChartBasis)mode;
            ChartData.CalculateDataSeries();

            return GetValue(ChartData.ATL, date);
        }

        /// <summary>
        /// Gets the CTL time constant as a number of days.
        /// </summary>
        public static float TCc
        {
            get { return Settings.UserData.TCc; }
        }

        /// <summary>
        /// Gets the ATL time constant as a number of days.
        /// </summary>
        public static float TCa
        {
            get { return Settings.UserData.TCa; }
        }

        /// <summary>
        /// Get the associated Trimp Activity
        /// </summary>
        /// <param name="activity">Activity in which to find associated activity</param>
        /// <returns></returns>
        private static TrimpActivity FindTrimpActivity(IActivity activity)
        {
            ActivityCollection trimpActivities = ChartData.GetTrimpActivities();

            foreach (TrimpActivity trimpActivity in trimpActivities)
            {
                if (activity.ReferenceId == trimpActivity.ReferenceId)
                {
                    // Return the requested activity
                    return trimpActivity;
                }
            }

            // If not found, create a new one
            // (Generally this should not be the case)
            return new TrimpActivity(activity);
        }

        private static float GetValue(CurveItem primary, DateTime date)
        {
            // Provide CTL
            int index = ChartData.PointExists(ViewTrainingLoadPage.Instance.Pane, primary, date);
            if (index != -1)
                return (float)primary[index].Y;
            else
                return 0;
        }
    }
}
