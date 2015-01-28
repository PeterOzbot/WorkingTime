using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkingTime.Library.Framework {
    /// <summary>
    /// Implementation of the ICommand without the parameter.
    /// </summary>
    public class Command : Command<object> {
        /// <summary>
        /// Creates a new instance of the  <see cref="Command"/> class.
        /// </summary>
        public Command(Action execute, Func<bool> canExecute)
            : base((obj) => {
                if (execute != null) { execute(); }
            }, (obj) => {
                if (canExecute != null) { return canExecute(); }
                else { return true; }
            }, (obj) => {
                return obj;
            }) {
        }
    }
    /// <summary>
    /// Implementation of the ICommand.
    /// </summary>
    public class Command<T> : ICommand {
        private Func<object, T> _converter;
        private Action<T> _execute;
        private Func<T, bool> _canExecute;


        /// <summary>
        /// Creates a new instance of the  <see cref="Command"/> class.
        /// </summary>
        public Command(Action<T> execute, Func<T, bool> canExecute, Func<object, T> converter) {
            _execute = execute;
            _canExecute = canExecute;
            _converter = converter;

        }


        /// <summary>
        /// Method for checking if ExecuteCommand can be executed.
        /// </summary>
        public bool CanExecute(object parameter) {
            if (_canExecute != null) {
                return _canExecute(_converter(parameter));
            }
            else { return true; }
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        public void Execute(object parameter) {
            if (_execute != null && CanExecute(parameter)) {
                _execute(_converter(parameter));
            }
        }

        /// <summary>
        /// When can execute changes this must be called to trigger another check.
        /// </summary>
        public void OnCanExecuteChanged() {
            if (CanExecuteChanged != null) {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
        public event EventHandler CanExecuteChanged;
    }
}
