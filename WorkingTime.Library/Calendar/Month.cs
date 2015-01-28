using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingTime.Library.Calendar {
    /// <summary>
    /// Represents information about specific month.
    /// </summary>
    public class Month : IEnumerable<Day> {
        private Day[] _days;

        /// <summary>
        /// Month's user identification
        /// </summary>
        public string Label { get; private set; }
        /// <summary>
        /// Month index of the year
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// Day on specific index in this month
        /// </summary>
        public Day this[int index] {
            get {
                if (index < 1) {
                    throw new ArgumentOutOfRangeException("Index must be 1 or more.");
                }
                if (index > 31) {
                    throw new ArgumentOutOfRangeException("Index can not be more than 31.");
                }
                if (index > _days.Length) {
                    throw new ArgumentOutOfRangeException("There is not so much days in this month.");
                }

                return _days[index - 1];
            }
        }


        /// <summary>
        /// Creates empty month
        /// </summary>
        /// <param name="label"></param>
        /// <param name="daysCount"></param>
        public Month(string label, int index) {
            Label = label;
            Index = index;
        }


        /// <summary>
        /// Fill months with days for specific year
        /// </summary>
        /// <param name="days"></param>
        public void Fill(int yearIndex) {
            // get number of days
            int daysCount = DateTime.DaysInMonth(yearIndex, Index);

            // initialize days array
            _days = new Day[daysCount];

            // create days
            for (int index = 0 ; index < daysCount ; index++) {
                _days[index] = new Day(yearIndex, Index, index + 1);
            }
        }

        #region IEnumerable<Day> Members

        public IEnumerator<Day> GetEnumerator() {
            for (int index = 0 ; index < _days.Length ; index++) {
                yield return _days[index];
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion
    }
}
