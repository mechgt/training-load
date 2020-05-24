using System.Drawing;
using ZoneFiveSoftware.Common.Visuals;
using TrainingLoad.UI;
using TrainingLoad.Resources;

namespace TrainingLoad.Data
{
    class ColumnDef : IListColumnDefinition
    {
        //public const string CTL = "e54d362a-c93b-4cef-9a0a-688095119118";
        //public const string ATL = "f3fef582-5a13-47fa-8667-cb6d965ed9fa";
        //public const string TSB = "180583c5-3514-459c-aef6-9f5043682413";
        //public const string Score = "bad9fd7a-3117-470c-af1e-c588d681a427";
        //public const string Influence = "950c2fdc-ef1c-4675-a264-0007d3bbfa8e";
        //public const string FTP = "2605e1af-56fb-47a0-945d-bee30b490853";
        //public const string Weight = "380f1c70-75d7-4416-b242-1efd61b27c9a";
        //public const string BMI = "0be51b53-0133-40a2-b344-e153659ec36e";
        //public const string BodyFat = "b438d9b4-0ec3-4348-b5ca-4bac964595da";
        //public const string RollingSum1 = "021396b5-927e-4c68-9825-ac7e85667644";
        //public const string RollingSum2 = "cf76dd77-048a-498a-a9df-91ec8021040d";
        //public const string HRR = "175e88d0-73b8-47de-ab93-ddfbc64c652f";
        //public const string HRmax = "93b8df6a-31e7-4c1e-86fd-96cc864a38ac";

        public const string CTL = "CTL";
        public const string ATL = "ATL";
        public const string TSB = "TSB";
        public const string Score = "SCORE";
        public const string Influence = "INF";
        public const string FTPcycle = "FTPcycle";
        public const string FTPrun = "FTPrun";
        public const string Weight = "WEIGHT";
        public const string BMI = "BMI";
        public const string BodyFat = "BF";
        public const string RollingSum1 = "MOV1";
        public const string RollingSum2 = "MOV2";
        public const string HRR = "HRR";
        public const string HRmax = "HRM";

        private string id;
        private string text;
        private string groupName;
        private int width;
        private StringAlignment align;
        private TextSource source;

        public enum TextSource
        {
            Plugin,
            SportTracks,
            Static
        }

        public ColumnDef(string id, string text, string groupName, int width, StringAlignment align, TextSource source)
            : this(id, text, groupName, width, align)
        {
            // TODO: Manage units text on column title display
            this.source = source;
        }

        public ColumnDef(string id, string text, string groupName, int width, StringAlignment align)
        {
            this.id = id;
            this.text = text;
            this.groupName = groupName;
            this.width = width;
            this.align = align;
            this.source = TextSource.Static;
        }

        public ColumnDef(string id, string text, int width)
            : this(id, text, string.Empty, width, StringAlignment.Near)
        {
        }

        #region IListColumnDefinition Members

        public StringAlignment Align
        {
            get { return this.align; }
        }

        public string GroupName
        {
            get { return this.groupName; }
        }

        public string Id
        {
            get { return this.id; }
        }

        public string Text(string columnId)
        {
            try
            {
                switch (source)
                {
                    case TextSource.Plugin:
                        // columnId = "{0} {1}|Formatted, text";
                        string[] vals = { this.text };
                        if (this.text != null)
                        {
                            vals = this.text.Split('|');
                        }

                        string format = Resources.Strings.ResourceManager.GetString(vals[0]);
                        string[] args = { string.Empty };
                        if (1 < vals.Length)
                            args = vals[1].Split(',');

                        return string.Format(format, args);

                    case TextSource.SportTracks:
                        // TODO: Dynamically retreive ST resource text (code below does NOT work)
                        //CommonResources.Text.ActionAbort
                        return Resources.SportTracks.ResourceManager.GetString(this.text, System.Globalization.CultureInfo.CurrentCulture);

                    default:
                    case TextSource.Static:
                        return this.text;
                }
            }
            catch (System.Exception)
            {
                return this.text;
            }
        }

        public int Width
        {
            get { return this.width; }
            set { width = value; }
        }

        #endregion

        public override bool Equals(object obj)
        {
            ColumnDef col = obj as ColumnDef;
            return this.Id == col.Id;
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public override string ToString()
        {
            return Text(null);
        }
    }
}
