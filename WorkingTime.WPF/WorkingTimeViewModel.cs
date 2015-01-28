using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingTime.Library;
using WorkingTime.Library.Calendar;
using WorkingTime.Library.Framework;
using WorkingTime.Library.TimeTracking;

namespace WorkingTime.WPF {
    /// <summary>
    /// Main view model for WorkingTime GUI
    /// </summary>
    public class WorkingTimeViewModel : INotifyPropertyChanged {
        private Calendar _calendar;
        private int _currentMonth = 1;
        private Command<MoveDirection> _moveMonthCommand;
        private Command _exitCommand;
        private MainWindow _window;

        private Task _exitingTask;

        /// <summary>
        /// Current year
        /// </summary>
        public Year CurrentYear { get { return _calendar[2015]; } }
        /// <summary>
        /// Month displayed.
        /// </summary>
        public MonthTracking CurrentMonth {
            get {
                return MonthTrackingManager.Instance.Get(CurrentYear[_currentMonth]);
            }
        }
        /// <summary>
        /// Command used to move current month to next one or previous one.
        /// </summary>
        public Command<MoveDirection> MoveMonthCommand { get { return _moveMonthCommand; } }
        /// <summary>
        /// Command used to exit the application.
        /// </summary>
        public Command ExitCommand { get { return _exitCommand; } }



        /// <summary>
        /// Create view model
        /// </summary>
        public WorkingTimeViewModel(MainWindow window) {
            _calendar = new Calendar();
            _moveMonthCommand = new Command<MoveDirection>(MoveMonth, CanMove, (obj) => { return (MoveDirection) obj; });
            _exitCommand = new Command(Exit, null);
            _window = window;


            // init startup month
            _currentMonth = DateTime.UtcNow.Month;
        }



        /// <summary>
        /// Moves the current month in specific direction.
        /// </summary>
        private void MoveMonth(MoveDirection direction) {
            if (direction == MoveDirection.Backward) {
                _currentMonth--;
            }
            else if (direction == MoveDirection.Forward) {
                _currentMonth++;
            }
            OnPropertyChanged("CurrentMonth");
            _moveMonthCommand.OnCanExecuteChanged();
        }

        /// <summary>
        /// Checks if month can be moved in specific direction.
        /// </summary>
        private bool CanMove(MoveDirection direction) {
            if (direction == MoveDirection.Backward) {
                return _currentMonth > 1;
            }
            else if (direction == MoveDirection.Forward) {
                return _currentMonth < 12;
            }
            else {
                throw new ArgumentOutOfRangeException("MoveDirection not supported.");
            }
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        private void Exit() {
            if (_exitingTask == null) {
                _exitingTask = Task.Factory.StartNew(() => {

                    // start saving
                    MonthTrackingManager.Instance.Save();

                }).ContinueWith((previousTask) => {

                    // close the window
                    _window.Close();

                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
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

    /// <summary>
    /// Available directions to for moving which is current month.
    /// </summary>
    public enum MoveDirection {
        /// <summary>
        /// Move to the next one.
        /// </summary>
        Forward,
        /// <summary>
        /// Move to the previous one.
        /// </summary>
        Backward
    }
}
