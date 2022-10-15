using System;
using System.Globalization;
using UGB.Paysa.UI.Assets;
using Xamarin.Forms;

namespace UGB.Paysa.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var resource = AssetsHelper.GetImageNamespace(value.ToString());
            return ImageSource.FromResource(resource);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


