using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Pages.Partials;
using Middle_Manager_Mobile.ViewModels;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace Middle_Manager_Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        private HomeViewModel ViewModel => vm ?? (vm = BindingContext as HomeViewModel);
        private HomeViewModel vm;

        public HomePage()
        {
            InitializeComponent();
            //Need same for IOS

            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            
            UnselectedTabColor = Color.Black;
            BarBackgroundColor = Color.LightGray;
            SelectedTabColor = Color.MediumBlue;
        }

        protected override async void OnAppearing()
        {
            ViewModel.UsersTasksGrouped = await ViewModel.GetAllUserTaskInstance();
            TaskInstanceListItem.ItemsSource = ViewModel.UsersTasksGrouped;
            ViewModel.SetCalendarEvents();
        }

        private void UpdateTaskInstance(object sender, EventArgs e)
        {
                var taskInstance = (TaskInstance)((Xamarin.Forms.Button)sender).BindingContext;
                Navigation.PushAsync(new CreateTaskInstance(taskInstance, false));
        }
    }
}