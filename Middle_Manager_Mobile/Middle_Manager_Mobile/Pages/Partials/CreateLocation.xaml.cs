using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Middle_Manager_Mobile.ViewModels;
using SharedClassLibrary;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Pages.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateLocation : ContentPage
    {
        private ManageLocationsViewModel ViewModel => vm ?? (vm = BindingContext as ManageLocationsViewModel);
        private ManageLocationsViewModel vm;

        public CreateLocation(Location location = null)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);

            if (location != null)
            {
                UserSelectionStack.IsVisible = true;
                ViewModel.EditableLocation = location;
                CreateButton.Text = "Update";
            }
        }

        protected override async void OnAppearing()
        {
            ViewModel.IsLoading = true;
            if(ViewModel.EditableLocation != null)
                ViewModel.LocationUsersList = await ViewModel.GetLocationUsers(ViewModel.EditableLocation.LocationId);

            ViewModel.UsersList = await ViewModel.GetUsers();

            LocationUsersListView.ItemsSource = ViewModel.LocationUsersList;
            UsersListView.ItemsSource = ViewModel.UsersList.Where(u => 
                !ViewModel.LocationUsersList.Select(uid => uid.UserId).Contains(u.UserId));
            ViewModel.IsLoading = false;
        }

        async void OnCreateButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel.EditableLocation != null)
            {
                //Ugly but just tempo would like to move all CRUD operations onto a ViewModel entitiy.
                var editedLocation = ViewModel.EditableLocation;
                editedLocation.Name = nameEntry.Text;
                await ViewModel.UpdateLocation(editedLocation);
                await Navigation.PopAsync();
            }
            else
            {
                var location = new Location
                {
                    Name = nameEntry.Text,
                    OrganisationId = 1,
                };

                var res = await ViewModel.CreateLocation(location);
                await Navigation.PopAsync();
            }
        }

        private async void CreateLocationUser(object sender, EventArgs e)
        {
            var user = (UserForDetailedDto)((Button)sender).BindingContext;
            var locationUser = new LocationUser()
            {
                LocationId = ViewModel.EditableLocation.LocationId,
                UserId = user.UserId,
                Permissions = 0
            };
            await ViewModel.CreateLocationUser(locationUser);
            OnAppearing();
        }

        private async void RemoveLocationUser(object sender, EventArgs e)
        {
            var user = (LocationUser)((Button)sender).BindingContext;
            var confirmed = await DisplayAlert(SharedResources.Confirm,
                String.Format(SharedResources.DeleteItemConfirmation, user.User.UserName),
                SharedResources.Yes, SharedResources.No);
            if (confirmed)
            {
                await ViewModel.DeleteLocationUser(user.UserId, ViewModel.EditableLocation.LocationId);
                ViewModel.UsersList = await ViewModel.GetUsers();
                OnAppearing();
            }
            else
            {
                //Nothing
            }

        }
    }
}