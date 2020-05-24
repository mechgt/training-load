using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingLoad.Data
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Chart;
    using ZoneFiveSoftware.Common.Visuals.Util;
    using TrainingLoad.Settings;

    class ChartColumns
    {
        private static IList<IListColumnDefinition> allColumns;

        internal static IList<IListColumnDefinition> AllCharts
        {
            get
            {
                if (allColumns == null)
                {
                    CreateAllColumns();
                }

                return allColumns;
            }
        }

        internal static IList<IListColumnDefinition> SelectedCharts
        {
            get
            {
                IList<IListColumnDefinition> selected = new List<IListColumnDefinition>();
                string[] info;

                // Get user settings
                foreach (string item in UserData.UserCharts.Split(';'))
                {
                    info = item.Split('|');

                    // Build list of selected columns
                    foreach (ColumnDef column in AllCharts)
                    {
                        if (info[0].Equals(column.Id))
                        {
                            column.Width = int.Parse(info[1]);
                            selected.Add(column);
                            break;
                        }
                    }
                }

                return selected;
            }
        }

        private static void CreateAllColumns()
        {
            allColumns = new List<IListColumnDefinition>();

            allColumns.Add(new ColumnDef(ColumnDef.ATL, "Label_ATL", null, 100, StringAlignment.Near, ColumnDef.TextSource.Plugin));
            allColumns.Add(new ColumnDef(ColumnDef.BMI, "Label_BMI", null, 100, StringAlignment.Near, ColumnDef.TextSource.Plugin));
            allColumns.Add(new ColumnDef(ColumnDef.BodyFat, CommonResources.Text.LabelBodyFatPct, null, 100, StringAlignment.Near, ColumnDef.TextSource.Static));
            allColumns.Add(new ColumnDef(ColumnDef.CTL, "Label_CTL", null, 100, StringAlignment.Near, ColumnDef.TextSource.Plugin));
            allColumns.Add(new ColumnDef(ColumnDef.FTPcycle, "Label_FTP|" + Resources.Strings.Label_Cycle, null, 100, StringAlignment.Near, ColumnDef.TextSource.Plugin));
            allColumns.Add(new ColumnDef(ColumnDef.FTPrun, "Label_FTP|" + Resources.Strings.Label_Run, null, 100, StringAlignment.Near, ColumnDef.TextSource.Plugin));
            allColumns.Add(new ColumnDef(ColumnDef.Influence, "Label_TrainingInfluence", null, 100, StringAlignment.Near, ColumnDef.TextSource.Plugin));
            allColumns.Add(new ColumnDef(ColumnDef.RollingSum1, Resources.Strings.Label_RollingSum + " 1", null, 100, StringAlignment.Near, ColumnDef.TextSource.Static));
            allColumns.Add(new ColumnDef(ColumnDef.RollingSum2, Resources.Strings.Label_RollingSum + " 2", null, 100, StringAlignment.Near, ColumnDef.TextSource.Static));
            allColumns.Add(new ColumnDef(ColumnDef.Score, "Label_TrainingStressScore", null, 100, StringAlignment.Near, ColumnDef.TextSource.Plugin));
            allColumns.Add(new ColumnDef(ColumnDef.TSB, "Label_TrainingStressBalance", null, 100, StringAlignment.Near, ColumnDef.TextSource.Plugin));
            allColumns.Add(new ColumnDef(ColumnDef.Weight, CommonResources.Text.LabelWeight, null, 100, StringAlignment.Near, ColumnDef.TextSource.Static));
            allColumns.Add(new ColumnDef(ColumnDef.HRR, CommonResources.Text.LabelRestingHR, null, 100, StringAlignment.Near, ColumnDef.TextSource.Static));
            allColumns.Add(new ColumnDef(ColumnDef.HRmax, CommonResources.Text.LabelMaxHR, null, 100, StringAlignment.Near, ColumnDef.TextSource.Static));
        }
    }
}
