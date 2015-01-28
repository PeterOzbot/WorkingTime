using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkingTime.Library.Framework;

namespace WorkingTime.Library.Data {
    //TODO: implement dinmic holiday generation for 
    //    -    velikonočna nedelja, velikonočni ponedeljek
    //-     binkoštna nedelja - binkošti

    /// <summary>
    /// Meta-data handler, loads day's meta-datum
    /// </summary>
    public class DayMetadataHandler {
        private Dictionary<DateTime, DayMetadatum> _holidays;


        /// <summary>
        /// Creates a new instance of the  <see cref="DayMetadataHandler"/> class.
        /// </summary>
        public DayMetadataHandler() {
            _holidays = new Dictionary<DateTime, DayMetadatum>();
            Initialize();
        }


        /// <summary>
        /// Returns meta-data for specific day.
        /// </summary>
        public DayMetadatum Get(DateTime date) {
            if (_holidays.ContainsKey(date.Date)) {
                return _holidays[date.Date];
            }
            else {
                return DayMetadatum.Empty(date.Date);
            }
        }

        /// <summary>
        /// Loads meta-data for all days..
        /// </summary>
        private void Initialize() {
            XMLReaderWriter<List<DayMetadatum>> reader = new XMLReaderWriter<List<DayMetadatum>>("DayMetadata.xml", "AppData");
            List<DayMetadatum> metadata = reader.Read();
            if (metadata != null) {
                foreach (DayMetadatum metadatum in metadata) {
                    _holidays.Add(metadatum.Date.Date, metadatum);
                }
            }
        }
    }
}
