using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel ViewModel => vm ?? (vm = BindingContext as SettingsViewModel);
        private SettingsViewModel vm;

        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void LogoutClicked(object sender, EventArgs e)
        {
            await ViewModel.Logout();
            await Navigation.PushAsync(new LoginPage());
        }
    }
}