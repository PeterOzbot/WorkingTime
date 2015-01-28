using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTime.Library.Framework;

namespace WorkingTime.Library.Data {
    /// <summary>
    /// Holds information about the specific day.
    /// </summary>
    public class DayData : INotifyPropertyChanged {
        private Command _switchWorkingCommand;
        private bool _working;
        private double _time;

        /// <summary>
        /// Flag if day is working.
        /// </summary>
        public bool Working { get { return _working; } set { _working = value; OnPropertyChanged("Working"); } }
        /// <summary>
        /// Number of hours worked.
        /// </summary>
        public double Time { get { return _time; } set { _time = value; OnPropertyChanged("Time"); } }
        /// <summary>
        /// Command to switch Working flag.
        /// </summary>
        public Command SwitchWorkingCommand { get { return _switchWorkingCommand; } }
        /// <summary>
        /// Date to which this day data belongs.
        /// </summary>
        public DateTime Date { get; set; }


        /// <summary>
        /// Creates a new instance of the  <see cref="DayData"/> class.
        /// </summary>
        public DayData() {
            _switchWorkingCommand = new Command(SwitchWorking, null);
        }
        /// <summary>
        /// Creates a new instance of the  <see cref="DayData"/> class.
        /// </summary>
        public DayData(DateTime date)
            : this() {
            Date = date;
        }


        /// <summary>
        /// Switches the Working flag
        /// </summary>
        private void SwitchWorking() {
            Working = !Working;
        }




        /// <summary>
        /// PropertyChanged executing helper method.
        /// </summary>
        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;



        /// <summary>
        /// Creates empty/default DayData. Working flag is set depending if day is weekend or not.
        /// </summary>
        public static DayData Empty(DateTime date) {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) {
                return new DayData(date) { Working = false, Time = 0 };
            }
            else {
                return new DayData(date) { Working = true, Time = 0 };
            }
        }
    }
}
