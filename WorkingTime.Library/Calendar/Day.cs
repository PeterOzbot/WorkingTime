using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingTime.Library.Calendar {
    /// <summary>
    /// Represents a single day
    /// </summary>
    public class Day {
        /// <summary>
        /// Date for this specific day
        /// </summary>
        public DateTime Date { get; private set; }


        /// <summary>
        /// Creates a day
        /// </summary>
        public Day(int year, int month, int day) {
            Date = new DateTime(year, month, day);
        }
    }
}
