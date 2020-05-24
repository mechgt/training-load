namespace TrainingLoad.UI.DetailPage
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    using ZoneFiveSoftware.Common.Visuals.Util;
    using System.Collections.Generic;

    public class MeanMax : IDetailPage
    {
        #region Fields

        private IActivity activity;
        private MeanMaxDetail control;
        private IDailyActivityView view;
        private bool maximized;
        private static MeanMax instance;

        #endregion

        #region Constructor

        public MeanMax(IDailyActivityView view)
        {
            this.view = view;
            view.SelectionProvider.SelectedItemsChanged += new EventHandler(OnViewSelectedItemsChanged);
            instance = this;
        }

        #endregion

        #region IActivityDetailPage Members

        public IActivity Activity
        {
            set
            {
                activity = value;
                if (control != null)
                {
                    control.Activity = activity;
                    control.RefreshPage();
                }
            }
        }

        public void RefreshPage()
        {
            if (control != null)
            {
                control.RefreshPage();
            }
        }

        #endregion

        #region IDialogPage Members

        public Control CreatePageControl()
        {
            if (control == null)
            {
                control = new MeanMaxDetail();
                control.Activity = activity;
            }

            return control;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return Resources.Strings.Label_MeanMax; }
        }

        public void ShowPage(string bookmark)
        {
            if (control != null)
            {
                control.RefreshPage();
            }
        }

        public IPageStatus Status
        {
            get { throw new NotImplementedException(); }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            control.ThemeChanged(visualTheme);
        }

        public string Title
        {
            get { return Resources.Strings.Label_MeanMax; }
        }

        public void UICultureChanged(CultureInfo culture)
        {
            control = CreatePageControl() as MeanMaxDetail;
            control.UICultureChanged(culture);
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnViewSelectedItemsChanged(object sender, EventArgs e)
        {
            Activity = CollectionUtils.GetSingleItemOfType<IActivity>(view.SelectionProvider.SelectedItems);
            RefreshPage();
        }

        #region IDetailPage Members

        public Guid Id
        {
            get { return GUIDs.MeanMax; }
        }

        public bool MenuEnabled
        {
            get { return true; }
        }

        public IList<string> MenuPath
        {
            get { return new List<string> { CommonResources.Text.LabelPower }; }
        }

        public bool MenuVisible
        {
            get
            {
                return true;
            }
        }

        public bool PageMaximized
        {
            get
            {
                return maximized;
            }
            set
            {
                maximized = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs("PageMaximized"));
                }
            }
        }

        #endregion

        public static MeanMax Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
