// <copyright file="DataSelectForm.Designer.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.UI.View
{
    using ZoneFiveSoftware.Common.Visuals;

    partial class DataSelectForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSelectForm));
            this.BackPanel = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblAvailable = new System.Windows.Forms.Label();
            this.btnAdd = new ZoneFiveSoftware.Common.Visuals.Button();
            this.unselectedList = new System.Windows.Forms.ListBox();
            this.lblSelected = new System.Windows.Forms.Label();
            this.btnRemove = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnDown = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnUp = new ZoneFiveSoftware.Common.Visuals.Button();
            this.selectedList = new System.Windows.Forms.ListBox();
            this.BottomPanel = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.btnOK = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnCancel = new ZoneFiveSoftware.Common.Visuals.Button();
            this.BackPanel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackPanel
            // 
            this.BackPanel.BackColor = System.Drawing.Color.Transparent;
            this.BackPanel.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.None;
            this.BackPanel.BorderColor = System.Drawing.Color.Gray;
            this.BackPanel.Controls.Add(this.splitContainer1);
            this.BackPanel.Controls.Add(this.BottomPanel);
            this.BackPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackPanel.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.BackPanel.HeadingFont = null;
            this.BackPanel.HeadingLeftMargin = 0;
            this.BackPanel.HeadingText = null;
            this.BackPanel.HeadingTextColor = System.Drawing.Color.Black;
            this.BackPanel.HeadingTopMargin = 3;
            this.BackPanel.Location = new System.Drawing.Point(0, 0);
            this.BackPanel.Name = "BackPanel";
            this.BackPanel.Size = new System.Drawing.Size(492, 366);
            this.BackPanel.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblAvailable);
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel1.Controls.Add(this.unselectedList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblSelected);
            this.splitContainer1.Panel2.Controls.Add(this.btnRemove);
            this.splitContainer1.Panel2.Controls.Add(this.btnDown);
            this.splitContainer1.Panel2.Controls.Add(this.btnUp);
            this.splitContainer1.Panel2.Controls.Add(this.selectedList);
            this.splitContainer1.Size = new System.Drawing.Size(492, 331);
            this.splitContainer1.SplitterDistance = 241;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // lblAvailable
            // 
            this.lblAvailable.AutoSize = true;
            this.lblAvailable.Location = new System.Drawing.Point(12, 9);
            this.lblAvailable.Name = "lblAvailable";
            this.lblAvailable.Size = new System.Drawing.Size(53, 13);
            this.lblAvailable.TabIndex = 2;
            this.lblAvailable.Text = "Available:";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImage = global::TrainingLoad.Resources.Strings.add;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAdd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnAdd.CenterImage = null;
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdd.HyperlinkStyle = false;
            this.btnAdd.ImageMargin = 2;
            this.btnAdd.LeftImage = null;
            this.btnAdd.Location = new System.Drawing.Point(23, 302);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.PushStyle = true;
            this.btnAdd.RightImage = null;
            this.btnAdd.Size = new System.Drawing.Size(120, 20);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.TabStop = false;
            this.btnAdd.Text = "      Add Chart";
            this.btnAdd.TextAlign = System.Drawing.StringAlignment.Near;
            this.btnAdd.TextLeftMargin = 2;
            this.btnAdd.TextRightMargin = 2;
            this.btnAdd.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // unselectedList
            // 
            this.unselectedList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.unselectedList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.unselectedList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.unselectedList.FormattingEnabled = true;
            this.unselectedList.Location = new System.Drawing.Point(12, 25);
            this.unselectedList.Margin = new System.Windows.Forms.Padding(0);
            this.unselectedList.Name = "unselectedList";
            this.unselectedList.Size = new System.Drawing.Size(229, 273);
            this.unselectedList.TabIndex = 0;
            this.unselectedList.TabStop = false;
            this.unselectedList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.UnselectedList_DrawItem);
            this.unselectedList.SelectedIndexChanged += new System.EventHandler(this.UnselectedList_SelectedIndexChanged);
            this.unselectedList.DoubleClick += new System.EventHandler(this.UnselectedList_DoubleClick);
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Location = new System.Drawing.Point(3, 9);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(84, 13);
            this.lblSelected.TabIndex = 3;
            this.lblSelected.Text = "Selected charts:";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.BackColor = System.Drawing.Color.Transparent;
            this.btnRemove.BackgroundImage = global::TrainingLoad.Resources.Strings.x;
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRemove.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnRemove.CenterImage = null;
            this.btnRemove.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRemove.HyperlinkStyle = false;
            this.btnRemove.ImageMargin = 2;
            this.btnRemove.LeftImage = null;
            this.btnRemove.Location = new System.Drawing.Point(64, 302);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.PushStyle = true;
            this.btnRemove.RightImage = null;
            this.btnRemove.Size = new System.Drawing.Size(86, 20);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.TabStop = false;
            this.btnRemove.Text = "      Remove";
            this.btnRemove.TextAlign = System.Drawing.StringAlignment.Near;
            this.btnRemove.TextLeftMargin = 2;
            this.btnRemove.TextRightMargin = 2;
            this.btnRemove.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.BackgroundImage = global::TrainingLoad.Resources.Strings.arrow_down;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnDown.CenterImage = null;
            this.btnDown.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDown.HyperlinkStyle = false;
            this.btnDown.ImageMargin = 2;
            this.btnDown.LeftImage = null;
            this.btnDown.Location = new System.Drawing.Point(38, 302);
            this.btnDown.Name = "btnDown";
            this.btnDown.PushStyle = true;
            this.btnDown.RightImage = null;
            this.btnDown.Size = new System.Drawing.Size(20, 20);
            this.btnDown.TabIndex = 3;
            this.btnDown.TabStop = false;
            this.btnDown.TextAlign = System.Drawing.StringAlignment.Near;
            this.btnDown.TextLeftMargin = 2;
            this.btnDown.TextRightMargin = 2;
            this.btnDown.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.BackgroundImage = global::TrainingLoad.Resources.Strings.arrow_up;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnUp.CenterImage = null;
            this.btnUp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnUp.HyperlinkStyle = false;
            this.btnUp.ImageMargin = 2;
            this.btnUp.LeftImage = null;
            this.btnUp.Location = new System.Drawing.Point(12, 302);
            this.btnUp.Name = "btnUp";
            this.btnUp.PushStyle = true;
            this.btnUp.RightImage = null;
            this.btnUp.Size = new System.Drawing.Size(20, 20);
            this.btnUp.TabIndex = 2;
            this.btnUp.TabStop = false;
            this.btnUp.TextAlign = System.Drawing.StringAlignment.Near;
            this.btnUp.TextLeftMargin = 2;
            this.btnUp.TextRightMargin = 2;
            this.btnUp.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // selectedList
            // 
            this.selectedList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.selectedList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.selectedList.FormattingEnabled = true;
            this.selectedList.Location = new System.Drawing.Point(1, 25);
            this.selectedList.Margin = new System.Windows.Forms.Padding(0);
            this.selectedList.Name = "selectedList";
            this.selectedList.Size = new System.Drawing.Size(229, 273);
            this.selectedList.TabIndex = 1;
            this.selectedList.TabStop = false;
            this.selectedList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.SelectedList_DrawItem);
            this.selectedList.SelectedIndexChanged += new System.EventHandler(this.SelectedList_SelectedIndexChanged);
            this.selectedList.DoubleClick += new System.EventHandler(this.SelectedList_DoubleClick);
            // 
            // BottomPanel
            // 
            this.BottomPanel.BackColor = System.Drawing.Color.Transparent;
            this.BottomPanel.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.Square;
            this.BottomPanel.BorderColor = System.Drawing.Color.Gray;
            this.BottomPanel.Controls.Add(this.btnOK);
            this.BottomPanel.Controls.Add(this.btnCancel);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.BottomPanel.HeadingFont = null;
            this.BottomPanel.HeadingLeftMargin = 0;
            this.BottomPanel.HeadingText = null;
            this.BottomPanel.HeadingTextColor = System.Drawing.Color.Black;
            this.BottomPanel.HeadingTopMargin = 3;
            this.BottomPanel.Location = new System.Drawing.Point(0, 331);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.RightGradientColor = System.Drawing.Color.Black;
            this.BottomPanel.RightGradientPercent = 0.25;
            this.BottomPanel.Size = new System.Drawing.Size(492, 35);
            this.BottomPanel.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnOK.CenterImage = null;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.HyperlinkStyle = false;
            this.btnOK.ImageMargin = 2;
            this.btnOK.LeftImage = null;
            this.btnOK.Location = new System.Drawing.Point(324, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.PushStyle = true;
            this.btnOK.RightImage = null;
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnOK.TextLeftMargin = 2;
            this.btnOK.TextRightMargin = 2;
            this.btnOK.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnCancel.CenterImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.HyperlinkStyle = false;
            this.btnCancel.ImageMargin = 2;
            this.btnCancel.LeftImage = null;
            this.btnCancel.Location = new System.Drawing.Point(405, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PushStyle = true;
            this.btnCancel.RightImage = null;
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnCancel.TextLeftMargin = 2;
            this.btnCancel.TextRightMargin = 2;
            this.btnCancel.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // DataSelectForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(492, 366);
            this.Controls.Add(this.BackPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(340, 300);
            this.Name = "DataSelectForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chart Details";
            this.BackPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.BottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.Panel BackPanel;
        private ZoneFiveSoftware.Common.Visuals.Panel BottomPanel;
        private ZoneFiveSoftware.Common.Visuals.Button btnCancel;
        private ZoneFiveSoftware.Common.Visuals.Button btnOK;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox selectedList;
        private System.Windows.Forms.ListBox unselectedList;
        private ZoneFiveSoftware.Common.Visuals.Button btnAdd;
        private ZoneFiveSoftware.Common.Visuals.Button btnUp;
        private ZoneFiveSoftware.Common.Visuals.Button btnDown;
        private ZoneFiveSoftware.Common.Visuals.Button btnRemove;
        private System.Windows.Forms.Label lblAvailable;
        private System.Windows.Forms.Label lblSelected;
        private ITheme currentTheme;
    }
}