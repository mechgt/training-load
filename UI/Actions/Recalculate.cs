// <copyright file="Recalculate.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.UI.Actions
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using TrainingLoad.Settings;
    using TrainingLoad.UI.View;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Data.Fitness.CustomData;
    using TrainingLoad.Data;

    class Recalculate : IAction
    {
        private CalcType calcType;
        private ContextMenuStrip mnuRecalculate;

        public enum CalcType
        {
            Trimp,
            TSS,
            Logbook
        }

        #region IAction Members

        public bool Enabled
        {
            get { return true; }
        }

        public bool HasMenuArrow
        {
            get { return true; }
        }

        public Image Image
        {
            get { return CommonResources.Images.Calculator16; }
        }

        public string Title
        {
            get
            {
                string title = Resources.Strings.Label_Recalculate + " ";
                return title;

                switch (calcType)
                {

                    case CalcType.Trimp:
                        title += Resources.Strings.Label_TRIMP;
                        break;

                    case CalcType.TSS:
                        title += Resources.Strings.Label_TrainingStressScore;
                        break;
                }

                return title;
            }
        }

        public void Refresh()
        {
            // No idea when this is called.
        }

        public void Run(Rectangle rectButton)
        {
            if (mnuRecalculate == null)
            {
                mnuRecalculate = new System.Windows.Forms.ContextMenuStrip();

                mnuRecalculate.Items.Add("Trimp", CommonResources.Images.TrackHeartRate16, mnuRecalculate_ItemSelected);
                mnuRecalculate.Items.Add("TSS", CommonResources.Images.TrackPower16, mnuRecalculate_ItemSelected);
                mnuRecalculate.Items.Add("Logbook", CommonResources.Images.Refresh16, mnuRecalculate_ItemSelected);
            }

            mnuRecalculate.Show(rectButton.Right + 3, rectButton.Top);
        }

        private void mnuRecalculate_ItemSelected(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in mnuRecalculate.Items)
            {
                item.Checked = false;
            }

            ToolStripMenuItem selected = sender as ToolStripMenuItem;
            selected.Checked = true;

            switch (selected.Text)
            {
                case "Trimp":
                    calcType = CalcType.Trimp;
                    break;
                case "TSS":
                    calcType = CalcType.TSS;
                    break;
                default:
                    calcType = CalcType.Logbook;
                    break;
            }

            RecalculateData();
        }

        internal void RecalculateData()
        {
            ViewTrainingLoadPageControl control = ViewTrainingLoadPage.Instance;

            // This is called when the target is clicked.
            switch (calcType)
            {
                case CalcType.Trimp:

                    // Clear stored values
                    ICustomDataFieldDefinition trimpField = TrimpActivity.TrimpField;

                    foreach (TrimpActivity activity in control.treeActivity.Selected)
                    {
                        activity.SetCustomDataValue(TrimpActivity.TrimpField, null);
                    }

                    break;

                case CalcType.TSS:
                    ICustomDataFieldDefinition tssField = TrimpActivity.TSSField;
                    ICustomDataFieldDefinition npField = TrimpActivity.NormPwrField;

                    // Clear stored values
                    foreach (TrimpActivity activity in control.treeActivity.Selected)
                    {
                        activity.SetCustomDataValue(npField, null);
                        activity.SetCustomDataValue(tssField, null);
                    }
                    break;
                case CalcType.Logbook:
                    // Recalculate=True during calculation will cause all values in memory to be ignored and everything will be recalculated 
                    TrimpActivity.Recalculate = true;
                    break;
            }

            ChartData.IsCalculated = false;
            control.RefreshPage();
            TrimpActivity.Recalculate = false;
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IAction Members


        public IList<string> MenuPath
        {
            get { return null; }
        }

        public bool Visible
        {
            get { return true; }
        }

        #endregion
    }
}
