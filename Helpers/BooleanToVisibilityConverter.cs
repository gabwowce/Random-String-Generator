using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Random_String_Generator.Helpers
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is bool booleanValue)
                {
                    return booleanValue ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    throw new ArgumentException("Value is not a boolean");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Conversion error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
