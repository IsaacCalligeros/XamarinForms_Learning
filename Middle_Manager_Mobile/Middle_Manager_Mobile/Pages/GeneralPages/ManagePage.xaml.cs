using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Middle_Manager_Mobile.Pages.Partials;
using Middle_Manager_Mobile.ViewModels;
using MvvmHelpers;
using SharedClassLibrary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagePage : ContentPage
    {
        public ManagePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void OnuserButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageUsers());
        }

        async void OnTaskButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageTaskTemplates());
        }

        async void OnLocationButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageLocations());
        }

        async void OnTaskInstanceButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageTaskInstances());
        }
    }
}