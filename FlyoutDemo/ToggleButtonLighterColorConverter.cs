using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FlyoutDemo
{
    public class ToggleButtonLighterColorConverter : IValueConverter
    {
        //https://stackoverflow.com/questions/13126224/lighten-background-color-on-button-click-per-binding-with-converter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = value as SolidColorBrush;
            if (brush == null) return null;
            return new SolidColorBrush(ChangeLightness(brush.Color, 0.8f));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public Color ChangeLightness(Color color, float coef)
        {
            return Color.FromArgb(byte.MaxValue, (byte)(color.R * coef), (byte)(color.G * coef),(byte)(color.B * coef));
        }
    }
}
