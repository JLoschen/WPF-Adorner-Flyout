using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FlyoutDemo.Flyouts.Converters
{
    public class LocationToVerticalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var location = (FlyoutPlacement)value;
            switch (location)
            {
                case FlyoutPlacement.TopLeft:
                case FlyoutPlacement.TopRight:
                    return VerticalAlignment.Top;
                case FlyoutPlacement.BottomLeft:
                case FlyoutPlacement.BottomRight:
                    return VerticalAlignment.Bottom;
            }
            return HorizontalAlignment.Stretch;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
