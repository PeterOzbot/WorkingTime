using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTime.Library.Calendar;
using WorkingTime.Library.Data;
using WorkingTime.Library.Framework;
using WorkingTime.Library.Statistics;

namespace WorkingTime.Library.TimeTracking {
    /// <summary>
    /// Month logic for the time tracking
    /// </summary>
    public class MonthTracking : IEnumerable<DayTracking> {
        /// <summary>
        /// List of weeks in this month
        /// </summary>
        public IEnumerable<WeekTracking> Weeks { get; private set; }
        /// <summary>
        /// Calendar month data
        /// </summary>
        public Month Month { get; private set; }
        /// <summary>
        /// Statistic about the month working time
        /// </summary>
        public Calculator StatisticsCalculator { get; private set; }


        /// <summary>
        /// Creates a new instance of the  <see cref="MonthTracking"/> class.
        /// </summary>
        public MonthTracking(Month month, DataProvider dataProvider) {
            Month = month;
            Initialize(dataProvider);
            StatisticsCalculator = new Calculator(this);
        }


        /// <summary>
        /// Splits days into weeks
        /// </summary>
        private void Initialize(DataProvider dataProvider) {
            List<WeekTracking> weeks = new List<WeekTracking>();

            List<DayTracking> week = new List<DayTracking>();

            foreach (Day day in Month) {
                DayTracking dayTracking = new DayTracking(day, dataProvider);

                // hook on changed events
                dayTracking.Data.PropertyChanged += Data_PropertyChanged;

                if (day.Date.DayOfWeek == DayOfWeek.Sunday) {
                    week.Add(dayTracking);
                    weeks.Add(new WeekTracking(week));
                    week = new List<DayTracking>();
                }
                else {
                    week.Add(dayTracking);
                }
            }

            if (week.Count > 0) {
                weeks.Add(new WeekTracking(week));
            }

            Weeks = weeks;
        }

        /// <summary>
        /// When any property of day's data changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            StatisticsCalculator.Calculate();
        }

        #region IEnumerable<DayTracking> Members

        public IEnumerator<DayTracking> GetEnumerator() {
            foreach (WeekTracking week in Weeks) {
                foreach (DayTracking day in week.Days) {
                    yield return day;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        #endregion
    }

    /// <summary>
    /// Manager for months. Holds all months and loads them lazily.
    /// </summary>
    public class MonthTrackingManager {
        private Dictionary<int, MonthTracking> _months;
        private DataProvider _dataProvider;


        /// <summary>
        /// Creates a new instance of the  <see cref="MonthTrackingManager"/> class.
        /// </summary>
        public MonthTrackingManager() {
            _months = new Dictionary<int, MonthTracking>();
            _dataProvider = new DataProvider();
        }



        /// <summary>
        /// Creates or returns existing month.
        /// </summary>
        public MonthTracking Get(Month month) {
            if (!_months.ContainsKey(month.Index)) {
                _months.Add(month.Index, new MonthTracking(month, _dataProvider));
            }
            return _months[month.Index];
        }

        /// <summary>
        /// Saves the data
        /// </summary>
        public void Save() {
            _dataProvider.SaveDayData();
        }


        private static MonthTrackingManager _instance = new MonthTrackingManager();
        /// <summary>
        /// Singleton instance of month manager.
        /// </summary>
        public static MonthTrackingManager Instance { get { return _instance; } }
    }
}
