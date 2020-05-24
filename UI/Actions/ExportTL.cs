// <copyright file="ExportTL.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.UI.Actions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Visuals;
    using TrainingLoad.UI.View;
    using TrainingLoad.Data;

    /// <summary>
    /// Export Training Load data to a csv file.
    /// </summary>
    partial class ExportTL : IAction
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private static string exportTitle = CommonResources.Text.ActionExport;

        #region IAction Members

        /// <summary>
        /// Gets a value indicating whether the export task item is enabled or not.
        /// </summary>
        public bool Enabled
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the export task item has a menu arrow, i.e. submenu.
        /// </summary>
        public bool HasMenuArrow
        {
            get { return false; }
        }

        /// <summary>
        /// Gets image to be displayed in the task pane.
        /// </summary>
        public Image Image
        {
            get { return CommonResources.Images.Export16; }
        }

        /// <summary>
        /// Gets or sets text shown in the task pane.
        /// </summary>
        public string Title
        {
            get { return exportTitle; }
            set { exportTitle = value; }
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public void Refresh()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes when task is selected by user.
        /// </summary>
        /// <param name="rectButton"></param>
        public void Run(Rectangle rectButton)
        {
            ExportTLtoCSV();
        }

        /// <summary>
        /// Export TL data to CSV file.  
        /// Pops up file selection dialog for user to define where to save file.
        /// </summary>
        public void ExportTLtoCSV()
        {
            // Open File Save dialog to create new CSV Document
            SaveFileDialog saveFile = new SaveFileDialog();
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

            // Define output file.  Try... Catch... used in case the file is opened elsewhere (spreadsheet for example).
            StreamWriter fileStream;
            try
            {
                fileStream = File.CreateText(saveFile.FileName);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while exporting: " + e.Message, Resources.Strings.Label_TrainingLoad, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Begin Exporting...
            Cursor.Current = Cursors.WaitCursor;

            // *********************************************************
            // Write Heading
            fileStream.Write(
                CommonResources.Text.LabelStartTime + comma +
                CommonResources.Text.LabelCategory + comma +
                CommonResources.Text.LabelName + comma +
                CommonResources.Text.LabelDistance + comma +
                CommonResources.Text.LabelTimeElapsed + comma +
                CommonResources.Text.LabelAvgSpeed + comma +
                CommonResources.Text.LabelAvgPace + comma +
                CommonResources.Text.LabelAvgHR + comma +
                "Norm Power" + comma +
                Resources.Strings.Label_IntensityFactor + comma +
                CommonResources.Text.LabelAvgPower + comma +
                CommonResources.Text.LabelAvgGrade + comma +
                CommonResources.Text.LabelAscending + comma +
                CommonResources.Text.LabelDescending + comma +

                Resources.Strings.Label_TRIMP + comma +
                Resources.Strings.Label_TrainingStressScore + comma +
                Resources.Strings.Label_ATL + " " + ChartData.TLChartBasis.ToString() + comma +
                Resources.Strings.Label_CTL + " " + ChartData.TLChartBasis.ToString() + comma +
                Resources.Strings.Label_TSB + " (Pre)" + comma +
                Resources.Strings.Label_TSB + " (Post)" + comma +
                CommonResources.Text.LabelAvgCadence);

            fileStream.WriteLine();

            // *********************************************************
            // Write TL Data to CSV

            try
            {
                int j = 1;

                ActivityCollection activities = new ActivityCollection(ChartData.TrimpActivities);
                activities.Sort();

                foreach (TrimpActivity activity in activities)
                {
                    // Progress status
                    float percent = (float)j / (float)ChartData.TrimpActivities.Count * 100F;
                    exportTitle = CommonResources.Text.ActionExport + " (" + (int)percent + "%)";
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Title"));
                    j++;

                    List<string> items = new List<string>();
                    items.Add(activity.GetFormattedText("StartTime"));
                    items.Add(activity.GetFormattedText("Category"));
                    items.Add(activity.Name);
                    items.Add(activity.GetFormattedText("Distance"));
                    items.Add(activity.TotalTime.ToString());
                    items.Add(activity.GetFormattedText("AvgSpeed"));
                    items.Add(activity.AvgPace.ToString());
                    items.Add(activity.AvgHR.ToString("#", CultureInfo.CurrentCulture));
                    items.Add(activity.GetFormattedText("NormalizedPower"));
                    items.Add(activity.GetFormattedText("IntensityFactor"));
                    items.Add(activity.GetFormattedText("AvgPower"));
                    items.Add(activity.GetFormattedText("AvgGrade"));
                    items.Add(activity.TotalAscend.ToString("#",CultureInfo.CurrentCulture));
                    items.Add(activity.TotalDescend.ToString("#", CultureInfo.CurrentCulture));
                    items.Add(activity.TRIMP.ToString("0", CultureInfo.CurrentCulture));
                    items.Add(activity.TSS.ToString("0", CultureInfo.CurrentCulture));
                    items.Add(activity.ATL.ToString("0", CultureInfo.CurrentCulture));
                    items.Add(activity.CTL.ToString("0", CultureInfo.CurrentCulture));
                    items.Add(activity.TSBPre.ToString("0", CultureInfo.CurrentCulture));
                    items.Add(activity.TSBPost.ToString("0", CultureInfo.CurrentCulture));
                    items.Add(activity.GetFormattedText("AvgCadence"));

                    // Assemble data into CSV line ready to write to file
                    string line = string.Empty;
                    foreach (string item in items)
                    {
                        if (item.IndexOf(comma) != -1)
                        {
                            line = line + '"' + item + '"' + comma;
                        }
                        else
                        {
                            line = line + item + comma;
                        }
                    }

                    line = line.TrimEnd(comma.ToCharArray());

                    // Write line to file.
                    fileStream.WriteLine(line);
                    Application.DoEvents();
                }

                // Exporting has successfully completed
                MessageDialog.Show(CommonResources.Text.MessageExportComplete, Resources.Strings.Label_TrainingLoad);
            }
            catch
            {
                // Some error occurred.  The only time I've seen this is when the file was opened during the export.
                MessageDialog.Show(CommonResources.Text.ErrorText_FileCouldNotBeOpened, Resources.Strings.Label_TrainingLoad);
            }
            finally
            {
                // Close document
                fileStream.Close();

                exportTitle = CommonResources.Text.ActionExport;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Title"));
            }
        }

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
