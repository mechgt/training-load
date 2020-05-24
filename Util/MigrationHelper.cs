using System;
using System.Collections.Generic;
using System.Text;
using TrainingLoad.Settings;
using TrainingLoad.UI.Actions;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness.CustomData;
using System.Windows.Forms;
using TrainingLoad.Resources;
using ZoneFiveSoftware.Common.Visuals;

namespace TrainingLoad.Util
{
    class MigrationHelper
    {
        /// <summary>
        /// Helper to Migrate FTP from ST2 field to ST3 custom field
        /// </summary>
        internal static void MigrateFTP()
        {
            DialogResult result = MessageDialog.Show(String.Format(Strings.Text_MigrationAnnouncement, UserData.FTPfield.ToString()), Strings.Label_TrainingLoad, MessageBoxButtons.OKCancel);
            if (result != DialogResult.OK)
            {
                return;
            }


            ILogbook logbook = PluginMain.GetLogbook();
            IList<DateTime> entryDates = logbook.Athlete.InfoEntries.EntryDates;

            if (entryDates != null && entryDates.Count > 0)
            {
                foreach (DateTime date in entryDates)
                {
                    float ftp = GetST2FTP(date, true);

                    if (!float.IsNaN(ftp))
                    {
                        double? entry = ftp;
                        ICustomDataFieldDefinition fieldDef = Data.CustomDataFields.GetCustomProperty(TrainingLoad.Data.CustomDataFields.CustomFields.FTPcycle, true);

                        // Add ftp entry to custom field
                        logbook.Athlete.InfoEntries.EntryForDate(date).SetCustomDataValue(fieldDef, entry);
                    }
                    else
                    {
                    }
                }
            }

            UserData.StoreSettings();
        }

        /// <summary>
        /// Get the FTP entry for this date.  If clear is true, it will also remove the entry from the 
        /// traditional athlete info as it is collected.
        /// NOTE: This is slightly different than the TrimpActivity.GetFtp.  This gets the value for this 
        /// date or float.Nan if no entry for this date.  TrimpActivity gets the last entry as of a particular date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static float GetST2FTP(DateTime date, bool clear)
        {
            float ftp = float.NaN;
            IAthleteInfoEntry athleteInfo = PluginMain.GetLogbook().Athlete.InfoEntries.EntryForDate(date);

            switch (TrainingLoad.Settings.UserData.FTPfield)
            {
                case TrainingLoad.Settings.UserData.AthleteField.BPHigh:
                    ftp = athleteInfo.SystolicBloodPressure;
                    athleteInfo.SystolicBloodPressure = float.NaN;
                    break;
                case TrainingLoad.Settings.UserData.AthleteField.BPLow:
                    ftp = athleteInfo.DiastolicBloodPressure;
                    athleteInfo.DiastolicBloodPressure = float.NaN;
                    break;
                case TrainingLoad.Settings.UserData.AthleteField.BodyFat:
                    ftp = athleteInfo.BodyFatPercentage;
                    athleteInfo.BodyFatPercentage = float.NaN;
                    break;
                case TrainingLoad.Settings.UserData.AthleteField.Skinfold:
                    //default:
                    ftp = athleteInfo.Skinfold;
                    athleteInfo.Skinfold = float.NaN;
                    break;
            }

            return ftp;
        }
    }
}
