// <copyright file="ChartData.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2009-08-27</date>
namespace TrainingLoad.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using TrainingLoad.Settings;
    using Mechgt.Licensing;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals.Chart;
    using ZoneFiveSoftware.Common.Data.Fitness.CustomData;
    using System.Timers;
    using TrainingLoad.Data;
    using TrainingLoad.UI;
    using TrainingLoad.UI.View;
    using System.Drawing.Drawing2D;

    static class ChartData
    {
        #region Enumerations

        internal enum DataType
        {
            SCORE,
            ATL,
            CTL,
            TSB,
            FTPcycle,
            FTPrun,
            MOV1,
            MOV2,
            INF,
            Weight,
            BF,
            BMI
        }

        #endregion

        #region Constructor

        static ChartData()
        {
            UserData.PropertyChanged += new PropertyChangedEventHandler(UserData_PropertyChanged);
        }

        #endregion

        #region Fields

        private static Timer athleteChange;

        private static BarItem dsScore;
        private static LineItem dsCTL;
        private static LineItem dsATL;
        private static LineItem dsTSB;
        private static LineItem dsINF;
        private static LineItem dsMOV1;
        private static LineItem dsMOV2;
        private static LineItem dsBodyFat;
        private static LineItem dsBmi;
        private static LineItem dsFtpCycle;
        private static LineItem dsFtpRun;
        private static LineItem dsWeight;
        private static LineItem dsHRrest;
        private static LineItem dsHRmax;
        private static CurveList ctlstack;
        private static GradientFillObj highlightFocus;
        private static LineObj highlightToday;
        private static IList<DateTime> entryDates;

        /// <summary>
        /// Type of chart to be displayed.  Setting comes from menu selection.
        /// </summary>
        private static Common.ChartBasis tlChartBasis = Common.ChartBasis.HR;

        /// <summary>
        /// First activity displayed on the chart.  This is the first activity with a score, not necessarily 
        /// the first activity in the filtered list.
        /// </summary>
        private static DateTime firstActivity;

        /// <summary>
        /// List of activities to be shown in the treelist
        /// </summary>
        private static ActivityCollection trimpActivities;

        /// <summary>
        /// List of activities to process
        /// </summary>
        private static IEnumerable<IActivity> activities;

        /// <summary>
        /// Value indicating whether or not all calculations have been done.
        /// </summary>
        private static bool dataCalculated;

        /// <summary>
        /// A list of all of the activities awaiting data refresh.  As activities are modified in other 
        /// views, the reference id's are collected here to be processed later.
        /// </summary>
        private static List<string> queuedActivities = new List<string>();

        /// <summary>
        /// Identifiers for logbook to discern if a new logbook has been loaded or not.
        /// </summary>
        private static string logbookId;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the TRIMP or TSS data to be displayed on the chart.
        /// </summary>
        internal static BarItem Score
        {
            get
            {
                if (dsScore == null)
                {
                    dsScore = new BarItem("Score");
                    dsScore.Color = Color.Goldenrod;
                    dsScore.Tag = "SCORE";
                    dsScore.Bar.Border.Color = Color.DarkGoldenrod;
                    dsScore.Bar.Fill.SecondaryValueGradientColor = Color.DarkGoldenrod;
                    dsScore.Bar.SelectedFill = new Fill(Color.SaddleBrown);

                    if (FitPlanPlugin.IsInstalled)
                    {
                        // 'GradientFill' fills the line in variuos colors
                        Color past = Color.Green;
                        //Color future = Color.FromArgb(252, 105, 0);
                        Color future = Color.Fuchsia;
                        dsScore.Bar.Fill = new Fill(past, future); // past = low numbers, future = high numbers
                        dsScore.Bar.Fill.SecondaryValueGradientColor = Color.DarkGoldenrod;
                        dsScore.Bar.Fill.Type = FillType.GradientByX;
                        dsScore.Bar.Fill.RangeMin = new XDate(DateTime.Today);
                        dsScore.Bar.Fill.RangeMax = dsScore.Bar.Fill.RangeMin;
                    }
                }

                return dsScore;
            }
        }

        /// <summary>
        /// Gets the CTL data to be displayed on the chart.
        /// </summary>
        internal static LineItem CTL
        {
            get
            {
                if (dsCTL == null)
                    dsCTL = InitializeLine("CTL", Color.Blue, Color.FromArgb(80, Color.Blue), true);

                return dsCTL;
            }
        }

        /// <summary>
        /// Multisport CTL curve list.  Each line represents a parent category (Running, Cycling, Swimming, etc.).
        /// </summary>
        internal static CurveList CTLstack
        {
            get
            {
                if (ctlstack == null)
                    CreateCTLStack();

                return ctlstack;
            }
        }

        /// <summary>
        /// Gets the ATL data to be displayed on the chart.
        /// </summary>
        internal static LineItem ATL
        {
            get
            {
                if (dsATL == null)
                {
                    // Initialize with CTL to associate with proper axis
                    dsATL = InitializeLine("CTL", Color.Red, Color.Empty, true);
                    dsATL.Label.Text = "ATL";
                    dsATL.Tag = "ATL";
                }

                return dsATL;
            }
        }

        /// <summary>
        /// Gets the TSB data to be displayed on the chart.
        /// </summary>
        internal static LineItem TSB
        {
            get
            {
                if (dsTSB == null)
                    dsTSB = InitializeLine("TSB", Color.DarkOliveGreen, Color.FromArgb(80, Color.DarkOliveGreen), true);

                return dsTSB;
            }
        }

        /// <summary>
        /// Gets the Influence data to be displayed on the chart.
        /// </summary>
        internal static LineItem Influence
        {
            get
            {
                if (dsINF == null)
                {
                    dsINF = InitializeLine("INF", Color.OrangeRed, Color.FromArgb(65, Color.Orange), false);
                    SetInfluenceDate(UserData.PerfDate);
                }

                return dsINF;
            }
        }

        /// <summary>
        /// Gets the #1 Moving Value data to be displayed on the chart.
        /// </summary>
        internal static LineItem Moving1
        {
            get
            {
                if (dsMOV1 == null)
                    dsMOV1 = InitializeLine("MOV1", Color.Sienna, Color.Empty, true);

                return dsMOV1;
            }
        }

        /// <summary>
        /// Gets the #2 Moving Value data to be displayed on the chart.
        /// </summary>
        internal static LineItem Moving2
        {
            get
            {
                if (dsMOV2 == null)
                    dsMOV2 = InitializeLine("MOV2", Color.Peru, Color.Empty, false);

                return dsMOV2;
            }
        }

        /// <summary>
        /// Gets the Body Fat data to be displayed on the chart.
        /// </summary>
        internal static LineItem BodyFat
        {
            get
            {
                if (dsBodyFat == null)
                    dsBodyFat = InitializeLine("BF", Color.Green, Color.Empty, false);

                return dsBodyFat;
            }
        }

        /// <summary>
        /// Gets the BMI data to be displayed on the chart.
        /// </summary>
        internal static LineItem BMI
        {
            get
            {
                if (dsBmi == null)
                    dsBmi = InitializeLine("BMI", Color.Maroon, Color.Empty, false);

                return dsBmi;
            }
        }

        /// <summary>
        /// Gets the Body Weight data to be displayed on the chart.
        /// </summary>
        internal static LineItem BodyWeight
        {
            get
            {
                if (dsWeight == null)
                    dsWeight = InitializeLine("WEIGHT", Color.HotPink, Color.Empty, false);

                return dsWeight;
            }
        }

        /// <summary>
        /// Gets the HR rest data to be displayed on the chart.
        /// </summary>
        internal static LineItem HRrest
        {
            get
            {
                if (dsHRrest == null)
                    dsHRrest = InitializeLine("HRR", Color.DarkRed, Color.Empty, false);

                return dsHRrest;
            }
        }

        /// <summary>
        /// Gets the HR Max data to be displayed on the chart.
        /// </summary>
        internal static LineItem HRmax
        {
            get
            {
                if (dsHRmax == null)
                    dsHRmax = InitializeLine("HRM", Color.DeepPink, Color.Empty, false);

                return dsHRmax;
            }
        }

        internal static IList<DateTime> AthleteEntryDates
        {
            get
            {
                if (entryDates == null)
                    entryDates = PluginMain.GetApplication().Logbook.Athlete.InfoEntries.EntryDates;

                return entryDates;
            }
        }

        /// <summary>
        /// Gets the FTP data to be displayed on the chart.
        /// </summary>
        internal static LineItem FTPcycle
        {
            get
            {
                if (dsFtpCycle == null)
                {
                    dsFtpCycle = InitializeLine("FTP", Color.FromArgb(92, 53, 102), Color.Empty, false);
                    dsFtpCycle.Line.StepType = StepType.ForwardStep;
                    dsFtpCycle.Label.Text = "FTPcycle";
                    dsFtpCycle.Tag = "FTPcycle";
                }

                //dsFtp.Line.StepType = StepType.NonStep;
                return dsFtpCycle;
            }
        }

        /// <summary>
        /// Gets the Running FTP data to be displayed on the chart.
        /// </summary>
        internal static LineItem FTPrun
        {
            get
            {
                if (dsFtpRun == null)
                {
                    dsFtpRun = InitializeLine("FTP", Color.FromArgb(115, 173, 198), Color.Empty, false);
                    dsFtpRun.Line.StepType = StepType.ForwardStep;
                    dsFtpRun.Label.Text = "FTPrun";
                    dsFtpRun.Tag = "FTPrun";
                }

                return dsFtpRun;
            }
        }

        /// <summary>
        /// Gets or sets a value describing the date of the first charted activity.
        /// </summary>
        internal static DateTime FirstActivity
        {
            get { return firstActivity; }
            set { firstActivity = value; }
        }

        /// <summary>
        /// Last StartTime in TrimpActivity list (in the current top category: My Activities, etc).
        /// </summary>
        internal static DateTime LastActivity
        {
            get
            {
                DateTime lastActivity = FirstActivity;
                IActivityCategory topCategory = Utilities.GetTopmostCategory(PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter);

                foreach (TrimpActivity activity in TrimpActivities)
                {
                    if (lastActivity < activity.StartTime && Utilities.GetTopmostCategory(activity) != topCategory)
                    {
                        lastActivity = activity.StartTime;
                    }
                }

                return lastActivity;
            }
        }

        /// <summary>
        /// Gets a list of Trimp and TSS scored activities.  It begins with all activities in logbook, 
        /// but is filtered to contain only My Activities, or My Friends Activities
        /// </summary>
        public static ActivityCollection TrimpActivities
        {
            get
            {
                return trimpActivities;
            }
        }

        /// <summary>
        /// Gets a list of Activities to be displayed to the treelist.  
        /// This list is filtered to match the currently selected category, and is a subset of TrimpActivities.
        /// </summary>
        public static List<TrimpActivity> TreeActivities
        {
            get
            {
                List<TrimpActivity> treeData = new List<TrimpActivity>();

                foreach (TrimpActivity activity in ChartData.TrimpActivities)
                {
                    if (!activity.FilterActivity)
                    {
                        treeData.Add(activity);
                    }
                }

                return treeData;
            }
        }

        /// <summary>
        /// Sets the activities to calculate TL from.
        /// </summary>
        public static IEnumerable<IActivity> Activities
        {
            set
            {
                if (activities != value)
                {
                    dataCalculated = false;
                    activities = value;

                    // This list will only change when all activities are reset.  
                    // May even be able to add/remove items instead of completely re-initializing it
                    trimpActivities = new ActivityCollection();

                    foreach (IActivity activity in activities)
                    {
                        if (activity.StartTime.Year != 1)
                        {
                            trimpActivities.Add(new TrimpActivity(activity));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a list of the activities requiring a data refresh.  
        /// These activities have been added, or modified since the last data calculation.
        /// This is a list of the ReferenceIds.
        /// </summary>
        public static List<string> QueuedActivites
        {
            get { return queuedActivities; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether training load statistics have been calculated or not.
        /// </summary>
        internal static bool IsCalculated
        {
            get
            {
                dataCalculated = (dataCalculated && queuedActivities.Count == 0);
                return dataCalculated;
            }

            set
            {
                dataCalculated = value;
            }
        }

        /// <summary>
        /// LogbookId of currently loaded logbook
        /// </summary>
        internal static string LogbookId
        {
            get
            {
                return logbookId;
            }

            set
            {
                logbookId = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating TL's mode: HR or Power.  HR and Power are currently the only valid modes.  Invalid modes will result in HR mode.
        /// </summary>
        internal static Common.ChartBasis TLChartBasis
        {
            get
            {
                return tlChartBasis;
            }

            set
            {
                if (tlChartBasis != value)
                {
                    dataCalculated = false;
                    tlChartBasis = value;
                }
            }
        }

        internal static GradientFillObj HighlightFocus
        {
            get
            {
                if (highlightFocus == null)
                {
                    double xToday = XDate.DateTimeToXLDate(DateTime.Today);
                    highlightFocus = new GradientFillObj(xToday - 1, 0, 2, 1, CoordType.XScaleYChartFraction);
                    Color[] colors = { Color.Silver, Color.Transparent, Color.Silver };
                    highlightFocus.Fill = new Fill(colors, 0);
                    highlightFocus.ZOrder = ZOrder.E_BehindCurves;
                    highlightFocus.IsVisible = false;
                }

                return highlightFocus;
            }
        }

        internal static LineObj HighlightToday
        {
            get
            {
                if (highlightToday == null)
                {
                    double xToday = XDate.DateTimeToXLDate(DateTime.Today);
                    highlightToday = new LineObj(Color.Red, xToday, 0, xToday, 1);
                    highlightToday.Location.CoordinateFrame = CoordType.XScaleYChartFraction;
                    highlightFocus.ZOrder = ZOrder.E_BehindCurves;
                    highlightFocus.IsVisible = true;
                }

                return highlightToday;
            }
        }

        #endregion

        #region Methods

        internal static CurveItem GetCurve(DataType type)
        {
            // Look in appropriate data series (current or forecasted CTL/ATL/TSB series)
            switch (type)
            {
                case DataType.ATL:
                    return ATL;
                case DataType.BF:
                    return BodyFat;
                case DataType.BMI:
                    return BMI;
                case DataType.CTL:
                    return CTL;
                case DataType.FTPcycle:
                    return FTPcycle;
                case DataType.FTPrun:
                    return FTPrun;
                case DataType.INF:
                    return Influence;
                case DataType.MOV1:
                    return Moving1;
                case DataType.MOV2:
                    return Moving2;
                case DataType.SCORE:
                    return Score;
                case DataType.TSB:
                    return TSB;
                case DataType.Weight:
                    return BodyWeight;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Get a CTL/ATL/TSB value from the chart 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date">Date requested.  Should be in local time.</param>
        /// <returns></returns>
        internal static float GetValue(DataType type, DateTime date)
        {
            //int today = (int)(date - ChartData.FirstActivity.Date.ToLocalTime()).TotalDays;
            CurveItem data = GetCurve(type);
            if (data == null)
                return 0;

            float value;

            // Return value
            value = (float)data.InterpolateX(new XDate(date));

            if (float.IsNaN(value))
                return 0;
            else
                return value;

        }

        internal static DateTime IndexToDate(int index)
        {
            return FirstActivity.AddDays(index);
        }

        /// <summary>
        /// Add the Training Influence curve (by reference) to dsINF using firstActivity and target parameters.
        /// </summary>
        /// <param name="target">Target date of performance, or 'Race Day'.</param>
        /// <param name="dsInf">ChartDataSeries (by reference) that will contain Influence curve.</param>
        internal static void SetInfluenceDate(DateTime target)
        {
            DateTime start = firstActivity;
            int maxModelDays = 180;
            Influence.Clear();
            double inf;

            // Model is best suited for maxModelDays days prior to target date.  Only show this region
            if (target.AddDays(-maxModelDays) > firstActivity)
                start = target.AddDays(-maxModelDays);
            else
                start = firstActivity;

            for (DateTime day = start; day <= target; day = day.AddDays(1))
            {
                int mu = (int)(target.Date - day.Date).TotalDays; // mu = 0 at target date.
                inf = ((UserData.Kc * Math.Exp(-(double)mu / (double)UserData.TCc)) - (UserData.Ka * Math.Exp(-(double)mu / (double)UserData.TCa)));
                Influence.AddPoint(XDate.DateTimeToXLDate(day), inf);
            }
        }

        /// <summary>
        /// Returns the index of the specified date if it exists.  Otherwise it returns -1 if an exact match is not found.
        /// </summary>
        /// <param name="pane"></param>
        /// <param name="curve"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        internal static int PointExists(GraphPane pane, CurveItem curve, DateTime date)
        {
            int lo, hi;
            if (curve.GetPointIndex(XDate.DateTimeToXLDate(date), out lo, out hi, pane) && lo == hi)
                return lo;
            else
                return -1;
        }

        private static CurveList CreateCTLStack()
        {
            ctlstack = new CurveList();
            Color[] colors = { Color.Chocolate, Color.Chartreuse, Color.Indigo, Color.DarkOrange, Color.DarkTurquoise, Color.Green };
            int colorIndex = 0;

            foreach (IActivityCategory category in Utilities.CategoryIndex)
            {
                if (category == Utilities.GetUpperCategory(category))
                {
                    // Create multisport line
                    LineItem line = new LineItem(category.Name, new PointPairList(), Utilities.RandomColor(160, 170, category.GetHashCode()), SymbolType.None);

                    if (colorIndex < colors.Length)
                        line.Color = colors[colorIndex];

                    line.Tag = category.ReferenceId;
                    line.IsY2Axis = CTL.IsY2Axis;
                    line.YAxisIndex = CTL.YAxisIndex;
                    line.Line.Fill = new Fill(Color.FromArgb(60, line.Color));
                    line.Line.SelectedFill = new Fill(Color.FromArgb(120, line.Color));

                    // 'GradientFill' fills the line in variuos colors
                    Color past = line.Color;
                    Color future = Color.FromArgb(20, line.Color);
                    line.Line.Fill = new Fill(past, future); // past = low numbers, future = high numbers
                    line.Line.Fill.Type = FillType.GradientByX;
                    line.Line.Fill.RangeMin = new XDate(DateTime.Today);
                    line.Line.Fill.RangeMax = line.Line.Fill.RangeMin;

                    ctlstack.Add(line);
                    colorIndex++;
                }
            }

            return ctlstack;
        }

        internal static LineItem GetCTLmultisportLine(string catId)
        {
            foreach (LineItem curve in CTLstack)
            {
                string curveId = curve.Tag as string;
                if (Utilities.GetUpperCategory(catId) == Utilities.GetUpperCategory(curveId))
                    return curve;
            }

            return null;
        }

        internal static LineItem GetCTLmultisportLine(IActivity activity)
        {
            foreach (LineItem curve in CTLstack)
            {
                string catId = curve.Tag as string;
                if (Utilities.GetUpperCategory(activity) == Utilities.GetUpperCategory(catId))
                    return curve;
            }

            return null;
        }

        private static LineItem InitializeLine(string id, Color color, Color fillColor, bool dimFutureSegment)
        {
            LineItem line = new LineItem(id);
            int y2index = ViewTrainingLoadPage.Instance.Pane.Y2AxisList.IndexOfTag(id);
            line.IsY2Axis = 0 <= y2index;
            line.Color = color;
            line.Symbol.Type = SymbolType.None;
            line.Tag = id;

            if (line.IsY2Axis)
                line.YAxisIndex = y2index;

            if (fillColor != Color.Empty && !dimFutureSegment)
            {
                line.Line.Fill = new Fill(fillColor);
                line.Line.SelectedFill = new Fill(Color.FromArgb(120, fillColor));
            }
            else if (dimFutureSegment)
            {
                line.Line.SelectedFill = new Fill(Color.FromArgb(120, fillColor));

                // 'GradientFill' fills the line in variuos colors
                Color past = color;
                Color future = Color.FromArgb(50, color);
                line.Line.Fill = new Fill(past, future); // past = low numbers, future = high numbers
                line.Line.Fill.Type = FillType.GradientByX;
                line.Line.Fill.RangeMin = new XDate(DateTime.Today);
                line.Line.Fill.RangeMax = line.Line.Fill.RangeMin;
            }

            return line;
        }

        /// <summary>
        /// Calculate all data required for Training Load.  Specifically, this will create and populate all of the dataSeries.
        /// </summary>
        /// <returns>Returns true when successful, or false if there was an error.</returns>
        internal static void CalculateDataSeries()
        {
            // Re-calculate if data has changed.
            if (!IsCalculated)
            {
                IsCalculated = true;
                ProcessActivityUpdateQueue();
                Calculate();
            }
        }

        /// <summary>
        /// Calculate dataseries.  This is an internal function, please use CalculateDataSeries() to re-calculate data.
        /// </summary>
        private static void Calculate()
        {
            // Initialize the variables
            IAthleteInfoEntry athleteEntry;
            int index;
            ActivityCollection currentActivities = new ActivityCollection();

            double score, ctl, ctl_y, atl, atl_y, tsb, ftpCycle_y, ftpRun_y;
            score = 0;
            ctl = 0;
            ctl_y = UserData.InitialCTL;
            atl = 0;
            atl_y = UserData.InitialATL;
            tsb = 0;
            ftpCycle_y = float.NaN;
            ftpRun_y = float.NaN;

            // Access currently loaded logbook
            ILogbook logbook = PluginMain.GetLogbook();

            // Calculate data tracks for Score (Trimp or TSS), ATL, & CTL
            double xDay = XDate.XLDayMin; // Any default value.
            int forecastDays = 0;

            // Initialize series
            CTL.Clear();
            ATL.Clear();
            TSB.Clear();
            Score.Clear();
            Moving1.Clear();
            Moving2.Clear();
            BMI.Clear();
            BodyFat.Clear();
            BodyWeight.Clear();
            FTPcycle.Clear();
            FTPrun.Clear();

            foreach (LineItem curve in CTLstack)
                curve.Clear();

            // Sort Activities by date TODO: Activity sort: Date should be oldest first through newest last
            TrimpActivities.Sort(new ActivityComparer(ActivityCollection.CompareType.StartDate, true));
            int activityIndex = 0, fpIndex = 0;

            if (0 < TrimpActivities.Count)
                firstActivity = TrimpActivities[0].StartTime.Date;
            else
                firstActivity = DateTime.Today;

            DateTime day = firstActivity;

            // Setup Initial CTL values.
            // <Associated Category RefId, score value> <-- Category RefId is what's used to decompose the charted CTL values
            Dictionary<string, double> ctl_multisport = new Dictionary<string, double>();
            Dictionary<string, double> scores = new Dictionary<string, double>();

            // TODO: Can this be used to sort by sub-categories?
            IActivityCategory topCategory = Utilities.GetTopmostCategory(PluginMain.GetApplication().DisplayOptions.SelectedCategoryFilter);

            ctl_multisport.Add(topCategory.ReferenceId, 0);
            scores.Add(topCategory.ReferenceId, 0);
            foreach (IActivityCategory category in topCategory.SubCategories)
            {
                // Distribute CTL to each major category equally for initial calcs.
                ctl_multisport.Add(category.ReferenceId, UserData.InitialCTL / topCategory.SubCategories.Count);
                scores.Add(category.ReferenceId, 0);
            }

            PointPairList fpScores;

            if (FitPlanPlugin.IsInstalled && GlobalSettings.Instance.ShowFitPlanWorkouts)
            {
                fpScores = FitPlanPlugin.GetScores(DateTime.Today.AddDays(1), DateTime.MaxValue);
            }
            else
            {
                fpScores = new PointPairList();
            }

            List<string> categoryIds = new List<string>(ctl_multisport.Keys);

            while (day <= DateTime.Now.AddMinutes(-1) || (UserData.Forecast && forecastDays < 7))
            {
                xDay = new XDate(day);
                currentActivities.Clear();

                score = 0;

                /*
                 * Should uncomment this to revert back to 'old way'
                // Moving Trimp Sum 1
                Mov1 = Mov1 + Trimp;
                if (currentDay - UserData.MovingSumDays1 >= 0)
                {
                    Mov1 = Mov1 - dsTrimp.Points[currentDay - UserData.MovingSumDays1].Y;
                }
                point.Y = Mov1;
                dsMOV1.Points.Add(currentDay, point);

                // Moving Trimp Sum 2
                Mov2 = Mov2 + Trimp;
                if (currentDay - UserData.MovingSumDays2 >= 0)
                {
                    Mov2 = Mov2 - dsTrimp.Points[currentDay - UserData.MovingSumDays2].Y;
                }
                point.Y = Mov2;
                dsMOV2.Points.Add(currentDay, point);
                */

                // CTL/ATL/TSB (Displayed as TSB at the beginning of the day)
                while (activityIndex < TrimpActivities.Count && TrimpActivities[activityIndex].StartTime.Date <= day)
                {
                    // Calculate CTL composition for each major category
                    // CTL1 + CTL2 + CTL3 = CTLtotal (tested in Excel)
                    TrimpActivity activity = TrimpActivities[activityIndex] as TrimpActivity;

                    if ((!UserData.FilterCharts || (UserData.FilterCharts && !activity.FilterActivity)) && Utilities.GetTopmostCategory(activity) == topCategory)
                    {
                        string upperCat = Utilities.GetUpperCategory(activity).ReferenceId;

                        scores[upperCat] += activity.Score; // Used for CTL below
                        score += activity.Score;
                    }

                    activityIndex += 1;
                }

                // Include all FP Scores in calcs
                if (fpIndex < fpScores.Count && fpScores[fpIndex].X == xDay)
                {
                    scores[topCategory.ReferenceId] += fpScores[fpIndex].Y;
                    score += fpScores[fpIndex].Y;
                    fpIndex++;
                }

                tsb = ctl - atl;
                atl = atl_y + ((score - atl_y) / UserData.TCa);
                atl_y = atl;

                ctl = 0;
                foreach (string key in categoryIds)
                {
                    // Sum all components and store values to 'yesterday' for next iteration
                    ctl_multisport[key] = ctl_multisport[key] + (scores[key] - ctl_multisport[key]) / UserData.TCc;
                    ctl += ctl_multisport[key];

                    scores[key] = 0;
                }

                ctl_y = ctl;

                
                if (ctl > 0.01)
                {
                    TSB.AddPoint(xDay, tsb);
                    ATL.AddPoint(xDay, atl);
                    CTL.AddPoint(xDay, ctl);

                    double chartCTL = 0;
                    foreach (string key in categoryIds)
                    {
                        PointPair point;
                        if (GlobalSettings.Instance.CTLstacked)
                            chartCTL += ctl_multisport[key];
                        else
                            chartCTL = ctl_multisport[key];

                        // x = date, y = CTL line value (may be stacked value), z = CTL single component (used for tooltip display)
                        point = new PointPair(xDay, chartCTL, ctl_multisport[key]);
                        ChartData.GetCTLmultisportLine(key).AddPoint(point);
                    }
                }
                else
                {
                    // Skip charting extended inactive periods
                }

                if (logbook.Athlete.InfoEntries.EntryForDate(day) != null)
                {
                    athleteEntry = logbook.Athlete.InfoEntries.EntryForDate(day);

                    // TODO: (MED) Several charts are not showing up: Weight, MaxHR, MinHR
                    // Add athlete's weight
                    if (!double.IsNaN(athleteEntry.WeightKilograms))
                    {
                        // if the entry is a number, add that point
                        BodyWeight.AddPoint(xDay, Weight.Convert(athleteEntry.WeightKilograms, Weight.Units.Kilogram, PluginMain.GetApplication().SystemPreferences.WeightUnits));
                    }

                    // Add athlete's body fat
                    if (!double.IsNaN(athleteEntry.BodyFatPercentage))
                    {
                        // if the entry is a number, add that point
                        BodyFat.AddPoint(xDay, athleteEntry.BodyFatPercentage);
                    }

                    // Add athlete's BMI
                    if (!float.IsNaN(athleteEntry.BMI))
                    {
                        // if the entry is a number, add that point
                        BMI.AddPoint(xDay, athleteEntry.BMI);
                    }

                    // Add athlete's FTP (Cycling)
                    double ftp = TrimpActivity.GetFTP(day, Shared.Sports.Cycling);
                    if (!double.IsNaN(ftp) && ftp != ftpCycle_y)
                    {
                        // if the entry is a number, add that point
                        FTPcycle.AddPoint(xDay, ftp);
                        ftpCycle_y = ftp;
                    }

                    // Add athlete's FTP (Running)
                    ftp = TrimpActivity.GetFTP(day, Shared.Sports.Running);
                    if (!double.IsNaN(ftp) && ftp != ftpRun_y)
                    {
                        // if the entry is a number, add that point
                        FTPrun.AddPoint(xDay, ftp);
                        ftpRun_y = ftp;
                    }
                }
                
                if (DateTime.Now <= day)
                {
                    if (tsb < ctl - atl || score != 0)
                        forecastDays = 0; // TSB rising
                    else
                        forecastDays++; // TSB declining
                }

                day = day.AddDays(1);
            }

            // Finalize Athlete charts by extending data line to 'today'
            List<CurveItem> athleteCharts = new List<CurveItem>();
            athleteCharts.Add(BMI);
            athleteCharts.Add(BodyFat);
            athleteCharts.Add(FTPcycle);
            athleteCharts.Add(FTPrun);
            athleteCharts.Add(BodyWeight);
            athleteCharts.Add(HRmax);
            athleteCharts.Add(HRrest);

            for (int i = 0; i < athleteCharts.Count; i++)
            {
                if (athleteCharts[i].Points.Count > 0 && PointExists(ViewTrainingLoadPage.Instance.Pane, athleteCharts[i], XDate.XLDateToDateTime(xDay - 1)) == -1)
                {
                    // Extend Line to 'today'
                    athleteCharts[i].AddPoint(xDay - 1, athleteCharts[i].Points[athleteCharts[i].NPts - 1].Y);
                }
            }

            // Populate the Trimp Activities list with manually entered items such as CTL, ATL, DayIndex, etc (these cannot be calculated on the fly)
            // Also, build TSS/Trimp bar series
            foreach (TrimpActivity activity in TrimpActivities)
            {
                activity.DayIndex = activity.GetDayIndex(firstActivity);

                // Build TSS/TRIMP bars.  It's easier to do this here because we're 
                //   looping by activity, not date (and thus can filter activities)
                if (!activity.FilterActivity)
                {
                    index = PointExists(ViewTrainingLoadPage.Instance.Pane, Score, activity.StartTime.Date);
                    if (index != -1)
                    {       
                        // Sum multiple activity days
                        Score[index].Y += activity.Score;
                    }
                    else
                    {
                        // Single entry date/first entry
                        Score.AddPoint(XDate.DateTimeToXLDate(activity.StartTime.Date), activity.Score);
                    }
                }

                // Assign CTL, ATL, TSBPre, Rolling Sums
                index = PointExists(ViewTrainingLoadPage.Instance.Pane, ATL, activity.StartTime.Date);
                if (index != -1)
                {
                    // Current day data
                    activity.CTL = (float)CTL[index].Y;
                    activity.ATL = (float)ATL[index].Y;
                    activity.TSBPre = (float)TSB[index].Y;
                }
            }

            // Add FitPlan scores
            foreach (PointPair point in fpScores)
            {
                Score.AddPoint(point);
            }

            // Sort the points.
            ((PointPairList)dsMOV1.Points).Sort();
            ((PointPairList)dsMOV2.Points).Sort();

            // Build Rolling Sum table data.  Since activities are not in order, this must be done 
            //   afterward (unless the building could be done earlier like CTL/ATL?).
            foreach (TrimpActivity activity in TrimpActivities)
            {
                // Skip My Friend's Activities
                if (Utilities.GetTopmostCategory(activity) != topCategory)
                    continue;

                // Build Moving Sum charts
                AddMovingSum(Moving1, activity.StartTime.Date, UserData.MovingSumCat1, UserData.MovingSumDays1, activity);
                AddMovingSum(Moving2, activity.StartTime.Date, UserData.MovingSumCat2, UserData.MovingSumDays2, activity);

                index = PointExists(ViewTrainingLoadPage.Instance.Pane, Moving1, activity.StartTime.Date);
                if (index != -1)
                {
                    activity.RollingSum1 = (float)Moving1[index].Y;
                }

                index = PointExists(ViewTrainingLoadPage.Instance.Pane, Moving2, activity.StartTime.Date);
                if (index != -1)
                {
                    activity.RollingSum2 = (float)Moving2[index].Y;
                }
            }

            // Sort rolling sum values
            ((PointPairList)Moving1.Points).Sort(SortType.XValues);
            ((PointPairList)Moving2.Points).Sort(SortType.XValues);

            RefreshTodaySplit(CTL);
            RefreshTodaySplit(TSB);
            RefreshTodaySplit(ATL);

            foreach (LineItem line in CTLstack)
            {
                RefreshTodaySplit(line);
            }
        }

        private static LineItem AddMovingSum(LineItem curve, DateTime date, int movingCat, int movingDays, TrimpActivity activity)
        {
            // Moving Sum
            ActivityInfo info = activity.Info;
            int index;
            XDate xDay;

            switch (movingCat)
            {
                case 0: // TRIMP/TSS
                    if (activity.Score > 0)
                    {
                        for (int i = 0; i < movingDays; i++)
                        {
                            xDay = new XDate(date.AddDays(i));
                            index = PointExists(ViewTrainingLoadPage.Instance.Pane, curve, xDay.DateTime);

                            if (index != -1)
                                curve.Points[index].Y += activity.Score;
                            else
                                curve.AddPoint(xDay, activity.Score);
                        }
                    }
                    break;

                case 1: // Distance
                    if (info.DistanceMeters > 0)
                    {
                        for (int i = 0; i < movingDays; i++)
                        {
                            xDay = new XDate(date.AddDays(i));
                            index = PointExists(ViewTrainingLoadPage.Instance.Pane, curve, xDay.DateTime);

                            if (index != -1)
                                curve.Points[index].Y += Length.Convert(info.DistanceMeters, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits);
                            else
                                curve.AddPoint(xDay, Length.Convert(info.DistanceMeters, Length.Units.Meter, PluginMain.GetApplication().SystemPreferences.DistanceUnits));
                        }
                    }

                    break;

                case 2: // Time (in total seconds)
                    if (info.Time > TimeSpan.Zero)
                    {
                        for (int i = 0; i < movingDays; i++)
                        {
                            xDay = new XDate(date.AddDays(i));
                            index = PointExists(ViewTrainingLoadPage.Instance.Pane, curve, xDay.DateTime);

                            if (index != -1)
                                curve.Points[index].Y += info.Time.TotalSeconds;
                            else
                                curve.AddPoint(xDay, info.Time.TotalSeconds);
                        }
                    }

                    break;
            }

            return curve;
        }

        /// <summary>
        /// Adds an activity to the update queue to be processed at an appropriate time
        /// Can handle adds, updates, or removals
        /// </summary>
        /// <param name="activity">Activity to be updated/added/removed</param>
        internal static void QueueActivityUpdate(string refId)
        {
            if (!queuedActivities.Contains(refId))
            {
                queuedActivities.Add(refId);
                dataCalculated = false;
            }
        }

        /// <summary>
        /// Adds, Updates, and Removes all activities that have been queued for data update.
        /// </summary>
        internal static void ProcessActivityUpdateQueue()
        {
            // Activities may be queued several ways:
            // #1 Updated - They'll be removed from TrimpActivities, then re-added
            // #2 Added - A new activity will be added
            // #3 Removed - Activity will be deleted from TrimpActivities
            // This routine will handle all three cases by removing, then adding separately.

            ILogbook logbook = PluginMain.GetLogbook();

            foreach (string id in queuedActivities)
            {
                // Remove selected activity if it exists
                for (int i = 0; i < trimpActivities.Count; i++)
                {
                    if (id == trimpActivities[i].ReferenceId)
                    {
                        // Remove existing/outdated activity
                        trimpActivities.RemoveAt(i);
                        break;
                    }
                }

                // Add new or updated activity if it exists
                IActivity activity = Common.Activities.GetActivity(id);
                if (activity != null)
                {
                    TrimpActivity trimpActivity = new TrimpActivity(activity);

                    // Clear old data
                    trimpActivity.SetCustomDataValue(TrimpActivity.TrimpField, null);
                    trimpActivity.SetCustomDataValue(TrimpActivity.NormPwrField, null);
                    trimpActivity.SetCustomDataValue(TrimpActivity.TSSField, null);

                    trimpActivities.Add(trimpActivity);
                }
            }

            queuedActivities.Clear();
        }

        /// <summary>
        /// Gets the full unfiltered list of TrimpActivities
        /// </summary>
        /// <returns>Returns the full list of TrimpActivities</returns>
        internal static ActivityCollection GetTrimpActivities()
        {
            return trimpActivities;
        }

        /// <summary>
        /// Check Athlete Entries for modification.  Queue activities after changed entries
        /// </summary>
        private static void QueueAthleteEntryActivityUpdates(object empty)
        {
            double? athleteFTP;
            IAthleteInfoEntry entry;
            double tlFTP = double.NaN;
            DateTime date = FirstActivity, maxDate = LastActivity;
            IAthlete athlete = PluginMain.GetLogbook().Athlete;
            ICustomDataFieldDefinition field = TrimpActivity.FTPCycleField;

            if (field != null)
            {
                // Loop through all dates checking for changes.  
                //  This is required (rather than looking through entries) because an FTP entry may have been deleted
                while (date < maxDate)
                {
                    entry = athlete.InfoEntries.LastEntryAsOfDate(date);
                    athleteFTP = entry.GetCustomDataValue(field) as double?;

                    if (athleteFTP != null)
                    {
                        // FTP entry on this date...
                        int index = PointExists(ViewTrainingLoadPage.Instance.Pane, FTPcycle, date);
                        if (index != -1)
                        {
                            tlFTP = FTPcycle[index].Y;
                        }

                        // Look for change, then update all activities after this date.
                        if (athleteFTP != tlFTP && !double.IsNaN(tlFTP))
                        {
                            foreach (TrimpActivity activity in TrimpActivities)
                            {
                                if (activity.StartTime >= date)
                                    QueueActivityUpdate(activity.ReferenceId);
                            }

                            break;
                        }
                    }

                    // TODO: Validate Max/Min HR updates and queue activity update if necessary

                    // Next day
                    date = date.AddDays(1);
                }
            }
        }

        private static void RefreshTodaySplit(LineItem curve)
        {
            XDate today = new XDate(DateTime.Today);

            if (0 < curve.NPts && curve[0].X < today && today < curve[curve.NPts - 1].X)
            {
                float trans = (float)((today - curve[0].X) / (curve[curve.NPts - 1].X - curve[0].X));

                Color past = Color.FromArgb(80, curve.Line.Color);
                Color future = Color.FromArgb(20, Color.Silver);

                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, int.MaxValue, 1), past, future, 0f);
                Blend blend = new Blend();
                blend.Factors = new float[] { 0, 0, 1, 1 };
                blend.Positions = new float[] { 0, trans, trans, 1 };
                brush.Blend = blend;

                //ColorBlend blend = new ColorBlend();
                //blend.Colors = new Color[] { past, past, future, future };
                //blend.Positions = new float[] { 0, trans, trans, 1 };
                //brush.InterpolationColors = blend;

                curve.Line.Fill = new Fill(brush, true);
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Maintains a monitor on the logbook to watch for data changes in ChartData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void SportTracksApplication_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Logbook")
            {
                PluginMain.GetLogbook().Activities.CollectionChanged += new NotifyCollectionChangedEventHandler<IActivity>(Activities_CollectionChanged);
                PluginMain.GetLogbook().Athlete.PropertyChanged += Athlete_PropertyChanged;

                athleteChange = new Timer(500);
                athleteChange.AutoReset = false;
                athleteChange.Elapsed += new ElapsedEventHandler(athleteChange_Elapsed);

                entryDates = null;

                dataCalculated = false;
                ctlstack = null;
            }
        }

        /// <summary>
        /// Monitors for new or deleted activities.  TL should add/remove activities based on this.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void Activities_CollectionChanged(object sender, NotifyCollectionChangedEventArgs<IActivity> e)
        {
            dataCalculated = false;
            Common.ClearActivityList();

            // Process new things
            foreach (object item in e.NewItems)
            {
                IActivity activity = item as IActivity;

                if (activity != null)
                {
                    QueueActivityUpdate(activity.ReferenceId);
                }
            }

            // process old things
            foreach (object item in e.OldItems)
            {
                IActivity activity = item as IActivity;

                if (activity != null)
                {
                    QueueActivityUpdate(activity.ReferenceId);
                }
            }

            // Refresh view if it's active (this might occur during an import)
            if (PluginMain.GetApplication().ActiveView.Id == GUIDs.PluginMain)
            {
                ViewTrainingLoadPage.Instance.RefreshPage();
            }
        }

        /// <summary>
        /// Occurs when athlete info is changed in this logbook.
        /// TL is using this to monitor for athlete FTP changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Athlete_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Intelligent way to only request update of those activities affected by the changes 
            //  (changed date and newer)
            if (e.PropertyName == "InfoEntries")
            {
                // TODO: (MED) Address athlete info changes (ftp, HRrest, HRmax, etc.)
                // HACK: use a timer to slightly delay processing of ftp updates.  Could this be related to FTP null values?
                // Turns out, I believe the timer is executing this request in a worker thread of some sort?
                // Timer resets during quick changes (like typing in notes)
                athleteChange.Stop();
                athleteChange.Start();

                // Initialize InfoEntries in the event that a value for a new date was added.
                entryDates = null;
            }

            // No FTP changes found (not an InfoEntry change, different field, FTP re-purposed, etc.)
        }

        private static void athleteChange_Elapsed(object sender, ElapsedEventArgs e)
        {
            QueueAthleteEntryActivityUpdates(null);
        }

        private static void UserData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PerfDate")
            {
                SetInfluenceDate(UserData.PerfDate);
            }
        }

        #endregion
    }
}
