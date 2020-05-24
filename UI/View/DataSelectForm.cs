// <copyright file="DataSelectForm.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
// <summary>Form used for selecting/deselecting from a list of items.  Used for things such as treelist columns, chartlines, etc.</summary>
namespace TrainingLoad.UI.View
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Visuals;

    /// <summary>
    /// Form used for selecting/deselecting from a list of items.  Used for things such as treelist columns, chartlines, etc.
    /// </summary>
    internal partial class DataSelectForm : Form
    {
        #region Fields

        /// <summary>
        /// Selected/enabled items
        /// </summary>
        private System.Collections.Generic.List<string> items;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DataSelectForm class.
        /// </summary>
        /// <param name="visualTheme">Theme to be applied</param>
        /// <param name="selected">List of selected items</param>
        /// <param name="unselected">List of unselected itmes</param>
        internal DataSelectForm(ITheme visualTheme, List<string> selected, List<string> unselected)
        {
            InitializeComponent();

            foreach (string s in selected)
            {
                selectedList.Items.Add(s);
            }

            foreach (string s in unselected)
            {
                unselectedList.Items.Add(s);
            }

            Items = new List<string>();
            unselectedList.Sorted = true;
            this.currentTheme = visualTheme;
            ThemeChanged(visualTheme);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets selected items (?)
        /// </summary>
        internal System.Collections.Generic.List<string> Items
        {
            get { return items; }
            set { items = value; }
        }

        #endregion

        #region Public/Private Methods

        /// <summary>
        /// Refresh standard localized form labels
        /// </summary>
        internal void RefreshLabels()
        {
            btnRemove.Text = "      " + CommonResources.Text.ActionRemove;
            btnAdd.Text = "      " + CommonResources.Text.ActionAdd;
            btnOK.Text = CommonResources.Text.ActionOk;
            btnCancel.Text = CommonResources.Text.ActionCancel;
            lblAvailable.Text = Resources.Strings.Label_Available;
        }

        /// <summary>
        /// Set the Title Icon
        /// </summary>
        /// <param name="image">Icon image (bitmap)</param>
        internal void SetIcon(Image image)
        {
            Bitmap bitmap = image as Bitmap;
            if (bitmap != null)
            {
                this.Icon = Icon.FromHandle(bitmap.GetHicon());
            }
        }

        /// <summary>
        /// Set label for 'Selected' items on form
        /// </summary>
        /// <param name="label">Text label over 'Selected' items</param>
        internal void SetLabelSelected(string label)
        {
            lblSelected.Text = label;
        }

        /// <summary>
        /// Apply theme to form
        /// </summary>
        /// <param name="visualTheme">Theme to be applied</param>
        internal void ThemeChanged(ITheme visualTheme)
        {
            this.currentTheme = visualTheme;
            BackPanel.ThemeChanged(visualTheme);
            BottomPanel.ThemeChanged(visualTheme);
            selectedList.BackColor = visualTheme.Window;
            unselectedList.BackColor = visualTheme.Window;
            splitContainer1.Panel1.BackColor = visualTheme.Control;
            splitContainer1.Panel2.BackColor = visualTheme.Control;
            BottomPanel.BackColor = visualTheme.Control;
            splitContainer1.BackColor = visualTheme.Control;
            BottomPanel.BackColor = visualTheme.Window;
            BottomPanel.RightGradientColor = Color.FromArgb(30, Color.Black);
            lblSelected.ForeColor = visualTheme.ControlText;
            lblAvailable.ForeColor = visualTheme.ControlText;
        }

        /// <summary>
        /// Localize the form
        /// </summary>
        /// <param name="culture">Culture to be applied</param>
        internal void UICultureChanged(CultureInfo culture)
        {
            btnRemove.Text = CommonResources.Text.ActionRemove;
            btnAdd.Text = CommonResources.Text.ActionAdd;
            btnOK.Text = CommonResources.Text.ActionOk;
            btnCancel.Text = CommonResources.Text.ActionCancel;
        }

        /// <summary>
        /// Cancel button clicked event.
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// OK button clicked event
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            foreach (string s in selectedList.Items)
            {
                Items.Add(s);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Unselected List double click event
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void UnselectedList_DoubleClick(object sender, EventArgs e)
        {
            int curSelection = unselectedList.SelectedIndex;
            if (unselectedList.SelectedIndex != -1)
            {
                selectedList.Items.Add(unselectedList.Items[curSelection]);
                unselectedList.SelectedIndex = -1;
                selectedList.SelectedItem = unselectedList.Items[curSelection];
                unselectedList.Items.Remove(unselectedList.Items[curSelection]);
            }
        }

        /// <summary>
        /// Selected List double click event
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void SelectedList_DoubleClick(object sender, EventArgs e)
        {
            int curSelection = selectedList.SelectedIndex;
            if (selectedList.SelectedIndex != -1)
            {
                unselectedList.Items.Add(selectedList.Items[curSelection]);
                selectedList.SelectedIndex = -1;
                unselectedList.SelectedItem = selectedList.Items[curSelection];
                selectedList.Items.Remove(selectedList.Items[curSelection]);
            }
        }

        /// <summary>
        /// Add button click event
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            int curSelection = unselectedList.SelectedIndex;
            if (unselectedList.SelectedIndex != -1)
            {
                selectedList.Items.Add(unselectedList.Items[curSelection]);
                unselectedList.SelectedIndex = -1;
                selectedList.SelectedItem = unselectedList.Items[curSelection];
                unselectedList.Items.Remove(unselectedList.Items[curSelection]);
            }
        }

        /// <summary>
        /// Remove button click event
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            int curSelection = selectedList.SelectedIndex;
            if (selectedList.SelectedIndex != -1)
            {
                unselectedList.Items.Add(selectedList.Items[curSelection]);
                selectedList.SelectedIndex = -1;
                unselectedList.SelectedItem = selectedList.Items[curSelection];
                selectedList.Items.Remove(selectedList.Items[curSelection]);
            }
        }

        /// <summary>
        /// Move Up button click event
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void UpButton_Click(object sender, EventArgs e)
        {
            if (selectedList.SelectedIndex > 0)
            {
                int curSelection = selectedList.SelectedIndex;
                object cur = selectedList.Items[curSelection];
                selectedList.Items.RemoveAt(curSelection);
                selectedList.Items.Insert(curSelection - 1, cur);
                selectedList.SelectedIndex = curSelection - 1;
            }
        }

        /// <summary>
        /// Move Down button click event
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void DownButton_Click(object sender, EventArgs e)
        {
            if (selectedList.SelectedIndex < selectedList.Items.Count - 1)
            {
                int curSelection = selectedList.SelectedIndex;
                object cur = selectedList.Items[curSelection];
                selectedList.Items.RemoveAt(curSelection);
                selectedList.Items.Insert(curSelection + 1, cur);
                selectedList.SelectedIndex = curSelection + 1;
            }
        }

        /// <summary>
        /// Selected List selection changed event
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void SelectedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int curSelection = selectedList.SelectedIndex;
            unselectedList.SelectedIndex = -1;
            selectedList.SelectedIndex = curSelection;
        }

        /// <summary>
        /// Unselected List selection changed event
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void UnselectedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int curSelection = unselectedList.SelectedIndex;
            selectedList.SelectedIndex = -1;
            unselectedList.SelectedIndex = curSelection;
        }

        /// <summary>
        /// Unselected List draw item event
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void UnselectedList_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            // Create a new Brush and initialize to a Black colored brush
            // by default.
            e.DrawBackground();
            Brush untextBrush = new SolidBrush(this.currentTheme.ControlText);
            Brush textBrush = new SolidBrush(this.currentTheme.SelectedText);
            Brush selectedBrush = new SolidBrush(this.currentTheme.Selected);
            ListBox listBox = sender as ListBox;

            // Draw the current item text based on the current 
            // Font and the custom brush settings.
            if (e.Index != -1 && listBox != null)
            {
                e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, untextBrush, e.Bounds, StringFormat.GenericDefault);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    // Draw the focus rectangle
                    e.Graphics.FillRectangle(selectedBrush, e.Bounds);

                    // The focus rect will be on top of the text so redraw the text
                    e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
                }
            }
        }

        /// <summary>
        /// Selected List draw item event
        /// </summary>
        /// <param name="sender">Object Sender</param>
        /// <param name="e">Event arguments</param>
        private void SelectedList_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            // Create a new Brush and initialize to a Black colored brush
            // by default.
            e.DrawBackground();
            Brush untextBrush = new SolidBrush(this.currentTheme.ControlText);
            Brush textBrush = new SolidBrush(this.currentTheme.SelectedText);
            Brush selectedBrush = new SolidBrush(this.currentTheme.Selected);
            ListBox listBox = sender as ListBox;

            // Draw the current item text based on the current 
            // Font and the custom brush settings.
            if (e.Index != -1 && listBox != null)
            {
                e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, untextBrush, e.Bounds, StringFormat.GenericDefault);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    // Draw the focus rectangle
                    e.Graphics.FillRectangle(selectedBrush, e.Bounds);

                    // The focus rect will be on top of the text so redraw the text
                    e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
                }
            }
        }

        #endregion
    }
}
