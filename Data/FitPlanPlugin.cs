namespace TrainingLoad.Data
{
    using System;
    using System.Reflection;
    using FitPlan.Schedule;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using ZoneFiveSoftware.Common.Visuals.Chart;
    using System.ComponentModel;

    internal static class FitPlanPlugin
    {
        private static event PropertyChangedEventHandler PropertyChanged;
        private static bool isInstalled;
        private static bool installChecked;
        private static Type Common;

        internal static bool IsInstalled
        {
            get
            {
                if (installChecked)
                {
                    // Return true if plugin has already been found
                    return isInstalled;
                }

                installChecked = true;

                foreach (Assembly loadedAssembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    AssemblyName name = loadedAssembly.GetName();

                    if (name.Name.Equals("FitPlan"))
                    {
                        if (name.Version >= new Version(1, 1, 5, 0))
                        {
                            try
                            {
                                Common = loadedAssembly.GetType("FitPlan.Data.Shared");
                                MethodInfo method = Common.GetMethod("GetWorkouts");
                                isInstalled = true;
                            }
                            catch
                            {
                                isInstalled = false;
                            }
                            return isInstalled;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Get Scores for all points between 2 dates.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        internal static PointPairList GetScores(DateTime start, DateTime end)
        {
            PointPairList scores = new PointPairList();
            if (!IsInstalled)
                return scores;

            foreach (Workout workout in GetWorkouts())
            {
                if (start <= workout.StartDate && workout.StartDate <= end && !float.IsNaN(workout.Score) && 0 < workout.Score)
                {
                    XDate date = new XDate(workout.StartDate);
                    if (0 < scores.Count && scores[scores.Count - 1].X == date)
                    {
                        // Combine scores
                        scores[scores.Count - 1].Y += workout.Score;
                    }
                    else
                    {
                        // First workout on a particular date
                        scores.Add(date, workout.Score);
                    }
                }
            }

            return scores;
        }

        private static WorkoutCollection GetWorkouts()
        {
            if (!IsInstalled)
                return null;

            return FitPlan.Data.Shared.GetWorkouts();
        }
    }
}
