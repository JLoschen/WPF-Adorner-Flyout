using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FlyoutDemo.Flyouts.Converters
{
    public class LocationToHandleVerticalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var location = (FlyoutPlacement)value;
            switch (location)
            {
                case FlyoutPlacement.TopLeft:
                case FlyoutPlacement.TopRight:
                    return VerticalAlignment.Bottom;
                case FlyoutPlacement.BottomLeft:
                case FlyoutPlacement.BottomRight:
                    return VerticalAlignment.Top;
            }
            return HorizontalAlignment.Stretch;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
