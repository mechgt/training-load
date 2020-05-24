namespace TrainingLoad.UI.DetailPage
{
    partial class MeanMaxDetail
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
            this.zedChart = new ZedGraph.ZedGraphControl();
            this.ZoomInButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.ChartBanner = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.MaximizeButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.ZoomOutButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.ZoomChartButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.SaveImageButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.ButtonPanel = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.ExportButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.RefreshButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.ExtraChartsButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.panelMain = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.mnuDetail = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.heartRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChartBanner.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.mnuDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // zedChart
            // 
            this.zedChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedChart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zedChart.IsShowPointValues = true;
            this.zedChart.IsZoomOnMouseCenter = true;
            this.zedChart.Location = new System.Drawing.Point(1, 47);
            this.zedChart.Name = "zedChart";
            this.zedChart.ScrollGrace = 0;
            this.zedChart.ScrollMaxX = 0;
            this.zedChart.ScrollMaxY = 0;
            this.zedChart.ScrollMaxY2 = 0;
            this.zedChart.ScrollMinX = 0;
            this.zedChart.ScrollMinY = 0;
            this.zedChart.ScrollMinY2 = 0;
            this.zedChart.Size = new System.Drawing.Size(398, 350);
            this.zedChart.TabIndex = 1;
            // 
            // ZoomInButton
            // 
            this.ZoomInButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomInButton.BackColor = System.Drawing.Color.Transparent;
            this.ZoomInButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.ZoomInButton.CenterImage = null;
            this.ZoomInButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ZoomInButton.HyperlinkStyle = false;
            this.ZoomInButton.ImageMargin = 2;
            this.ZoomInButton.LeftImage = null;
            this.ZoomInButton.Location = new System.Drawing.Point(374, 0);
            this.ZoomInButton.Margin = new System.Windows.Forms.Padding(0);
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.PushStyle = true;
            this.ZoomInButton.RightImage = null;
            this.ZoomInButton.Size = new System.Drawing.Size(24, 24);
            this.ZoomInButton.TabIndex = 0;
            this.ZoomInButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.ZoomInButton.TextLeftMargin = 2;
            this.ZoomInButton.TextRightMargin = 2;
            this.ZoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // ChartBanner
            // 
            this.ChartBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ChartBanner.BackColor = System.Drawing.Color.Transparent;
            this.ChartBanner.Controls.Add(this.MaximizeButton);
            this.ChartBanner.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChartBanner.HasMenuButton = true;
            this.ChartBanner.Location = new System.Drawing.Point(0, 0);
            this.ChartBanner.Name = "ChartBanner";
            this.ChartBanner.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ChartBanner.Size = new System.Drawing.Size(400, 24);
            this.ChartBanner.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.ChartBanner.TabIndex = 7;
            this.ChartBanner.Text = "Detail Pane Chart";
            this.ChartBanner.UseStyleFont = true;
            this.ChartBanner.MenuClicked += new System.EventHandler(this.ChartBanner_MenuClicked);
            // 
            // MaximizeButton
            // 
            this.MaximizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaximizeButton.BackColor = System.Drawing.Color.Transparent;
            this.MaximizeButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.MaximizeButton.CenterImage = null;
            this.MaximizeButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.MaximizeButton.HyperlinkStyle = false;
            this.MaximizeButton.ImageMargin = 2;
            this.MaximizeButton.LeftImage = null;
            this.MaximizeButton.Location = new System.Drawing.Point(350, 0);
            this.MaximizeButton.Margin = new System.Windows.Forms.Padding(0);
            this.MaximizeButton.Name = "MaximizeButton";
            this.MaximizeButton.PushStyle = true;
            this.MaximizeButton.RightImage = null;
            this.MaximizeButton.Size = new System.Drawing.Size(24, 24);
            this.MaximizeButton.TabIndex = 1;
            this.MaximizeButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.MaximizeButton.TextLeftMargin = 2;
            this.MaximizeButton.TextRightMargin = 2;
            this.MaximizeButton.Click += new System.EventHandler(this.MaximizeButton_Click);
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomOutButton.BackColor = System.Drawing.Color.Transparent;
            this.ZoomOutButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.ZoomOutButton.CenterImage = null;
            this.ZoomOutButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ZoomOutButton.HyperlinkStyle = false;
            this.ZoomOutButton.ImageMargin = 2;
            this.ZoomOutButton.LeftImage = null;
            this.ZoomOutButton.Location = new System.Drawing.Point(350, 0);
            this.ZoomOutButton.Margin = new System.Windows.Forms.Padding(0);
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.PushStyle = true;
            this.ZoomOutButton.RightImage = null;
            this.ZoomOutButton.Size = new System.Drawing.Size(24, 24);
            this.ZoomOutButton.TabIndex = 0;
            this.ZoomOutButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.ZoomOutButton.TextLeftMargin = 2;
            this.ZoomOutButton.TextRightMargin = 2;
            this.ZoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // ZoomChartButton
            // 
            this.ZoomChartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomChartButton.BackColor = System.Drawing.Color.Transparent;
            this.ZoomChartButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.ZoomChartButton.CenterImage = null;
            this.ZoomChartButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ZoomChartButton.HyperlinkStyle = false;
            this.ZoomChartButton.ImageMargin = 2;
            this.ZoomChartButton.LeftImage = null;
            this.ZoomChartButton.Location = new System.Drawing.Point(326, 0);
            this.ZoomChartButton.Margin = new System.Windows.Forms.Padding(0);
            this.ZoomChartButton.Name = "ZoomChartButton";
            this.ZoomChartButton.PushStyle = true;
            this.ZoomChartButton.RightImage = null;
            this.ZoomChartButton.Size = new System.Drawing.Size(24, 24);
            this.ZoomChartButton.TabIndex = 0;
            this.ZoomChartButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.ZoomChartButton.TextLeftMargin = 2;
            this.ZoomChartButton.TextRightMargin = 2;
            this.ZoomChartButton.Click += new System.EventHandler(this.ZoomChartButton_Click);
            // 
            // SaveImageButton
            // 
            this.SaveImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveImageButton.BackColor = System.Drawing.Color.Transparent;
            this.SaveImageButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.SaveImageButton.CenterImage = null;
            this.SaveImageButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.SaveImageButton.HyperlinkStyle = false;
            this.SaveImageButton.ImageMargin = 2;
            this.SaveImageButton.LeftImage = null;
            this.SaveImageButton.Location = new System.Drawing.Point(302, 0);
            this.SaveImageButton.Margin = new System.Windows.Forms.Padding(0);
            this.SaveImageButton.Name = "SaveImageButton";
            this.SaveImageButton.PushStyle = true;
            this.SaveImageButton.RightImage = null;
            this.SaveImageButton.Size = new System.Drawing.Size(24, 24);
            this.SaveImageButton.TabIndex = 0;
            this.SaveImageButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.SaveImageButton.TextLeftMargin = 2;
            this.SaveImageButton.TextRightMargin = 2;
            this.SaveImageButton.Click += new System.EventHandler(this.SaveImageButton_Click);
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonPanel.BackColor = System.Drawing.Color.Transparent;
            this.ButtonPanel.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.Square;
            this.ButtonPanel.BorderColor = System.Drawing.Color.Gray;
            this.ButtonPanel.Controls.Add(this.ZoomInButton);
            this.ButtonPanel.Controls.Add(this.ZoomOutButton);
            this.ButtonPanel.Controls.Add(this.ZoomChartButton);
            this.ButtonPanel.Controls.Add(this.ExportButton);
            this.ButtonPanel.Controls.Add(this.RefreshButton);
            this.ButtonPanel.Controls.Add(this.ExtraChartsButton);
            this.ButtonPanel.Controls.Add(this.SaveImageButton);
            this.ButtonPanel.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.ButtonPanel.HeadingFont = null;
            this.ButtonPanel.HeadingLeftMargin = 0;
            this.ButtonPanel.HeadingText = null;
            this.ButtonPanel.HeadingTextColor = System.Drawing.Color.Black;
            this.ButtonPanel.HeadingTopMargin = 0;
            this.ButtonPanel.Location = new System.Drawing.Point(0, 23);
            this.ButtonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(400, 24);
            this.ButtonPanel.TabIndex = 6;
            // 
            // ExportButton
            // 
            this.ExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportButton.BackColor = System.Drawing.Color.Transparent;
            this.ExportButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.ExportButton.CenterImage = null;
            this.ExportButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ExportButton.HyperlinkStyle = false;
            this.ExportButton.ImageMargin = 2;
            this.ExportButton.LeftImage = null;
            this.ExportButton.Location = new System.Drawing.Point(278, 0);
            this.ExportButton.Margin = new System.Windows.Forms.Padding(0);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.PushStyle = true;
            this.ExportButton.RightImage = null;
            this.ExportButton.Size = new System.Drawing.Size(24, 24);
            this.ExportButton.TabIndex = 0;
            this.ExportButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.ExportButton.TextLeftMargin = 2;
            this.ExportButton.TextRightMargin = 2;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshButton.BackColor = System.Drawing.Color.Transparent;
            this.RefreshButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.RefreshButton.CenterImage = null;
            this.RefreshButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.RefreshButton.HyperlinkStyle = false;
            this.RefreshButton.ImageMargin = 2;
            this.RefreshButton.LeftImage = null;
            this.RefreshButton.Location = new System.Drawing.Point(254, 0);
            this.RefreshButton.Margin = new System.Windows.Forms.Padding(0);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.PushStyle = true;
            this.RefreshButton.RightImage = null;
            this.RefreshButton.Size = new System.Drawing.Size(24, 24);
            this.RefreshButton.TabIndex = 0;
            this.RefreshButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.RefreshButton.TextLeftMargin = 2;
            this.RefreshButton.TextRightMargin = 2;
            this.RefreshButton.Visible = false;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ExtraChartsButton
            // 
            this.ExtraChartsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExtraChartsButton.BackColor = System.Drawing.Color.Transparent;
            this.ExtraChartsButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.ExtraChartsButton.CenterImage = null;
            this.ExtraChartsButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ExtraChartsButton.HyperlinkStyle = false;
            this.ExtraChartsButton.ImageMargin = 2;
            this.ExtraChartsButton.LeftImage = null;
            this.ExtraChartsButton.Location = new System.Drawing.Point(230, 0);
            this.ExtraChartsButton.Margin = new System.Windows.Forms.Padding(0);
            this.ExtraChartsButton.Name = "ExtraChartsButton";
            this.ExtraChartsButton.PushStyle = true;
            this.ExtraChartsButton.RightImage = null;
            this.ExtraChartsButton.Size = new System.Drawing.Size(24, 24);
            this.ExtraChartsButton.TabIndex = 0;
            this.ExtraChartsButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.ExtraChartsButton.TextLeftMargin = 2;
            this.ExtraChartsButton.TextRightMargin = 2;
            this.ExtraChartsButton.Visible = false;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.Round;
            this.panelMain.BorderColor = System.Drawing.Color.Gray;
            this.panelMain.Controls.Add(this.zedChart);
            this.panelMain.Controls.Add(this.ChartBanner);
            this.panelMain.Controls.Add(this.ButtonPanel);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.panelMain.HeadingFont = null;
            this.panelMain.HeadingLeftMargin = 0;
            this.panelMain.HeadingText = null;
            this.panelMain.HeadingTextColor = System.Drawing.Color.Black;
            this.panelMain.HeadingTopMargin = 0;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(400, 400);
            this.panelMain.TabIndex = 8;
            // 
            // mnuDetail
            // 
            this.mnuDetail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.heartRateToolStripMenuItem,
            this.powerToolStripMenuItem,
            this.cadenceToolStripMenuItem});
            this.mnuDetail.Name = "mnuDetailMenu";
            this.mnuDetail.Size = new System.Drawing.Size(139, 70);
            // 
            // heartRateToolStripMenuItem
            // 
            this.heartRateToolStripMenuItem.Name = "heartRateToolStripMenuItem";
            this.heartRateToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.heartRateToolStripMenuItem.Tag = "HR";
            this.heartRateToolStripMenuItem.Text = "Heart Rate";
            this.heartRateToolStripMenuItem.Click += new System.EventHandler(this.DetailMenuItem_Click);
            // 
            // powerToolStripMenuItem
            // 
            this.powerToolStripMenuItem.Checked = true;
            this.powerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.powerToolStripMenuItem.Name = "powerToolStripMenuItem";
            this.powerToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.powerToolStripMenuItem.Tag = "Power";
            this.powerToolStripMenuItem.Text = "Power";
            this.powerToolStripMenuItem.Click += new System.EventHandler(this.DetailMenuItem_Click);
            // 
            // cadenceToolStripMenuItem
            // 
            this.cadenceToolStripMenuItem.Name = "cadenceToolStripMenuItem";
            this.cadenceToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.cadenceToolStripMenuItem.Tag = "Cadence";
            this.cadenceToolStripMenuItem.Text = "Cadence";
            this.cadenceToolStripMenuItem.Click += new System.EventHandler(this.DetailMenuItem_Click);
            // 
            // MeanMaxDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Name = "MeanMaxDetail";
            this.Size = new System.Drawing.Size(400, 400);
            this.ChartBanner.ResumeLayout(false);
            this.ButtonPanel.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.mnuDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedChart;
        private ZoneFiveSoftware.Common.Visuals.Button ZoomInButton;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner ChartBanner;
        private ZoneFiveSoftware.Common.Visuals.Button ZoomOutButton;
        private ZoneFiveSoftware.Common.Visuals.Button ZoomChartButton;
        private ZoneFiveSoftware.Common.Visuals.Button SaveImageButton;
        private ZoneFiveSoftware.Common.Visuals.Panel ButtonPanel;
        private ZoneFiveSoftware.Common.Visuals.Button ExtraChartsButton;
        private ZoneFiveSoftware.Common.Visuals.Panel panelMain;
        private System.Windows.Forms.ContextMenuStrip mnuDetail;
        private System.Windows.Forms.ToolStripMenuItem heartRateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadenceToolStripMenuItem;
        private ZoneFiveSoftware.Common.Visuals.Button ExportButton;
        private ZoneFiveSoftware.Common.Visuals.Button RefreshButton;
        private ZoneFiveSoftware.Common.Visuals.Button MaximizeButton;
    }
}
