using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTime.Library.Calendar;
using WorkingTime.Library.Data;

namespace WorkingTime.Library.TimeTracking {
    /// <summary>
    /// Day logic for the tracking
    /// </summary>
    public class DayTracking {
        /// <summary>
        /// Calendar day
        /// </summary>
        public Day Day { get; private set; }

        /// <summary>
        /// INformation about a day
        /// </summary>
        public DayData Data { get; private set; }

        /// <summary>
        /// Mata-datum about a day
        /// </summary>
        public DayMetadatum Metadatum { get; private set; }



        /// <summary>
        /// Creates a new instance of the  <see cref="DayTracking"/> class.
        /// </summary>
        public DayTracking(Day day, DataProvider dataProvider) {
            Day = day;
            Data = dataProvider.GetDayData(day);
            Metadatum = dataProvider.GetDayMetadatum(day);

            // update working on data from metadatum
            if (!Metadatum.Working) { Data.Working = false; }
        }
    }
}
