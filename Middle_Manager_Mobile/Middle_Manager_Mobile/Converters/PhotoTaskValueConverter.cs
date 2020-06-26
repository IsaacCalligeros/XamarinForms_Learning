using SharedClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Middle_Manager_Mobile.Converters
{
    public class PhotoTaskValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is Enum)
            {
                if ((Enums.TaskValue)value == Enums.TaskValue.Photo)
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
