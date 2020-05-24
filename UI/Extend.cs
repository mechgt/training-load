// <copyright file="ExtendActions.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.UI
{
    using System.Collections.Generic;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    using TrainingLoad.UI.View;

    class Extend : IExtendViews, IExtendActivityReportsViewActions, IExtendDailyActivityViewActions
    {
        #region IExtendViews Members

        // Also see http://james-p-smith.blogspot.com/2009/08/powercranks-final-verdict.html
        // For some more interesting charts

        public IList<IView> Views
        {
            get
            {
                // Create & return the new menu item under 'Select View'
                IList<IView> views = new List<IView>();
                views.Add(new ViewTrainingLoadPage());
                return views;
            }
        }

        #endregion

        #region IExtendActivityReportsViewActions Members

        public IList<IAction> GetActions(IActivityReportsView view, ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.EditMenu)
            {
#if Debug
                //return new List<IAction> { new UI.Actions.CreateRunningPowerTrack(view) };
#endif
            }

            return null;
        }

        #endregion

        #region IExtendDailyActivityViewActions Members

        public IList<IAction> GetActions(IDailyActivityView view, ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.EditMenu)
            {
#if Debug
                //return new List<IAction> { new UI.Actions.CreateRunningPowerTrack(view) };
#endif
            }

            return null;
        }

        #endregion
    }
}
