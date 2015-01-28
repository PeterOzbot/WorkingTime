using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingTime.Library.Calendar {
    /// <summary>
    /// Offers calendar data about specific year
    /// </summary>
    public class Calendar {
        private Dictionary<int, Year> _years = new Dictionary<int, Year>();

        /// <summary>
        /// Returns specific year
        /// </summary>
        public Year this[int index] {
            get {
                if (!_years.ContainsKey(index)) {
                    _years.Add(index, new Year(index));
                }
                return _years[index];
            }
        }


        /// <summary>
        /// Initializes calendar class
        /// </summary>
        public Calendar() { }
    }
}
