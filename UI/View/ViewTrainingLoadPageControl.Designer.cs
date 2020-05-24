// <copyright file="ViewTrainingLoadPageControl.Designer.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.UI.View
{
    partial class ViewTrainingLoadPageControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewTrainingLoadPageControl));
            this.vertSplitContainer = new System.Windows.Forms.SplitContainer();
            this.vertSubSplitContainer = new System.Windows.Forms.SplitContainer();
            this.headerSplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.trainingViewActionBanner = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.controlPanel = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.zoomInButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.stripesButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.zoomOutButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.selectFieldsButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.autoFitButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.savePicButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.MainChart = new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl();
            this.bottomHorSplitContainer = new System.Windows.Forms.SplitContainer();
            this.treeActivity = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.currentViewActionBanner = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTCc = new System.Windows.Forms.TextBox();
            this.txtTCa = new System.Windows.Forms.TextBox();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblPerf = new System.Windows.Forms.Label();
            this.lblFitness = new System.Windows.Forms.Label();
            this.lblATL = new System.Windows.Forms.Label();
            this.valTaperDate = new System.Windows.Forms.Label();
            this.lblTaperDate = new System.Windows.Forms.Label();
            this.lblCTL = new System.Windows.Forms.Label();
            this.valATL = new System.Windows.Forms.Label();
            this.valTSB = new System.Windows.Forms.Label();
            this.lblMaxEffect = new System.Windows.Forms.Label();
            this.valCTL = new System.Windows.Forms.Label();
            this.valMaxEffect = new System.Windows.Forms.Label();
            this.lblTSB = new System.Windows.Forms.Label();
            this.lblTargetDate = new System.Windows.Forms.Label();
            this.valTargetDate = new System.Windows.Forms.Label();
            this.lblFatigue = new System.Windows.Forms.Label();
            this.pnlHRchart = new System.Windows.Forms.Panel();
            this.chartStatus = new ZoneFiveSoftware.Common.Visuals.Chart.ChartBase();
            this.currentActivityActionBanner = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.mnuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItemTrimp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemTSS = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemDaniels = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemMultisportEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemStackCTL = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemShowFitPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecalculate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chartToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.vertSplitContainer.Panel1.SuspendLayout();
            this.vertSplitContainer.Panel2.SuspendLayout();
            this.vertSplitContainer.SuspendLayout();
            this.vertSubSplitContainer.Panel1.SuspendLayout();
            this.vertSubSplitContainer.Panel2.SuspendLayout();
            this.vertSubSplitContainer.SuspendLayout();
            this.headerSplitContainer2.Panel1.SuspendLayout();
            this.headerSplitContainer2.Panel2.SuspendLayout();
            this.headerSplitContainer2.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.bottomHorSplitContainer.Panel1.SuspendLayout();
            this.bottomHorSplitContainer.Panel2.SuspendLayout();
            this.bottomHorSplitContainer.SuspendLayout();
            this.pnlSettings.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlHRchart.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // vertSplitContainer
            // 
            this.vertSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vertSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.vertSplitContainer.Name = "vertSplitContainer";
            this.vertSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // vertSplitContainer.Panel1
            // 
            this.vertSplitContainer.Panel1.Controls.Add(this.vertSubSplitContainer);
            // 
            // vertSplitContainer.Panel2
            // 
            this.vertSplitContainer.Panel2.Controls.Add(this.bottomHorSplitContainer);
            this.vertSplitContainer.Panel2MinSize = 150;
            this.vertSplitContainer.Size = new System.Drawing.Size(678, 596);
            this.vertSplitContainer.SplitterDistance = 431;
            this.vertSplitContainer.TabIndex = 0;
            // 
            // vertSubSplitContainer
            // 
            this.vertSubSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vertSubSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.vertSubSplitContainer.IsSplitterFixed = true;
            this.vertSubSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.vertSubSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.vertSubSplitContainer.Name = "vertSubSplitContainer";
            this.vertSubSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // vertSubSplitContainer.Panel1
            // 
            this.vertSubSplitContainer.Panel1.Controls.Add(this.headerSplitContainer2);
            // 
            // vertSubSplitContainer.Panel2
            // 
            this.vertSubSplitContainer.Panel2.Controls.Add(this.MainChart);
            this.vertSubSplitContainer.Size = new System.Drawing.Size(678, 431);
            this.vertSubSplitContainer.SplitterDistance = 49;
            this.vertSubSplitContainer.SplitterWidth = 1;
            this.vertSubSplitContainer.TabIndex = 0;
            // 
            // headerSplitContainer2
            // 
            this.headerSplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerSplitContainer2.IsSplitterFixed = true;
            this.headerSplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.headerSplitContainer2.Name = "headerSplitContainer2";
            this.headerSplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // headerSplitContainer2.Panel1
            // 
            this.headerSplitContainer2.Panel1.Controls.Add(this.trainingViewActionBanner);
            // 
            // headerSplitContainer2.Panel2
            // 
            this.headerSplitContainer2.Panel2.Controls.Add(this.controlPanel);
            this.headerSplitContainer2.Panel2MinSize = 20;
            this.headerSplitContainer2.Size = new System.Drawing.Size(678, 49);
            this.headerSplitContainer2.SplitterDistance = 25;
            this.headerSplitContainer2.SplitterWidth = 1;
            this.headerSplitContainer2.TabIndex = 3;
            // 
            // trainingViewActionBanner
            // 
            this.trainingViewActionBanner.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.trainingViewActionBanner.BackColor = System.Drawing.Color.Transparent;
            this.trainingViewActionBanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trainingViewActionBanner.HasMenuButton = true;
            this.trainingViewActionBanner.Location = new System.Drawing.Point(0, 0);
            this.trainingViewActionBanner.Name = "trainingViewActionBanner";
            this.trainingViewActionBanner.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trainingViewActionBanner.Size = new System.Drawing.Size(678, 25);
            this.trainingViewActionBanner.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header1;
            this.trainingViewActionBanner.TabIndex = 0;
            this.trainingViewActionBanner.Text = "Training Load Chart";
            this.trainingViewActionBanner.UseStyleFont = true;
            this.trainingViewActionBanner.MenuClicked += new System.EventHandler(this.trainingViewActionBanner_MenuClicked);
            // 
            // controlPanel
            // 
            this.controlPanel.BackColor = System.Drawing.Color.Transparent;
            this.controlPanel.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.None;
            this.controlPanel.BorderColor = System.Drawing.Color.Transparent;
            this.controlPanel.BorderShadowColor = System.Drawing.Color.Transparent;
            this.controlPanel.Controls.Add(this.zoomInButton);
            this.controlPanel.Controls.Add(this.stripesButton);
            this.controlPanel.Controls.Add(this.zoomOutButton);
            this.controlPanel.Controls.Add(this.selectFieldsButton);
            this.controlPanel.Controls.Add(this.autoFitButton);
            this.controlPanel.Controls.Add(this.savePicButton);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.controlPanel.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.controlPanel.HeadingFont = null;
            this.controlPanel.HeadingLeftMargin = 0;
            this.controlPanel.HeadingText = null;
            this.controlPanel.HeadingTextColor = System.Drawing.Color.Black;
            this.controlPanel.HeadingTopMargin = 3;
            this.controlPanel.Location = new System.Drawing.Point(456, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(222, 23);
            this.controlPanel.TabIndex = 7;
            // 
            // zoomInButton
            // 
            this.zoomInButton.BackColor = System.Drawing.Color.Transparent;
            this.zoomInButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("zoomInButton.BackgroundImage")));
            this.zoomInButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.zoomInButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.zoomInButton.CenterImage = null;
            this.zoomInButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.zoomInButton.HyperlinkStyle = false;
            this.zoomInButton.ImageMargin = 2;
            this.zoomInButton.LeftImage = null;
            this.zoomInButton.Location = new System.Drawing.Point(195, 0);
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.PushStyle = true;
            this.zoomInButton.RightImage = null;
            this.zoomInButton.Size = new System.Drawing.Size(20, 20);
            this.zoomInButton.TabIndex = 2;
            this.zoomInButton.TabStop = false;
            this.zoomInButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.zoomInButton.TextLeftMargin = 2;
            this.zoomInButton.TextRightMargin = 2;
            this.zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);
            // 
            // stripesButton
            // 
            this.stripesButton.BackColor = System.Drawing.Color.Transparent;
            this.stripesButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("stripesButton.BackgroundImage")));
            this.stripesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.stripesButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.stripesButton.CenterImage = null;
            this.stripesButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.stripesButton.HyperlinkStyle = false;
            this.stripesButton.ImageMargin = 2;
            this.stripesButton.LeftImage = null;
            this.stripesButton.Location = new System.Drawing.Point(91, 1);
            this.stripesButton.Name = "stripesButton";
            this.stripesButton.PushStyle = true;
            this.stripesButton.RightImage = null;
            this.stripesButton.Size = new System.Drawing.Size(20, 20);
            this.stripesButton.TabIndex = 3;
            this.stripesButton.TabStop = false;
            this.stripesButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.stripesButton.TextLeftMargin = 2;
            this.stripesButton.TextRightMargin = 2;
            this.stripesButton.Click += new System.EventHandler(this.stripesButton_Click);
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.BackColor = System.Drawing.Color.Transparent;
            this.zoomOutButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("zoomOutButton.BackgroundImage")));
            this.zoomOutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.zoomOutButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.zoomOutButton.CenterImage = null;
            this.zoomOutButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.zoomOutButton.HyperlinkStyle = false;
            this.zoomOutButton.ImageMargin = 2;
            this.zoomOutButton.LeftImage = null;
            this.zoomOutButton.Location = new System.Drawing.Point(169, 0);
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.PushStyle = true;
            this.zoomOutButton.RightImage = null;
            this.zoomOutButton.Size = new System.Drawing.Size(20, 20);
            this.zoomOutButton.TabIndex = 3;
            this.zoomOutButton.TabStop = false;
            this.zoomOutButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.zoomOutButton.TextLeftMargin = 2;
            this.zoomOutButton.TextRightMargin = 2;
            this.zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);
            // 
            // selectFieldsButton
            // 
            this.selectFieldsButton.BackColor = System.Drawing.Color.Transparent;
            this.selectFieldsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("selectFieldsButton.BackgroundImage")));
            this.selectFieldsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.selectFieldsButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.selectFieldsButton.CenterImage = null;
            this.selectFieldsButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.selectFieldsButton.HyperlinkStyle = false;
            this.selectFieldsButton.ImageMargin = 2;
            this.selectFieldsButton.LeftImage = null;
            this.selectFieldsButton.Location = new System.Drawing.Point(117, 0);
            this.selectFieldsButton.Name = "selectFieldsButton";
            this.selectFieldsButton.PushStyle = true;
            this.selectFieldsButton.RightImage = null;
            this.selectFieldsButton.Size = new System.Drawing.Size(20, 20);
            this.selectFieldsButton.TabIndex = 6;
            this.selectFieldsButton.TabStop = false;
            this.selectFieldsButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.selectFieldsButton.TextLeftMargin = 2;
            this.selectFieldsButton.TextRightMargin = 2;
            this.selectFieldsButton.Click += new System.EventHandler(this.selectFieldsButton_Click);
            // 
            // autoFitButton
            // 
            this.autoFitButton.BackColor = System.Drawing.Color.Transparent;
            this.autoFitButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("autoFitButton.BackgroundImage")));
            this.autoFitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.autoFitButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.autoFitButton.CenterImage = null;
            this.autoFitButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.autoFitButton.HyperlinkStyle = false;
            this.autoFitButton.ImageMargin = 2;
            this.autoFitButton.LeftImage = null;
            this.autoFitButton.Location = new System.Drawing.Point(143, 0);
            this.autoFitButton.Name = "autoFitButton";
            this.autoFitButton.PushStyle = true;
            this.autoFitButton.RightImage = null;
            this.autoFitButton.Size = new System.Drawing.Size(20, 20);
            this.autoFitButton.TabIndex = 4;
            this.autoFitButton.TabStop = false;
            this.autoFitButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.autoFitButton.TextLeftMargin = 2;
            this.autoFitButton.TextRightMargin = 2;
            this.autoFitButton.Click += new System.EventHandler(this.zoomFitButton_Click);
            // 
            // savePicButton
            // 
            this.savePicButton.BackColor = System.Drawing.Color.Transparent;
            this.savePicButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("savePicButton.BackgroundImage")));
            this.savePicButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.savePicButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.savePicButton.CenterImage = null;
            this.savePicButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.savePicButton.HyperlinkStyle = false;
            this.savePicButton.ImageMargin = 2;
            this.savePicButton.LeftImage = null;
            this.savePicButton.Location = new System.Drawing.Point(65, 0);
            this.savePicButton.Name = "savePicButton";
            this.savePicButton.PushStyle = true;
            this.savePicButton.RightImage = null;
            this.savePicButton.Size = new System.Drawing.Size(20, 20);
            this.savePicButton.TabIndex = 5;
            this.savePicButton.TabStop = false;
            this.savePicButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.savePicButton.TextLeftMargin = 2;
            this.savePicButton.TextRightMargin = 2;
            this.savePicButton.Click += new System.EventHandler(this.savePicButton_Click);
            // 
            // MainChart
            // 
            this.MainChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainChart.FitToSelection = true;
            this.MainChart.IsShowPointValues = true;
            this.MainChart.Location = new System.Drawing.Point(0, 0);
            this.MainChart.Name = "MainChart";
            this.MainChart.ScrollGrace = 0D;
            this.MainChart.ScrollMaxX = 0D;
            this.MainChart.ScrollMaxY = 0D;
            this.MainChart.ScrollMaxY2 = 0D;
            this.MainChart.ScrollMinX = 0D;
            this.MainChart.ScrollMinY = 0D;
            this.MainChart.ScrollMinY2 = 0D;
            this.MainChart.Size = new System.Drawing.Size(678, 381);
            this.MainChart.TabIndex = 0;
            this.MainChart.PointValueEvent += new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl.PointValueHandler(this.MainChart_PointValueEvent);
            this.MainChart.DoubleClickEvent += new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl.ZedMouseEventHandler(this.MainChart_DoubleClickEvent);
            this.MainChart.SelectData += new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl.SelectDataHandler(this.MainChart_SelectData);
            this.MainChart.SelectingData += new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl.SelectDataHandler(this.MainChart_SelectingData);
            // 
            // bottomHorSplitContainer
            // 
            this.bottomHorSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomHorSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.bottomHorSplitContainer.Name = "bottomHorSplitContainer";
            // 
            // bottomHorSplitContainer.Panel1
            // 
            this.bottomHorSplitContainer.Panel1.Controls.Add(this.treeActivity);
            this.bottomHorSplitContainer.Panel1.Controls.Add(this.currentViewActionBanner);
            // 
            // bottomHorSplitContainer.Panel2
            // 
            this.bottomHorSplitContainer.Panel2.Controls.Add(this.pnlSettings);
            this.bottomHorSplitContainer.Panel2.Controls.Add(this.pnlStatus);
            this.bottomHorSplitContainer.Panel2.Controls.Add(this.pnlHRchart);
            this.bottomHorSplitContainer.Panel2.Controls.Add(this.currentActivityActionBanner);
            this.bottomHorSplitContainer.Panel2MinSize = 100;
            this.bottomHorSplitContainer.Size = new System.Drawing.Size(678, 161);
            this.bottomHorSplitContainer.SplitterDistance = 463;
            this.bottomHorSplitContainer.TabIndex = 0;
            // 
            // treeActivity
            // 
            this.treeActivity.BackColor = System.Drawing.Color.Transparent;
            this.treeActivity.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeActivity.CheckBoxes = false;
            this.treeActivity.DefaultIndent = 15;
            this.treeActivity.DefaultRowHeight = -1;
            this.treeActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeActivity.HeaderRowHeight = 21;
            this.treeActivity.Location = new System.Drawing.Point(0, 31);
            this.treeActivity.MultiSelect = true;
            this.treeActivity.Name = "treeActivity";
            this.treeActivity.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeActivity.NumLockedColumns = 0;
            this.treeActivity.RowAlternatingColors = true;
            this.treeActivity.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(36)))), ((int)(((byte)(106)))));
            this.treeActivity.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeActivity.RowHotlightMouse = true;
            this.treeActivity.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeActivity.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeActivity.RowSeparatorLines = true;
            this.treeActivity.ShowLines = false;
            this.treeActivity.ShowPlusMinus = false;
            this.treeActivity.Size = new System.Drawing.Size(463, 130);
            this.treeActivity.TabIndex = 1;
            this.treeActivity.SelectedItemsChanged += new System.EventHandler(this.treeActivity_SelectedChanged);
            this.treeActivity.ColumnClicked += new ZoneFiveSoftware.Common.Visuals.TreeList.ColumnEventHandler(this.treeActivity_ColumnClicked);
            this.treeActivity.Click += new System.EventHandler(this.treeActivity_Click);
            this.treeActivity.DoubleClick += new System.EventHandler(this.treeActivity_DoubleClick);
            // 
            // currentViewActionBanner
            // 
            this.currentViewActionBanner.BackColor = System.Drawing.Color.Transparent;
            this.currentViewActionBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.currentViewActionBanner.HasMenuButton = false;
            this.currentViewActionBanner.Location = new System.Drawing.Point(0, 0);
            this.currentViewActionBanner.Name = "currentViewActionBanner";
            this.currentViewActionBanner.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.currentViewActionBanner.Size = new System.Drawing.Size(463, 31);
            this.currentViewActionBanner.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header1;
            this.currentViewActionBanner.TabIndex = 0;
            this.currentViewActionBanner.Text = "Activities";
            this.currentViewActionBanner.UseStyleFont = true;
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.label1);
            this.pnlSettings.Controls.Add(this.label2);
            this.pnlSettings.Controls.Add(this.txtTCc);
            this.pnlSettings.Controls.Add(this.txtTCa);
            this.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSettings.Location = new System.Drawing.Point(0, 31);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(211, 130);
            this.pnlSettings.TabIndex = 3;
            this.pnlSettings.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "CTL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ATL";
            // 
            // txtTCc
            // 
            this.txtTCc.Location = new System.Drawing.Point(40, 29);
            this.txtTCc.Name = "txtTCc";
            this.txtTCc.Size = new System.Drawing.Size(50, 20);
            this.txtTCc.TabIndex = 5;
            this.txtTCc.Text = "42";
            this.txtTCc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTCc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitValidator);
            this.txtTCc.Leave += new System.EventHandler(this.txtTCc_TextChanged);
            // 
            // txtTCa
            // 
            this.txtTCa.Location = new System.Drawing.Point(40, 3);
            this.txtTCa.Name = "txtTCa";
            this.txtTCa.Size = new System.Drawing.Size(50, 20);
            this.txtTCa.TabIndex = 3;
            this.txtTCa.Text = "15";
            this.txtTCa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTCa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitValidator);
            this.txtTCa.Leave += new System.EventHandler(this.txtTCa_TextChanged);
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.lblPerf);
            this.pnlStatus.Controls.Add(this.lblFitness);
            this.pnlStatus.Controls.Add(this.lblATL);
            this.pnlStatus.Controls.Add(this.valTaperDate);
            this.pnlStatus.Controls.Add(this.lblTaperDate);
            this.pnlStatus.Controls.Add(this.lblCTL);
            this.pnlStatus.Controls.Add(this.valATL);
            this.pnlStatus.Controls.Add(this.valTSB);
            this.pnlStatus.Controls.Add(this.lblMaxEffect);
            this.pnlStatus.Controls.Add(this.valCTL);
            this.pnlStatus.Controls.Add(this.valMaxEffect);
            this.pnlStatus.Controls.Add(this.lblTSB);
            this.pnlStatus.Controls.Add(this.lblTargetDate);
            this.pnlStatus.Controls.Add(this.valTargetDate);
            this.pnlStatus.Controls.Add(this.lblFatigue);
            this.pnlStatus.Location = new System.Drawing.Point(4, 37);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(190, 99);
            this.pnlStatus.TabIndex = 2;
            // 
            // lblPerf
            // 
            this.lblPerf.AutoSize = true;
            this.lblPerf.Location = new System.Drawing.Point(3, 62);
            this.lblPerf.Name = "lblPerf";
            this.lblPerf.Size = new System.Drawing.Size(67, 13);
            this.lblPerf.TabIndex = 1;
            this.lblPerf.Text = "Performance";
            this.lblPerf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFitness
            // 
            this.lblFitness.AutoSize = true;
            this.lblFitness.Location = new System.Drawing.Point(3, 0);
            this.lblFitness.Name = "lblFitness";
            this.lblFitness.Size = new System.Drawing.Size(40, 13);
            this.lblFitness.TabIndex = 1;
            this.lblFitness.Text = "Fitness";
            this.lblFitness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblATL
            // 
            this.lblATL.AutoSize = true;
            this.lblATL.Image = ((System.Drawing.Image)(resources.GetObject("lblATL.Image")));
            this.lblATL.Location = new System.Drawing.Point(8, 44);
            this.lblATL.Name = "lblATL";
            this.lblATL.Size = new System.Drawing.Size(27, 13);
            this.lblATL.TabIndex = 1;
            this.lblATL.Text = "ATL";
            // 
            // valTaperDate
            // 
            this.valTaperDate.AutoSize = true;
            this.valTaperDate.Location = new System.Drawing.Point(100, 75);
            this.valTaperDate.Name = "valTaperDate";
            this.valTaperDate.Size = new System.Drawing.Size(65, 13);
            this.valTaperDate.TabIndex = 1;
            this.valTaperDate.Text = "01/01/2008";
            this.valTaperDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTaperDate
            // 
            this.lblTaperDate.AutoSize = true;
            this.lblTaperDate.Location = new System.Drawing.Point(83, 62);
            this.lblTaperDate.Name = "lblTaperDate";
            this.lblTaperDate.Size = new System.Drawing.Size(61, 13);
            this.lblTaperDate.TabIndex = 1;
            this.lblTaperDate.Text = "Taper Date";
            this.lblTaperDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCTL
            // 
            this.lblCTL.AutoSize = true;
            this.lblCTL.Image = ((System.Drawing.Image)(resources.GetObject("lblCTL.Image")));
            this.lblCTL.Location = new System.Drawing.Point(8, 13);
            this.lblCTL.Name = "lblCTL";
            this.lblCTL.Size = new System.Drawing.Size(27, 13);
            this.lblCTL.TabIndex = 1;
            this.lblCTL.Text = "CTL";
            // 
            // valATL
            // 
            this.valATL.AutoSize = true;
            this.valATL.Location = new System.Drawing.Point(49, 44);
            this.valATL.Name = "valATL";
            this.valATL.Size = new System.Drawing.Size(27, 13);
            this.valATL.TabIndex = 1;
            this.valATL.Text = "ATL";
            this.valATL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valTSB
            // 
            this.valTSB.AutoSize = true;
            this.valTSB.Location = new System.Drawing.Point(49, 75);
            this.valTSB.Name = "valTSB";
            this.valTSB.Size = new System.Drawing.Size(28, 13);
            this.valTSB.TabIndex = 1;
            this.valTSB.Text = "TSB";
            this.valTSB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMaxEffect
            // 
            this.lblMaxEffect.AutoSize = true;
            this.lblMaxEffect.Location = new System.Drawing.Point(83, 31);
            this.lblMaxEffect.Name = "lblMaxEffect";
            this.lblMaxEffect.Size = new System.Drawing.Size(99, 13);
            this.lblMaxEffect.TabIndex = 1;
            this.lblMaxEffect.Text = "Max Training Effect";
            this.lblMaxEffect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // valCTL
            // 
            this.valCTL.AutoSize = true;
            this.valCTL.Location = new System.Drawing.Point(49, 13);
            this.valCTL.Name = "valCTL";
            this.valCTL.Size = new System.Drawing.Size(27, 13);
            this.valCTL.TabIndex = 1;
            this.valCTL.Text = "CTL";
            this.valCTL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valMaxEffect
            // 
            this.valMaxEffect.AutoSize = true;
            this.valMaxEffect.Location = new System.Drawing.Point(100, 44);
            this.valMaxEffect.Name = "valMaxEffect";
            this.valMaxEffect.Size = new System.Drawing.Size(65, 13);
            this.valMaxEffect.TabIndex = 1;
            this.valMaxEffect.Text = "01/01/2008";
            this.valMaxEffect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTSB
            // 
            this.lblTSB.AutoSize = true;
            this.lblTSB.Image = ((System.Drawing.Image)(resources.GetObject("lblTSB.Image")));
            this.lblTSB.Location = new System.Drawing.Point(8, 75);
            this.lblTSB.Name = "lblTSB";
            this.lblTSB.Size = new System.Drawing.Size(28, 13);
            this.lblTSB.TabIndex = 1;
            this.lblTSB.Text = "TSB";
            // 
            // lblTargetDate
            // 
            this.lblTargetDate.AutoSize = true;
            this.lblTargetDate.Location = new System.Drawing.Point(83, 0);
            this.lblTargetDate.Name = "lblTargetDate";
            this.lblTargetDate.Size = new System.Drawing.Size(64, 13);
            this.lblTargetDate.TabIndex = 1;
            this.lblTargetDate.Text = "Target Date";
            this.lblTargetDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // valTargetDate
            // 
            this.valTargetDate.AutoSize = true;
            this.valTargetDate.Location = new System.Drawing.Point(100, 13);
            this.valTargetDate.Name = "valTargetDate";
            this.valTargetDate.Size = new System.Drawing.Size(65, 13);
            this.valTargetDate.TabIndex = 1;
            this.valTargetDate.Text = "01/01/2008";
            this.valTargetDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFatigue
            // 
            this.lblFatigue.AutoSize = true;
            this.lblFatigue.Location = new System.Drawing.Point(3, 31);
            this.lblFatigue.Name = "lblFatigue";
            this.lblFatigue.Size = new System.Drawing.Size(42, 13);
            this.lblFatigue.TabIndex = 1;
            this.lblFatigue.Text = "Fatigue";
            this.lblFatigue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHRchart
            // 
            this.pnlHRchart.Controls.Add(this.chartStatus);
            this.pnlHRchart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHRchart.Location = new System.Drawing.Point(0, 31);
            this.pnlHRchart.Name = "pnlHRchart";
            this.pnlHRchart.Size = new System.Drawing.Size(211, 130);
            this.pnlHRchart.TabIndex = 2;
            // 
            // chartStatus
            // 
            this.chartStatus.BackColor = System.Drawing.Color.Transparent;
            this.chartStatus.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.chartStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartStatus.Location = new System.Drawing.Point(0, 0);
            this.chartStatus.Name = "chartStatus";
            this.chartStatus.Padding = new System.Windows.Forms.Padding(5);
            this.chartStatus.Size = new System.Drawing.Size(211, 130);
            this.chartStatus.TabIndex = 2;
            // 
            // currentActivityActionBanner
            // 
            this.currentActivityActionBanner.BackColor = System.Drawing.Color.Transparent;
            this.currentActivityActionBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.currentActivityActionBanner.HasMenuButton = true;
            this.currentActivityActionBanner.Location = new System.Drawing.Point(0, 0);
            this.currentActivityActionBanner.Name = "currentActivityActionBanner";
            this.currentActivityActionBanner.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.currentActivityActionBanner.Size = new System.Drawing.Size(211, 31);
            this.currentActivityActionBanner.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header1;
            this.currentActivityActionBanner.TabIndex = 0;
            this.currentActivityActionBanner.Text = "Current Training Status";
            this.currentActivityActionBanner.UseStyleFont = true;
            this.currentActivityActionBanner.MenuClicked += new System.EventHandler(this.ActivityActionBanner_MenuClicked);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemTrimp,
            this.mnuItemTSS,
            this.mnuItemDaniels,
            this.toolStripSeparator1,
            this.mnuItemMultisportEnable,
            this.mnuItemStackCTL,
            this.mnuItemShowFitPlan});
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(197, 142);
            // 
            // mnuItemTrimp
            // 
            this.mnuItemTrimp.Name = "mnuItemTrimp";
            this.mnuItemTrimp.Size = new System.Drawing.Size(196, 22);
            this.mnuItemTrimp.Tag = "Trimp";
            this.mnuItemTrimp.Text = "Trimp";
            this.mnuItemTrimp.Click += new System.EventHandler(this.menuChartItem_Click);
            // 
            // mnuItemTSS
            // 
            this.mnuItemTSS.Name = "mnuItemTSS";
            this.mnuItemTSS.Size = new System.Drawing.Size(196, 22);
            this.mnuItemTSS.Tag = "TSS";
            this.mnuItemTSS.Text = "Training Stress Score";
            this.mnuItemTSS.Click += new System.EventHandler(this.menuChartItem_Click);
            // 
            // mnuItemDaniels
            // 
            this.mnuItemDaniels.Name = "mnuItemDaniels";
            this.mnuItemDaniels.Size = new System.Drawing.Size(196, 22);
            this.mnuItemDaniels.Tag = "DanielsPoints";
            this.mnuItemDaniels.Text = "Daniels Points";
            this.mnuItemDaniels.Visible = false;
            this.mnuItemDaniels.Click += new System.EventHandler(this.menuChartItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // mnuItemMultisportEnable
            // 
            this.mnuItemMultisportEnable.Name = "mnuItemMultisportEnable";
            this.mnuItemMultisportEnable.Size = new System.Drawing.Size(196, 22);
            this.mnuItemMultisportEnable.Tag = "EnableMultiSport";
            this.mnuItemMultisportEnable.Text = "Enable MultiSport";
            this.mnuItemMultisportEnable.Click += new System.EventHandler(this.menuMultiSportItem_Click);
            // 
            // mnuItemStackCTL
            // 
            this.mnuItemStackCTL.Name = "mnuItemStackCTL";
            this.mnuItemStackCTL.Size = new System.Drawing.Size(196, 22);
            this.mnuItemStackCTL.Tag = "StackCTL";
            this.mnuItemStackCTL.Text = "Stack CTL";
            this.mnuItemStackCTL.Click += new System.EventHandler(this.menuMultiSportItem_Click);
            // 
            // mnuItemShowFitPlan
            // 
            this.mnuItemShowFitPlan.Name = "mnuItemShowFitPlan";
            this.mnuItemShowFitPlan.Size = new System.Drawing.Size(196, 22);
            this.mnuItemShowFitPlan.Text = "Show FitPlan Workouts";
            this.mnuItemShowFitPlan.Click += new System.EventHandler(this.mnuItemShowFitPlan_Click);
            // 
            // mnuRecalculate
            // 
            this.mnuRecalculate.Name = "mnuRecalculate";
            this.mnuRecalculate.Size = new System.Drawing.Size(61, 4);
            // 
            // chartToolTip
            // 
            this.chartToolTip.AutoPopDelay = 5000;
            this.chartToolTip.InitialDelay = 100;
            this.chartToolTip.ReshowDelay = 100;
            // 
            // ViewTrainingLoadPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vertSplitContainer);
            this.Name = "ViewTrainingLoadPageControl";
            this.Size = new System.Drawing.Size(678, 596);
            this.vertSplitContainer.Panel1.ResumeLayout(false);
            this.vertSplitContainer.Panel2.ResumeLayout(false);
            this.vertSplitContainer.ResumeLayout(false);
            this.vertSubSplitContainer.Panel1.ResumeLayout(false);
            this.vertSubSplitContainer.Panel2.ResumeLayout(false);
            this.vertSubSplitContainer.ResumeLayout(false);
            this.headerSplitContainer2.Panel1.ResumeLayout(false);
            this.headerSplitContainer2.Panel2.ResumeLayout(false);
            this.headerSplitContainer2.ResumeLayout(false);
            this.controlPanel.ResumeLayout(false);
            this.bottomHorSplitContainer.Panel1.ResumeLayout(false);
            this.bottomHorSplitContainer.Panel2.ResumeLayout(false);
            this.bottomHorSplitContainer.ResumeLayout(false);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.pnlHRchart.ResumeLayout(false);
            this.mnuMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer vertSplitContainer;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner trainingViewActionBanner;
        private System.Windows.Forms.SplitContainer bottomHorSplitContainer;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner currentViewActionBanner;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner currentActivityActionBanner;
        private ZoneFiveSoftware.Common.Visuals.Button zoomInButton;
        private System.Windows.Forms.SplitContainer vertSubSplitContainer;
        private ZoneFiveSoftware.Common.Visuals.Button stripesButton;
        private ZoneFiveSoftware.Common.Visuals.Button selectFieldsButton;
        private ZoneFiveSoftware.Common.Visuals.Button savePicButton;
        private ZoneFiveSoftware.Common.Visuals.Button autoFitButton;
        private ZoneFiveSoftware.Common.Visuals.Button zoomOutButton;
        private ZoneFiveSoftware.Common.Visuals.Panel controlPanel;
        private ZoneFiveSoftware.Common.Visuals.ITheme currentTheme;
        private System.Windows.Forms.Label lblCTL;
        private System.Windows.Forms.Label lblTSB;
        private System.Windows.Forms.Label lblATL;
        private System.Windows.Forms.Label valTSB;
        private System.Windows.Forms.Label valATL;
        private System.Windows.Forms.Label valCTL;
        private System.Windows.Forms.Label lblPerf;
        private System.Windows.Forms.Label lblFatigue;
        private System.Windows.Forms.Label lblFitness;
        private System.Windows.Forms.Label lblTaperDate;
        private System.Windows.Forms.Label lblMaxEffect;
        private System.Windows.Forms.Label lblTargetDate;
        private System.Windows.Forms.Label valTaperDate;
        private System.Windows.Forms.Label valMaxEffect;
        private System.Windows.Forms.Label valTargetDate;
        private System.Windows.Forms.Panel pnlHRchart;
        private ZoneFiveSoftware.Common.Visuals.Chart.ChartBase chartStatus;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTCc;
        private System.Windows.Forms.TextBox txtTCa;
        private System.Windows.Forms.ContextMenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuItemTrimp;
        private System.Windows.Forms.ToolStripMenuItem mnuItemTSS;
        private System.Windows.Forms.SplitContainer headerSplitContainer2;
        internal ZoneFiveSoftware.Common.Visuals.TreeList treeActivity;
        private System.Windows.Forms.ContextMenuStrip mnuRecalculate;
        private System.Windows.Forms.ToolStripMenuItem mnuItemDaniels;
        private ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl MainChart;
        private System.Windows.Forms.ToolTip chartToolTip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuItemMultisportEnable;
        private System.Windows.Forms.ToolStripMenuItem mnuItemStackCTL;
        private System.Windows.Forms.ToolStripMenuItem mnuItemShowFitPlan;
    }
}
