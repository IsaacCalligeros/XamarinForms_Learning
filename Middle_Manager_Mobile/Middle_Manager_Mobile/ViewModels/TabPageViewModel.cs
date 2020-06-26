using System;
using System.Collections.Generic;
using System.Text;
using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using SharedClassLibrary;
using Xamarin.Forms;

namespace Middle_Manager_Mobile.ViewModels
{
    public class TabPageViewModel : ViewModelBase
    {
        private readonly INavigation _navigation;

        public bool IsAdmin => UserDetails.UserRole == Enums.UserRole.Admin;

        public TabPageViewModel()
        {

        }

    }
}
