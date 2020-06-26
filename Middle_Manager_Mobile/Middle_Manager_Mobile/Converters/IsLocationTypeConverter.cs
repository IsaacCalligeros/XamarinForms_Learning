using System;
using System.Collections.Generic;
using System.Text;
using SharedClassLibrary;
using Xamarin.Forms;

namespace Middle_Manager_Mobile.Converters
{
    public class IsLocationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is Enum)
            {
                if ((Enums.TaskTarget)value == Enums.TaskTarget.Location)
                {
                    return true;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return Enum.ToObject(targetType, value);
            }
            return 0;
        }
    }
}
