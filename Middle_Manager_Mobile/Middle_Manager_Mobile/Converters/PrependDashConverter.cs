using System;
using System.Collections.Generic;
using System.Text;
using SharedClassLibrary;
using Xamarin.Forms;

namespace Middle_Manager_Mobile.Converters
{
    public class PrependDashConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return String.Format(SharedResources.PrependDashItem, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
