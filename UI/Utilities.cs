// <copyright file="Utilities.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainingLoad.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.GPS;
    using System.Drawing;

    /// <summary>
    /// Generic utilities class that can be used on many projects
    /// </summary>
    internal static class Utilities
    {
        private static Random rand = new Random(1);
        private static IList<IActivityCategory> categoryIndex;

        /// <summary>
        /// Gets a flat list of ALL logbook categories
        /// </summary>
        internal static IList<IActivityCategory> CategoryIndex
        {
            get
            {
                if (categoryIndex == null || categoryIndex.Count == 0)
                    categoryIndex = GetCategoryIndex();

                return categoryIndex;
            }
        }

        /// <summary>
        /// Creates and returns a flat list of ALL categories
        /// </summary>
        /// <returns></returns>
        private static IList<IActivityCategory> GetCategoryIndex()
        {
            IList<IActivityCategory> categoryIndex = new List<IActivityCategory>();

            foreach (IActivityCategory category in PluginMain.GetApplication().Logbook.ActivityCategories)
            {
                categoryIndex.Add(category);
                AddSubcategories(ref categoryIndex, category);
            }

            return categoryIndex;
        }

        /// <summary>
        /// Used by GetCategoryIndex() to recurse through subcategories
        /// </summary>
        /// <param name="categoryIndex"></param>
        /// <param name="parent"></param>
        private static void AddSubcategories(ref IList<IActivityCategory> categoryIndex, IActivityCategory parent)
        {
            foreach (IActivityCategory category in parent.SubCategories)
            {
                categoryIndex.Add(category);
                AddSubcategories(ref categoryIndex, category);
            }
        }

        public static IActivityCategory GetCategory(string refId)
        {
            foreach (IActivityCategory category in CategoryIndex)
            {
                if (category.ReferenceId == refId)
                    return category;
            }

            return null;
        }

        /// <summary>
        /// Get the major/parent category (running, cycling, swimming, etc.).  This is the 2nd level below My Activity Category.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        internal static IActivityCategory GetUpperCategory(IActivity activity)
        {
            return GetUpperCategory(activity.Category);
        }

        /// <summary>
        /// Get the major/parent category (running, cycling, swimming, etc.).  This is the 2nd level below My Activity Category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        internal static IActivityCategory GetUpperCategory(IActivityCategory category)
        {
            while (category != null && category.Parent != null)
            {
                if (category.Parent.Parent == null)
                    return category;
                else
                    category = category.Parent;
            }

            // Should only land here for top level activities (My Activities, My Friend's Activities, etc.)
            return category;
        }

        /// <summary>
        /// Get the major/parent category (running, cycling, swimming, etc.).  This is the 2nd level below My Activity Category.
        /// </summary>
        /// <returns></returns>
        internal static IActivityCategory GetUpperCategory(string refId)
        {
            IActivityCategory cat = GetCategory(refId);
            return GetUpperCategory(cat);
        }

        /// <summary>
        /// Get the top most category such as My Activities or My Friends Activities.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        internal static IActivityCategory GetTopmostCategory(IActivityCategory category)
        {
            category = GetUpperCategory(category);
            if (category.Parent != null)
                return category.Parent;
            else
                return category;
        }

        /// <summary>
        /// Get the top most category such as My Activities or My Friends Activities.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        internal static IActivityCategory GetTopmostCategory(IActivity activity)
        {
            return GetTopmostCategory(activity.Category);
        }

        /// <summary>
        /// Get the top most category such as My Activities or My Friends Activities.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        internal static IActivityCategory GetTopmostCategory(string refId)
        {
            return GetTopmostCategory(GetCategory(refId));
        }

        /// <summary>
        /// Get a random, but controlled color.
        /// </summary>
        /// <param name="alpha">Alpha value of the returned color.</param>
        /// <param name="harshness"></param>
        /// <param name="seed">Re-seed the random number and return random color.</param>
        /// <returns></returns>
        internal static Color RandomColor(int alpha, int harshness, int seed)
        {
            rand = new Random(seed);
            return RandomColor(alpha, harshness);
        }

        /// <summary>
        /// Get a random, but controlled color.
        /// </summary>
        /// <param name="alpha">Alpha value of the returned color.</param>
        /// <param name="harshness"></param>
        /// <returns></returns>
        internal static Color RandomColor(int alpha, int harshness)
        {
            return Color.FromArgb(alpha, rand.Next(harshness), rand.Next(harshness), rand.Next(harshness));
        }

        /// <summary>
        /// Refresh the SportTracks Calendar with the selected activities
        /// </summary>
        /// <param name="activities">These activity dates will be highlighted on the calendar</param>
        internal static void RefreshCalendar(IEnumerable<IActivity> activities)
        {
            IList<DateTime> dates = new List<DateTime>();
            foreach (IActivity activity in activities)
            {
                dates.Add(activity.StartTime.ToLocalTime().Date);
            }

            PluginMain.GetApplication().Calendar.SetHighlightedDates(dates);
        }

        /// <summary>
        /// Filters activities for a category (and it's subcategories), and includes only those activities with a HeartRateTrack.
        /// </summary>
        /// <param name="category">Category to filter for</param>
        /// <param name="activityList">List of activities to be filtered from.</param>
        /// <returns>Returns a list of activities filtered by category</returns>
        internal static IEnumerable<IActivity> FilterActivities(IActivityCategory category, IList<IActivity> activityList)
        {
            IList<IActivity> filteredActivities = new List<IActivity>();
            ActivityInfoCache info = ActivityInfoCache.Instance;
            IActivityCategory activityCategory;
            foreach (IActivity activity in activityList)
            {
                activityCategory = info.GetInfo(activity).Activity.Category;
                while (true)
                {
                    if (activityCategory == category)
                    {
                        // Include Activity
                        filteredActivities.Add(info.GetInfo(activity).Activity);
                        break;
                    }
                    else if (activityCategory.Parent != null)
                    {
                        // Keep searching
                        activityCategory = activityCategory.Parent;
                    }
                    else
                    {
                        // Exclude Activity
                        break;
                    }
                }
            }

            return filteredActivities;
        }

        /// <summary>
        /// Create Grade track from GPSRoute
        /// </summary>
        /// <returns></returns>
        internal static INumericTimeDataSeries GetGradeTrack(IActivity activity)
        {
            INumericTimeDataSeries gradeTrack = new NumericTimeDataSeries();

            if (activity.GPSRoute != null && activity.GPSRoute.Count > 0)
            {
                IGPSPoint lastpoint = activity.GPSRoute[0].Value;

                foreach (ITimeValueEntry<IGPSPoint> item in activity.GPSRoute)
                {
                    float length = item.Value.DistanceMetersToPoint(lastpoint);
                    float height = item.Value.ElevationMeters - lastpoint.ElevationMeters;
                    float grade = height / length;

                    gradeTrack.Add(activity.GPSRoute.EntryDateTime(item), grade);

                    lastpoint = item.Value;
                }
            }
            else
            {
                // Bad or invalid data
                return null;
            }

            return gradeTrack;
        }

        /// <summary>
        /// Create Speed track from GPSRoute.  Speed track is measured in meters.
        /// Always uses GPS track.
        /// </summary>
        /// <returns></returns>
        internal static INumericTimeDataSeries GetSpeedTrack(IActivity activity)
        {
            INumericTimeDataSeries speedTrack = new NumericTimeDataSeries();

            if (activity.GPSRoute != null && activity.GPSRoute.Count > 0)
            {
                ITimeValueEntry<IGPSPoint> lastpoint = activity.GPSRoute[0];

                foreach (ITimeValueEntry<IGPSPoint> item in activity.GPSRoute)
                {
                    float length = item.Value.DistanceMetersToPoint(lastpoint.Value);
                    float deltaSeconds = (float)((activity.GPSRoute.EntryDateTime(item) - activity.GPSRoute.EntryDateTime(lastpoint)).TotalSeconds);
                    float speed = length / deltaSeconds;

                    speedTrack.Add(activity.GPSRoute.EntryDateTime(item), speed);

                    lastpoint = item;
                }
            }
            else
            {
                // Bad or invalid data
                return null;
            }

            return speedTrack;
        }

        /// <summary>
        /// Perform a smoothing operation using a moving average on the data series
        /// </summary>
        /// <param name="track">The data series to smooth</param>
        /// <param name="period">The range to smooth.  This is the total number of seconds to smooth across (slightly different than the ST method.)</param>
        /// <param name="min">An out parameter set to the minimum value of the smoothed data series</param>
        /// <param name="max">An out parameter set to the maximum value of the smoothed data series</param>
        /// <returns></returns>
        internal static INumericTimeDataSeries Smooth(INumericTimeDataSeries track, uint period, out float min, out float max)
        {
            min = float.NaN;
            max = float.NaN;
            INumericTimeDataSeries smooth = new NumericTimeDataSeries();

            if (track != null && track.Count > 0 && period > 1)
            {
                //min = float.NaN;
                //max = float.NaN;
                int start = 0;
                int index = 0;
                float value = 0;
                float delta;

                float per = period;

                // Iterate through track
                // For each point, create average starting with 'start' index and go forward averaging 'period' seconds.
                // Stop when last 'full' period can be created ([start].ElapsedSeconds + 'period' seconds >= TotalElapsedSeconds)
                while (track[start].ElapsedSeconds + period < track.TotalElapsedSeconds)
                {
                    while (track[index].ElapsedSeconds < track[start].ElapsedSeconds + period)
                    {
                        delta = track[index + 1].ElapsedSeconds - track[index].ElapsedSeconds;
                        value += track[index].Value * delta;
                        index++;
                    }

                    // Finish value calculation
                    per = track[index].ElapsedSeconds - track[start].ElapsedSeconds;
                    value = value / per;

                    // Add value to track
                    // TODO: (LOW)I really don't need the smoothed track... really just need max.  Kill this for efficiency?
                    //smooth.Add(track.StartTime.AddSeconds(start), value);
                    smooth.Add(track.EntryDateTime(track[index]), value);

                    // Remove beginning point for next cycle
                    delta = track[start + 1].ElapsedSeconds - track[start].ElapsedSeconds;
                    value = (per * value - delta * track[start].Value);

                    // Next point
                    start++;
                }

                max = smooth.Max;
                min = smooth.Min;
            }
            else if (track != null && track.Count > 0 && period == 1)
            {
                min = track.Min;
                max = track.Max;
                return track;
            }

            return smooth;
        }

        internal static INumericTimeDataSeries STSmooth(INumericTimeDataSeries data, int seconds, out float min, out float max)
        {
            min = float.NaN;
            max = float.NaN;
            if (data.Count == 0)
            {
                // Special case, no data
                return new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
            }
            else if (data.Count == 1 || seconds < 1)
            {
                // Special case
                INumericTimeDataSeries copyData = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
                min = data[0].Value;
                max = data[0].Value;
                foreach (ITimeValueEntry<float> entry in data)
                {
                    copyData.Add(data.StartTime.AddSeconds(entry.ElapsedSeconds), entry.Value);
                    min = Math.Min(min, entry.Value);
                    max = Math.Max(max, entry.Value);
                }
                return copyData;
            }
            min = float.MaxValue;
            max = float.MinValue;
            int smoothWidth = Math.Max(0, seconds * 2); // Total width/period.  'seconds' is the half-width... seconds on each side to smooth
            int denom = smoothWidth * 2; // Final value to divide by.  It's divide by 2 because we're double-adding everything
            INumericTimeDataSeries smoothedData = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();

            // Loop through entire dataset
            for (int nEntry = 0; nEntry < data.Count; nEntry++)
            {
                ITimeValueEntry<float> entry = data[nEntry];
                // TODO: (LOW)STSmooth: Don't reset value & index markers, instead continue data here...
                double value = 0;
                double delta;
                // Data prior to entry
                long secondsRemaining = seconds;
                ITimeValueEntry<float> p1, p2;
                int increment = -1;
                int pos = nEntry - 1;
                p2 = data[nEntry];


                while (secondsRemaining > 0 && pos >= 0)
                {
                    p1 = data[pos];
                    if (SumValues(p2, p1, ref value, ref secondsRemaining))
                    {
                        pos += increment;
                        p2 = p1;
                    }
                    else
                    {
                        break;
                    }
                }
                if (secondsRemaining > 0)
                {
                    // Occurs at beginning of track when period extends before beginning of track.
                    delta = data[0].Value * secondsRemaining * 2;
                    value += delta;
                }
                // Data after entry
                secondsRemaining = seconds;
                increment = 1;
                pos = nEntry;
                p1 = data[nEntry];
                while (secondsRemaining > 0 && pos < data.Count - 1)
                {
                    p2 = data[pos + 1];
                    if (SumValues(p1, p2, ref value, ref secondsRemaining))
                    {
                        // Move to next point
                        pos += increment;
                        p1 = p2;
                    }
                    else
                    {
                        break;
                    }
                }
                if (secondsRemaining > 0)
                {
                    // Occurs at end of track when period extends past end of track
                    value += data[data.Count - 1].Value * secondsRemaining * 2;
                }
                float entryValue = (float)(value / denom);
                smoothedData.Add(data.StartTime.AddSeconds(entry.ElapsedSeconds), entryValue);
                min = Math.Min(min, entryValue);
                max = Math.Max(max, entryValue);

                // STSmooth: TODO: Remove 'first' p1 & p2 SumValues from 'value'
                if (data[nEntry].ElapsedSeconds - seconds < 0)
                {
                    // Remove 1 second worth of first data point (multiply by 2 because everything is double here)
                    value -= data[0].Value * 2;
                }
                else
                {
                    // Remove data in middle of track (typical scenario)
                    //value -= 
                }
            }
            return smoothedData;
        }

        private static bool SumValues(ITimeValueEntry<float> p1, ITimeValueEntry<float> p2, ref double value, ref long secondsRemaining)
        {
            double spanSeconds = Math.Abs((double)p2.ElapsedSeconds - (double)p1.ElapsedSeconds);
            if (spanSeconds <= secondsRemaining)
            {
                value += (p1.Value + p2.Value) * spanSeconds;
                secondsRemaining -= (long)spanSeconds;
                return true;
            }
            else
            {
                double percent = (double)secondsRemaining / (double)spanSeconds;
                value += (p1.Value * ((float)2 - percent) + p2.Value * percent) * secondsRemaining;
                secondsRemaining = 0;
                return false;
            }
        }

        /// <summary>
        /// Removes paused (but not stopped?) times in track.
        /// </summary>
        /// <param name="sourceTrack">Source data track to remove paused times</param>
        /// <param name="activity"></param>
        /// <returns>Returns an INumericTimeDataSeries with the paused times removed.</returns>
        public static INumericTimeDataSeries RemovePausedTimesInTrack(INumericTimeDataSeries sourceTrack, IActivity activity)
        {
            ActivityInfo activityInfo = ActivityInfoCache.Instance.GetInfo(activity);

            if (activityInfo != null && sourceTrack != null)
            {
                INumericTimeDataSeries result = new NumericTimeDataSeries();

                if (activityInfo.NonMovingTimes.Count == 0)
                {
                    // Remove invalid data nonetheless
                    DateTime currentTime = sourceTrack.StartTime;
                    IEnumerator<ITimeValueEntry<float>> sourceEnumerator = sourceTrack.GetEnumerator();
                    bool sourceEnumeratorIsValid;

                    sourceEnumeratorIsValid = sourceEnumerator.MoveNext();

                    while (sourceEnumeratorIsValid)
                    {
                        if (!float.IsNaN(sourceEnumerator.Current.Value))
                        {
                            result.Add(currentTime, sourceEnumerator.Current.Value);
                        }

                        sourceEnumeratorIsValid = sourceEnumerator.MoveNext();
                        currentTime = sourceTrack.StartTime + new TimeSpan(0, 0, (int)sourceEnumerator.Current.ElapsedSeconds);
                    }
                }
                else
                {
                    DateTime currentTime = sourceTrack.StartTime;
                    IEnumerator<ITimeValueEntry<float>> sourceEnumerator = sourceTrack.GetEnumerator();
                    IEnumerator<IValueRange<DateTime>> pauseEnumerator = activityInfo.NonMovingTimes.GetEnumerator();
                    double totalPausedTimeToDate = 0;
                    bool sourceEnumeratorIsValid;
                    bool pauseEnumeratorIsValid;

                    pauseEnumeratorIsValid = pauseEnumerator.MoveNext();
                    sourceEnumeratorIsValid = sourceEnumerator.MoveNext();

                    while (sourceEnumeratorIsValid)
                    {
                        bool addCurrentSourceEntry = true;
                        bool advanceCurrentSourceEntry = true;

                        // Loop to handle all pauses up to this current track point
                        if (pauseEnumeratorIsValid)
                        {
                            if (currentTime > pauseEnumerator.Current.Lower &&
                                currentTime <= pauseEnumerator.Current.Upper)
                            {
                                addCurrentSourceEntry = false;
                            }
                            else if (currentTime > pauseEnumerator.Current.Upper)
                            {
                                // Advance pause enumerator
                                totalPausedTimeToDate += (pauseEnumerator.Current.Upper - pauseEnumerator.Current.Lower).TotalSeconds;
                                pauseEnumeratorIsValid = pauseEnumerator.MoveNext();

                                // Make sure we retry with the next pause
                                addCurrentSourceEntry = false;
                                advanceCurrentSourceEntry = false;
                            }
                        }

                        if (addCurrentSourceEntry && !float.IsNaN(sourceEnumerator.Current.Value))
                        {
                            DateTime entryTime = currentTime - new TimeSpan(0, 0, (int)totalPausedTimeToDate);

                            result.Add(entryTime, sourceEnumerator.Current.Value);
                        }

                        if (advanceCurrentSourceEntry)
                        {
                            sourceEnumeratorIsValid = sourceEnumerator.MoveNext();
                            currentTime = sourceTrack.StartTime + new TimeSpan(0, 0, (int)sourceEnumerator.Current.ElapsedSeconds);
                        }
                    }
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// Temporary output routine.  Eventually should be deleted.
        /// </summary>
        /// <param name="track"></param>
        internal static void ExportTrack(INumericTimeDataSeries track, string name)
        {
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(name);

                // Write Header
                writer.WriteLine("Seconds, Value");

                foreach (ITimeValueEntry<float> item in track)
                {
                    // Write data
                    writer.WriteLine(item.ElapsedSeconds + ", " + item.Value);
                }

                writer.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Open a popup treelist.
        /// </summary>
        /// <typeparam name="T">The type of items to be listed</typeparam>
        /// <param name="theme">Visual Theme</param>
        /// <param name="items">Items to be listed</param>
        /// <param name="control">The control that the list will appear attached to</param>
        /// <param name="selected">selected item</param>
        /// <param name="selectHandler">Handler that will handle when an item is clicked</param>
        internal static void OpenListPopup<T>(ITheme theme, IList<T> items, System.Windows.Forms.Control control, T selected, TreeListPopup.ItemSelectedEventHandler selectHandler)
        {
            TreeListPopup popup = new TreeListPopup();
            popup.ThemeChanged(theme);
            popup.Tree.Columns.Add(new TreeList.Column());
            popup.Tree.RowData = items;
            if (selected != null)
            {
                popup.Tree.Selected = new object[] { selected };
            }

            popup.ItemSelected += delegate(object sender, TreeListPopup.ItemSelectedEventArgs e)
            {
                if (e.Item is T)
                {
                    selectHandler((T)e.Item, e);
                }
            };
            popup.Popup(control.Parent.RectangleToScreen(control.Bounds));
        }

        /// <summary>
        /// Open a context menu.
        /// </summary>
        /// <param name="theme">Visual Theme</param>
        /// <param name="items">Items to be listed</param>
        /// <param name="mouse"></param>
        /// <param name="selectHandler">Handler that will handle when an item is clicked</param>
        internal static void OpenContextPopup(ITheme theme, ToolStripItemCollection items, MouseEventArgs mouse, ToolStripItemClickedEventHandler selectHandler)
        {
            ContextMenuStrip menuStrip = new ContextMenuStrip();

            menuStrip.Items.AddRange(items);

            menuStrip.ItemClicked += delegate(object sender, ToolStripItemClickedEventArgs e)
            {
                selectHandler(e.ClickedItem, e);
            };
            menuStrip.Show(mouse.Location);
        }

        /// <summary>
        /// Formats a timespan into hh:mm:ss format.
        /// </summary>
        /// <param name="span">Timespan</param>
        /// <returns>hh:mm:ss formatted string (omits hours if less than 1 hour).  Returns empty string if timespan = 0.</returns>
        internal static string ToTimeString(TimeSpan span)
        {
            if (span == TimeSpan.Zero)
            {
                // Return empty if zero.
                return string.Empty;
            }

            string displayTime;

            if ((span.Days * 24) + span.Hours > 0)
            {
                // Hours & minutes
                displayTime = ((span.Days * 24) + span.Hours).ToString("#0") + ":" +
                              span.Minutes.ToString("00") + ":";
            }
            else if (span.Minutes < 10)
            {
                // Single digit minutes
                displayTime = span.Minutes.ToString("#0") + ":";
            }
            else
            {
                // Double digit minutes
                displayTime = span.Minutes.ToString("00") + ":";
            }

            displayTime = displayTime +
                          span.Seconds.ToString("00");

            return displayTime;
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        internal static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return constructedString;
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        internal static byte[] StringToUTF8ByteArray(string pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        /// <summary>
        /// Get an icon from an image
        /// </summary>
        /// <param name="image">Icon image (bitmap)</param>
        internal static Icon GetIcon(Image image)
        {
            Bitmap bitmap = image as Bitmap;

            if (bitmap != null)
            {
                return Icon.FromHandle(bitmap.GetHicon());
            }

            return null;
        }

        /// <summary>
        /// Initialize all utility values.  Required for exmaple when opening a new logbook.
        /// </summary>
        internal static void Initialize()
        {
            categoryIndex = null;
        }
    }
}
