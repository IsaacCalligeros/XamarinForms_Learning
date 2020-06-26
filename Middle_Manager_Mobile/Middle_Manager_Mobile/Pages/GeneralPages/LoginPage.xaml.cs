using Middle_Manager_Mobile.Services;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.Helpers;
using Xamarin.Forms.Internals;

namespace Middle_Manager_Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel ViewModel => vm ?? (vm = BindingContext as LoginViewModel);
        private LoginViewModel vm;

        private readonly IUserStorage _userStorage;

        public LoginPage()
        {
            _userStorage = UserStorage.Current;
            //BackgroundImageSource = FileImageSource.FromFile("drawable/login-background.jpg");
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var res = await ViewModel.Login(ViewModel.UserForLogin);
            if(res)
            {
                await Navigation.PushAsync(new TabPage());
                var loginAndRegisterPages = Navigation.NavigationStack.Where(n => n.GetType() == typeof(LoginPage) || n.GetType() == typeof(RegisterPage)).ToList();
                foreach (var page in loginAndRegisterPages)
                {
                    Navigation.RemovePage(page);
                }
                return;
            }

            FailedLogin();
        }

        void FailedLogin()
        {
            ViewModel.UserForLogin.Password = String.Empty;
            FailedText.Text = "Failed To Login";
        }

        private async void SignUp(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage());
        }

        private async void ForgotPassword(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ForgotPasswordPage()));
        }

        protected void UserOrPassChanged(object sender, EventArgs e)
        {
            if (PasswordEntry.Text != null && PasswordEntry.Text.Length > 0 && UsernameEntry.Text.Length > 0)
            {
                    LoginButton.IsEnabled = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
            }
        }
    }
}