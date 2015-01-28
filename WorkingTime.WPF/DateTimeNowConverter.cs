using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WorkingTime.WPF {
    public class DateTimeNowConverter : IValueConverter {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            try {
                DateTime dateTime = System.Convert.ToDateTime(value);
                return dateTime.Date.Equals(DateTime.Now.Date);
            }
            catch { }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }

        #endregion
    }
}
