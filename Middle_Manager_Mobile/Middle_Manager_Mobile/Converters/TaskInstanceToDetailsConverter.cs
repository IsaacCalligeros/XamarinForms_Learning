using System;
using System.Collections.Generic;
using System.Text;
using SharedClassLibrary.Models;
using Xamarin.Forms;

namespace Middle_Manager_Mobile.Converters
{
    public class TaskInstanceToDetailsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            var taskInstance = ((TaskInstance)value);
            return (taskInstance.TaskTemplate.Title + taskInstance.AssignedToId);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
