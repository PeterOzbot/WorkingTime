using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingTime.Library.Data {
    /// <summary>
    /// Represents days meta datum. Holds additional information about a day.
    /// </summary>
    public class DayMetadatum {
        /// <summary>
        /// Special day label
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// If day is working
        /// </summary>
        public bool Working { get; set; }
        /// <summary>
        /// Type of the day
        /// </summary>
        public DayType Type { get; set; }


        /// <summary>
        /// Creates a new instance of the  <see cref="DayMetadatum"/> class.
        /// </summary>
        public DayMetadatum() { }


        /// <summary>
        /// Creates empty meta-datum.
        /// </summary>
        public static DayMetadatum Empty(DateTime date) { return new DayMetadatum() { Label = String.Empty, Date = date, Working = true, Type = DayType.Normal }; }
    }


    /// <summary>
    /// Type of the day
    /// </summary>
    public enum DayType {
        /// <summary>
        /// Day is a holiday
        /// </summary>
        Holiday,
        /// <summary>
        /// Day is normal working day
        /// </summary>
        Normal
    }
}
