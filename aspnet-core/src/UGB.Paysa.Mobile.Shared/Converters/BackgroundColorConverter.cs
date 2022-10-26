using System;
using System.Globalization;
using Xamarin.Forms;

namespace UGB.Paysa.Converters
{
    public class BackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && boolValue)
            {
                return (Color)parameter;
            }
            else if (value.GetType() == typeof(string) && (string)value == (string)parameter)
            {
                return Color.FromHex("#708090"); // paysacolor
            }
            else if (value.GetType() == typeof(string))
            {
                return Color.Transparent;
            }
            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}