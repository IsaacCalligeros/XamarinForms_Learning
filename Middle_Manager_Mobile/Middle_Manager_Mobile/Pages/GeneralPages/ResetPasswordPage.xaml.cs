using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.Services;
using Middle_Manager_Mobile.Services.Interfaces;
using Middle_Manager_Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPage : ContentPage
    {
        private ResetPasswordViewModel ViewModel => vm ?? (vm = BindingContext as ResetPasswordViewModel);
        private ResetPasswordViewModel vm;
        private readonly IUserService _userService;
        private readonly IUserStorage _userStorage;

        public ResetPasswordPage(string emailAddress, int verificationCode)
        {
            InitializeComponent();
            _userService = new UserService();
            BindingContext = new ResetPasswordViewModel(emailAddress, verificationCode);
        }

        private async void OnResetButtonClicked(object sender, EventArgs e)
        {
            if (Password1.Text != Password2.Text)
            {
                await DisplayAlert("Email sent", "Please check your email address for verification code", "OK");
                return;
            }
            else
            {
                await ViewModel.ResetPassword(Password1.Text);
            }
        }
    }
}