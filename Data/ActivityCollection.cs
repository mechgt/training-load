namespace TrainingLoad.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using ZoneFiveSoftware.Common.Data.Fitness;

    public class ActivityCollection : CollectionBase
    {
        public event CollectionChangeEventHandler CollectionChanged;

        public enum CompareType
        {
            StartDate,
            Distance,
            Time,
            Score,
            ActualDistance,
            ActualTime,
            Category
        }

        private ActivityComparer comparer;

        public ActivityCollection()
        {
            comparer = new ActivityComparer(CompareType.StartDate, false);
        }

        /// <summary>
        /// Clone an existing collection to a new object.
        /// </summary>
        /// <param name="list"></param>
        public ActivityCollection(ActivityCollection list)
            : this()
        {
            this.AddRange(list);
            this.comparer.ascending = list.comparer.ascending;
            this.comparer.sortType = list.comparer.sortType;
        }

        public IActivity this[int index]
        {
            get { return (IActivity)this.InnerList[index]; }
            set { this.InnerList[index] = value; }
        }

        public DateTime MinDate
        {
            get
            {
                DateTime date = DateTime.MaxValue;
                foreach (IActivity item in this)
                {
                    if (item.StartTime.Date < date)
                        date = item.StartTime.Date;
                }

                return date;
            }
        }

        public ActivityComparer Comparer
        {
            get { return comparer; }
            set { comparer = value; }
        }

        new public int Count
        {
            get { return this.List.Count; }
        }

        public void Add(IActivity activity)
        {
            this.List.Add(activity);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, activity));
            }
        }

        public void AddRange(ActivityCollection list)
        {
            this.InnerList.AddRange(list);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, list));
            }
        }

        public void AddRange(ILogbookActivityList list)
        {
            foreach (IActivity activity in list)
            {
                Add(activity);
            }
        }

        public void Remove(IActivity activity)
        {
            this.List.Remove(activity);

            if (CollectionChanged != null)
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, activity));
        }

        public void Sort()
        {
            this.Sort(this.comparer);
        }

        public void Sort(ActivityComparer comparer)
        {
            this.InnerList.Sort(comparer);
        }

        public void Sort(CompareType sortType, bool ascending)
        {
            // Update compare type
            this.comparer.sortType = sortType;
            this.comparer.ascending = ascending;

            // Sort list
            this.Sort();
        }

        public bool Contains(object value)
        {
            return this.List.Contains(value);
        }

        public int IndexOf(IActivity activity)
        {
            return List.IndexOf(activity);
        }

        public IActivity GetActivity(string refId)
        {
            foreach (IActivity activity in this)
            {
                if (activity.ReferenceId == refId)
                    return activity;
            }

            return null;
        }

        public IActivity GetActivity(DateTime startTime)
        {
            foreach (IActivity activity in this)
            {
                if (activity.StartTime == startTime || activity.StartTime.Add(activity.TimeZoneUtcOffset) == startTime)
                    return activity;
            }

            return null;
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (!(value is IActivity))
            {
                throw new ArgumentException("Collection only supports IActivity objects");
            }
        }

        #region IList<IActivity> Members


        public void Insert(int index, IActivity item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class ActivityComparer : IComparer, IComparer<IActivity>
    {
        internal ActivityCollection.CompareType sortType = ActivityCollection.CompareType.StartDate;
        internal bool ascending = false;

        public ActivityComparer(ActivityCollection.CompareType sortType, bool ascending)
        {
            this.sortType = sortType;
            this.ascending = ascending;
        }

        #region IComparer<IActivity> Members

        public int Compare(IActivity x, IActivity y)
        {
            int result = 0;

            switch (sortType)
            {
                default:
                case ActivityCollection.CompareType.StartDate:
                    result = x.StartTime.CompareTo(y.StartTime);
                    break;

                case ActivityCollection.CompareType.Distance:
                    result = x.TotalDistanceMetersEntered.CompareTo(y.TotalDistanceMetersEntered);
                    break;

                case ActivityCollection.CompareType.Time:
                    result = x.TotalTimeEntered.CompareTo(y.TotalTimeEntered);
                    break;
            }

            if (result == 0)
                result = x.StartTime.CompareTo(y.StartTime);

            return result;
        }

        #endregion

        #region IComparer Members

        public int Compare(object x, object y)
        {
            IActivity x1 = x as IActivity;
            IActivity y1 = y as IActivity;
            int result = 0;

            if (x1 != null && y1 != null)
                result = Compare(x1, y1);

            if (!ascending)
                result = result * -1;

            return result;
        }

        #endregion
    }


}
