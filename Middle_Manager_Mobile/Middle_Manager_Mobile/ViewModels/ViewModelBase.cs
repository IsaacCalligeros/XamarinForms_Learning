using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using SharedClassLibrary;
using Xamarin.Forms;

namespace Middle_Manager_Mobile.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        protected INavigation Navigation { get; }
        protected Page MainPage => Application.Current.MainPage;
        protected readonly IUserStorage UserDetails;

        public bool AppearingRan { get; set; }


        public ViewModelBase(INavigation navigation = null)
        {
            Navigation = navigation;
            UserDetails = UserStorage.Current;
            AppearingRan = false;
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;

            backingField = value;

            OnPropertyChanged(propertyName);
        }
    }
}
