using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.ViewModels;
using SharedClassLibrary;
using SharedClassLibrary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Pages.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageLocations : ContentPage
    {
        private ManageLocationsViewModel ViewModel => vm ?? (vm = BindingContext as ManageLocationsViewModel);
        private ManageLocationsViewModel vm;

        public ObservableCollection<Location> LocationsList { get; set; }

        public ManageLocations()
        {
            InitializeComponent();
            BindingContext = new ManageLocationsViewModel();
        }

        protected override async void OnAppearing()
        {
            ViewModel.LocationsList = await ViewModel.GetLocations();
            LocationList.ItemsSource = ViewModel.LocationsList;
        }

        private void AddLocations_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateLocation());
        }

        private void EditLocation(object sender, EventArgs e)
        {
            var location = (Location)((Button)sender).BindingContext;
            Navigation.PushAsync(new CreateLocation(location));
        }

        private async void DeleteLocation(object sender, EventArgs e)
        {
            var location = (Location)((Button)sender).BindingContext;
            var confirmed = await DisplayAlert(SharedResources.Confirm,
                String.Format(SharedResources.DeleteItemConfirmation, location.Name),
                SharedResources.Yes, SharedResources.No);
            if (confirmed)
            {
                await ViewModel.DeleteLocation(location.LocationId);
                ViewModel.LocationsList = await ViewModel.GetLocations();
                LocationList.ItemsSource = ViewModel.LocationsList;
            }
            else
            {
                //Nothing
            }
        }
    }
}