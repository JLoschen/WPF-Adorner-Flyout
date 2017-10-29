﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FlyoutDemo.Flyouts.Converters
{
    public class LocationToHandleHorizontalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var location = (FlyoutPlacement)value;
            switch (location)
            {
                case FlyoutPlacement.TopLeft:
                case FlyoutPlacement.BottomLeft:
                    return HorizontalAlignment.Right;
                case FlyoutPlacement.TopRight:
                case FlyoutPlacement.BottomRight:
                    return HorizontalAlignment.Left;
            }
            return HorizontalAlignment.Stretch;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
