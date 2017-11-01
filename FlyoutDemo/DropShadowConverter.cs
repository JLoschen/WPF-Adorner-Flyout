using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace FlyoutDemo
{
    public class DropShadowConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var x = new DropShadowEffect
            {
                BlurRadius = 30,
                ShadowDepth = 5
            };
            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
