using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Services;
using Middle_Manager_Mobile.Services.Interfaces;
using Middle_Manager_Mobile.ViewModels;
using SharedClassLibrary;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Pages.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageUsers : ContentPage
    {
        private ManageUsersViewModel ViewModel => vm ?? (vm = BindingContext as ManageUsersViewModel);
        private ManageUsersViewModel vm;

        public ManageUsers()
        {
            InitializeComponent();
            BindingContext = new ManageUsersViewModel();
        }

        protected override async void OnAppearing()
        {
            ViewModel.UsersList = await ViewModel.GetUsers();
            UsersList.ItemsSource = ViewModel.UsersList;
        }

        private void AddUsers_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InviteNewUserPage());
        }

        private void EditUser(object sender, EventArgs e)
        {
            var user = (UserForDetailedDto)((Button)sender).BindingContext;
            Navigation.PushAsync(new InviteNewUserPage(user));
        }

        private async void DeleteUser(object sender, EventArgs e)
        {
            var user = (UserForDetailedDto)((Button)sender).BindingContext;
            var confirmed = await DisplayAlert(SharedResources.Confirm, 
                String.Format(SharedResources.DeleteItemConfirmation, user.UserName), 
                SharedResources.Yes, SharedResources.No);
            if (confirmed)
            {
                await ViewModel.DeleteUser(user.UserId);
                ViewModel.UsersList = await ViewModel.GetUsers();
                UsersList.ItemsSource = ViewModel.UsersList;
            }
            else
            {
                //Nothing
            }
        }
    }
}