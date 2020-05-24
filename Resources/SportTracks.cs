using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using ZoneFiveSoftware.Common.Visuals;

namespace TrainingLoad.Resources
{
    class SportTracks
    {
        private static ResourceManager resourceMan;

        internal static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ZoneFiveSoftware.Common.Visuals.CommonResources.Text", typeof(CommonResources.Text).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
    }
}
