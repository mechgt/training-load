// <copyright file="CreateGOVSS.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2010-08-10</date>
namespace TrainingLoad.UI.Actions
{
    using ZoneFiveSoftware.Common.Visuals.Util;
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using TrainingLoad.Settings;
    using TrainingLoad.UI.Actions;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    using ZoneFiveSoftware.Common.Data.Fitness.CustomData;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;

    class CreateRunningPowerTrack : IAction
    {
        #region Fields

        private bool minetti = true;
        private IDailyActivityView viewActivity;
        private IActivityReportsView viewReport;
        private Location location;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for Tasks action item on main Training Load view
        /// </summary>
        public CreateRunningPowerTrack()
        {
            location = Location.TrainingLoadTask;
        }

        /// <summary>
        /// Constructor for Edit action item on daily activity view 
        /// </summary>
        /// <param name="view">A value indicating whether this Action is for display on the 
        /// TL View as task, or elsewhere in ST (Edit menu)</param>
        public CreateRunningPowerTrack(IDailyActivityView view)
        {
            this.viewActivity = view;
            location = Location.DailyActivity;
        }

        /// <summary>
        /// Constructor for Edit action item on reports view 
        /// </summary>
        /// <param name="view">A value indicating whether this Action is for display on the 
        /// TL View as task, or elsewhere in ST (Edit menu)</param>
        public CreateRunningPowerTrack(IActivityReportsView view)
        {
            this.viewReport = view;
            location = Location.ActivityReports;
        }

        #endregion

        #region Enumerations

        internal enum Location
        {
            TrainingLoadTask,
            DailyActivity,
            ActivityReports
        }

        #endregion

        #region IAction Members

        public bool Enabled
        {
            get
            {
                // TODO: Only enable for 'running' activity.
                return true;
            }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public Image Image
        {
            get { return Resources.Images.GOVSS_16; }
        }

        public IList<string> MenuPath
        {
            get { return null; }
        }

        public void Refresh()
        {

        }

        public void Run(Rectangle rectButton)
        {
            IList<IActivity> activities = GetActivities();

            foreach (IActivity activity in activities)
            {
                INumericTimeDataSeries powerTrack;

                // Select proper power track to get
                powerTrack = TrimpActivity.GetRunningPowerTrack(activity, true);
                //float min,max;
                //powerTrack = Utilities.STSmooth(powerTrack, 60, out min, out max);

                if (powerTrack != null && powerTrack.Count > 0)
                {
                    // TODO: Does this cause TL view to refresh a lot?
                    //activity.SetActivityMonitoring(false);
                    activity.PowerWattsTrack = powerTrack;
                    //activity.SetActivityMonitoring(true);

                    ChartData.DataCalculated = false;
                    ViewTrainingLoadPage.Instance.RefreshPage();
                }
            }
        }

        public string Title
        {
            get
            {
                if (minetti)
                {
                    return Resources.Strings.Action_CreateRunningPowerTrack;
                }
                else
                {
                    return "Create GOVSS Running Power Track";
                }
            }
        }

        public bool Visible
        {
            get { return true; }
        }

        #endregion

        public IList<IActivity> GetActivities()
        {
            IList<IActivity> activities = new List<IActivity>();

            switch (location)
            {
                case Location.TrainingLoadTask:
                    {
                        // Get running power track and assign to activity
                        ViewTrainingLoadPageControl control = ViewTrainingLoadPage.Instance;

                        // Training Load View.  Get activities from treelist
                        foreach (TrimpActivity activity in control.treeActivity.Selected)
                        {
                            activities.Add(activity.Activity);
                        }
                        break;
                    }
                case Location.ActivityReports:
                    {
                        // Daily or Report view.  Get activities selected in ST core list.
                        activities = CollectionUtils.GetItemsOfType<IActivity>(viewReport.SelectionProvider.SelectedItems);
                        break;
                    }
                case Location.DailyActivity:
                    {
                        // Daily or Report view.  Get activities selected in ST core list.
                        activities = CollectionUtils.GetItemsOfType<IActivity>(viewActivity.SelectionProvider.SelectedItems);
                        break;
                    }
            }

            return activities;
        }

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
