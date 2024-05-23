using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Random_String_Generator.Helpers
{
    public class RunningAndEmptyToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values.Length == 2 && values[0] is bool isRunning && values[1] is int count)
                {
                    return isRunning && count == 0 ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    throw new ArgumentException("Invalid values for conversion");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Conversion error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return Visibility.Collapsed;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
