using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingTime.Library.TimeTracking {
    /// <summary>
    /// Representation of week. List of days from Monday to Sunday.
    /// </summary>
    public class WeekTracking {
        /// <summary>
        /// List of days in this week.
        /// </summary>
        public IEnumerable<DayTracking> Days { get; private set; }

        /// <summary>
        /// Creates a new instance of the  <see cref="WeekTracking"/> class.
        /// </summary>
        public WeekTracking(IEnumerable<DayTracking> days) {
            Days = days;
        }
    }
}
