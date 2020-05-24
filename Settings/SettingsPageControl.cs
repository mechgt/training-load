// <copyright file="SettingsPageControl.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Chart;
    using Mechgt.Licensing;

    public partial class SettingsPageControl : UserControl
    {
        #region Fields

        private ILogbook logbook = PluginMain.GetLogbook();
        private SortedList<string, string> zoneCatIDs = new SortedList<string, string>();
        private bool controlInitialized;
        private Activation pageActivate;

        #endregion

        #region Constructors

        public SettingsPageControl()
        {
            InitializeComponent();
        }

        #endregion

        internal void RefreshPage()
        {
            // Setting several of the above items would unnecessarily set logbook to 'modified'.
            //  This variable is used in the handlers to prevent this.
            this.controlInitialized = false;
            IZoneCategoryList zoneCategory = PluginMain.GetLogbook().HeartRateZones;

            // Populate all control members with user settings
            txtTCa.Text = UserData.TCa.ToString(CultureInfo.CurrentCulture);
            txtTCc.Text = UserData.TCc.ToString(CultureInfo.CurrentCulture);
            chkSingleZone.Checked = !UserData.Multizone;
            cboCategories.Enabled = !UserData.Multizone;
            chkForecast.Checked = UserData.Forecast;
            chkCusFTPcycle.Checked = UserData.EnableFTP;
            chkCusNP.Checked = UserData.EnableNormPwr;
            chkCusTrimp.Checked = UserData.EnableTRIMP;
            chkCusTSS.Checked = UserData.EnableTSS;

            chkDynamicZones.Checked = UserData.AutoTrimp;
            btnCatReset.Enabled = !UserData.AutoTrimp;
            treelistHRCats.Enabled = !UserData.AutoTrimp;
            treelistHRZones.Enabled = !UserData.AutoTrimp;
            chkSingleZone.Enabled = !UserData.AutoTrimp;
            cboCategories.Enabled = !UserData.AutoTrimp;
            chkFilterCharts.Checked = UserData.FilterCharts;
            txtInitialATL.Text = UserData.InitialATL.ToString(CultureInfo.CurrentCulture);
            txtInitialCTL.Text = UserData.InitialCTL.ToString(CultureInfo.CurrentCulture);
            txtSum1.Text = UserData.MovingSumDays1.ToString(CultureInfo.CurrentCulture);
            txtSum2.Text = UserData.MovingSumDays2.ToString(CultureInfo.CurrentCulture);
            txtPast.Text = GlobalSettings.Instance.PastDays.ToString(CultureInfo.CurrentCulture);
            txtFuture.Text = GlobalSettings.Instance.FutureDays.ToString(CultureInfo.CurrentCulture);

            // Populate SingleZone Combobox
            zoneCatIDs.Clear();
            cboCategories.Items.Clear();
            if (UserData.ZoneID != null)
            {
                // IList<IZoneCategory> ZoneCategory = logbook.HeartRateZones;
                foreach (IZoneCategory zone in zoneCategory)
                {
                    zoneCatIDs.Add(zone.ReferenceId, zone.Name);
                    cboCategories.Items.Add(zone.Name);
                    if (zoneCatIDs.Keys.Contains(UserData.ZoneID))
                    {
                        cboCategories.SelectedItem = zoneCatIDs[UserData.ZoneID];
                    }
                }
            }

            // Populate Rolling Sum combo
            cboRoll1.SelectedIndexChanged -= cboRoll1_SelectedIndexChanged;
            cboRoll2.SelectedIndexChanged -= cboRoll2_SelectedIndexChanged;
            cboRoll1.SelectedIndex = UserData.MovingSumCat1;
            cboRoll2.SelectedIndex = UserData.MovingSumCat2;
            cboRoll1.SelectedIndexChanged += cboRoll1_SelectedIndexChanged;
            cboRoll2.SelectedIndexChanged += cboRoll2_SelectedIndexChanged;

            // HR Category Selection
            treelistHRCats.Columns.Clear();
            treelistHRCats.Columns.Add(new TreeList.Column("Name", CommonResources.Text.LabelCategory, 150, StringAlignment.Center));
            treelistHRCats.Columns.Add(new TreeList.Column("index"));
            treelistHRCats.RowData = zoneCategory;

            this.controlInitialized = true;
        }

        #region Control Event Handlers (Store Settings)

        private void txtInitATL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // digitValidator ensures only numbers have been entered
                if (this.controlInitialized)
                {
                    UserData.InitialATL = Convert.ToInt32(txtInitialATL.Text, CultureInfo.CurrentCulture);
                    UserData.StoreSettings();
                }
            }
            catch
            {
            }
        }

        private void txtInitCTL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // digitValidator ensures only numbers have been entered
                if (this.controlInitialized)
                {
                    UserData.InitialCTL = Convert.ToInt32(txtInitialCTL.Text, CultureInfo.CurrentCulture);
                    UserData.StoreSettings();
                }
            }
            catch
            {
            }
        }

        private void txtTCa_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // digitValidator ensures only numbers have been entered
                if (this.controlInitialized)
                {
                    UserData.TCa = Convert.ToInt32(txtTCa.Text, CultureInfo.CurrentCulture);
                    UserData.StoreSettings();
                }
            }
            catch
            {
            }
        }

        private void txtTCc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // digitValidator ensures only numbers have been entered
                if (this.controlInitialized)
                {
                    UserData.TCc = Convert.ToInt32(txtTCc.Text, CultureInfo.CurrentCulture);
                    UserData.StoreSettings();
                }
            }
            catch
            {
            }
        }


        private void txtPast_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // digitValidator ensures only numbers have been entered
                if (this.controlInitialized)
                {
                    GlobalSettings.Instance.PastDays = Convert.ToInt32(txtPast.Text, CultureInfo.CurrentCulture);
                }
            }
            catch
            {
            }
        }


        private void txtFuture_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // digitValidator ensures only numbers have been entered
                if (this.controlInitialized)
                {
                    GlobalSettings.Instance.FutureDays = Convert.ToInt32(txtFuture.Text, CultureInfo.CurrentCulture);
                }
            }
            catch
            {
            }
        }

        private void txtSum1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // digitValidator ensures only numbers have been entered
                if (this.controlInitialized)
                {
                    UserData.MovingSumDays1 = Convert.ToInt32(txtSum1.Text, CultureInfo.CurrentCulture);
                    UserData.StoreSettings();
                }
            }
            catch
            {
            }
        }

        private void txtSum2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // digitValidator ensures only numbers have been entered
                if (this.controlInitialized)
                {
                    UserData.MovingSumDays2 = Convert.ToInt32(txtSum2.Text, CultureInfo.CurrentCulture);
                    UserData.StoreSettings();
                }
            }
            catch
            {
            }
        }

        private void txtFactor_Leave(object sender, EventArgs e)
        {
            // digitValidator ensures only numbers have been entered
            try
            {
                // Collect the selected Category and namedZone
                IZoneCategory selectedCategory = (IZoneCategory)treelistHRCats.Selected[0];
                INamedLowHighZone selectedZone = (INamedLowHighZone)treelistHRZones.Selected[0];

                // Store Zone info (Recovery, Aerobic, etc.) and Trimp factor
                int iZone = GetZoneIndex(selectedCategory, selectedZone);
                if (string.IsNullOrEmpty(txtFactor.Text))
                {
                    txtFactor.Text = "0.0";
                }

                float factor = (float)Convert.ToDouble(txtFactor.Text, CultureInfo.CurrentCulture);

                // Store ReferenceId and zone and factor info (trimpZone)
                TrimpZone trimpZone = new TrimpZone(selectedZone, factor);

                if (UserData.TrimpZones.ContainsKey(selectedCategory.ReferenceId))
                {
                    // Update existing zone category entry
                    if (UserData.TrimpZones[selectedCategory.ReferenceId].TrimpZones.ContainsKey(iZone))
                    {
                        // Update existing Trimp HR zone
                        if (factor != UserData.TrimpZones[selectedCategory.ReferenceId].TrimpZones[iZone].Factor)
                        {
                            UserData.TrimpZones[selectedCategory.ReferenceId].TrimpZones[iZone].Add(trimpZone, factor);
                            UserData.StoreSettings();
                        }
                    }
                    else
                    {
                        // Add new Trimp HR zone to existing category
                        UserData.TrimpZones[selectedCategory.ReferenceId].TrimpZones.Add(iZone, trimpZone);
                        UserData.TrimpZones[selectedCategory.ReferenceId].TrimpZones[iZone].Factor = factor;
                        UserData.StoreSettings();
                    }
                }
                else
                {
                    // Add new zone category entry
                    TrimpZoneCategory trimpZoneCat = new TrimpZoneCategory(selectedCategory.ReferenceId);
                    trimpZoneCat.TrimpZones.Add(iZone, trimpZone);
                    UserData.TrimpZones.Add(trimpZoneCat.ReferenceId, trimpZoneCat);
                    UserData.StoreSettings();
                }

                RefreshPage();
                RefreshChart();
            }
            catch
            {
            }
        }

        private void treelistHRCats_Click(object sender, EventArgs e)
        {
            // Show category reset button
            btnCatReset.Visible = true;
            txtFactor.Visible = false;
        }

        private void treelistHRCats_SelectedChanged(object sender, EventArgs e)
        {
            if (treelistHRCats.Selected.Count != 0)
            {
                // Show category reset button
                btnCatReset.Visible = true;
                txtFactor.Visible = false;
            }
            else
            {
                btnCatReset.Visible = true;
                txtFactor.Visible = false;
                btnCatReset.Enabled = true;
            }

            // Refresh HR Zones table & chart
            RefreshHRZonesTable();
            RefreshChart();
        }

        private void treelistHRZones_Click(object sender, EventArgs e)
        {
            // Show factor textbox
            btnCatReset.Visible = false;
            txtFactor.Visible = true;
        }

        private void treelistHRZones_SelectedChanged(object sender, EventArgs e)
        {
            if (treelistHRZones.Selected.Count != 0)
            {
                // Collect the selected Category and namedZone
                IZoneCategory selectedCategory = (IZoneCategory)treelistHRCats.Selected[0];
                INamedLowHighZone selectedZone = (INamedLowHighZone)treelistHRZones.Selected[0];

                // Get the factor from settings and put it in the textbox.
                try
                {
                    float factor = UserData.TrimpZones[selectedCategory.ReferenceId].TrimpZones[GetZoneIndex(selectedCategory, selectedZone)].Factor;
                    if (factor >= 0)
                    {
                        txtFactor.Text = Math.Round(factor,2).ToString(CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        txtFactor.Text = string.Empty;
                    }
                }
                catch
                {
                    txtFactor.Text = string.Empty;
                }

                // Show factor textbox
                btnCatReset.Visible = false;
                txtFactor.Visible = true;
            }
        }

        private void treelist_EnabledChanged(object sender, EventArgs e)
        {
            TreeList treeList = sender as TreeList;
            if (treeList != null)
            {
                if (treeList.Enabled)
                {
                    treelistHRCats.ForeColor = Color.Black;
                }
                else
                {
                    treelistHRCats.ForeColor = Color.Gray;
                    treeList.Selected = null;
                }
            }
        }

        private void chkSingleZone_CheckedChanged(object sender, EventArgs e)
        {
            cboCategories.Enabled = chkSingleZone.Checked;

            // Prevent unintentional marking of logbook as 'Modified'
            if (this.controlInitialized)
            {
                UserData.Multizone = !chkSingleZone.Checked;
                UserData.StoreSettings();
            }

            // Prevent error of selecting Single Zone without a Zone selected.
            if (!UserData.Multizone && string.IsNullOrEmpty(UserData.ZoneID) && cboCategories.Items.Count > 0)
            {
                cboCategories.SelectedIndex = 0;
            }
        }

        private void chkForecast_CheckedChanged(object sender, EventArgs e)
        {
            // Prevent unintentional marking of logbook as 'Modified'
            if (this.controlInitialized)
            {
                UserData.Forecast = chkForecast.Checked;
                UserData.StoreSettings();
            }
        }

        private void chkFilterCharts_CheckedChanged(object sender, EventArgs e)
        {
            // Prevent unintentional marking of logbook as 'Modified'
            if (this.controlInitialized)
            {
                UserData.FilterCharts = chkFilterCharts.Checked;
                UserData.StoreSettings();
            }
        }

        private void chkDynamicZones_CheckedChanged(object sender, EventArgs e)
        {
            // Prevent unintentional marking of logbook as 'Modified'
            if (this.controlInitialized)
            {
                UserData.AutoTrimp = chkDynamicZones.Checked;

                treelistHRCats.Enabled = !UserData.AutoTrimp;
                treelistHRZones.Enabled = !UserData.AutoTrimp;
                btnCatReset.Enabled = !UserData.AutoTrimp;
                chkSingleZone.Enabled = !UserData.AutoTrimp;
                cboCategories.Enabled = !UserData.AutoTrimp;

                UserData.StoreSettings();
            }
        }

        private void cboCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            IZoneCategoryList zoneCategory = logbook.HeartRateZones;
            foreach (IZoneCategory zone in zoneCategory)
            {
                if (zone.Name.Equals(cboCategories.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    if (this.controlInitialized)
                    {
                        UserData.ZoneID = zone.ReferenceId;
                        UserData.StoreSettings();
                    }

                    break;
                }
            }
        }

        private void cboRoll1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            UserData.MovingSumCat1 = cbo.SelectedIndex;
            UserData.StoreSettings();
        }

        private void cboRoll2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            UserData.MovingSumCat2 = cbo.SelectedIndex;
            UserData.StoreSettings();
        }

        private void btnResetDef_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageDialog.Show("Reset all plugin settings to default values?", "Training Load", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Reset Default values
                UserData.LoadDefaultSettings();
                UserData.StoreSettings();

                // Refresh data on page
                RefreshPage();
            }
        }

        private void btnCatReset_Click(object sender, EventArgs e)
        {
            if (treelistHRCats.Selected.Count != 0)
            {
                // Initialize variables
                float factor;

                // Get Category to reset
                IZoneCategory selectedCategory = (IZoneCategory)treelistHRCats.Selected[0];

                // Reset Each zone factor within the selected category
                foreach (INamedLowHighZone zone in selectedCategory.Zones)
                {
                    factor = TrimpZone.GetDefaultZoneFactor(zone, DateTime.Now);
                    UserData.TrimpZones[selectedCategory.ReferenceId].TrimpZones[GetZoneIndex(selectedCategory, zone)].Factor = factor;
                }

                UserData.StoreSettings();
                RefreshChart();
                RefreshHRZonesTable();
            }
        }

        #endregion

        #region Utilities

        internal void ThemeChanged(ITheme visualTheme)
        {
            // Adapt settings page to look right
            chartFactor.ThemeChanged(visualTheme);
        }

        internal void UICultureChanged(CultureInfo culture)
        {
            lblATL.Text = Resources.Strings.Label_ATL;
            lblATLInit.Text = Resources.Strings.Label_InitialATL;
            lblCTL.Text = Resources.Strings.Label_CTL;
            lblCTLInit.Text = Resources.Strings.Label_InitialCTL;
            lblFactor.Text = Resources.Strings.Label_Factor;
            lblDays1.Text = Resources.Strings.Label_Days;
            lblDays2.Text = Resources.Strings.Label_Days;
            lblSum1.Text = Resources.Strings.Label_RollingSum + " 1";
            lblSum2.Text = Resources.Strings.Label_RollingSum + " 2";
            lblDefaultView.Text = Resources.Strings.Label_DefaultChartZoom;
            lblPast.Text = Resources.Strings.Label_Past;
            lblFuture.Text = Resources.Strings.Label_Future;

            chkForecast.Text = Resources.Strings.Label_ForecastCTLATLTSB;
            chkSingleZone.Text = Resources.Strings.Label_SingleZone;
            chkDynamicZones.Text = Resources.Strings.Label_AutomaticMode;
            chkFilterCharts.Text = Resources.Strings.Label_FilterCharts;
            
            chkCusFTPcycle.Text = Data.CustomDataFields.GetCustomText(TrainingLoad.Data.CustomDataFields.CustomFields.FTPcycle);
            chkCusNP.Text = Data.CustomDataFields.GetCustomText(TrainingLoad.Data.CustomDataFields.CustomFields.NormPwr);
            chkCusTrimp.Text = Data.CustomDataFields.GetCustomText(TrainingLoad.Data.CustomDataFields.CustomFields.Trimp);
            chkCusTSS.Text = Data.CustomDataFields.GetCustomText(TrainingLoad.Data.CustomDataFields.CustomFields.TSS);
            
            btnCatReset.Text = Resources.Strings.Label_ResetCategory;
            btnResetDef.Text = Resources.Strings.Label_Reset;
            
            grpHRZones.Text = Resources.Strings.Label_HRZones;
            grpTimeConst.Text = Resources.Strings.Label_TimeConstants;
            grpRolling.Text = Resources.Strings.Label_RollingSum;
            grpOptions.Text = Resources.Strings.Label_Options;
            grpCustomParams.Text = Resources.Strings.Label_CustomFields;

            cboRoll1.Items.Clear();
            cboRoll1.Items.Add(string.Format("{0}/{1}",Resources.Strings.Label_TRIMP, Resources.Strings.Label_TSS));
            cboRoll1.Items.Add(CommonResources.Text.LabelDistance);
            cboRoll1.Items.Add(CommonResources.Text.LabelTime);

            cboRoll2.Items.Clear();
            cboRoll2.Items.Add(string.Format("{0}/{1}", Resources.Strings.Label_TRIMP, Resources.Strings.Label_TSS));
            cboRoll2.Items.Add(CommonResources.Text.LabelDistance);
            cboRoll2.Items.Add(CommonResources.Text.LabelTime);

            toolTipHelp.SetToolTip(lblATL, Resources.Strings.ToolTip_ATL);
            toolTipHelp.SetToolTip(lblCTL, Resources.Strings.ToolTip_CTL);
            toolTipHelp.SetToolTip(chkFilterCharts, Resources.Strings.ToolTip_FilterCharts);
            toolTipHelp.SetToolTip(chkForecast, Resources.Strings.ToolTip_Forecast);
            toolTipHelp.SetToolTip(chkDynamicZones, Resources.Strings.ToolTip_DynamicZones);
            toolTipHelp.SetToolTip(chkSingleZone, Resources.Strings.ToolTip_SingleZone);
            toolTipHelp.SetToolTip(cboCategories, Resources.Strings.ToolTip_SingleZone2);
            toolTipHelp.SetToolTip(txtInitialATL, Resources.Strings.ToolTip_InitialATL);
            toolTipHelp.SetToolTip(txtInitialCTL, Resources.Strings.ToolTip_InitialCTL);
            toolTipHelp.SetToolTip(txtTCa, Resources.Strings.ToolTip_TCa);
            toolTipHelp.SetToolTip(txtTCc, Resources.Strings.ToolTip_TCc);
            toolTipHelp.SetToolTip(btnCatReset, Resources.Strings.ToolTip_ResetCategory);
        }

        private void digitValidator(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true; // input is not passed on to the control(TextBox)`
            }
        }

        /// <summary>
        /// Refresh factor/HR Zone relation chart.
        /// </summary>
        private void RefreshChart()
        {
            // Initialize the charts, dataseries, and variables
            IAxis axis = chartFactor.YAxis;
            ChartDataSeries dsFactor = new ChartDataSeries(chartFactor, axis);
            ChartDataSeries dsFactorLine = new ChartDataSeries(chartFactor, axis);
            PointF point = new PointF();

            if (treelistHRCats.Selected.Count != 0)
            {
                IZoneCategory selectedCategory = (IZoneCategory)treelistHRCats.Selected[0];
                int i = 0;
                float maxHR = logbook.Athlete.InfoEntries.LastEntryAsOfDate(DateTime.Now).MaximumHeartRatePerMinute;

                // Add each point
                foreach (TrimpZone zone in UserData.TrimpZones[selectedCategory.ReferenceId].TrimpZones.Values)
                {
                    if (zone.Factor != 0)
                    {
                        point.X = zone.Low;
                        point.Y = zone.Factor;
                        dsFactor.Points.Add(i, point);

                        if (!double.IsInfinity(zone.High))
                        {
                            point.X = (zone.Low + zone.High) / 2;
                            dsFactorLine.Points.Add(i, point);
                        }
                        else
                        {
                            point.X = (zone.Low + maxHR) / 2;
                            dsFactorLine.Points.Add(i, point);
                        }

                        i++;
                    }
                }

                // Complete chart up to athlete's max HR
                if (point.X < maxHR)
                {
                    point.X = maxHR;
                    dsFactor.Points.Add(i, point);
                }

                axis.Label = Resources.Strings.Label_Factor;
                chartFactor.XAxis.Label = CommonResources.Text.LabelHeartRate;
                chartFactor.DataSeries.Clear();
                dsFactor.ChartType = ChartDataSeries.Type.StepFill;
                dsFactor.LineColor = Color.Blue;
                dsFactor.ValueAxis = axis;
                chartFactor.DataSeries.Add(dsFactor);

                // Add a line for trend visualization
                dsFactorLine.ChartType = ChartDataSeries.Type.Line;
                dsFactorLine.LineColor = Color.Red;
                dsFactorLine.ValueAxis = axis;
                chartFactor.DataSeries.Add(dsFactorLine);

                chartFactor.AutozoomToData(true);
            }
            else
            {
                chartFactor.DataSeries.Clear();
                chartFactor.Refresh();
            }
        }

        private void RefreshHRZonesTable()
        {
            if (treelistHRCats.Selected.Count != 0)
            {
                // Called to update the view of each of the HR Zones
                IZoneCategory selectedCat = (IZoneCategory)treelistHRCats.Selected[0];

                if (!UserData.TrimpZones.ContainsKey(selectedCat.ReferenceId))
                {
                    // Initialize with default values if a user has added a new category since things were loaded.
                    UserData.ResetCategory(selectedCat);
                }

                // Update changed HR Zones
                int iZone = 0;
                foreach (INamedLowHighZone zone in selectedCat.Zones)
                {
                    if (UserData.TrimpZones[selectedCat.ReferenceId].TrimpZones.ContainsKey(iZone))
                    {
                        UserData.TrimpZones[selectedCat.ReferenceId].TrimpZones[iZone].Add(zone);
                    }
                    else
                    {
                        UserData.TrimpZones[selectedCat.ReferenceId].TrimpZones.Add(iZone, new TrimpZone(zone));
                    }

                    iZone++;
                }

                // HR Zones
                treelistHRZones.Columns.Clear();
                treelistHRZones.Columns.Add(new TreeList.Column("Name", CommonResources.Text.LabelZone, 120, StringAlignment.Near));
                treelistHRZones.Columns.Add(new TreeList.Column("Low", Resources.Strings.Label_Low, 60, StringAlignment.Center));
                treelistHRZones.Columns.Add(new TreeList.Column("High", Resources.Strings.Label_High, 60, StringAlignment.Center));
                treelistHRZones.Columns.Add(new TreeList.Column("Factor", Resources.Strings.Label_Factor, 60, StringAlignment.Center));
                treelistHRZones.RowData = UserData.TrimpZones[selectedCat.ReferenceId].TrimpZones.Values;
            }
            else
            {
                treelistHRZones.RowData = null;
            }
        }

        /// <summary>
        /// Returns the index of the selected Zone within the Category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="lowHighZone"></param>
        /// <returns>Returns zone index, or 0 if not found</returns>
        internal static int GetZoneIndex(IZoneCategory category, INamedLowHighZone lowHighZone)
        {
            // Try to compare HashCodes first
            int i = 0;
            foreach (INamedLowHighZone zone in category.Zones)
            {
                if (zone.GetHashCode() == lowHighZone.GetHashCode())
                {
                    return i;
                }

                i++;
            }

            // If hash codes do not return proper value, compare low values
            i = 0;
            foreach (INamedLowHighZone zone in category.Zones)
            {
                if (zone.Low == lowHighZone.Low)
                {
                    return i;
                }

                i++;
            }

            return 0;
        }

        #endregion

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            if (pageActivate == null || pageActivate.IsDisposed)
            {
                pageActivate = new Activation();
            }

            pageActivate.Show();
        }

        private void chkEnableCustomParam_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = sender as CheckBox;
            string id = item.Tag as string;

            switch (id)
            {
                case "FTPcycle":
                    UserData.EnableFTP = chkCusFTPcycle.Checked;
                    break;
                case "NormPwr":
                    UserData.EnableNormPwr = chkCusNP.Checked;
                    break;
                case "TRIMP":
                    UserData.EnableTRIMP = chkCusTrimp.Checked;
                    break;
                case "TSS":
                    UserData.EnableTSS = chkCusTSS.Checked;
                    break;

            }
        }
    }
}
