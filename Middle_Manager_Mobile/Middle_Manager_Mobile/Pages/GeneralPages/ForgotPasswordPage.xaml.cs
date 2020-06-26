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
    public partial class ForgotPasswordPage : ContentPage
    {

        private ForgotPasswordViewModel ViewModel => vm ?? (vm = BindingContext as ForgotPasswordViewModel);
        private ForgotPasswordViewModel vm;
        private readonly IUserService _userService;
        private readonly IUserStorage _userStorage;

        public ForgotPasswordPage()
        {
            InitializeComponent();
            _userService = new UserService();
            BindingContext = new ForgotPasswordViewModel();
        }

        private async void OnEmailVerificationButtonClicked(object sender, EventArgs e)
        {
            var res = await ViewModel.OnEmailVerificationButtonClicked(Email.Text);
            if(res)
            {
                await DisplayAlert("Email sent", "Please check your email address for verification code", "OK");
            }
            else
            {
                await DisplayAlert("Email failed to send", "", "OK");
            }
        }

        private async void OnResetButtonClicked(object sender, EventArgs e)
        {
            int verificationCode = 0;
            try
            {
                verificationCode = int.Parse(VerificationCode.Text);
            }
            catch(Exception ex)
            {
                await DisplayAlert("Verification code invalid", "Verification code should be a 6 digit number.", "OK");
                return;
            }

            var res = await ViewModel.OnResetButtonClicked(Email.Text, verificationCode);
            if (res)
            {
                await Navigation.PushAsync(new ResetPasswordPage(Email.Text, verificationCode));
            }
        }
    }
}