using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTime.Library.TimeTracking;

namespace WorkingTime.Library.Statistics {
    /// <summary>
    /// Calculates the statics from working data
    /// </summary>
    public class Calculator : INotifyPropertyChanged {
        private const double ObligatoryHoursPerDay = 8;

        private MonthTracking _monthTracking;

        private double _averageHoursDay;
        private double _surplus;

        /// <summary>
        /// Average hours per day.
        /// </summary>
        public double AverageHoursDay { get { return _averageHoursDay; } private set { _averageHoursDay = value; OnPropertyChanged("AverageHoursDay"); } }
        /// <summary>
        /// Surplus of hours. Can be positive or negative.
        /// </summary>
        public double Surplus { get { return _surplus; } private set { _surplus = value; OnPropertyChanged("Surplus"); } }



        /// <summary>
        /// Creates a new instance of the  <see cref="Calculator"/> class.
        /// </summary>
        public Calculator(MonthTracking month) {
            _monthTracking = month;

            // do the initial calculations
            Calculate();
        }


        /// <summary>
        /// Calculates the stats
        /// </summary>
        public void Calculate() {
            double hours = 0;
            double numberOfDays = 0;

            // loop through all days and calculate values
            foreach (DayTracking dayTracking in _monthTracking) {
                // do not calculate data from dates in the future
                if (dayTracking.Day.Date.Date > DateTime.UtcNow.Date) { break; }


                if (dayTracking.Data.Working) {
                    numberOfDays++;
                }
                hours += dayTracking.Data.Time;
            }

            AverageHoursDay = Math.Round(hours / numberOfDays, 2);
            Surplus = hours - (numberOfDays * ObligatoryHoursPerDay);
        }


        /// <summary>
        /// PropertyChanged executing helper method.
        /// </summary>
        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
