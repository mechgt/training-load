// <copyright file="SetPerfDate.cs" company="N/A">
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
    using TrainingLoad.Settings;
    using ZoneFiveSoftware.Common.Visuals;
    using System.ComponentModel;

    class SetPerfDate : IAction
    {
        #region IAction Members

        public bool Enabled
        {
            get { return true; }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public Image Image
        {
            get { return CommonResources.Images.Calendar16; }
        }

        public string Title
        {
            get { return Resources.Strings.Label_SetTargetDate; }
        }

        public void Refresh()
        {
            // No idea when this is called.
        }

        public void Run(Rectangle rectButton)
        {
            // This is called when the target is clicked.
            UserData.PerfDate = PluginMain.GetApplication().Calendar.Selected.Date;
        }
        
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

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
