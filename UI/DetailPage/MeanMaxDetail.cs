namespace TrainingLoad.UI.DetailPage
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using TrainingLoad.UI.View;
    using ZedGraph;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;

    internal partial class MeanMaxDetail : UserControl
    {
        #region Fields

        private IActivity activity;
        private ActivityInfo info;
        private Common.ChartBasis chartType;
        private static MeanMaxDetail control;

        #endregion

        #region Constructor

        internal MeanMaxDetail()
        {
            InitializeComponent();

            // Setup button images
            SaveImageButton.CenterImage = CommonResources.Images.Save16;
            RefreshButton.CenterImage = CommonResources.Images.Refresh16;
            ZoomInButton.CenterImage = CommonResources.Images.ZoomIn16;
            ZoomOutButton.CenterImage = CommonResources.Images.ZoomOut16;
            ZoomChartButton.CenterImage = Resources.Images.ZoomFit;
            ExtraChartsButton.CenterImage = Resources.Images.Charts;
            ExportButton.CenterImage = CommonResources.Images.Export16;
            MaximizeButton.CenterImage = Resources.Images.PanelExpand;

            chartType = Common.ChartBasis.Power;
            ChartBanner.Text = CommonResources.Text.LabelPower;
            zedChart.GraphPane.XAxis.ScaleFormatEvent += new Axis.ScaleFormatHandler(XScaleFormatEvent);
            zedChart.PointValueEvent += new ZedGraphControl.PointValueHandler(zedChart_PointValueEvent);
            control = this;

            // Setup tool tips
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip.SetToolTip(this.SaveImageButton, CommonResources.Text.ActionSave);
            toolTip.SetToolTip(this.ZoomInButton, CommonResources.Text.ActionZoomIn);
            toolTip.SetToolTip(this.ZoomOutButton, CommonResources.Text.ActionZoomOut);
            toolTip.SetToolTip(this.ZoomChartButton, "Fit to Window");
            toolTip.SetToolTip(this.ExtraChartsButton, "More Charts");
            toolTip.SetToolTip(this.RefreshButton, CommonResources.Text.ActionRefresh);
            toolTip.SetToolTip(this.ExportButton, CommonResources.Text.ActionExport);
        }

        #endregion

        #region Properties

        internal IActivity Activity
        {
            set
            {
                activity = value;
                if (activity != null)
                {
                    info = ActivityInfoCache.Instance.GetInfo(activity);

                    if (activity.CadencePerMinuteTrack != null)
                    {
                        cadenceToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        cadenceToolStripMenuItem.Enabled = false;
                        if (cadenceToolStripMenuItem.Checked)
                        {
                            DetailMenuItem_Click(heartRateToolStripMenuItem, null);
                        }
                    }

                    if (activity.HeartRatePerMinuteTrack != null)
                    {
                        heartRateToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        heartRateToolStripMenuItem.Enabled = false;
                        if (heartRateToolStripMenuItem.Checked)
                        {
                            DetailMenuItem_Click(powerToolStripMenuItem, null);
                        }
                    }

                    if (activity.PowerWattsTrack != null)
                    {
                        powerToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        powerToolStripMenuItem.Enabled = false;
                        if (powerToolStripMenuItem.Checked)
                        {
                            DetailMenuItem_Click(heartRateToolStripMenuItem, null);
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the chart type.  Note that chart is automatically 
        /// refreshed any time charttype is changed.
        /// </summary>
        internal Common.ChartBasis ChartType
        {
            get
            {
                return chartType;
            }

            set
            {
                chartType = value;
                RefreshPage();
            }
        }

        #endregion

        internal void RefreshPage()
        {
            if (activity != null)
            {
                INumericTimeDataSeries track = new NumericTimeDataSeries();

                // Try to pull from memory if available
                track = MeanMaxCache.GetTrack(activity, ChartType);
                updateZedGraph(track, zedChart, ChartType);
            }
        }

        /// <summary>
        /// Add 'track' to 'graph' and apply labels based on 'chartType'
        /// </summary>
        /// <param name="track">Data track</param>
        /// <param name="graph">Which graph to stick the data on</param>
        /// <param name="chartType">This determines the labeling, coloring, etc. (all appearance related)</param>
        internal static void updateZedGraph(INumericTimeDataSeries track, ZedGraphControl graph, Common.ChartBasis chartType)
        {
            GraphPane myPane = graph.GraphPane;
            myPane.XAxis.Title.Text = CommonResources.Text.LabelTime;
            myPane.XAxis.Type = AxisType.Log;

            Color mainCurveColor = Color.FromArgb(204, 0, 0);

            switch (chartType)
            {
                case Common.ChartBasis.Cadence:
                    mainCurveColor = Common.ColorCadence;
                    myPane.YAxis.Title.Text = CommonResources.Text.LabelCadence;
                    break;
                case Common.ChartBasis.HR:
                    mainCurveColor = Common.ColorHR;
                    myPane.YAxis.Title.Text = CommonResources.Text.LabelHeartRate + " " + CommonResources.Text.LabelBPM;
                    break;
                case Common.ChartBasis.Power:
                    mainCurveColor = Common.ColorPower;
                    myPane.YAxis.Title.Text = CommonResources.Text.LabelPower;
                    break;
            }

            myPane.XAxis.MinorTic.IsOutside = true;

            PointPairList zedTrack = new PointPairList();
            foreach (ITimeValueEntry<float> item in track)
            {
                zedTrack.Add(item.ElapsedSeconds, item.Value);
            }

            myPane.CurveList.Clear();
            LineItem curve = myPane.AddCurve("Curve Label", zedTrack, mainCurveColor, SymbolType.None);
            curve.Line.Width = 2f;
            curve.Line.Fill.Type = FillType.Solid;
            curve.Line.Fill.Color = Color.FromArgb(50, mainCurveColor);
            myPane.YAxis.Title.FontSpec.FontColor = mainCurveColor;
            myPane.YAxis.Scale.FontSpec.FontColor = mainCurveColor;

            graph.AxisChange();
            graph.Refresh();
        }

        internal void ThemeChanged(ITheme visualTheme)
        {
            ButtonPanel.ThemeChanged(visualTheme);
            ButtonPanel.BackColor = visualTheme.Window;
            ChartBanner.ThemeChanged(visualTheme);
            panelMain.ThemeChanged(visualTheme);
            panelMain.BackColor = visualTheme.Window;
            zedThemeChanged(visualTheme, zedChart);
        }

        /// <summary>
        /// Setup 'graph' to look like a SportTracks chart.
        /// </summary>
        /// <param name="visualTheme">Theme to apply</param>
        /// <param name="graph">ZedChart that we're masquerading as a SportTracks chart</param>
        internal static void zedThemeChanged(ITheme visualTheme, ZedGraphControl graph)
        {
            GraphPane myPane = graph.GraphPane;

            // Overall appearance settings
            graph.BorderStyle = BorderStyle.None;
            myPane.Legend.IsVisible = false;
            myPane.Border.IsVisible = false;
            myPane.Title.IsVisible = false;

            // Add a background color
            myPane.Fill.Color = visualTheme.Window;
            myPane.Chart.Fill = new Fill(visualTheme.Window);
            myPane.Chart.Border.IsVisible = false;

            // Add gridlines to the plot, and make them gray
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.Color = Color.DarkGray;
            myPane.YAxis.MajorGrid.Color = myPane.XAxis.MajorGrid.Color;
            myPane.XAxis.MajorGrid.DashOff = 1f;
            myPane.XAxis.MajorGrid.DashOff = myPane.XAxis.MajorGrid.DashOn;
            myPane.YAxis.MajorGrid.DashOff = myPane.XAxis.MajorGrid.DashOn;
            myPane.YAxis.MajorGrid.DashOff = myPane.YAxis.MajorGrid.DashOn;
            myPane.XAxis.IsAxisSegmentVisible = true;
            myPane.YAxis.IsAxisSegmentVisible = true;

            // Update axis Tic marks
            myPane.XAxis.MinorTic.IsAllTics = false;
            myPane.XAxis.MajorTic.IsAllTics = false;
            myPane.YAxis.MinorTic.IsAllTics = false;
            myPane.YAxis.MajorTic.IsAllTics = false;
            myPane.XAxis.MajorTic.IsOutside = true;
            myPane.YAxis.MajorTic.IsOutside = true;

            // Setup Text Appearance
            string fontName = "Microsoft Sans Sarif";
            myPane.IsFontsScaled = false;
            myPane.XAxis.Title.FontSpec.Family = fontName;
            myPane.XAxis.Title.FontSpec.IsBold = true;
            myPane.XAxis.Scale.FontSpec.Family = fontName;
            myPane.XAxis.Scale.IsUseTenPower = false;

            Color mainCurveColor;
            if (myPane.CurveList.Count > 0)
            {
                mainCurveColor = myPane.CurveList[0].Color;
            }
            else
            {
                mainCurveColor = Color.Black;
            }

            myPane.YAxis.Title.FontSpec.Family = fontName;
            myPane.YAxis.Title.FontSpec.IsBold = true;
            myPane.YAxis.Title.FontSpec.FontColor = mainCurveColor;
            myPane.YAxis.Scale.FontSpec.FontColor = mainCurveColor;
            myPane.YAxis.Scale.FontSpec.Family = fontName;

            graph.Refresh();
        }

        internal void UICultureChanged(CultureInfo culture)
        {

        }

        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            SaveImage dlg = new SaveImage();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
                zedChart.GetImage().Save(dlg.FileName, dlg.ImageFormat);
            }

            dlg.Dispose();
        }

        private void ZoomChartButton_Click(object sender, EventArgs e)
        {
            zedChart.ZoomOutAll(zedChart.GraphPane);
        }

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            zedChart.ZoomPane(zedChart.GraphPane, 1.1, zedChart.GraphPane.Chart.Rect.Location, false);
        }

        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            zedChart.ZoomPane(zedChart.GraphPane, 0.9, zedChart.GraphPane.Chart.Rect.Location, false);
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            // Nothing to export if activity is empty
            if (activity == null)
            {
                return;
            }

            // Open File Save dialog to create new CSV Document
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "MeanMax " + activity.StartTime.ToLocalTime().ToString("yyyy-MM-dd");
            saveFile.Filter = "All Files (*.*)|*.*|Comma Separated Values (*.csv)|*.csv";
            saveFile.FilterIndex = 2;
            saveFile.DefaultExt = "csv";
            saveFile.OverwritePrompt = true;

            string comma = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;

            // Cancel if user doesn't select a file
            if (saveFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            // Export mean-max data
            INumericTimeDataSeries track = MeanMaxCache.GetTrack(activity, ChartType);
            Utilities.ExportTrack(track, saveFile.FileName);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            if (activity != null)
            {
                MeanMaxCache.ClearTrack(activity.ReferenceId + ChartType);
                RefreshPage();
            }
        }

        private void MaximizeButton_Click(object sender, EventArgs e)
        {
            MeanMax.Instance.PageMaximized = !MeanMax.Instance.PageMaximized;
        }

        private void ChartBanner_MenuClicked(object sender, EventArgs e)
        {
            mnuDetail.Show(ChartBanner, new Point(ChartBanner.Right - 2, ChartBanner.Bottom), ToolStripDropDownDirection.BelowLeft);
        }

        /// <summary>
        /// Change ChartType (HR, Power, Cadence, etc.) from menu
        /// </summary>
        /// <param name="sender">menu item that was clicked</param>
        /// <param name="e">This item is not used</param>
        private void DetailMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selected = sender as ToolStripMenuItem;

            for (int i = 0; i < mnuDetail.Items.Count; i++)
            {
                ToolStripMenuItem item = mnuDetail.Items[i] as ToolStripMenuItem;

                if (item != null)
                {
                    if (item != selected)
                    {
                        item.Checked = false;
                    }
                    else
                    {
                        item.Checked = true;
                    }
                }
                else
                {
                    // ToolStrip Separator encountered.  Stop evaluating
                    break;
                }
            }

            ChartBanner.Text = selected.Text.ToString();
            ChartType = (Common.ChartBasis)Enum.Parse(typeof(Common.ChartBasis), selected.Tag.ToString());
        }

        /// <summary>
        /// Formats labels for x-axis of ZedChart.
        /// </summary>
        /// <param name="pane">The parameter is not used.</param>
        /// <param name="axis">The parameter is not used.</param>
        /// <param name="val">The parameter is not used.</param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string XScaleFormatEvent(GraphPane pane, Axis axis, double val, int index)
        {
            TimeSpan span = new TimeSpan(0, 0, (int)Math.Pow(10, index));
            return (int)span.TotalMinutes + ":" + span.Seconds.ToString("00");
        }

        /// <summary>
        /// Formats the tooltip popups
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="pane">The parameter is not used.</param>
        /// <param name="curve">The curve containing the points</param>
        /// <param name="iPt">The index of the point of interest</param>
        /// <returns>A tooltip string</returns>
        string zedChart_PointValueEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            string tooltip;
            TimeSpan time = new TimeSpan(0, 0, (int)curve[iPt].X);
            tooltip = time.ToString() + "\r\n";
            //tooltip = (time.Hours * 60 + time.Minutes).ToString("00") + ":" + time.Seconds.ToString("00") + "\r\n";
            tooltip = tooltip + curve[iPt].Y.ToString("0");
            switch (ChartType)
            {
                case Common.ChartBasis.Cadence:
                    tooltip = tooltip + " RPM";
                    break;
                case Common.ChartBasis.HR:
                    tooltip = tooltip + " BPM";
                    break;
                case Common.ChartBasis.Power:
                    tooltip = tooltip + " watts";
                    break;
            }
            return tooltip;
        }
    }
}
