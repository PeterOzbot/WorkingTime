using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTime.Library.Framework;

namespace WorkingTime.Library.Data {
    /// <summary>
    /// Used to load or save the day data
    /// </summary>
    public class DayDataHandler {
        private Dictionary<DateTime, DayData> _dayDatas;


        /// <summary>
        /// Creates a new instance of the  <see cref="DayDataHandler"/> class.
        /// </summary>
        public DayDataHandler() {
            _dayDatas = new Dictionary<DateTime, DayData>();
            Initialize();
        }


        /// <summary>
        /// Loads data for all days..
        /// </summary>
        private void Initialize() {
            XMLReaderWriter<List<DayData>> reader = new XMLReaderWriter<List<DayData>>("DayData.xml", "AppData");
            List<DayData> dayDatas = reader.Read();
            if (dayDatas != null) {
                foreach (DayData dayData in dayDatas) {
                    _dayDatas.Add(dayData.Date.Date, dayData);
                }
            }
        }

        /// <summary>
        /// Returns meta-data for specific day.
        /// </summary>
        public DayData Get(DateTime date) {
            if (_dayDatas.ContainsKey(date.Date)) {
                return _dayDatas[date.Date];
            }
            else {
                DayData dayData = DayData.Empty(date.Date);
                _dayDatas.Add(date.Date, dayData);

                return dayData;
            }
        }

        /// <summary>
        /// Saves the days
        /// </summary>
        public void Save() {
            // get day data
            List<DayData> dayData = _dayDatas.Values.ToList();

            // save
            XMLReaderWriter<List<DayData>> writer = new XMLReaderWriter<List<DayData>>("DayData.xml", "AppData");
            writer.Save(dayData);
        }
    }
}
