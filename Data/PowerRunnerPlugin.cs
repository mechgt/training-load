using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace TrainingLoad.Data
{
    static class PowerRunnerPlugin
    {
        private static bool isInstalled;
        private static bool installChecked;
        private static Type Common;

        internal static bool IsInstalled
        {
            get
            {
                if (installChecked)
                {
                    // Return true if plugin has already been found
                    return isInstalled;
                }

                installChecked = true;

                foreach (Assembly loadedAssembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    AssemblyName name = loadedAssembly.GetName();

                    if (name.Name.Equals("PowerRunner"))
                    {
                        if (name.Version >= new Version(0, 9, 1, 0))
                        {
                            try
                            {
                                Common = loadedAssembly.GetType("PowerRunner.Data.Shared");
                                MethodInfo method = Common.GetMethod("IsRunningCategory");
                                isInstalled = true;
                            }
                            catch
                            {
                                isInstalled = false;
                            }
                            return isInstalled;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return false;
            }
        }

        internal static float TCa
        {
            get
            {
                if (IsInstalled)
                {
                    return (float)Common.GetProperty("TCa").GetValue(null, null);
                }

                return float.NaN;
            }
        }

        internal static bool IsRunningCategory(IActivityCategory category)
        {
            if (IsInstalled)
            {
                MethodInfo method = Common.GetMethod("IsRunningCategory");
                object[] args = { category };
                return (bool)method.Invoke(null, args);
            }
            else
            {
                return false;
            }
        }
    }
}