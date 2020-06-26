using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Middle_Manager_Mobile.ViewModels;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Pages.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLocationUser : ContentPage
    {
        private LocationDetailsViewModel ViewModel => vm ?? (vm = BindingContext as LocationDetailsViewModel);
        private LocationDetailsViewModel vm;

        public ObservableCollection<Location> LocationsList { get; set; }

        public AddLocationUser(Location location)
        {
            InitializeComponent();
            if (location != null)
            {
                ViewModel.SelectedLocation = location;
            }
        }

        protected override async void OnAppearing()
        {
            ViewModel.LocationUsersList = await ViewModel.GetLocationUsers(ViewModel.SelectedLocation.LocationId);
            ViewModel.UsersList = await ViewModel.GetUsers();
            UsersListView.ItemsSource = ViewModel.UsersList;

        }

        private async void CreateLocationUser(object sender, EventArgs e)
        {
            var user = (UserForDetailedDto)((Button)sender).BindingContext;
            var locationUser = new LocationUser()
            {
                LocationId = ViewModel.SelectedLocation.LocationId,
                UserId = user.UserId,
                Permissions = 0
            };
            await ViewModel.CreateLocationUser(locationUser);
            //ViewModel.UsersList = await ViewModel.GetUsers();
            //UsersList.ItemsSource = ViewModel.UsersList;
        }

        private async void RemoveLocationUser(object sender, EventArgs e)
        {
            var user = (UserForDetailedDto)((Button)sender).BindingContext;
            await ViewModel.DeleteLocationUser(ViewModel.EditableLocation.LocationId, user.UserId);
            //ViewModel.UsersList = await ViewModel.GetUsers();
            //UsersList.ItemsSource = ViewModel.UsersList;
        }
    }
}
