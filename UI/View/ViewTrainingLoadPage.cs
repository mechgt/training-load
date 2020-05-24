// <copyright file="ViewTrainingLoadPage.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.UI.View
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;
    using TrainingLoad.Settings;
    using TrainingLoad.UI;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    using TrainingLoad.UI.Actions;
    using TrainingLoad.Data;

    /// <summary>
    /// Training Load page view.  This is the plugin interface.
    /// </summary>
    public class ViewTrainingLoadPage : IView
    {
        #region Private members

        private static bool eventsSubscribed;
        private static ViewTrainingLoadPageControl control;
        private bool pageLoaded;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ViewTrainingLoadPage class.
        /// </summary>
        internal ViewTrainingLoadPage()
        {
            if (!eventsSubscribed)
            {
                eventsSubscribed = true;

                // Event to recognize when Logbook has completed loading
                // Maintains a monitor on the logbook to watch for data changes in ChartData
                PluginMain.GetApplication().PropertyChanged += ChartData.SportTracksApplication_PropertyChanged;

                // This one must monitor to update when a user opens a different logbook.
                PluginMain.GetApplication().PropertyChanged += SportTracksApplication_PropertyChanged;
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IView Members

        public IList<IAction> Actions
        {
            // A List of Actions that appear in the task pane area for this view.
            get
            {
                IList<IAction> actions = new List<IAction>();

                IAction setRaceDay = new SetPerfDate();
                actions.Add(setRaceDay);
                IAction exportTL = new ExportTL();
                actions.Add(exportTL);
                actions.Add(new Recalculate());

                return actions;
            }
        }

        public Guid Id
        {
            get { return GUIDs.PluginMain; }
        }

        public string SubTitle
        {
            get
            {
                IActivityCategory selected = PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter;
                string subtitle = selected.Name;
                while (selected.Parent != null)
                {
                    selected = selected.Parent;
                    subtitle = selected.Name + ": " + subtitle;
                }

                return subtitle;
            }
        }

        public bool SubTitleHyperlink
        {
            get { return true; }
        }

        public string TasksHeading
        {
            get { return "Analysis Tasks"; }
        }

        public void SubTitleClicked(Rectangle subTitleRect)
        {
            // From: http://www.zonefivesoftware.com/sporttracks/Forums/viewtopic.php?p=40402
            //OpenActivityCategoriesFilterPopup(subTitleRect);

            List<PopupCategory> categories = new List<PopupCategory>();
            ILogbook logbook = PluginMain.GetLogbook();
            ITheme theme = PluginMain.GetApplication().VisualTheme;

            // Collect all categories
            SearchSubcategory(logbook.ActivityCategories, ref categories, 0);

            int i = 0;
            string name;
            foreach (PopupCategory cat in categories)
            {
                name = string.Empty;
                for (int j = 0; j < cat.Depth; j++)
                {
                    name = name + "   ";
                }

                cat.DisplayName = name + cat.Category.Name;
                cat.DisplayIndex = i;
                i++;
            }

            // Create TreeListPopup
            TreeListPopup popup = new TreeListPopup();
            popup.ThemeChanged(theme);
            popup.Tree.Columns.Add(new TreeList.Column("DisplayName"));
            popup.Tree.RowData = categories;

            popup.ItemSelected += delegate(object sender, TreeListPopup.ItemSelectedEventArgs e)
            {
                CategoryChangedHandler(sender, e);
            };

            // Show the popup
            popup.Popup(subTitleRect);
        }

        #endregion

        #region IDialogPage Members

        public Control CreatePageControl()
        {
            // Called the first time the page is loaded.
            if (control == null)
            {
                control = new ViewTrainingLoadPageControl(PluginMain.GetLogbook().Activities);
            }

            return control;
        }

        public bool HidePage()
        {
            // Called on leaving the page.
            PluginMain.GetApplication().Calendar.SelectedChanged -= control.Calendar_SelectedChanged;
            pageLoaded = false;
            return true;
        }

        public string PageName
        {
            get { return Title; }
        }

        public void ShowPage(string bookmark)
        {
            // This is called on View selected (every time), and also when trying to navigate away
            // Think this is to process data prior to actually displaying the page.

            /* Display the page. Use the application reference to refresh
             * data, selected state, etc before showing the page.
             * The bookmark string is specific to the page implementor.
             */

            // Initialize some items only if this is not already the active view
            if (!pageLoaded)
            {
                control = CreatePageControl() as ViewTrainingLoadPageControl;
                PluginMain.GetApplication().Calendar.SelectedChanged += control.Calendar_SelectedChanged;
            }

            // Data calculation is marked dirty specifically to account for FitPlan changes.  Could also handle this with FitPlan event.
            ChartData.IsCalculated = false;
            control.RefreshPage();

            pageLoaded = true;
        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            if (control != null)
            {
                control.ThemeChanged(visualTheme);
            }
        }

        /// <summary>
        /// Gets the value displayed on ST's View menu and on the view's title banner.
        /// </summary>
        public string Title
        {
            get { return Resources.Strings.Label_TrainingLoad; }
        }

        public void UICultureChanged(CultureInfo culture)
        {
            if (control != null)
            {
                control.UICultureChanged(culture);
            }
        }

        #endregion

        #region Properties

        public static ViewTrainingLoadPageControl Instance
        {
            get { return control; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Handler for changing category selection filter
        /// </summary>
        /// <param name="sender">The paratmeter is not used.</param>
        /// <param name="e">Used to acquire activity category</param>
        private void CategoryChangedHandler(object sender, TreeListPopup.ItemSelectedEventArgs e)
        {
            // Add handler for when something is selected.
            // Store category selected in ST application
            IActivityCategory selectedCategory = ((PopupCategory)e.Item).Category;
            if (PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter != selectedCategory)
            {
                PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter = selectedCategory;
                ChartData.IsCalculated = false;

                // Notify that Subtitle has changed
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SubTitle"));

                // Refresh display
                control.RefreshPage();
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Traverses through all categories in order and records their depth within the tree
        /// </summary>
        /// <param name="subCategory">List of Categories to search</param>
        /// <param name="categories">List of PopupCategory (By Reference)</param>
        /// <param name="depth">Depth associated with subCategory</param>
        private void SearchSubcategory(IEnumerable<IActivityCategory> subCategory, ref List<PopupCategory> categories, int depth)
        {
            foreach (IActivityCategory activityCategory in subCategory)
            {
                PopupCategory catInfo = new PopupCategory();
                catInfo.Category = activityCategory;
                catInfo.Depth = depth;

                categories.Add(catInfo);
                if (activityCategory.SubCategories.Count > 0)
                {
                    SearchSubcategory(activityCategory.SubCategories, ref categories, depth + 1);
                }
            }
        }

        private void SportTracksApplication_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Logbook")
            {
                // Need to recalculate data anytime logbook is changed
                ILogbook logbook = PluginMain.GetLogbook();
                UserData.Loaded = false;
                Utilities.Initialize();

                if (logbook != null)
                {
                    CreatePageControl();
                    control.Activities = PluginMain.GetLogbook().Activities;
                    ChartData.LogbookId = logbook.ReferenceId;
                }

                // Logbook was changed.  Reload data from new logbook.
                if (PluginMain.GetApplication().ActiveView.Id == this.Id)
                {
                    // Refresh Current view if applicable
                    ShowPage(string.Empty);
                }
            }
        }

        #endregion

        private class PopupCategory
        {
            #region Fields

            IActivityCategory category;
            string textName;
            int depth;
            int displayIndex;

            #endregion

            #region Properties

            public IActivityCategory Category
            {
                get { return this.category; }
                set { this.category = value; }
            }

            public string DisplayName
            {
                get { return this.textName; }
                set { this.textName = value; }
            }

            public int Depth
            {
                get { return this.depth; }
                set { this.depth = value; }
            }

            public int DisplayIndex
            {
                get { return this.displayIndex; }
                set { this.displayIndex = value; }
            }

            public string ReferenceId
            {
                get { return this.category.ReferenceId; }
            }

            #endregion
        }
    }
}
