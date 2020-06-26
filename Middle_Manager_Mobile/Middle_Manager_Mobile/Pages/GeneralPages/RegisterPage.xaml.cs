using AutoMapper;
using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.ViewModels;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private LoginViewModel ViewModel => vm ?? (vm = BindingContext as LoginViewModel);
        private LoginViewModel vm;

        private readonly IUserStorage _userStorage;

        public RegisterPage()
        {
            _userStorage = UserStorage.Current;
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }


        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            var user = new UserForRegisterDto
            {
                UserName = signUpUsernameEntry.Text,
                Password = signUpPasswordEntry.Text,
                FirstName = firstNameEntry.Text,
                LastName = lastNameEntry.Text,
                Email = Email.Text,
                DateOfBirth = DateOfBirthPicker.Date,
                Created = DateTime.Now,
                LastActive = DateTime.Now,
                Role = UserRole.Admin // Tempo for now.
            };

            var res = await ViewModel.Register(user);

            await DisplayAlert("Alert", "You have been alerted Register status:" + res.ToString(), "OK");
        }



    }
}