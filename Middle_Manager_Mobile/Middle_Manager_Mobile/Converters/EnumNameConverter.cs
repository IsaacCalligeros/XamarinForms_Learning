using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Middle_Manager_Mobile.Converters
{
    public class EnumNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is Enum)
            {
                return (int)value;
            }
            return 0;
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
