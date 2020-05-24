// <copyright file="ViewTrainingLoadPageControl.cs" company="N/A">
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
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using Mechgt.Licensing;
    using TrainingLoad.Data;
    using TrainingLoad.Resources;
    using TrainingLoad.Settings;
    using TrainingLoad.UI;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Chart;
    using ZoneFiveSoftware.Common.Visuals.Forms;
    using System.ComponentModel;

    public partial class ViewTrainingLoadPageControl : UserControl
    {
        #region Fields

        private IEnumerable<IActivity> activities;
        private string currentSortColumnId = "StartTime";
        private bool currentSortDirection;
        private bool autozoom = true;
        private List<TreeList.Column> columns = new List<TreeList.Column>();
        private Status pageStatus = Status.Status;
        private Point tooltipMousePt;

        private static string defaultSeries;

        #endregion

        #region Constructor

        public ViewTrainingLoadPageControl(ILogbookActivityList activities)
        {
            this.Activities = activities;

            // Define all available columns and setup default characteristics
            string elevUnits = Length.LabelAbbr(PluginMain.GetApplication().SystemPreferences.ElevationUnits);
            string distUnits = Length.LabelAbbr(PluginMain.GetApplication().SystemPreferences.DistanceUnits);
            string paceUnits = Speed.Label(Speed.Units.Pace, new Length(1, PluginMain.GetApplication().SystemPreferences.DistanceUnits));
            string spdUnits = Speed.Label(Speed.Units.Speed, new Length(1, PluginMain.GetApplication().SystemPreferences.DistanceUnits));

            /* NOTE: All columns added will need to have:
             *  1) Sort method enumeration in TrimpActivity.TrimpActivityComparer.ComparisonType
             *  2) Define ComparerInstance in SortActivityTreeList
             *  3) Define how to sort in TrimpActivity.CompareTO(...) method
             *  4) Setup the column for localization changes
             */
            this.columns.Add(new TreeList.Column("StartTime", CommonResources.Text.LabelStartTime, 140, StringAlignment.Near));
            this.columns.Add(new TreeList.Column("TRIMP", Resources.Strings.Label_TRIMP, 45, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("CTL", Resources.Strings.Label_CTL, 40, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("ATL", Resources.Strings.Label_ATL, 40, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("TSBPre", Resources.Strings.Label_TSB + " (" + Resources.Strings.Label_Before.ToLower(CultureInfo.CurrentCulture) + ")", 50, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("TSBPost", Resources.Strings.Label_TSB + " (" + Resources.Strings.Label_After.ToLower(CultureInfo.CurrentCulture) + ")", 50, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("NormalizedPower", Resources.Strings.Label_NormPower + " (" + CommonResources.Text.LabelWatts + ")", 50, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("AvgPower", CommonResources.Text.LabelAvgPower + " (" + CommonResources.Text.LabelWatts + ")", 50, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("Location", CommonResources.Text.LabelLocation, 150, StringAlignment.Near));
            this.columns.Add(new TreeList.Column("Category", CommonResources.Text.LabelCategory, 120, StringAlignment.Near));
            this.columns.Add(new TreeList.Column("Distance", CommonResources.Text.LabelDistance + " (" + distUnits + ")", 55, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("TotalTime", CommonResources.Text.LabelTime, 50, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("TotalAscend", CommonResources.Text.LabelAscending + " (" + elevUnits + ")", 65, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("TotalDescend", CommonResources.Text.LabelDescending + " (" + elevUnits + ")", 70, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("AvgPace", CommonResources.Text.LabelAvgPace + " (" + paceUnits + ")", 55, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("AvgSpeed", CommonResources.Text.LabelAvgSpeed + " (" + spdUnits + ")", 55, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("AvgHR", CommonResources.Text.LabelAvgHR, 65, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("Calories", CommonResources.Text.LabelCalories, 60, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("AvgCadence", CommonResources.Text.LabelAvgCadence, 55, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("AvgGrade", CommonResources.Text.LabelAvgGrade, 50, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("Day", CommonResources.Text.LabelDayOfWeek, 75, StringAlignment.Near));
            this.columns.Add(new TreeList.Column("FastestPace", CommonResources.Text.LabelFastestPace + " (" + paceUnits + ")", 55, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("FastestSpeed", CommonResources.Text.LabelFastestSpeed + " (" + spdUnits + ")", 55, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("MaxHR", CommonResources.Text.LabelMaxHR, 50, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("MaxPower", CommonResources.Text.LabelMaxPower + " (" + CommonResources.Text.LabelWatts + ")", 50, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("Name", CommonResources.Text.LabelName, 150, StringAlignment.Near));
            this.columns.Add(new TreeList.Column("Notes", CommonResources.Text.LabelNotes, 150, StringAlignment.Near));
            this.columns.Add(new TreeList.Column("RollingSum1", Resources.Strings.Label_RollingSum + " 1", 50, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("RollingSum2", Resources.Strings.Label_RollingSum + " 2", 50, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("TSS", Resources.Strings.Label_TSS, 45, StringAlignment.Far));
            this.columns.Add(new TreeList.Column("IntensityFactor", Resources.Strings.Label_IntensityFactor, 55, StringAlignment.Far));

            InitializeComponent();

            Pane.YAxis.Title.FontSpec.FontColor = Color.DarkGoldenrod;
            Pane.YAxis.MajorTic.Color = Color.DarkGoldenrod;
            Pane.YAxis.Scale.FontSpec.FontColor = Color.DarkGoldenrod;
            Pane.YAxis.Tag = "SCORE";

            Pane.BarSettings.ClusterScaleWidth = 1;

            AddY2Axis(Pane, Resources.Strings.Label_CTLATL, Color.DarkSlateBlue, "CTL");
            AddY2Axis(Pane, Resources.Strings.Label_TrainingStressBalance, Color.DarkOliveGreen, "TSB");
            AddY2Axis(Pane, Resources.Strings.Label_TrainingInfluence, Color.OrangeRed, "INF");
            AddY2Axis(Pane, CommonResources.Text.LabelDistance, Color.Sienna, "MOV1");
            AddY2Axis(Pane, CommonResources.Text.LabelTime, Color.Peru, "MOV2");
            AddY2Axis(Pane, CommonResources.Text.LabelWeight, Color.HotPink, "WEIGHT");
            AddY2Axis(Pane, CommonResources.Text.LabelBodyFatPct, Color.Green, "BF");
            AddY2Axis(Pane, Resources.Strings.Label_BMI, Color.Maroon, "BMI");
            AddY2Axis(Pane, Resources.Strings.Label_FunctionalThresholdPower, Color.FromArgb(92, 53, 102), "FTP");
            SetChartGrace();

            Pane.XAxis.Type = AxisType.Date;
            Pane.XAxis.Title.Text = CommonResources.Text.LabelDate; // "Activity Date"

#if Debug
            // TODO: (LOW)Enable/disable Daniels points
            mnuItemDaniels.Visible = true;
#endif

            Zone easy = new Zone(Pane, 0, TrainingLoad.Resources.Strings.Label_Low, Color.Cornsilk, 100);
            Zone med = new Zone(Pane, 100, TrainingLoad.Resources.Strings.Label_Medium, Color.Brown, 100);
            Zone hard = new Zone(Pane, 200, TrainingLoad.Resources.Strings.Label_High, Color.BurlyWood, 100);
            Zone epic = new Zone(Pane, 300, TrainingLoad.Resources.Strings.Label_Epic, Color.Tomato, 10000);
            Pane.GraphObjList.AddRange(easy);
            Pane.GraphObjList.AddRange(med);
            Pane.GraphObjList.AddRange(hard);
            Pane.GraphObjList.AddRange(epic);

            // Add selction highlight
            Pane.GraphObjList.Add(ChartData.HighlightFocus);
            Pane.GraphObjList.Add(ChartData.HighlightToday);

            mnuItemMultisportEnable.Checked = GlobalSettings.Instance.EnableMultisport;
            mnuItemStackCTL.Checked = GlobalSettings.Instance.CTLstacked;
            mnuItemStackCTL.Enabled = GlobalSettings.Instance.EnableMultisport;
            mnuItemShowFitPlan.Checked = GlobalSettings.Instance.ShowFitPlanWorkouts;
            mnuItemShowFitPlan.Enabled = FitPlanPlugin.IsInstalled;

            GlobalSettings.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(GlobalSettings_PropertyChanged);
            UserData.PropertyChanged += new PropertyChangedEventHandler(UserData_PropertyChanged);
        }

        #endregion

        #region Enumerations

        private enum Status
        {
            Status, HR, Power, Speed, Pace, Cadence, Climb, Settings
        }

        #endregion

        #region Properties

        internal IEnumerable<IActivity> Activities
        {
            set
            {
                if (this.activities != value)
                {
                    this.activities = value;
                    ChartData.Activities = value;
                }
            }
        }

        internal GraphPane Pane
        {
            get { return MainChart.GraphPane; }
        }

        //internal static string DefaultSeries
        //{
        //    get { return defaultSeries; }
        //    set { defaultSeries = value; }
        //}

        #endregion

        #region Methods

        /// <summary>
        /// Refresh Training Load page
        /// </summary>
        internal void RefreshPage()
        {
            // Calculate data
            ChartData.CalculateDataSeries();

            // Get data for Influence Data Series and update status window
            UpdateInfluenceStatus();

            // Process TRIMP / TSS bars based on settings.  This should be the only difference between the two.
            switch (ChartData.TLChartBasis)
            {
                case Common.ChartBasis.Power:
                    Pane.YAxis.Title.Text = Resources.Strings.Label_TrainingStressScore;
                    mnuItemTSS.Checked = true;
                    break;

                case Common.ChartBasis.HR:
                    Pane.YAxis.Title.Text = Resources.Strings.Label_TRIMP;
                    mnuItemTrimp.Checked = true;
                    break;

                // TODO: (LOW)Daniels is untested
                case Common.ChartBasis.Daniels:
                    Pane.YAxis.Title.Text = Resources.Strings.Label_DanielsPoints;
                    mnuItemDaniels.Checked = true;
                    break;
            }

            string[] charts = UserData.UserCharts.Split(';');

            // Hide unselected chartlines
            foreach (CurveItem curve in Pane.CurveList)
            {
                string tag = curve.Tag as string;
                if (!UserData.UserCharts.Contains(tag))
                {
                    curve.IsVisible = false;
                    curve.ValueAxis(Pane).IsVisible = false;
                }
            }

            ChartData.CTL.Line.Fill.IsVisible = !GlobalSettings.Instance.EnableMultisport;

            // Show selected chartlines
            foreach (string item in charts)
            {
                int index = Pane.CurveList.IndexOfTag(item);

                if (item == "CTL" && GlobalSettings.Instance.EnableMultisport)
                {
                    // Handle CTL Stacked ChartLines: Display, formatting, etc.

                    // Stack colors (used if applicable)
                    Color[] colors = { Color.Chocolate, Color.Chartreuse, Color.Indigo, Color.DarkOrange, Color.DarkTurquoise, Color.Green };
                    int colorIndex = 0;

                    foreach (LineItem curve in ChartData.CTLstack)
                    {
                        if (!Pane.CurveList.Contains(curve))
                            Pane.CurveList.Add(curve);

                        // Hide empty stack categories
                        double minValue = 2;
                        for (int i = 0; i < curve.NPts; i++)
                        {
                            if (minValue < curve[i].Z)
                            {
                                curve.IsVisible = true;
                                break;
                            }
                        }

                        // CTL Stack display options (colors, etc.)
                        if (GlobalSettings.Instance.CTLstacked)
                        {
                            curve.Line.Color = Color.Blue;
                            curve.Line.Fill = new Fill(Color.FromArgb(20, Color.Blue));
                            curve.Line.SelectedFill = new Fill(Color.FromArgb(50, curve.Line.Color));
                        }
                        else if (colorIndex < colors.Length)
                        {
                            curve.Line.Color = colors[colorIndex];
                            curve.Line.Fill = new Fill(Color.FromArgb(40, colors[colorIndex]));
                            curve.Line.SelectedFill = new Fill(Color.FromArgb(120, curve.Line.Color));
                        }
                        else
                        {
                            Color color = Utilities.RandomColor(160, 170, Utilities.GetCategory(curve.Tag as string).GetHashCode());
                            curve.Line.Color = color;
                            curve.Line.Fill = new Fill(Color.FromArgb(40, color));
                            curve.Line.SelectedFill = new Fill(Color.FromArgb(120, curve.Line.Color));
                        }

                        colorIndex++;
                    }
                }

                if (index == -1)
                {
                    try
                    {
                        ChartData.DataType type = (ChartData.DataType)Enum.Parse(typeof(ChartData.DataType), item);
                        CurveItem curve = ChartData.GetCurve(type);
                        Pane.CurveList.Add(curve);
                        curve.ValueAxis(Pane).IsVisible = true;
                    }
                    catch (ArgumentException e)
                    {
                        // Could not parse text to enumeration
                    }
                }
                else
                {
                    Pane.CurveList[index].IsVisible = true;
                    Pane.CurveList[index].ValueAxis(Pane).IsVisible = true;
                }
            }

            // Define the Rolling Sum dataseries (contains all definitions pertaining to the line)
            GetY2Axis("MOV1").Title.Text = GetRollingAxisName(UserData.MovingSumDays1, UserData.MovingSumCat1);
            GetY2Axis("MOV2").Title.Text = GetRollingAxisName(UserData.MovingSumDays2, UserData.MovingSumCat2);

            if (UserData.MovingSumCat1 == (int)UserData.SumCat.Time)
                GetY2Axis("MOV1").Type = AxisType.Time;
            else
                GetY2Axis("MOV1").Type = AxisType.Linear;

            if (UserData.MovingSumCat2 == (int)UserData.SumCat.Time)
                GetY2Axis("MOV2").Type = AxisType.Time;
            else
                GetY2Axis("MOV2").Type = AxisType.Linear;

            // Only auto-zoom when specified (on the initial load for instance)
            if (autozoom)
            {
                ZoomDefault();
                autozoom = false;
            }
            else
            {
                // TODO: (LOW)Restore zoom?
            }

            // Populate the activity tree below the chart
            Pane.AxisChange();
            RefreshActivityTree();
            Utilities.RefreshCalendar(this.activities);

            valCTL.Text = ChartData.GetValue(ChartData.DataType.CTL, DateTime.Now.Date.ToLocalTime()).ToString("0", CultureInfo.CurrentCulture);
            valATL.Text = ChartData.GetValue(ChartData.DataType.ATL, DateTime.Now.Date.ToLocalTime()).ToString("0", CultureInfo.CurrentCulture);
            valTSB.Text = ChartData.GetValue(ChartData.DataType.TSB, DateTime.Now.Date.ToLocalTime()).ToString("0", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Sets up the min/max grace for selected chart lines.  Arranges chart lines such as 
        /// CTL & TSB into upper & lower regions.
        /// </summary>
        internal void SetChartGrace()
        {
            if ((UserData.UserCharts.Contains("CTL") || UserData.UserCharts.Contains("ATL")) && (UserData.UserCharts.Contains("INF") || UserData.UserCharts.Contains("TSB")))
            {
                // Split chart into upper & lower regions
                GetY2Axis("CTL").Scale.MaxGrace = GlobalSettings.Instance.GraceCTL;
                GetY2Axis("INF").Scale.MinGrace = GlobalSettings.Instance.GraceINF;
                GetY2Axis("TSB").Scale.MinGrace = GlobalSettings.Instance.GraceTSB;
                GetY2Axis("FTP").Scale.MinGrace = GlobalSettings.Instance.GraceTSB;
            }
            else
            {
                // Center all charts into a single area
                double defaultMax = ZoneFiveSoftware.Common.Visuals.Chart.Scale.Default.MaxGrace;
                double defaultMin = ZoneFiveSoftware.Common.Visuals.Chart.Scale.Default.MinGrace;

                GetY2Axis("CTL").Scale.MaxGrace = defaultMax;
                GetY2Axis("INF").Scale.MinGrace = defaultMin;
                GetY2Axis("TSB").Scale.MinGrace = defaultMin;
                GetY2Axis("FTP").Scale.MinGrace = defaultMin;
            }
        }

        /// <summary>
        /// Update Training Load page to current visual theme
        /// </summary>
        /// <param name="visualTheme">Theme to apply</param>
        internal void ThemeChanged(ITheme visualTheme)
        {
            this.currentTheme = visualTheme;
            trainingViewActionBanner.ThemeChanged(visualTheme);
            MainChart.ThemeChanged(visualTheme);
            currentViewActionBanner.ThemeChanged(visualTheme);
            currentActivityActionBanner.ThemeChanged(visualTheme);
            controlPanel.ThemeChanged(visualTheme);
            treeActivity.ThemeChanged(visualTheme);
        }

        /// <summary>
        /// Adapt display to current culture
        /// </summary>
        /// <param name="culture">Current culture settings to apply</param>
        internal void UICultureChanged(CultureInfo culture)
        {
            string elevUnits = Length.LabelAbbr(PluginMain.GetApplication().SystemPreferences.ElevationUnits);
            string distUnits = Length.LabelAbbr(PluginMain.GetApplication().SystemPreferences.DistanceUnits);
            string paceUnits = Speed.Label(Speed.Units.Pace, new Length(1, PluginMain.GetApplication().SystemPreferences.DistanceUnits));
            string spdUnits = Speed.Label(Speed.Units.Speed, new Length(1, PluginMain.GetApplication().SystemPreferences.DistanceUnits));

            // Data contains localized text also, refresh.
            lblFitness.Text = Resources.Strings.Label_Fitness;
            lblFatigue.Text = Resources.Strings.Label_Fatigue;
            lblPerf.Text = Resources.Strings.Label_Perf;
            lblTaperDate.Text = Resources.Strings.Label_TaperDate;
            lblTargetDate.Text = Resources.Strings.Label_TargetDate;
            lblMaxEffect.Text = Resources.Strings.Label_MaxTrainingEffect;
            lblCTL.Text = Resources.Strings.Label_CTL;
            lblATL.Text = Resources.Strings.Label_ATL;
            lblTSB.Text = Resources.Strings.Label_TSB;
            currentViewActionBanner.Text = Resources.Strings.Label_Activities;
            currentActivityActionBanner.Text = Resources.Strings.Label_CurrentTrainingStatus;

            switch (ChartData.TLChartBasis)
            {
                case Common.ChartBasis.HR:
                    trainingViewActionBanner.Text = Resources.Strings.Label_TRIMP;
                    break;
                case Common.ChartBasis.Power:
                    trainingViewActionBanner.Text = Resources.Strings.Label_TrainingStressScore;
                    break;
                case Common.ChartBasis.Daniels:
                    trainingViewActionBanner.Text = Resources.Strings.Label_DanielsPoints;
                    break;
                default:
                    trainingViewActionBanner.Text = ChartData.TLChartBasis.ToString();
                    break;
            }

            foreach (TreeList.Column curCol in this.columns)
            {
                if (curCol.Parent != null)
                {
                    switch (curCol.Id)
                    {
                        case "StartTime":
                            curCol.Text = CommonResources.Text.LabelStartTime;
                            break;
                        case "TRIMP":
                            curCol.Text = Resources.Strings.Label_TRIMP;
                            break;
                        case "CTL":
                            curCol.Text = Resources.Strings.Label_CTL;
                            break;
                        case "ATL":
                            curCol.Text = Resources.Strings.Label_ATL;
                            break;
                        case "TSBPre":
                            curCol.Text = Resources.Strings.Label_TSB + " (" + Resources.Strings.Label_Before.ToLower(CultureInfo.CurrentCulture) + ")";
                            break;
                        case "TSBPost":
                            curCol.Text = Resources.Strings.Label_TSB + " (" + Resources.Strings.Label_After.ToLower(CultureInfo.CurrentCulture) + ")";
                            break;
                        case "TSS":
                            curCol.Text = Resources.Strings.Label_TSS;
                            break;
                        case "NormalizedPower":
                            curCol.Text = Resources.Strings.Label_NormPower + " (" + CommonResources.Text.LabelWatts + ")";
                            break;
                        case "AvgPower":
                            curCol.Text = CommonResources.Text.LabelAvgPower + " (" + CommonResources.Text.LabelWatts + ")";
                            break;
                        case "Location":
                            curCol.Text = CommonResources.Text.LabelLocation;
                            break;
                        case "Category":
                            curCol.Text = CommonResources.Text.LabelCategory;
                            break;
                        case "Distance":
                            curCol.Text = CommonResources.Text.LabelDistance + " (" + distUnits + ")";
                            break;
                        case "TotalTime":
                            curCol.Text = CommonResources.Text.LabelTime;
                            break;
                        case "TotalAscend":
                            curCol.Text = CommonResources.Text.LabelAscending + " (" + elevUnits + ")";
                            break;
                        case "TotalDescend":
                            curCol.Text = CommonResources.Text.LabelDescending + " (" + elevUnits + ")";
                            break;
                        case "AvgPace":
                            curCol.Text = CommonResources.Text.LabelAvgPace + " (" + paceUnits + ")";
                            break;
                        case "AvgSpeed":
                            curCol.Text = CommonResources.Text.LabelAvgSpeed + " (" + spdUnits + ")";
                            break;
                        case "AvgHR":
                            curCol.Text = CommonResources.Text.LabelAvgHR;
                            break;
                        case "Calories":
                            curCol.Text = CommonResources.Text.LabelCalories;
                            break;
                        case "AvgCadence":
                            curCol.Text = CommonResources.Text.LabelAvgCadence;
                            break;
                        case "AvgGrade":
                            curCol.Text = CommonResources.Text.LabelAvgGrade;
                            break;
                        case "Day":
                            curCol.Text = CommonResources.Text.LabelDayOfWeek;
                            break;
                        case "FastestPace":
                            curCol.Text = CommonResources.Text.LabelFastestPace + " (" + paceUnits + ")";
                            break;
                        case "FastestSpeed":
                            curCol.Text = CommonResources.Text.LabelFastestSpeed + " (" + spdUnits + ")";
                            break;
                        case "MaxHR":
                            curCol.Text = CommonResources.Text.LabelMaxHR;
                            break;
                        case "MaxPower":
                            curCol.Text = CommonResources.Text.LabelMaxPower + " (" + CommonResources.Text.LabelWatts + ")";
                            break;
                        case "Name":
                            curCol.Text = CommonResources.Text.LabelName;
                            break;
                        case "RollingSum1":
                            curCol.Text = Resources.Strings.Label_RollingSum + " " + UserData.MovingSumDays1;
                            break;
                        case "RollingSum2":
                            curCol.Text = Resources.Strings.Label_RollingSum + " " + UserData.MovingSumDays2;
                            break;
                        case "IntensityFactor":
                            curCol.Text = Resources.Strings.Label_IntensityFactor;
                            break;
                        case "Notes":
                            curCol.Text = CommonResources.Text.LabelNotes;
                            break;
                        default:
                            {
                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// Set the highlighted date on the chart
        /// </summary>
        /// <param name="date">Date to highlight</param>
        internal void SetHighlightDate(GradientFillObj grad, DateTime date)
        {
            grad.Location = new Location(XDate.DateTimeToXLDate(date) - 1, 0, 2, 1, CoordType.XScaleYChartFraction, AlignH.Center, AlignV.Top);
            grad.IsVisible = true;
            MainChart.Refresh();
        }

        private void AddY2Axis(GraphPane pane, string label, Color color, object tag)
        {
            int index = pane.Y2AxisList.Add(label);
            Y2Axis axis = pane.Y2AxisList[index];
            axis.Tag = tag;
            axis.Title.IsVisible = true;
            axis.MajorTic.Color = color;
            axis.Title.FontSpec.FontColor = color;
            axis.Scale.FontSpec.FontColor = color;
        }

        private Y2Axis GetY2Axis(string tag)
        {
            return Pane.Y2AxisList[Pane.Y2AxisList.IndexOfTag(tag)];
        }

        private void GlobalSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "EnableMultisport")
            {
                ChartData.IsCalculated = false;
                mnuItemStackCTL.Enabled = GlobalSettings.Instance.EnableMultisport;
                RefreshPage();
            }
            else if (e.PropertyName == "CTLstacked" || e.PropertyName == "ShowFitPlanWorkouts")
            {
                ChartData.IsCalculated = false;
                RefreshPage();
            }
        }

        private void UserData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PerfDate")
            {
                UpdateInfluenceStatus();
            }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Zooms the main TL chart to the default view as defined on the settings page.
        /// </summary>
        internal void ZoomDefault()
        {
            MainChart.ZoomAuto(Pane);

            foreach (CurveItem curve in MainChart.GraphPane.CurveList)
            {
                if (curve.IsRangeSelected)
                {
                    foreach (RangeSelection range in curve.RangeSelection)
                    {
                        if (range.IsSinglePoint)
                        {
                            // Single point zooms cause weird displays, clear them out.  Especially single bar selections (cause chart exceptions).
                            foreach (CurveItem curve2 in MainChart.GraphPane.CurveList)
                            {
                                curve2.ClearSelectedRange();
                            }

                            MainChart.ZoomAuto(Pane);
                            break;
                        }
                        else
                        {
                            // Accept auto zoom behavior
                            return;
                        }
                    }
                }
            }

            XDate chartFirstActivity = new XDate(DateTime.Today.AddDays(-GlobalSettings.Instance.PastDays));
            if (chartFirstActivity < ChartData.FirstActivity)
                chartFirstActivity = ChartData.FirstActivity;

            // Define default number of days to show in chart
            int defaultDays = GlobalSettings.Instance.FutureDays + GlobalSettings.Instance.PastDays;

            Pane.XAxis.Scale.Min = chartFirstActivity;
            Pane.XAxis.Scale.Max = chartFirstActivity + defaultDays;

            if (UserData.UserCharts.Contains("TSB") && UserData.UserCharts.Contains("INF"))
            {
                // Arrange TSB & INF zero line
                Y2Axis tsb = GetY2Axis("TSB");
                Y2Axis inf = GetY2Axis("INF");

                // SetupScaleData prepares the transforms below to use the new scale data (after auto-zoom above happens).
                //   Not calling this will make the transforms use the previous zoom state as the transform basis.
                inf.Scale.SetupScaleData(MainChart.GraphPane, inf);
                tsb.Scale.SetupScaleData(MainChart.GraphPane, tsb);

                double offset = inf.Scale.ReverseTransform(tsb.Scale.Transform(0));
                inf.Scale.Max -= offset;
                inf.Scale.Min -= offset;
            }

            Pane.AxisChange();
        }

        /// <summary>
        /// Returns the Y-axis name for a Rolling Sum axis
        /// </summary>
        /// <param name="days">Number of days in the rolling sum</param>
        /// <param name="catIndex">Type index (0=trimp, 1=Distance, 2=Time, etc.)</param>
        /// <returns>Y-axis label</returns>
        private static string GetRollingAxisName(int days, int catIndex)
        {
            string axisName = string.Empty;
            string cat = string.Empty;

            if (catIndex == 0)
            {
                cat = Resources.Strings.Label_TRIMP;
            }
            else if (catIndex == 1)
            {
                cat = CommonResources.Text.LabelDistance;
            }
            else if (catIndex == 2)
            {
                cat = CommonResources.Text.LabelTime;
            }
            else
            {
                cat = string.Empty;
            }

            axisName = Resources.Strings.Label_RollingSum + " (" + cat + " " + days + " " + Resources.Strings.Label_Days + ")";

            return axisName;
        }

        /// <summary>
        /// Populate/Refresh the Activity Treelist
        /// </summary>
        private void RefreshActivityTree()
        {
            // Display and organize saved columns
            string[] userColumns = UserData.UserColumns.Split(';');
            int userColumnIndex = 0;
            foreach (string column in userColumns)
            {
                // Rearrange Columns
                int currentIndex = this.columns.FindIndex(delegate(TreeList.Column c) { return c.Id == column; });
                if (currentIndex != -1)
                {
                    this.columns[currentIndex].CanSelect = true;
                }

                if (currentIndex != userColumnIndex && currentIndex != -1)
                {
                    this.columns.Insert(userColumnIndex, columns[currentIndex]);
                    this.columns.RemoveAt(currentIndex + 1);
                }

                userColumnIndex++;
            }

            // Add columns to Activity treeList
            treeActivity.Columns.Clear();
            for (int i = 0; i < userColumnIndex; i++)
            {
                this.columns[i].CanSelect = true;
                treeActivity.Columns.Add(columns[i]);
            }

            // Populate Table and Maintain sort order
            treeActivity.SelectedItemsChanged -= treeActivity_SelectedChanged;
            treeActivity.RowDataRenderer = new ActivityTreeRenderer(treeActivity);
            treeActivity.RowData = ChartData.TreeActivities;
            SortActivityTreeList(currentSortColumnId, false);

            // Select proper activity based on calendar selection
            if (GetCalendarSelection(ChartData.TrimpActivities).Count > 0)
            {
                treeActivity.Selected = GetCalendarSelection(ChartData.TrimpActivities);
            }

            // Re-add handler
            treeActivity.SelectedItemsChanged += treeActivity_SelectedChanged;
        }

        /// <summary>
        /// Update the Training Dates in the status pane from UserData settings.
        /// </summary>
        private void UpdateInfluenceStatus()
        {
            valTargetDate.Text = UserData.PerfDate.ToShortDateString();
            valMaxEffect.Text = UserData.PerfDate.AddDays(-1 * UserData.TCa * UserData.TCc / (UserData.TCc - UserData.TCa) * Math.Log(UserData.Ka / UserData.Kc * UserData.TCc / UserData.TCa, Math.E)).ToShortDateString();
            valTaperDate.Text = UserData.PerfDate.AddDays(-1 * UserData.TCa * UserData.TCc / (UserData.TCc - UserData.TCa) * Math.Log(UserData.Ka / UserData.Kc, Math.E)).ToShortDateString();
        }

        /// <summary>
        /// Gets a list of activities that matches the selected date.  
        /// Often this will only be 1 activity, unless there were multiple activities in 1 day.
        /// </summary>
        /// <param name="trimpActivities"></param>
        /// <returns></returns>
        private static List<TrimpActivity> GetCalendarSelection(IEnumerable trimpActivities)
        {
            // Select item(s) from calendar
            DateTime selectedDate = PluginMain.GetApplication().Calendar.Selected;
            List<TrimpActivity> selected = new List<TrimpActivity>();
            foreach (TrimpActivity item in trimpActivities)
            {
                if (item.StartTime.Date == selectedDate)
                {
                    selected.Add(item);
                }
            }

            return selected;
        }

        /// <summary>
        /// Update Status Pane.
        /// </summary>
        /// <param name="selectedName">Status pane to display</param>
        /// <param name="obj">Currently unused</param>
        private void StatusPane(object selectedName, TreeListPopup.ItemSelectedEventArgs obj)
        {
            string item = selectedName as string;
            if (item == CommonResources.Text.LabelStatus)
            {
                pageStatus = Status.Status;
                pnlHRchart.Hide();
                pnlStatus.Show();
                pnlSettings.Hide();
            }
            else if (item == CommonResources.Text.LabelHeartRate)
            {
                pageStatus = Status.HR;
                pnlHRchart.Show();
                pnlStatus.Hide();
                pnlSettings.Hide();
                RefreshStatusChart(pageStatus);
            }
            else if (item == CommonResources.Text.LabelPower)
            {
                pageStatus = Status.Power;
                pnlHRchart.Show();
                pnlStatus.Hide();
                pnlSettings.Hide();
                RefreshStatusChart(pageStatus);
            }
            else if (item == CommonResources.Text.LabelSpeed)
            {
                pageStatus = Status.Speed;
                pnlHRchart.Show();
                pnlStatus.Hide();
                pnlSettings.Hide();
                RefreshStatusChart(pageStatus);
            }
            else if (item == CommonResources.Text.LabelPace)
            {
                pageStatus = Status.Pace;
                pnlHRchart.Show();
                pnlStatus.Hide();
                pnlSettings.Hide();
                RefreshStatusChart(pageStatus);
            }
            else if (item == CommonResources.Text.LabelCadence)
            {
                pageStatus = Status.Cadence;
                pnlHRchart.Show();
                pnlStatus.Hide();
                pnlSettings.Hide();
                RefreshStatusChart(pageStatus);
            }
            else if (item == CommonResources.Text.LabelClimb)
            {
                pageStatus = Status.Climb;
                pnlHRchart.Show();
                pnlStatus.Hide();
                pnlSettings.Hide();
                RefreshStatusChart(pageStatus);
            }
            else if (item == Resources.Strings.Label_Settings)
            {
                pageStatus = Status.Settings;
                pnlHRchart.Hide();
                pnlStatus.Hide();
                pnlSettings.Show();
                txtTCa.Leave -= txtTCa_TextChanged;
                txtTCc.Leave -= txtTCc_TextChanged;
                txtTCa.Text = UserData.TCa.ToString(CultureInfo.CurrentCulture);
                txtTCc.Text = UserData.TCc.ToString(CultureInfo.CurrentCulture);
                txtTCa.Leave += txtTCa_TextChanged;
                txtTCc.Leave += txtTCc_TextChanged;
            }
        }

        /// <summary>
        /// Populate distribution chart in Status pane
        /// </summary>
        /// <param name="id">Selected data to display</param>
        private void RefreshStatusChart(Status id)
        {
            // Initialize the charts and dataseries
            IAxis axisLeft = chartStatus.YAxis;
            ChartDataSeries dsData = new ChartDataSeries(chartStatus, axisLeft);

            ArrayList axisLabels = new ArrayList();
            IZoneCategory zoneCat;

            if (id == Status.HR)
            {
                // Heart rate distribution chart
                if (UserData.SingleZoneCategory != null)
                {
                    zoneCat = UserData.SingleZoneCategory;
                }
                else
                {
                    return;
                }

                // Calculate time in each zone for selected activities
                for (int i = 0; i < zoneCat.Zones.Count; i++)
                {
                    PointF point = new PointF();
                    point.X = i;
                    point.Y = 0;

                    foreach (TrimpActivity selActivity in treeActivity.Selected)
                    {
                        // Create charted 'points'
                        ZoneInfo zoneInfo = selActivity.Info.HeartRateZoneInfo(zoneCat).Zones[i];
                        point.Y = point.Y + (float)zoneInfo.TotalTime.TotalSeconds;
                    }

                    dsData.Points.Add(point.X, point);
                    axisLabels.Add(zoneCat.Zones[i].Low + "-" + zoneCat.Zones[i].High);
                }

                dsData.ChartType = ChartDataSeries.Type.Bar;
                dsData.FillColor = Color.FromArgb(50, Color.Firebrick);
                dsData.LineColor = Color.Firebrick;
                dsData.ValueAxis = axisLeft; // chartStatus.YAxis;

                chartStatus.XAxis.Label = CommonResources.Text.LabelHeartRate;
            }
            else if (id == Status.Power)
            {
                // Power distribution chart
                zoneCat = PluginMain.GetLogbook().PowerZones[0];

                // Calculate time in each zone for selected activities
                for (int i = 0; i < zoneCat.Zones.Count; i++)
                {
                    PointF point = new PointF();
                    point.X = i;
                    point.Y = 0;

                    foreach (TrimpActivity selActivity in treeActivity.Selected)
                    {
                        // Create charted 'points'
                        ZoneInfo zoneInfo = selActivity.Info.PowerZoneInfo(zoneCat).Zones[i];
                        point.Y = point.Y + (float)zoneInfo.TotalTime.TotalSeconds;
                    }

                    dsData.Points.Add(point.X, point);
                    axisLabels.Add(zoneCat.Zones[i].Low + "-" + zoneCat.Zones[i].High);
                }

                dsData.ChartType = ChartDataSeries.Type.Bar;
                dsData.FillColor = Color.FromArgb(50, Color.Indigo);
                dsData.LineColor = Color.Indigo;
                dsData.ValueAxis = axisLeft; // chartStatus.YAxis;

                chartStatus.XAxis.Label = CommonResources.Text.LabelPower;
            }
            else if (id == Status.Cadence)
            {
                // Cadence distribution chart
                zoneCat = PluginMain.GetLogbook().CadenceZones[0];

                // Calculate time in each zone for selected activities
                for (int i = 0; i < zoneCat.Zones.Count; i++)
                {
                    PointF point = new PointF();
                    point.X = i;
                    point.Y = 0;

                    foreach (TrimpActivity selActivity in treeActivity.Selected)
                    {
                        // Create charted 'points'
                        ZoneInfo zoneInfo = selActivity.Info.CadenceZoneInfo(zoneCat).Zones[i];
                        point.Y = point.Y + (float)zoneInfo.TotalTime.TotalSeconds;
                    }

                    dsData.Points.Add(point.X, point);
                    axisLabels.Add(zoneCat.Zones[i].Low + "-" + zoneCat.Zones[i].High);
                }

                dsData.ChartType = ChartDataSeries.Type.Bar;
                dsData.FillColor = Color.FromArgb(50, Color.ForestGreen);
                dsData.LineColor = Color.ForestGreen;
                dsData.ValueAxis = axisLeft; // chartStatus.YAxis;

                chartStatus.XAxis.Label = CommonResources.Text.LabelCadence;
            }
            else if (id == Status.Speed || id == Status.Pace)
            {
                // Speed distribution chart
                if (treeActivity.Selected.Count != 0)
                {
                    zoneCat = ((TrimpActivity)treeActivity.Selected[0]).Category.SpeedZone;
                }
                else
                {
                    zoneCat = PluginMain.GetLogbook().SpeedZones[0];
                }

                // Calculate time in each zone for selected activities
                for (int i = 0; i < zoneCat.Zones.Count; i++)
                {
                    PointF point = new PointF();
                    point.X = i;
                    point.Y = 0;

                    foreach (TrimpActivity selActivity in treeActivity.Selected)
                    {
                        // Create charted 'points'
                        ZoneInfo zoneInfo = selActivity.Info.SpeedZoneInfo(zoneCat).Zones[i];
                        point.Y = point.Y + (float)zoneInfo.TotalTime.TotalSeconds;
                    }

                    dsData.Points.Add(point.X, point);
                    if (id == Status.Speed)
                    {
                        double low = Length.Convert(zoneCat.Zones[i].Low, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits) * 60 * 60; // (ST Units)/sec
                        double high = Length.Convert(zoneCat.Zones[i].High, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits) * 60 * 60; // (ST Units)/sec
                        axisLabels.Add(string.Format("{0:#.#}", low) + "+");

                        chartStatus.XAxis.Label = CommonResources.Text.LabelSpeed;
                    }
                    else if (id == Status.Pace)
                    {
                        double low = 1 / Length.Convert(zoneCat.Zones[i].Low, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits); // (ST Units)/sec
                        double high = 1 / Length.Convert(zoneCat.Zones[i].High, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits); // (ST Units)/sec
                        TimeSpan spanLow = new TimeSpan(0, 0, (int)low);
                        TimeSpan spanHigh = new TimeSpan(0, 0, (int)high);

                        axisLabels.Add(spanLow.Minutes.ToString("#0") + ":" + spanLow.Seconds.ToString("00") + "-" + spanHigh.Minutes.ToString("#0") + ":" + spanHigh.Seconds.ToString("00"));

                        chartStatus.XAxis.Label = CommonResources.Text.LabelPace;
                    }
                }

                dsData.ChartType = ChartDataSeries.Type.Bar;
                dsData.FillColor = Color.FromArgb(50, Color.DarkBlue);
                dsData.LineColor = Color.DarkBlue;
                dsData.ValueAxis = axisLeft; // chartStatus.YAxis;
            }
            else if (id == Status.Climb)
            {
                // Climb zone distribution chart
                zoneCat = PluginMain.GetLogbook().ClimbZones[0];

                // Calculate time in each zone for selected activities
                for (int i = 0; i < zoneCat.Zones.Count; i++)
                {
                    PointF point = new PointF();
                    point.X = i;
                    point.Y = 0;

                    foreach (TrimpActivity selActivity in treeActivity.Selected)
                    {
                        // Create charted 'points'
                        ZoneInfo zoneInfo = selActivity.Info.ClimbZoneInfo(zoneCat).Zones[i];
                        point.Y = point.Y + (float)zoneInfo.TotalTime.TotalSeconds;
                    }

                    dsData.Points.Add(point.X, point);
                    axisLabels.Add(zoneCat.Zones[i].Low + "-" + zoneCat.Zones[i].High);
                }

                dsData.ChartType = ChartDataSeries.Type.Bar;
                dsData.FillColor = Color.FromArgb(50, Color.DarkGoldenrod);
                dsData.LineColor = Color.DarkGoldenrod;
                dsData.ValueAxis = axisLeft; // chartStatus.YAxis;

                chartStatus.XAxis.Label = CommonResources.Text.LabelClimb;
            }
            else
            {
                // Do nothing if none of the above are selected
                return;
            }

            // Define the Heart rate Distribution data series (contains all definitions pertaining to the lines)
            axisLeft.Label = CommonResources.Text.LabelTime; // "Heart rate"
            axisLeft.LabelColor = dsData.LineColor;
            axisLeft.SmartZoom = true;

            Formatter.Category xAxisFormatter = new Formatter.Category(axisLabels);
            chartStatus.XAxis.Formatter = xAxisFormatter;
            Formatter.SecondsToTime yAxisFormatter = new Formatter.SecondsToTime();
            chartStatus.YAxis.Formatter = yAxisFormatter;

            chartStatus.XAxis.ChartLines = false;

            chartStatus.DataSeries.Clear();
            chartStatus.DataSeries.Add(dsData);
            chartStatus.Refresh();
            chartStatus.AutozoomToData(true);
        }

        /// <summary>
        /// Ensures that only digits are allowed to be entered.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e"></param>
        private void digitValidator(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true; // input is not passed on to the control(TextBox)`
            }
        }

        #endregion

        #region Form controls

        private void zoomInButton_Click(object sender, EventArgs e)
        {
            MainChart.ZoomPane(Pane, .9, MainChart.GraphPane.Rect.Location, false);
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            MainChart.ZoomPane(Pane, 1.1, MainChart.GraphPane.Rect.Location, false);
        }

        private void zoomFitButton_Click(object sender, EventArgs e)
        {
            ZoomDefault();
        }

        private void stripesButton_Click(object sender, EventArgs e)
        {
            foreach (GraphObj item in Pane.GraphObjList)
            {
                string tag = item.Tag as string;
                if (tag != null && tag.StartsWith("Zone"))
                    item.IsVisible = !item.IsVisible;
            }

            MainChart.Invalidate();
        }

        /// <summary>
        /// Handles selecting extra chart lines via toolbar button
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void selectFieldsButton_Click(object sender, EventArgs e)
        {
            ListSettingsDialog dlg = new ListSettingsDialog();
            dlg.Text = CommonResources.Text.LabelCharts;

            //dlg.SelectedItemListLabel = Resources.Strings.Label_SelectedCharts;
            //dlg.AddButtonLabel = CommonResources.Text.ActionAdd;
            dlg.AllowFixedColumnSelect = false;
            dlg.AllowZeroSelected = false;
            dlg.Icon = Utilities.GetIcon(Images.Charts);

            //dlg.ThemeChanged(PluginMain.GetApplication().VisualTheme);

            // Set available and selected columns
            dlg.AvailableColumns = ChartColumns.AllCharts;
            IList<string> selected = new List<string>();
            foreach (string line in UserData.UserCharts.Split(';'))
            {
                selected.Add(line);
            }

            dlg.SelectedColumns = selected;

            // Dispaly selection box
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string selectedString = string.Empty;

            foreach (string column in dlg.SelectedColumns)
                selectedString += column + ";";

            // Store new column settings & Refresh
            selectedString = selectedString.TrimEnd(';');
            UserData.UserCharts = selectedString;

            SetChartGrace();

            RefreshPage();
        }

        private void treeActivity_ColumnClicked(object sender, TreeList.ColumnEventArgs e)
        {
            SortActivityTreeList(e.Column.Id, true);
        }

        /// <summary>
        /// Sort activities in TreeActivity table.
        /// </summary>
        /// <param name="columnId">Column to sort by.</param>
        /// <param name="reSort">Re-sort/invert sort of table.  For example, TRUE if column clicked, FALSE if maintain existing sort order.</param>
        private void SortActivityTreeList(string columnId, bool reSort)
        {
            // Exit if chart is empty
            if (treeActivity.RowData != null)
            {
                // Sort only the data currently in the treeList
                List<TrimpActivity> data = (List<TrimpActivity>)treeActivity.RowData;

                // Create a comparer instance
                TrimpActivity.TrimpActivityComparer comparer = new TrimpActivity.TrimpActivityComparer();

                // Set comparison method
                try
                {
                    comparer.ComparisonMethod = (TrimpActivity.TrimpActivityComparer.ComparisonType)Enum.Parse(typeof(TrimpActivity.TrimpActivityComparer.ComparisonType), columnId);
                }
                catch
                {
                    // Default to sort by start time if there's an error for some reason
                    comparer.ComparisonMethod = TrimpActivity.TrimpActivityComparer.ComparisonType.StartTime;
                }

                // Set sort direction
                if ((reSort && !currentSortDirection && columnId == currentSortColumnId) || (!reSort && currentSortDirection))
                {
                    // Sort ascending - same column clicked, invert sort order OR don't resort and already Ascending
                    currentSortColumnId = columnId;
                    currentSortDirection = true; // Ascending = True, Descending = False
                    comparer.SortOrder = TrimpActivity.TrimpActivityComparer.Order.Ascending;
                    treeActivity.SetSortIndicator(columnId, true);
                }
                else
                {
                    // Sort descending
                    currentSortColumnId = columnId;
                    currentSortDirection = false; // Ascending = True, Descending = False
                    comparer.SortOrder = TrimpActivity.TrimpActivityComparer.Order.Descending;
                    treeActivity.SetSortIndicator(columnId, false);
                }

                // Sort and display activities
                data.Sort(comparer);
                treeActivity.RowData = data;
            }
        }

        private void treeActivity_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            TreeList tree = (TreeList)sender;

            if (mouse.Button == MouseButtons.Right && (mouse.Y < tree.HeaderRowHeight || Cursor.Current == Cursors.Hand))
            {
                // Right Click on header: Context menu
                ITheme visualTheme = PluginMain.GetApplication().VisualTheme;
                ContextMenuStrip menuStrip = new ContextMenuStrip();
                menuStrip.Items.Add(Resources.Strings.Label_ListSettings + "...", CommonResources.Images.Table16);
                menuStrip.Items.Add(CommonResources.Text.ActionCopy, CommonResources.Images.DocumentCopy16);
                menuStrip.ItemClicked += new ToolStripItemClickedEventHandler(menuStrip_ItemClicked);

                // Display the Context Menu
                Point point = mouse.Location;
                point = tree.PointToScreen(mouse.Location);
                menuStrip.Show(point);
            }
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip strip = sender as ContextMenuStrip;
            if (e.ClickedItem.Text == Resources.Strings.Label_ListSettings + "...")
            {
                ListSettings();
            }
            else if (e.ClickedItem.Text == CommonResources.Text.ActionCopy)
            {
                CopyActivities(treeActivity);
            }

            strip.Dispose();
        }

        private static EventHandler CopyActivities(TreeList treeList)
        {
            treeList.CopyTextToClipboard(true, ",");
            return null;
        }

        /// <summary>
        /// Handles context menu to add more tree columns.
        /// </summary>
        /// <returns></returns>
        private EventHandler ListSettings()
        {
            List<string> selected = new List<string>();
            List<string> unselected = new List<string>();

            // Collect selected and unselected columns
            foreach (TreeList.Column column in this.columns)
            {
                if (column.CanSelect == true && !selected.Contains(column.Text))
                {
                    selected.Add(column.Text);
                }
                else if (!unselected.Contains(column.Text))
                {
                    unselected.Add(column.Text);
                }
            }

            // Sort unselected items to find items easily
            unselected.Sort();

            // Occurs when an item in the popup is selected
            //// ***************************************
            // Show Data Select Form
            DataSelectForm data = new DataSelectForm(this.currentTheme, selected, unselected);
            data.SetLabelSelected(Resources.Strings.Label_SelectedColumns + ":");
            data.Text = Resources.Strings.Label_ListSettings;
            data.SetIcon(CommonResources.Images.Table16);
            data.RefreshLabels();
            //// ***************************************

            if (data.ShowDialog() == DialogResult.OK)
            {
                foreach (TreeList.Column column in this.columns)
                {
                    // Show/Hide columns
                    if (data.Items.Contains(column.Text))
                    {
                        // Show column
                        column.CanSelect = true;
                    }
                    else
                    {
                        // Hide column
                        column.CanSelect = false;
                    }
                }

                string userColumns = string.Empty;

                // NOTE: This is the way to arrange items in a list.  Try to do this for the charts.
                foreach (string item in data.Items)
                {
                    // Rearrange Columns
                    int currentIndex = this.columns.FindIndex(delegate(TreeList.Column c) { return c.Text == item; });
                    if (currentIndex != data.Items.IndexOf(item))
                    {
                        this.columns.Insert(data.Items.IndexOf(item), columns[currentIndex]);
                        this.columns.RemoveAt(currentIndex + 1);
                        currentIndex = this.columns.FindIndex(delegate(TreeList.Column c) { return c.Text == item; });
                    }

                    userColumns += ";" + this.columns[currentIndex].Id;
                }

                UserData.UserColumns = userColumns;
                RefreshActivityTree();
            }

            data.Dispose();

            return null;
        }

        private void treeActivity_SelectedChanged(object sender, EventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (tree != null)
            {
                if (tree.Selected.Count > 0)
                {
                    if (tree.Selected[0] is TrimpActivity)
                    {
                        DateTime activityDate = ((TrimpActivity)(tree.Selected[0])).StartTime.Date;
                        PluginMain.GetApplication().Calendar.Selected = activityDate;
                        SetHighlightDate(ChartData.HighlightFocus, activityDate);
                    }

                    RefreshStatusChart(pageStatus);
                }
                else
                {
                    ChartData.HighlightFocus.IsVisible = false;
                }
            }
        }

        internal void Calendar_SelectedChanged(object sender, EventArgs e)
        {
            // Don't process handler when activity tree selection changes
            treeActivity.SelectedItemsChanged -= treeActivity_SelectedChanged;
            if (GetCalendarSelection(ChartData.TrimpActivities).Count > 0)
            {
                treeActivity.Selected = GetCalendarSelection(ChartData.TrimpActivities);
            }

            treeActivity.SelectedItemsChanged += treeActivity_SelectedChanged;
        }

        /// <summary>
        /// Handles selecting regions within the chart data.  This filters the activity list
        /// based the region (date range) selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">The parameter is not used.</param>
        private void chartBase_Click(object sender, EventArgs e)
        {
            Type t = sender.GetType();
            System.Reflection.FieldInfo o = t.GetField("selectRangeData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetField);
            ChartDataSeries rangeSeries = (ChartDataSeries)o.GetValue(sender);

            // Clear treelist
            if (rangeSeries == null)
            {
                List<TrimpActivity> currentData = treeActivity.RowData as List<TrimpActivity>;
                if (currentData != null)
                {
                    if (currentData.Count != ChartData.TreeActivities.Count)
                    {
                        // Select All activities
                        treeActivity.RowData = ChartData.TreeActivities;

                        // Maintain sort order
                        SortActivityTreeList(currentSortColumnId, false);

                    }
                }
            }
        }

        /// <summary>
        /// Used to 'link' the data series together
        /// </summary>
        /// <param name="sender">Chart (required)</param>
        /// <param name="e">Arguments containing the dataseries (required)</param>
        private void MainChart_SelectingData(object sender, ZedGraphControl.SelectDataEventArgs e)
        {
            ZedGraphControl chart = sender as ZedGraphControl;
            if (e.Curve != null && e.Curve.RangeSelection != null)
            {
                foreach (CurveItem curve in chart.GraphPane.CurveList)
                {
                    if (curve != e.Curve && curve.IsVisible && 2 < curve.NPts)
                    {
                        curve.ClearSelectedRange();
                        foreach (RangeSelection range in e.Curve.RangeSelection)
                        {
                            float min = range.Min.X;
                            float max = range.Max.X;
                            curve.AddSelectedRange(min, max, chart.GraphPane);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Used to filter treelist data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainChart_SelectData(object sender, ZedGraphControl.SelectDataEventArgs e)
        {
            ZedGraphControl chart = sender as ZedGraphControl;

            if (e.Curve != null && 0 < e.Curve.RangeSelection.Count)
            {
                foreach (RangeSelection range in e.Curve.RangeSelection)
                {
                    if (range.IsSinglePoint && e.Curve.IsLine)
                    {
                        LineItem line = e.Curve as LineItem;
                        line.Line.Width = 3;
                    }

                    List<TrimpActivity> rangeActivities = new List<TrimpActivity>();

                    // Filter Activities
                    foreach (TrimpActivity act in ChartData.TreeActivities)
                    {
                        if (range.Min.X <= XDate.DateTimeToXLDate(act.StartTime.Date) && XDate.DateTimeToXLDate(act.StartTime.Date) <= range.Max.X)
                            rangeActivities.Add(act);
                    }

                    if (rangeActivities.Count > 0)
                        // Add data to activity tree
                        treeActivity.RowData = rangeActivities;

                    // Maintain sort order
                    SortActivityTreeList(currentSortColumnId, false);
                }
            }
            else
            {
                // Reset activity list to 'everything'
                List<TrimpActivity> currentData = treeActivity.RowData as List<TrimpActivity>;
                if (currentData != null)
                {
                    if (currentData.Count != ChartData.TreeActivities.Count)
                    {
                        // Select All activities
                        treeActivity.RowData = ChartData.TreeActivities;

                        // Maintain sort order
                        SortActivityTreeList(currentSortColumnId, false);

                    }
                }

                foreach (CurveItem curve in chart.GraphPane.CurveList)
                    if (curve.IsLine)
                    {
                        LineItem line = curve as LineItem;
                        line.Line.Width = 1;
                    }
            }
        }

        /// <summary>
        /// Open save image dialog box and process save data.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e"></param>
        private void savePicButton_Click(object sender, EventArgs e)
        {
            ITheme theme = PluginMain.GetApplication().VisualTheme;

            SaveImageDialog save = new SaveImageDialog();
            save.ThemeChanged(theme);

            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filename = Resources.Strings.Label_TrainingLoad + " " + DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
            save.FileName = Path.Combine(folder, filename);

            if (save.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.File.Exists(save.FileName))
                {
                    string ff = CommonResources.Text.ErrorText_FileExists(save.FileName);
                    if (MessageDialog.Show("File exists.  Overwrite?", "File Save", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                MainChart.SaveImage(save.ImageSizes[save.ImageSize], save.FileName, save.ImageFormat);
            }

            save.Dispose();
        }

        /// <summary>
        /// Menu clicked on Status Pane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">The parameter is not used.</param>
        private void ActivityActionBanner_MenuClicked(object sender, EventArgs e)
        {
            List<string> names = new List<string>();
            names.Add(CommonResources.Text.LabelStatus);
            names.Add(CommonResources.Text.LabelHeartRate);
            names.Add(CommonResources.Text.LabelPower);
            names.Add(CommonResources.Text.LabelSpeed);
            names.Add(CommonResources.Text.LabelPace);
            names.Add(CommonResources.Text.LabelClimb);
            names.Add(CommonResources.Text.LabelCadence);
            names.Add(Resources.Strings.Label_Settings);

            string selected = string.Empty;

            switch (pageStatus)
            {
                case Status.Status:
                    selected = names[0];
                    break;
                case Status.HR:
                    selected = names[1];
                    break;
                case Status.Power:
                    selected = names[2];
                    break;
                case Status.Speed:
                    selected = names[3];
                    break;
                case Status.Pace:
                    selected = names[4];
                    break;
                case Status.Climb:
                    selected = names[5];
                    break;
                case Status.Cadence:
                    selected = names[6];
                    break;
                case Status.Settings:
                    selected = names[7];
                    break;
            }

            Control control = sender as ActionBanner;

            Utilities.OpenListPopup(PluginMain.GetApplication().VisualTheme, names, control, selected, StatusPane);
        }

        private void txtTCa_TextChanged(object sender, EventArgs e)
        {
            int atl_timeC;

            // digitValidator ensures only numbers have been entered
            if (int.TryParse(txtTCa.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out atl_timeC))
            {
                if (UserData.TCa != atl_timeC)
                {
                    UserData.TCa = atl_timeC;
                    RefreshPage();
                }
            }
            else
            {
                txtTCa.Text = UserData.TCa.ToString(CultureInfo.CurrentCulture);
            }
        }

        private void txtTCc_TextChanged(object sender, EventArgs e)
        {
            int timeC;

            // digitValidator ensures only numbers have been entered
            if (int.TryParse(txtTCc.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out timeC))
            {
                if (UserData.TCc != timeC)
                {
                    UserData.TCc = timeC;
                    RefreshPage();
                }
            }
            else
            {
                txtTCc.Text = UserData.TCc.ToString(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Show the menu when the title banner menu drop-down is clicked
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void trainingViewActionBanner_MenuClicked(object sender, EventArgs e)
        {
            mnuMain.Show(trainingViewActionBanner, new Point(trainingViewActionBanner.Right - 2, trainingViewActionBanner.Bottom), ToolStripDropDownDirection.BelowLeft);
        }

        /// <summary>
        /// Selects the Training Load Pwr/TSS based chart
        /// </summary>
        /// <param name="sender">The paratmeter is not used.</param>
        /// <param name="e">The paratmeter is not used.</param>
        private void menuChartItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in mnuMain.Items)
            {
                // Only clear out the 
                if (item is ToolStripSeparator)
                    break;

                ((ToolStripMenuItem)item).Checked = false;
            }

            ToolStripMenuItem clicked = sender as ToolStripMenuItem;
            if (clicked == null) return;

            Common.ChartBasis basis;
            string title;

            switch (clicked.Tag as string)
            {
                case "TSS":
                    basis = Common.ChartBasis.Power;
                    mnuItemTSS.Checked = true;
                    title = Resources.Strings.Label_TrainingStressScore;
                    break;
                case "DanielsPoints":
                    basis = Common.ChartBasis.Daniels;
                    mnuItemDaniels.Checked = true;
                    title = Resources.Strings.Label_DanielsPoints;
                    break;
                default:
                case "Trimp":
                    basis = Common.ChartBasis.HR;
                    mnuItemTrimp.Checked = true;
                    title = Resources.Strings.Label_TRIMP;
                    break;
            }

#if (!Debug)
            if (ChartData.TLChartBasis != basis)
#endif
            {
                ChartData.TLChartBasis = basis;
                trainingViewActionBanner.Text = title;
                autozoom = true;
                RefreshPage();
            }
        }

        private void menuMultiSportItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clicked = sender as ToolStripMenuItem;
            if (clicked == null) return;

            clicked.Checked = !clicked.Checked;

            switch (clicked.Tag as string)
            {
                case "EnableMultiSport":
                    GlobalSettings.Instance.EnableMultisport = clicked.Checked;
                    break;

                case "StackCTL":
                    GlobalSettings.Instance.CTLstacked = clicked.Checked;
                    break;
            }
        }

        private void treeActivity_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            TreeList tree = (TreeList)sender;

            if (mouse.Button == MouseButtons.Left && (mouse.Y > tree.HeaderRowHeight))
            {
                // Exit if nothing selected
                if (tree.Selected.Count > 0)
                {
                    // Open the selected activity in the detail pane view
                    TrimpActivity activity = treeActivity.Selected[0] as TrimpActivity;
                    if (activity != null)
                    {
                        string bookmark = "id=" + activity.ReferenceId;
                        PluginMain.GetApplication().ShowView(GUIDs.DailyActivities, bookmark);
                    }
                }
            }
        }

        private void mnuItemShowFitPlan_Click(object sender, EventArgs e)
        {
            mnuItemShowFitPlan.Checked = !mnuItemShowFitPlan.Checked;
            GlobalSettings.Instance.ShowFitPlanWorkouts = mnuItemShowFitPlan.Checked;
        }

        private string MainChart_PointValueEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            if (MousePosition != tooltipMousePt)
            {
                if (ChartData.CTLstack.Contains(curve))
                {
                    // Stacked CTL Tooltip
                    chartToolTip.Active = true;
                    double pct = curve[iPt].Z / ChartData.CTL[iPt].Y * 100.0;
                    string[] parameters = { curve.Label.Text, 
                                              XDate.ToString(curve[iPt].X,"d"),
                                              TrainingLoad.Resources.Strings.Label_CTL,
                                              curve[iPt].Z.ToString("0.#", CultureInfo.CurrentCulture), 
                                              pct.ToString("0.0", CultureInfo.CurrentCulture) 
                                          };
                    string label = string.Format("{0}:\r\n{1}\r\n{2}: {3}\r\n{4}%", parameters);

                    chartToolTip.SetToolTip(sender, label);
                    tooltipMousePt = MousePosition;
                }
                else if (curve.IsBar)
                {
                    // TRIMP Bar Tooltip
                    chartToolTip.Active = true;
                    string label = string.Format("{0}\r\n{1}\r\n{2}", XDate.ToString(curve[iPt].X, "d"), curve.Tag, curve[iPt].Y.ToString("0.#", CultureInfo.CurrentCulture));

                    chartToolTip.SetToolTip(sender, label);
                    tooltipMousePt = MousePosition;
                }
                else
                {
                    // Default Tooltip (TSB, ATL, FTP, Rolling Sums, etc.)
                    chartToolTip.Active = true;
                    string label = string.Format("{0}\r\n{1}\r\n{2}", XDate.ToString(curve[iPt].X, "d"), curve.Label.Text, curve[iPt].Y.ToString("0.#", CultureInfo.CurrentCulture));

                    chartToolTip.SetToolTip(sender, label);
                    tooltipMousePt = MousePosition;
                }
            }
            else
            {
                //chartToolTip.Active = false;
            }
            return default(string);
        }

        private bool MainChart_DoubleClickEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            if (sender.GraphPane.Chart.Rect.Contains(e.Location))
            {
                ZoomDefault();
                return true;
            }

            return false;
        }

        #endregion

        ///// <summary>
        ///// Gets a brush that will draw a hard break between 2 colors.
        ///// </summary>
        ///// <param name="pct">Percent.  Integer value between 0 and 100 indicating where the break between color1 and color2 is.</param>
        ///// <param name="color1">color1 will be on the left (from 0 to pct)</param>
        ///// <param name="color2">color2 will be on the right (from pct to 100)</param>
        ///// <returns></returns>
        //private TextureBrush GetTextureBrush(int pct, Color color1, Color color2)
        //{
        //    Bitmap bmp = new Bitmap(10000, 1);

        //    using (Graphics g = Graphics.FromImage(bmp))
        //    {
        //        g.FillRectangle(new SolidBrush(color1), new Rectangle(0, 0, bmp.Width * pct / 100, 1));
        //        g.FillRectangle(new SolidBrush(color2), new Rectangle(bmp.Width * pct / 100, 0, 25, 20));
        //    }

        //    TextureBrush brush = new TextureBrush(bmp);
        //    return brush;
        //}
    }
}
