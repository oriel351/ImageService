using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;

namespace GUI.View
{
    class LogColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            // throws an exception if its not a brush
            if (targetType.Name != "Brush")
            {
                throw new InvalidOperationException("should be converted to a brush");
            }

            //Checks if it is info, warning or fail
            string type_value = value.ToString();
            if (type_value.Equals("INFO"))
            {
                return Brushes.LawnGreen;
            }
            else if (type_value.Equals("WARNING"))
            {
                return Brushes.LightYellow;
            }
            else if (type_value.Equals("FAIL"))
            {
                return Brushes.DarkRed;
            }
            else
            {
                return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
