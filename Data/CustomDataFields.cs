using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data.Fitness.CustomData;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using System.Diagnostics;
using TrainingLoad.Settings;

namespace TrainingLoad.Data
{
    class CustomDataFields
    {
        private static bool warningMsgBadField;

        public enum CustomFields
        {
            Trimp, TSS, FTPcycle, FTPrun, NormPwr, VDOT, DanielsPoints
        }

        /// <summary>
        /// Get a Training Load related custom parameter
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static ICustomDataFieldDefinition GetCustomProperty(CustomFields field, bool autoCreate)
        {
            // All data types so far are numbers
            ICustomDataFieldDefinition fieldDef = null;
            ICustomDataFieldDataType dataType = CustomDataFieldDefinitions.StandardDataType(CustomDataFieldDefinitions.StandardDataTypes.NumberDataTypeId);
            ICustomDataFieldObjectType objType;
            Guid id;
            string name = GetCustomText(field);

            switch (field)
            {
                case CustomFields.Trimp:
                    objType = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));
                    id = GUIDs.customTrimp;
                    fieldDef = GetCustomProperty(dataType, objType, id, name, autoCreate, CustomDataFieldGroupAggregation.AggregationType.Sum);
                    break;

                case CustomFields.TSS:
                    objType = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));
                    id = GUIDs.customTSS;
                    fieldDef = GetCustomProperty(dataType, objType, id, name, autoCreate, CustomDataFieldGroupAggregation.AggregationType.Sum);
                    break;

                case CustomFields.NormPwr:
                    objType = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));
                    id = GUIDs.customNP;
                    fieldDef = GetCustomProperty(dataType, objType, id, name, autoCreate, CustomDataFieldGroupAggregation.AggregationType.TimeWeightedAverage);
                    break;

                case CustomFields.FTPcycle:
                    objType = CustomDataFieldDefinitions.StandardObjectType(typeof(IAthleteInfoEntry));
                    id = GUIDs.customFTPcycle;
                    fieldDef = GetCustomProperty(dataType, objType, id, name, autoCreate, CustomDataFieldGroupAggregation.AggregationType.Last);
                    break;

                case CustomFields.FTPrun:
                    objType = CustomDataFieldDefinitions.StandardObjectType(typeof(IAthleteInfoEntry));
                    id = GUIDs.customFTPrun;
                    fieldDef = GetCustomProperty(dataType, objType, id, name, autoCreate, CustomDataFieldGroupAggregation.AggregationType.Last);
                    break;

                case CustomFields.VDOT:
                    objType = CustomDataFieldDefinitions.StandardObjectType(typeof(IAthleteInfoEntry));
                    id = GUIDs.customAthleteVDOT;
                    fieldDef = GetCustomProperty(dataType, objType, id, name, autoCreate, CustomDataFieldGroupAggregation.AggregationType.Last);
                    break;

                case CustomFields.DanielsPoints:
                    objType = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));
                    id = GUIDs.customDanielsPoints;
                    fieldDef = GetCustomProperty(dataType, objType, id, name, autoCreate, CustomDataFieldGroupAggregation.AggregationType.Sum);
                    break;
            }

            return fieldDef;
        }

        /// <summary>
        /// Gets the default string value for this field.
        /// </summary>
        /// <param name="type">Which field type to get name for</param>
        /// <returns>Returns current field name (if available), or default name for this field.</returns>
        public static string GetCustomText(CustomFields type)
        {
            return GetCustomText(type, null);
        }

        /// <summary>
        /// Gets the string value assigned to this field, or the default value if one is not already assigned.
        /// </summary>
        /// <param name="type">Which field type to get name for</param>
        /// <param name="field">Field to get name value from.</param>
        /// <returns>Returns current field name (if available), or default name for this field.</returns>
        public static string GetCustomText(CustomFields type, ICustomDataFieldDefinition field)
        {
            string name;

            if (field != null)
            {
                // Return user named text
                return field.Name;
            }
            else
            {
                // Default localized custom field names
                switch (type)
                {
                    case CustomFields.NormPwr:
                        name = Resources.Strings.Label_NormPower;
                        break;
                    
                    case CustomFields.Trimp:
                        name = Resources.Strings.Label_TRIMP;
                        break;
                    
                    case CustomFields.TSS:
                        name = Resources.Strings.Label_TSS;
                        break;
                    
                    case CustomFields.DanielsPoints:
                        name = Resources.Strings.Label_DanielsPoints;
                        break;
                    
                    case CustomFields.FTPcycle:
                        name = "FTP (run/cycle)";
                        break;

                    default:
                        name = type.ToString();
                        break;
                }

                return name;
            }
        }

        /// <summary>
        /// Private helper to dig the logbook for a custom parameter
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static ICustomDataFieldDefinition GetCustomProperty(ICustomDataFieldDataType dataType, ICustomDataFieldObjectType objType, Guid id, string name, bool autoCreate, CustomDataFieldGroupAggregation.AggregationType summary)
        {
            // Dig all of the existing custom params looking for a match.
            foreach (ICustomDataFieldDefinition customDef in PluginMain.GetApplication().Logbook.CustomDataFieldDefinitions)
            {
                if (customDef.Id == id)
                {
                    // Is this really necessary...?
                    if (customDef.DataType != dataType)
                    {
                        // Invalid match found!!! Bad news.
                        // This might occur if a user re-purposes a field.
                        if (!warningMsgBadField)
                        {
                            warningMsgBadField = true;
                            MessageDialog.Show("Invalid " + name + " Custom Field.  Was this field data type modified? (" + customDef.Name + ")", Resources.Strings.Label_TrainingLoad);
                        }

                        return null;
                    }
                    else
                    {
                        // Return custom def
                        return customDef;
                    }
                }
            }

            // No match found, create it
            if (autoCreate)
            {
                ICustomDataFieldDefinition def = PluginMain.GetApplication().Logbook.CustomDataFieldDefinitions.Add(id, objType, dataType, name);
                def.GroupAggregation = summary;
                def.CreatedBy = GUIDs.PluginMain;
                return def;
            }
            else
            {
                return null;
            }
        }
    }
}
