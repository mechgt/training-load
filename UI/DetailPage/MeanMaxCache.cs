using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace TrainingLoad.UI.DetailPage
{
    static class MeanMaxCache
    {
        #region Fields

        private static SortedList<string, INumericTimeDataSeries> tracks = new SortedList<string, INumericTimeDataSeries>();

        #endregion

        /// <summary>
        /// Gets the cached track if available, or calculates from scratch if not available.
        /// </summary>
        /// <param name="activity">Activity to calculate</param>
        /// <param name="chartType">Which track to calculate</param>
        /// <returns>Cached track if available, or empty track if not found.</returns>
        internal static INumericTimeDataSeries GetTrack(IActivity activity, Common.ChartBasis chartType)
        {
            if (activity == null || chartType == null)
            {
                return new NumericTimeDataSeries();
            }

            string id = activity.ReferenceId + chartType;

            if (tracks.ContainsKey(id) && false)
            {
                // Returned cached value from memory :)
                return tracks[id];
            }
            else
            {
                // Not in cache, create a new mean-max track :(
                INumericTimeDataSeries track = new NumericTimeDataSeries();

                switch (chartType)
                {
                    case Common.ChartBasis.HR:
                        {
                            track = GetMeanMaxTrack(activity.HeartRatePerMinuteTrack);
                            break;
                        }

                    case Common.ChartBasis.Power:
                        {
                            track = GetMeanMaxTrack(activity.PowerWattsTrack);
                            break;
                        }

                    case Common.ChartBasis.Cadence:
                        {
                            track = GetMeanMaxTrack(activity.CadencePerMinuteTrack);
                            break;
                        }
                }

                // Add to cache for next time
                MeanMaxCache.AddTrack(track, id);

                return track;
            }
        }

        /// <summary>
        /// Adds track to cache for later recall.  Replaces cache with new 
        /// data track, if track already exists in cache.
        /// </summary>
        /// <param name="track">Data track to cache</param>
        /// <param name="refId">Unique Id of track</param>
        internal static void AddTrack(INumericTimeDataSeries track, string refId)
        {
            if (tracks.ContainsKey(refId))
            {
                tracks.Remove(refId);
            }

            tracks.Add(refId, track);
        }

        /// <summary>
        /// Calculate Mean-Max track from scratch
        /// </summary>
        /// <param name="source">Data track to calculate</param>
        /// <returns></returns>
        private static INumericTimeDataSeries GetMeanMaxTrack(INumericTimeDataSeries source)
        {
            INumericTimeDataSeries track = new NumericTimeDataSeries();

            float previous = 0;

            if (source != null && source.Count > 0)
            {
                track.AllowMultipleAtSameTime = true;

                float min, max;
                uint maxCalc = source.TotalElapsedSeconds - 1;
                maxCalc = maxCalc / 2;

                // Add the 'first' point.  This is necessary to align all other points with start.
                DateTime start = DateTime.Now.Date;
                track.Add(start, source.Max);


                for (uint period = maxCalc; period >= 1; )
                {
                    DateTime calcStart = DateTime.Now;
                    // Progress status
                    float percent = (((float)period / (float)maxCalc) - 1F) * -100F;
                    Application.DoEvents();

                    // NOTE: Smoothing is # seconds on each side FOR ST ALGO... not total seconds (i.e. '30' would be smoothing over 60 total seconds... 30 on each side)
                    //INumericTimeDataSeries smooth = ZoneFiveSoftware.Common.Data.Algorithm.NumericTimeDataSeries.Smooth(source, period, out min, out max);
                    //INumericTimeDataSeries smooth = Utilities.STSmooth(source, period, out min, out max);
                    //Utilities.ExportTrack(smooth, "C:\\Smooth\\" + period + "_ST.csv");
                    INumericTimeDataSeries smooth = Utilities.Smooth(source, period * 2, out min, out max);
                    //Utilities.ExportTrack(smooth, "C:\\Smooth\\" + period + "_TL.csv");

                    // Add current point to data track
                    max = Math.Max(max, previous);
                    track.Add(start.AddSeconds(period * 2), max);
                    //track.Add(start.AddSeconds(period), max);
                    period = (uint)Math.Min(period - 1, (float)period / 1.07);
                    previous = max;

                    System.Diagnostics.Debug.WriteLine("Period: " + period * 2 + ", " + (DateTime.Now - calcStart).TotalSeconds);
                }
            }

            // Return populated data track, or new (empty) data track if appropriate
            return track;
        }


        /// <summary>
        /// Clear a particular track from the cache.
        /// </summary>
        /// <param name="refId"></param>
        internal static void ClearTrack(string refId)
        {
            if (tracks.ContainsKey(refId))
            {
                tracks.Remove(refId);
            }
        }
    }
}
