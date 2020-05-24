// <copyright file="SettingsPageControl.Designer.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.Settings
{
    partial class SettingsPageControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsPageControl));
            this.txtTCa = new System.Windows.Forms.TextBox();
            this.lblATL = new System.Windows.Forms.Label();
            this.txtTCc = new System.Windows.Forms.TextBox();
            this.lblCTL = new System.Windows.Forms.Label();
            this.grpTimeConst = new System.Windows.Forms.GroupBox();
            this.lblCTLInit = new System.Windows.Forms.Label();
            this.lblATLInit = new System.Windows.Forms.Label();
            this.txtInitialCTL = new System.Windows.Forms.TextBox();
            this.txtInitialATL = new System.Windows.Forms.TextBox();
            this.lblSum2 = new System.Windows.Forms.Label();
            this.lblDays2 = new System.Windows.Forms.Label();
            this.lblSum1 = new System.Windows.Forms.Label();
            this.lblDays1 = new System.Windows.Forms.Label();
            this.txtSum2 = new System.Windows.Forms.TextBox();
            this.txtSum1 = new System.Windows.Forms.TextBox();
            this.grpHRZones = new System.Windows.Forms.GroupBox();
            this.pnlMidRight = new System.Windows.Forms.Panel();
            this.btnCatReset = new ZoneFiveSoftware.Common.Visuals.Button();
            this.chkSingleZone = new System.Windows.Forms.CheckBox();
            this.lblFactor = new System.Windows.Forms.Label();
            this.chkDynamicZones = new System.Windows.Forms.CheckBox();
            this.cboCategories = new System.Windows.Forms.ComboBox();
            this.txtFactor = new System.Windows.Forms.TextBox();
            this.treelistHRCats = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.treelistHRZones = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.chartFactor = new ZoneFiveSoftware.Common.Visuals.Chart.ChartBase();
            this.toolTipSmooth = new System.Windows.Forms.ToolTip(this.components);
            this.btnResetDef = new ZoneFiveSoftware.Common.Visuals.Button();
            this.chkForecast = new System.Windows.Forms.CheckBox();
            this.toolTipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTopRight = new System.Windows.Forms.Panel();
            this.grpCustomParams = new System.Windows.Forms.GroupBox();
            this.chkCusTSS = new System.Windows.Forms.CheckBox();
            this.chkCusFTPcycle = new System.Windows.Forms.CheckBox();
            this.chkCusTrimp = new System.Windows.Forms.CheckBox();
            this.chkCusNP = new System.Windows.Forms.CheckBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.txtPast = new System.Windows.Forms.TextBox();
            this.lblDefaultView = new System.Windows.Forms.Label();
            this.lblPast = new System.Windows.Forms.Label();
            this.chkFilterCharts = new System.Windows.Forms.CheckBox();
            this.txtFuture = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFuture = new System.Windows.Forms.Label();
            this.pnlTopLeft = new System.Windows.Forms.Panel();
            this.grpRolling = new System.Windows.Forms.GroupBox();
            this.cboRoll2 = new System.Windows.Forms.ComboBox();
            this.cboRoll1 = new System.Windows.Forms.ComboBox();
            this.grpTimeConst.SuspendLayout();
            this.grpHRZones.SuspendLayout();
            this.pnlMidRight.SuspendLayout();
            this.pnlTopRight.SuspendLayout();
            this.grpCustomParams.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.pnlTopLeft.SuspendLayout();
            this.grpRolling.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTCa
            // 
            this.txtTCa.Location = new System.Drawing.Point(39, 17);
            this.txtTCa.Name = "txtTCa";
            this.txtTCa.Size = new System.Drawing.Size(50, 20);
            this.txtTCa.TabIndex = 1;
            this.txtTCa.Text = "15";
            this.txtTCa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTipHelp.SetToolTip(this.txtTCa, resources.GetString("txtTCa.ToolTip"));
            this.txtTCa.TextChanged += new System.EventHandler(this.txtTCa_TextChanged);
            this.txtTCa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitValidator);
            // 
            // lblATL
            // 
            this.lblATL.AutoSize = true;
            this.lblATL.Location = new System.Drawing.Point(6, 20);
            this.lblATL.Name = "lblATL";
            this.lblATL.Size = new System.Drawing.Size(27, 13);
            this.lblATL.TabIndex = 2;
            this.lblATL.Text = "ATL";
            this.toolTipHelp.SetToolTip(this.lblATL, "Acute Training Load");
            // 
            // txtTCc
            // 
            this.txtTCc.Location = new System.Drawing.Point(39, 43);
            this.txtTCc.Name = "txtTCc";
            this.txtTCc.Size = new System.Drawing.Size(50, 20);
            this.txtTCc.TabIndex = 2;
            this.txtTCc.Text = "42";
            this.txtTCc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTipHelp.SetToolTip(this.txtTCc, resources.GetString("txtTCc.ToolTip"));
            this.txtTCc.TextChanged += new System.EventHandler(this.txtTCc_TextChanged);
            this.txtTCc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitValidator);
            // 
            // lblCTL
            // 
            this.lblCTL.AutoSize = true;
            this.lblCTL.Location = new System.Drawing.Point(6, 46);
            this.lblCTL.Name = "lblCTL";
            this.lblCTL.Size = new System.Drawing.Size(27, 13);
            this.lblCTL.TabIndex = 2;
            this.lblCTL.Text = "CTL";
            this.toolTipHelp.SetToolTip(this.lblCTL, "Chronic Training Load");
            // 
            // grpTimeConst
            // 
            this.grpTimeConst.Controls.Add(this.lblCTL);
            this.grpTimeConst.Controls.Add(this.lblCTLInit);
            this.grpTimeConst.Controls.Add(this.lblATLInit);
            this.grpTimeConst.Controls.Add(this.lblATL);
            this.grpTimeConst.Controls.Add(this.txtTCc);
            this.grpTimeConst.Controls.Add(this.txtInitialCTL);
            this.grpTimeConst.Controls.Add(this.txtInitialATL);
            this.grpTimeConst.Controls.Add(this.txtTCa);
            this.grpTimeConst.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTimeConst.Location = new System.Drawing.Point(0, 0);
            this.grpTimeConst.MinimumSize = new System.Drawing.Size(249, 68);
            this.grpTimeConst.Name = "grpTimeConst";
            this.grpTimeConst.Size = new System.Drawing.Size(249, 68);
            this.grpTimeConst.TabIndex = 3;
            this.grpTimeConst.TabStop = false;
            this.grpTimeConst.Text = "Time Constants";
            // 
            // lblCTLInit
            // 
            this.lblCTLInit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCTLInit.Location = new System.Drawing.Point(95, 46);
            this.lblCTLInit.Name = "lblCTLInit";
            this.lblCTLInit.Size = new System.Drawing.Size(91, 13);
            this.lblCTLInit.TabIndex = 2;
            this.lblCTLInit.Text = "Initial CTL";
            this.lblCTLInit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblATLInit
            // 
            this.lblATLInit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblATLInit.Location = new System.Drawing.Point(95, 20);
            this.lblATLInit.Name = "lblATLInit";
            this.lblATLInit.Size = new System.Drawing.Size(91, 13);
            this.lblATLInit.TabIndex = 2;
            this.lblATLInit.Text = "Initial ATL";
            this.lblATLInit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInitialCTL
            // 
            this.txtInitialCTL.Location = new System.Drawing.Point(187, 43);
            this.txtInitialCTL.Name = "txtInitialCTL";
            this.txtInitialCTL.Size = new System.Drawing.Size(50, 20);
            this.txtInitialCTL.TabIndex = 4;
            this.txtInitialCTL.Text = "0";
            this.txtInitialCTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTipHelp.SetToolTip(this.txtInitialCTL, "Starting point for CTL moving average");
            this.txtInitialCTL.TextChanged += new System.EventHandler(this.txtInitCTL_TextChanged);
            this.txtInitialCTL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitValidator);
            // 
            // txtInitialATL
            // 
            this.txtInitialATL.Location = new System.Drawing.Point(187, 17);
            this.txtInitialATL.Name = "txtInitialATL";
            this.txtInitialATL.Size = new System.Drawing.Size(50, 20);
            this.txtInitialATL.TabIndex = 3;
            this.txtInitialATL.Text = "0";
            this.txtInitialATL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTipHelp.SetToolTip(this.txtInitialATL, "Starting point for ATL moving average");
            this.txtInitialATL.TextChanged += new System.EventHandler(this.txtInitATL_TextChanged);
            this.txtInitialATL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitValidator);
            // 
            // lblSum2
            // 
            this.lblSum2.AutoSize = true;
            this.lblSum2.Location = new System.Drawing.Point(124, 16);
            this.lblSum2.Name = "lblSum2";
            this.lblSum2.Size = new System.Drawing.Size(41, 13);
            this.lblSum2.TabIndex = 2;
            this.lblSum2.Text = "Chart 2";
            this.lblSum2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDays2
            // 
            this.lblDays2.AutoSize = true;
            this.lblDays2.Location = new System.Drawing.Point(183, 35);
            this.lblDays2.Name = "lblDays2";
            this.lblDays2.Size = new System.Drawing.Size(29, 13);
            this.lblDays2.TabIndex = 2;
            this.lblDays2.Text = "days";
            // 
            // lblSum1
            // 
            this.lblSum1.AutoSize = true;
            this.lblSum1.Location = new System.Drawing.Point(6, 16);
            this.lblSum1.Name = "lblSum1";
            this.lblSum1.Size = new System.Drawing.Size(41, 13);
            this.lblSum1.TabIndex = 2;
            this.lblSum1.Text = "Chart 1";
            this.lblSum1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDays1
            // 
            this.lblDays1.AutoSize = true;
            this.lblDays1.Location = new System.Drawing.Point(65, 35);
            this.lblDays1.Name = "lblDays1";
            this.lblDays1.Size = new System.Drawing.Size(29, 13);
            this.lblDays1.TabIndex = 2;
            this.lblDays1.Text = "days";
            // 
            // txtSum2
            // 
            this.txtSum2.Location = new System.Drawing.Point(127, 32);
            this.txtSum2.Name = "txtSum2";
            this.txtSum2.Size = new System.Drawing.Size(50, 20);
            this.txtSum2.TabIndex = 2;
            this.txtSum2.Text = "28";
            this.txtSum2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSum2.TextChanged += new System.EventHandler(this.txtSum2_TextChanged);
            this.txtSum2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitValidator);
            // 
            // txtSum1
            // 
            this.txtSum1.Location = new System.Drawing.Point(9, 32);
            this.txtSum1.Name = "txtSum1";
            this.txtSum1.Size = new System.Drawing.Size(50, 20);
            this.txtSum1.TabIndex = 1;
            this.txtSum1.Text = "7";
            this.txtSum1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSum1.TextChanged += new System.EventHandler(this.txtSum1_TextChanged);
            this.txtSum1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitValidator);
            // 
            // grpHRZones
            // 
            this.grpHRZones.Controls.Add(this.pnlMidRight);
            this.grpHRZones.Controls.Add(this.treelistHRCats);
            this.grpHRZones.Controls.Add(this.treelistHRZones);
            this.grpHRZones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpHRZones.Location = new System.Drawing.Point(0, 163);
            this.grpHRZones.Name = "grpHRZones";
            this.grpHRZones.Size = new System.Drawing.Size(600, 337);
            this.grpHRZones.TabIndex = 4;
            this.grpHRZones.TabStop = false;
            this.grpHRZones.Text = "HR Zones";
            // 
            // pnlMidRight
            // 
            this.pnlMidRight.Controls.Add(this.btnCatReset);
            this.pnlMidRight.Controls.Add(this.chkSingleZone);
            this.pnlMidRight.Controls.Add(this.lblFactor);
            this.pnlMidRight.Controls.Add(this.chkDynamicZones);
            this.pnlMidRight.Controls.Add(this.cboCategories);
            this.pnlMidRight.Controls.Add(this.txtFactor);
            this.pnlMidRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMidRight.Location = new System.Drawing.Point(249, 16);
            this.pnlMidRight.MinimumSize = new System.Drawing.Size(214, 100);
            this.pnlMidRight.Name = "pnlMidRight";
            this.pnlMidRight.Size = new System.Drawing.Size(348, 131);
            this.pnlMidRight.TabIndex = 20;
            // 
            // btnCatReset
            // 
            this.btnCatReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCatReset.BackColor = System.Drawing.Color.Transparent;
            this.btnCatReset.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnCatReset.CenterImage = null;
            this.btnCatReset.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCatReset.HyperlinkStyle = false;
            this.btnCatReset.ImageMargin = 2;
            this.btnCatReset.LeftImage = null;
            this.btnCatReset.Location = new System.Drawing.Point(57, 105);
            this.btnCatReset.Name = "btnCatReset";
            this.btnCatReset.PushStyle = true;
            this.btnCatReset.RightImage = null;
            this.btnCatReset.Size = new System.Drawing.Size(149, 23);
            this.btnCatReset.TabIndex = 15;
            this.btnCatReset.Text = "Reset Category";
            this.btnCatReset.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnCatReset.TextLeftMargin = 2;
            this.btnCatReset.TextRightMargin = 2;
            this.toolTipHelp.SetToolTip(this.btnCatReset, "Reset HR zone factors to default.");
            this.btnCatReset.Click += new System.EventHandler(this.btnCatReset_Click);
            // 
            // chkSingleZone
            // 
            this.chkSingleZone.AutoSize = true;
            this.chkSingleZone.Location = new System.Drawing.Point(17, 26);
            this.chkSingleZone.Name = "chkSingleZone";
            this.chkSingleZone.Size = new System.Drawing.Size(83, 17);
            this.chkSingleZone.TabIndex = 13;
            this.chkSingleZone.Text = "Single Zone";
            this.toolTipHelp.SetToolTip(this.chkSingleZone, "Checked: All TRIMP calcs will use a single HR zone (such as a custom TRIMP HR zon" +
        "e).  \r\nUnchecked: Calcs use activity category & factors.");
            this.chkSingleZone.UseVisualStyleBackColor = true;
            this.chkSingleZone.CheckedChanged += new System.EventHandler(this.chkSingleZone_CheckedChanged);
            // 
            // lblFactor
            // 
            this.lblFactor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFactor.AutoSize = true;
            this.lblFactor.Location = new System.Drawing.Point(14, 108);
            this.lblFactor.Name = "lblFactor";
            this.lblFactor.Size = new System.Drawing.Size(37, 13);
            this.lblFactor.TabIndex = 3;
            this.lblFactor.Text = "Factor";
            // 
            // chkDynamicZones
            // 
            this.chkDynamicZones.AutoSize = true;
            this.chkDynamicZones.Location = new System.Drawing.Point(17, 3);
            this.chkDynamicZones.Name = "chkDynamicZones";
            this.chkDynamicZones.Size = new System.Drawing.Size(103, 17);
            this.chkDynamicZones.TabIndex = 12;
            this.chkDynamicZones.Text = "Automatic Mode";
            this.toolTipHelp.SetToolTip(this.chkDynamicZones, resources.GetString("chkDynamicZones.ToolTip"));
            this.chkDynamicZones.UseVisualStyleBackColor = true;
            this.chkDynamicZones.CheckedChanged += new System.EventHandler(this.chkDynamicZones_CheckedChanged);
            // 
            // cboCategories
            // 
            this.cboCategories.FormattingEnabled = true;
            this.cboCategories.Location = new System.Drawing.Point(18, 49);
            this.cboCategories.Name = "cboCategories";
            this.cboCategories.Size = new System.Drawing.Size(126, 21);
            this.cboCategories.TabIndex = 14;
            this.toolTipHelp.SetToolTip(this.cboCategories, "The single heart rate zone to use for all activity TRIMP calculations.");
            this.cboCategories.SelectedIndexChanged += new System.EventHandler(this.cboCategories_SelectedIndexChanged);
            // 
            // txtFactor
            // 
            this.txtFactor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFactor.Location = new System.Drawing.Point(57, 105);
            this.txtFactor.Name = "txtFactor";
            this.txtFactor.Size = new System.Drawing.Size(149, 20);
            this.txtFactor.TabIndex = 16;
            this.txtFactor.Visible = false;
            this.txtFactor.Leave += new System.EventHandler(this.txtFactor_Leave);
            // 
            // treelistHRCats
            // 
            this.treelistHRCats.BackColor = System.Drawing.Color.Transparent;
            this.treelistHRCats.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treelistHRCats.CheckBoxes = false;
            this.treelistHRCats.DefaultIndent = 15;
            this.treelistHRCats.DefaultRowHeight = -1;
            this.treelistHRCats.Dock = System.Windows.Forms.DockStyle.Left;
            this.treelistHRCats.HeaderRowHeight = 21;
            this.treelistHRCats.Location = new System.Drawing.Point(3, 16);
            this.treelistHRCats.MultiSelect = false;
            this.treelistHRCats.Name = "treelistHRCats";
            this.treelistHRCats.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treelistHRCats.NumLockedColumns = 0;
            this.treelistHRCats.RowAlternatingColors = true;
            this.treelistHRCats.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(36)))), ((int)(((byte)(106)))));
            this.treelistHRCats.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treelistHRCats.RowHotlightMouse = true;
            this.treelistHRCats.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treelistHRCats.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treelistHRCats.RowSeparatorLines = true;
            this.treelistHRCats.ShowLines = false;
            this.treelistHRCats.ShowPlusMinus = false;
            this.treelistHRCats.Size = new System.Drawing.Size(246, 131);
            this.treelistHRCats.TabIndex = 17;
            this.treelistHRCats.SelectedItemsChanged += new System.EventHandler(this.treelistHRCats_SelectedChanged);
            this.treelistHRCats.EnabledChanged += new System.EventHandler(this.treelist_EnabledChanged);
            this.treelistHRCats.Click += new System.EventHandler(this.treelistHRCats_Click);
            // 
            // treelistHRZones
            // 
            this.treelistHRZones.BackColor = System.Drawing.Color.Transparent;
            this.treelistHRZones.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treelistHRZones.CheckBoxes = false;
            this.treelistHRZones.DefaultIndent = 15;
            this.treelistHRZones.DefaultRowHeight = -1;
            this.treelistHRZones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.treelistHRZones.HeaderRowHeight = 21;
            this.treelistHRZones.Location = new System.Drawing.Point(3, 147);
            this.treelistHRZones.MultiSelect = false;
            this.treelistHRZones.Name = "treelistHRZones";
            this.treelistHRZones.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treelistHRZones.NumLockedColumns = 0;
            this.treelistHRZones.RowAlternatingColors = true;
            this.treelistHRZones.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(36)))), ((int)(((byte)(106)))));
            this.treelistHRZones.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treelistHRZones.RowHotlightMouse = true;
            this.treelistHRZones.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treelistHRZones.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treelistHRZones.RowSeparatorLines = true;
            this.treelistHRZones.ShowLines = false;
            this.treelistHRZones.ShowPlusMinus = false;
            this.treelistHRZones.Size = new System.Drawing.Size(594, 187);
            this.treelistHRZones.TabIndex = 19;
            this.treelistHRZones.SelectedItemsChanged += new System.EventHandler(this.treelistHRZones_SelectedChanged);
            this.treelistHRZones.Click += new System.EventHandler(this.treelistHRZones_Click);
            // 
            // chartFactor
            // 
            this.chartFactor.BackColor = System.Drawing.Color.Transparent;
            this.chartFactor.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.chartFactor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chartFactor.Location = new System.Drawing.Point(0, 500);
            this.chartFactor.Name = "chartFactor";
            this.chartFactor.Padding = new System.Windows.Forms.Padding(5);
            this.chartFactor.Size = new System.Drawing.Size(600, 160);
            this.chartFactor.TabIndex = 6;
            this.chartFactor.TabStop = false;
            // 
            // toolTipSmooth
            // 
            this.toolTipSmooth.AutoPopDelay = 3000;
            this.toolTipSmooth.InitialDelay = 0;
            this.toolTipSmooth.ReshowDelay = 100;
            // 
            // btnResetDef
            // 
            this.btnResetDef.BackColor = System.Drawing.Color.Transparent;
            this.btnResetDef.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnResetDef.CenterImage = null;
            this.btnResetDef.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnResetDef.HyperlinkStyle = false;
            this.btnResetDef.ImageMargin = 2;
            this.btnResetDef.LeftImage = null;
            this.btnResetDef.Location = new System.Drawing.Point(132, 0);
            this.btnResetDef.Name = "btnResetDef";
            this.btnResetDef.PushStyle = true;
            this.btnResetDef.RightImage = null;
            this.btnResetDef.Size = new System.Drawing.Size(159, 33);
            this.btnResetDef.TabIndex = 8;
            this.btnResetDef.Text = "Reset Defaults...";
            this.btnResetDef.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnResetDef.TextLeftMargin = 2;
            this.btnResetDef.TextRightMargin = 2;
            this.btnResetDef.Click += new System.EventHandler(this.btnResetDef_Click);
            // 
            // chkForecast
            // 
            this.chkForecast.AutoSize = true;
            this.chkForecast.Location = new System.Drawing.Point(11, 50);
            this.chkForecast.Name = "chkForecast";
            this.chkForecast.Size = new System.Drawing.Size(141, 17);
            this.chkForecast.TabIndex = 10;
            this.chkForecast.Text = "Forecast ATL/CTL/TSB";
            this.toolTipHelp.SetToolTip(this.chkForecast, "Extend CTL, ATL, & TSB 7 days past TSB peak.");
            this.chkForecast.UseVisualStyleBackColor = true;
            this.chkForecast.CheckedChanged += new System.EventHandler(this.chkForecast_CheckedChanged);
            // 
            // toolTipHelp
            // 
            this.toolTipHelp.AutoPopDelay = 10000;
            this.toolTipHelp.InitialDelay = 500;
            this.toolTipHelp.ReshowDelay = 100;
            // 
            // pnlTopRight
            // 
            this.pnlTopRight.Controls.Add(this.grpCustomParams);
            this.pnlTopRight.Controls.Add(this.grpOptions);
            this.pnlTopRight.Controls.Add(this.btnResetDef);
            this.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopRight.Location = new System.Drawing.Point(249, 0);
            this.pnlTopRight.MinimumSize = new System.Drawing.Size(251, 157);
            this.pnlTopRight.Name = "pnlTopRight";
            this.pnlTopRight.Size = new System.Drawing.Size(351, 163);
            this.pnlTopRight.TabIndex = 11;
            // 
            // grpCustomParams
            // 
            this.grpCustomParams.Controls.Add(this.chkCusTSS);
            this.grpCustomParams.Controls.Add(this.chkCusFTPcycle);
            this.grpCustomParams.Controls.Add(this.chkCusTrimp);
            this.grpCustomParams.Controls.Add(this.chkCusNP);
            this.grpCustomParams.Location = new System.Drawing.Point(206, 43);
            this.grpCustomParams.Name = "grpCustomParams";
            this.grpCustomParams.Size = new System.Drawing.Size(142, 120);
            this.grpCustomParams.TabIndex = 21;
            this.grpCustomParams.TabStop = false;
            this.grpCustomParams.Text = "Custom Fields";
            // 
            // chkCusTSS
            // 
            this.chkCusTSS.AutoSize = true;
            this.chkCusTSS.Checked = true;
            this.chkCusTSS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCusTSS.Location = new System.Drawing.Point(6, 28);
            this.chkCusTSS.Name = "chkCusTSS";
            this.chkCusTSS.Size = new System.Drawing.Size(47, 17);
            this.chkCusTSS.TabIndex = 10;
            this.chkCusTSS.Tag = "TSS";
            this.chkCusTSS.Text = "TSS";
            this.chkCusTSS.UseVisualStyleBackColor = true;
            this.chkCusTSS.CheckedChanged += new System.EventHandler(this.chkEnableCustomParam_CheckedChanged);
            // 
            // chkCusFTPcycle
            // 
            this.chkCusFTPcycle.AutoSize = true;
            this.chkCusFTPcycle.Checked = true;
            this.chkCusFTPcycle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCusFTPcycle.Location = new System.Drawing.Point(6, 96);
            this.chkCusFTPcycle.Name = "chkCusFTPcycle";
            this.chkCusFTPcycle.Size = new System.Drawing.Size(100, 17);
            this.chkCusFTPcycle.TabIndex = 10;
            this.chkCusFTPcycle.Tag = "FTPcycle";
            this.chkCusFTPcycle.Text = "FTP (cycle/run)";
            this.chkCusFTPcycle.UseVisualStyleBackColor = true;
            this.chkCusFTPcycle.CheckedChanged += new System.EventHandler(this.chkEnableCustomParam_CheckedChanged);
            // 
            // chkCusTrimp
            // 
            this.chkCusTrimp.AutoSize = true;
            this.chkCusTrimp.Checked = true;
            this.chkCusTrimp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCusTrimp.Location = new System.Drawing.Point(6, 50);
            this.chkCusTrimp.Name = "chkCusTrimp";
            this.chkCusTrimp.Size = new System.Drawing.Size(57, 17);
            this.chkCusTrimp.TabIndex = 10;
            this.chkCusTrimp.Tag = "TRIMP";
            this.chkCusTrimp.Text = "TRMP";
            this.chkCusTrimp.UseVisualStyleBackColor = true;
            this.chkCusTrimp.CheckedChanged += new System.EventHandler(this.chkEnableCustomParam_CheckedChanged);
            // 
            // chkCusNP
            // 
            this.chkCusNP.AutoSize = true;
            this.chkCusNP.Checked = true;
            this.chkCusNP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCusNP.Location = new System.Drawing.Point(6, 73);
            this.chkCusNP.Name = "chkCusNP";
            this.chkCusNP.Size = new System.Drawing.Size(78, 17);
            this.chkCusNP.TabIndex = 10;
            this.chkCusNP.Tag = "NormPwr";
            this.chkCusNP.Text = "Norm. Pwr.";
            this.chkCusNP.UseVisualStyleBackColor = true;
            this.chkCusNP.CheckedChanged += new System.EventHandler(this.chkEnableCustomParam_CheckedChanged);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.txtPast);
            this.grpOptions.Controls.Add(this.lblDefaultView);
            this.grpOptions.Controls.Add(this.lblPast);
            this.grpOptions.Controls.Add(this.chkFilterCharts);
            this.grpOptions.Controls.Add(this.txtFuture);
            this.grpOptions.Controls.Add(this.chkForecast);
            this.grpOptions.Controls.Add(this.label2);
            this.grpOptions.Controls.Add(this.lblFuture);
            this.grpOptions.Location = new System.Drawing.Point(6, 43);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(194, 120);
            this.grpOptions.TabIndex = 16;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // txtPast
            // 
            this.txtPast.Location = new System.Drawing.Point(58, 90);
            this.txtPast.Name = "txtPast";
            this.txtPast.Size = new System.Drawing.Size(26, 20);
            this.txtPast.TabIndex = 1;
            this.txtPast.Text = "120";
            this.txtPast.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPast.TextChanged += new System.EventHandler(this.txtPast_TextChanged);
            this.txtPast.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitValidator);
            // 
            // lblDefaultView
            // 
            this.lblDefaultView.AutoSize = true;
            this.lblDefaultView.Location = new System.Drawing.Point(8, 74);
            this.lblDefaultView.Name = "lblDefaultView";
            this.lblDefaultView.Size = new System.Drawing.Size(133, 13);
            this.lblDefaultView.TabIndex = 2;
            this.lblDefaultView.Text = "Default Chart Zoom (days):";
            this.lblDefaultView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPast
            // 
            this.lblPast.Location = new System.Drawing.Point(9, 93);
            this.lblPast.Name = "lblPast";
            this.lblPast.Size = new System.Drawing.Size(43, 13);
            this.lblPast.TabIndex = 2;
            this.lblPast.Text = "Past";
            this.lblPast.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFilterCharts
            // 
            this.chkFilterCharts.AutoSize = true;
            this.chkFilterCharts.Location = new System.Drawing.Point(11, 28);
            this.chkFilterCharts.Name = "chkFilterCharts";
            this.chkFilterCharts.Size = new System.Drawing.Size(81, 17);
            this.chkFilterCharts.TabIndex = 10;
            this.chkFilterCharts.Text = "Filter Charts";
            this.chkFilterCharts.UseVisualStyleBackColor = true;
            this.chkFilterCharts.CheckedChanged += new System.EventHandler(this.chkFilterCharts_CheckedChanged);
            // 
            // txtFuture
            // 
            this.txtFuture.Location = new System.Drawing.Point(162, 90);
            this.txtFuture.Name = "txtFuture";
            this.txtFuture.Size = new System.Drawing.Size(26, 20);
            this.txtFuture.TabIndex = 2;
            this.txtFuture.Text = "0";
            this.txtFuture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFuture.TextChanged += new System.EventHandler(this.txtFuture_TextChanged);
            this.txtFuture.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitValidator);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(352, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "days";
            // 
            // lblFuture
            // 
            this.lblFuture.Location = new System.Drawing.Point(90, 93);
            this.lblFuture.Name = "lblFuture";
            this.lblFuture.Size = new System.Drawing.Size(66, 13);
            this.lblFuture.TabIndex = 2;
            this.lblFuture.Text = "Future";
            this.lblFuture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlTopLeft
            // 
            this.pnlTopLeft.Controls.Add(this.grpRolling);
            this.pnlTopLeft.Controls.Add(this.grpTimeConst);
            this.pnlTopLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTopLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlTopLeft.MinimumSize = new System.Drawing.Size(249, 157);
            this.pnlTopLeft.Name = "pnlTopLeft";
            this.pnlTopLeft.Size = new System.Drawing.Size(249, 163);
            this.pnlTopLeft.TabIndex = 5;
            // 
            // grpRolling
            // 
            this.grpRolling.Controls.Add(this.cboRoll2);
            this.grpRolling.Controls.Add(this.cboRoll1);
            this.grpRolling.Controls.Add(this.lblSum1);
            this.grpRolling.Controls.Add(this.txtSum1);
            this.grpRolling.Controls.Add(this.lblDays2);
            this.grpRolling.Controls.Add(this.lblDays1);
            this.grpRolling.Controls.Add(this.txtSum2);
            this.grpRolling.Controls.Add(this.lblSum2);
            this.grpRolling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRolling.Location = new System.Drawing.Point(0, 68);
            this.grpRolling.MinimumSize = new System.Drawing.Size(249, 90);
            this.grpRolling.Name = "grpRolling";
            this.grpRolling.Size = new System.Drawing.Size(249, 95);
            this.grpRolling.TabIndex = 21;
            this.grpRolling.TabStop = false;
            this.grpRolling.Text = "Rolling Sums";
            // 
            // cboRoll2
            // 
            this.cboRoll2.FormattingEnabled = true;
            this.cboRoll2.Items.AddRange(new object[] {
            "Trimp",
            "Distance",
            "Time"});
            this.cboRoll2.Location = new System.Drawing.Point(127, 58);
            this.cboRoll2.Name = "cboRoll2";
            this.cboRoll2.Size = new System.Drawing.Size(110, 21);
            this.cboRoll2.TabIndex = 14;
            this.cboRoll2.SelectedIndexChanged += new System.EventHandler(this.cboRoll2_SelectedIndexChanged);
            // 
            // cboRoll1
            // 
            this.cboRoll1.FormattingEnabled = true;
            this.cboRoll1.Items.AddRange(new object[] {
            "Trimp",
            "Distance",
            "Time"});
            this.cboRoll1.Location = new System.Drawing.Point(9, 58);
            this.cboRoll1.Name = "cboRoll1";
            this.cboRoll1.Size = new System.Drawing.Size(110, 21);
            this.cboRoll1.TabIndex = 14;
            this.cboRoll1.SelectedIndexChanged += new System.EventHandler(this.cboRoll1_SelectedIndexChanged);
            // 
            // SettingsPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTopRight);
            this.Controls.Add(this.pnlTopLeft);
            this.Controls.Add(this.grpHRZones);
            this.Controls.Add(this.chartFactor);
            this.MaximumSize = new System.Drawing.Size(600, 660);
            this.MinimumSize = new System.Drawing.Size(600, 660);
            this.Name = "SettingsPageControl";
            this.Size = new System.Drawing.Size(600, 660);
            this.grpTimeConst.ResumeLayout(false);
            this.grpTimeConst.PerformLayout();
            this.grpHRZones.ResumeLayout(false);
            this.pnlMidRight.ResumeLayout(false);
            this.pnlMidRight.PerformLayout();
            this.pnlTopRight.ResumeLayout(false);
            this.grpCustomParams.ResumeLayout(false);
            this.grpCustomParams.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.pnlTopLeft.ResumeLayout(false);
            this.grpRolling.ResumeLayout(false);
            this.grpRolling.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtTCa;
        private System.Windows.Forms.Label lblATL;
        private System.Windows.Forms.TextBox txtTCc;
        private System.Windows.Forms.Label lblCTL;
        private System.Windows.Forms.GroupBox grpTimeConst;
        private System.Windows.Forms.GroupBox grpHRZones;
        private ZoneFiveSoftware.Common.Visuals.TreeList treelistHRZones;
        private ZoneFiveSoftware.Common.Visuals.TreeList treelistHRCats;
        private System.Windows.Forms.CheckBox chkSingleZone;
        private System.Windows.Forms.ComboBox cboCategories;
        private System.Windows.Forms.TextBox txtFactor;
        private System.Windows.Forms.Label lblFactor;
        private ZoneFiveSoftware.Common.Visuals.Chart.ChartBase chartFactor;
        private System.Windows.Forms.ToolTip toolTipSmooth;
        private System.Windows.Forms.Label lblCTLInit;
        private System.Windows.Forms.Label lblATLInit;
        private System.Windows.Forms.TextBox txtInitialCTL;
        private System.Windows.Forms.TextBox txtInitialATL;
        private ZoneFiveSoftware.Common.Visuals.Button btnCatReset;
        private ZoneFiveSoftware.Common.Visuals.Button btnResetDef;
        private System.Windows.Forms.CheckBox chkForecast;
        private System.Windows.Forms.CheckBox chkDynamicZones;
        private System.Windows.Forms.Panel pnlMidRight;
        private System.Windows.Forms.ToolTip toolTipHelp;
        private System.Windows.Forms.Panel pnlTopRight;
        private System.Windows.Forms.CheckBox chkFilterCharts;
        private System.Windows.Forms.Label lblSum2;
        private System.Windows.Forms.Label lblSum1;
        private System.Windows.Forms.TextBox txtSum2;
        private System.Windows.Forms.TextBox txtSum1;
        private System.Windows.Forms.Label lblDays2;
        private System.Windows.Forms.Label lblDays1;
        private System.Windows.Forms.Panel pnlTopLeft;
        private System.Windows.Forms.GroupBox grpRolling;
        private System.Windows.Forms.ComboBox cboRoll1;
        private System.Windows.Forms.ComboBox cboRoll2;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Label lblPast;
        private System.Windows.Forms.TextBox txtFuture;
        private System.Windows.Forms.Label lblFuture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPast;
        private System.Windows.Forms.Label lblDefaultView;
        private System.Windows.Forms.CheckBox chkCusTSS;
        private System.Windows.Forms.CheckBox chkCusNP;
        private System.Windows.Forms.CheckBox chkCusTrimp;
        private System.Windows.Forms.CheckBox chkCusFTPcycle;
        private System.Windows.Forms.GroupBox grpCustomParams;
    }
}
