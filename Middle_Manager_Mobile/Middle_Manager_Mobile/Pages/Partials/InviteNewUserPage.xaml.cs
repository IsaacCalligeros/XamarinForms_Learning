using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.ViewModels;
using SharedClassLibrary;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Pages.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InviteNewUserPage : ContentPage
    {
        private ManageUsersViewModel ViewModel => vm ?? (vm = BindingContext as ManageUsersViewModel);
        private ManageUsersViewModel vm;
        private bool isExistingUser = false;


        public InviteNewUserPage(UserForDetailedDto user = null)
        {
            InitializeComponent();
            if (user != null)
            {
                ViewModel.EditableUser = user;
                isExistingUser = true;
                RegisterButon.Text = SharedResources.Update;
            }
        }

        private async void OnInviteClicked(object sender, EventArgs e)
        {
            if (isExistingUser)
            {

            }
            else
            {
                var user = new UserForRegisterDto
                {
                    UserName = signUpUsernameEntry.Text,
                    Password = RandomString(10),
                    FirstName = firstNameEntry.Text,
                    LastName = lastNameEntry.Text,
                    Email = Email.Text,
                    DateOfBirth = DateOfBirthPicker.Date,
                    Created = DateTime.Now,
                    LastActive = DateTime.Now,
                    Role = Enums.UserRole.Admin // Tempo for now.
                };

                var res = await ViewModel.RegisterInvite(user);
                await DisplayAlert("Alert", "You have been alerted Register status:" + res.ToString(), "OK");
                await Navigation.PopAsync();
            }
        }

        static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }
    }
}