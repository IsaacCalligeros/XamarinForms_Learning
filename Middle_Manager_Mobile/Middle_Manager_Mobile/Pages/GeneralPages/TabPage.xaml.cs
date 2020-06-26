using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using static SharedClassLibrary.Enums;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace Middle_Manager_Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : Xamarin.Forms.TabbedPage
    {
        private TabPageViewModel ViewModel => vm ?? (vm = BindingContext as TabPageViewModel);
        private TabPageViewModel vm;

        private readonly IUserStorage _userStorage;

        public TabPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //Xamarin forms isVisible property doesn't work on tabs in tabbedPage, just hides content rather than the tab panel kinda stupid.
            if (!ViewModel.IsAdmin)
                this.Children.Remove(this.Children.Where(s => s.GetType() == typeof(ManagePage)).First());

            foreach (var page in this.Children)
            {
                switch (page)
                {
                    case ManagePage m:
                        //m.Title += ((char)0xf11a).ToString();
                        break;
                    default:
                        break;
                }
                 
                
            }

        }
    }
}