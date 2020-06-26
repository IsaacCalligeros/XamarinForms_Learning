using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SharedClassLibrary.Helpers
{
    public static class SharedExtensions
    {
        public static int CalculateAge(this DateTime theDateTime)
        {
            var Age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(Age) > DateTime.Today)
                Age--;

            return Age;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }
    }
}
