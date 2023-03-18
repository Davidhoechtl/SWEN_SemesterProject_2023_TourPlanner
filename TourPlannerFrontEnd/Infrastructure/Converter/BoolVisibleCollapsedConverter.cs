
namespace TourPlannerFrontEnd.Infrastructure.Converter
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    internal class BoolVisibleCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is not bool)
            {
                throw new ArgumentException($"The Value for the {nameof(BoolVisibleCollapsedConverter)} is not a bool");
            }

            bool convertedValue = (bool)value;
            if(convertedValue)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
