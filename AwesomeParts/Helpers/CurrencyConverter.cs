using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Text.RegularExpressions;

namespace AwesomeParts.Helpers
{
    public class CurrencyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var price = value as decimal?;

            if (price.HasValue)
            {
                return String.Format("{0:C2}", price.Value);
            }
            else
                return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = value as string;
            if (input != null)
                return decimal.Parse(input.Substring(0, input.Length - 3));
            else
                return 0m;
        }
    }
}
