using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTime.Library.Calendar;

namespace WorkingTime.Library.Data {
    /// <summary>
    /// Provides the data.
    /// </summary>
    public class DataProvider {
        private DayMetadataHandler _dayMetadataHandler;
        private DayDataHandler _dayDataHandler;


        /// <summary>
        /// Creates a new instance of the  <see cref="DataProvider"/> class.
        /// </summary>
        public DataProvider() {
            _dayMetadataHandler = new DayMetadataHandler();
            _dayDataHandler = new DayDataHandler();
        }



        /// <summary>
        /// Returns day data created from the day.
        /// </summary>
        public DayData GetDayData(Day day) {
            return _dayDataHandler.Get(day.Date);
        }

        /// <summary>
        /// Returns day matadatum created from the day.
        /// </summary>
        public DayMetadatum GetDayMetadatum(Day day) {
            return _dayMetadataHandler.Get(day.Date.Date);
        }

        /// <summary>
        /// Saves the day's data.
        /// </summary>
        /// <param name="dayDatas"></param>
        public void SaveDayData() {
            _dayDataHandler.Save();
        }
    }
}
