using Middle_Manager_Mobile.Pages;
using Middle_Manager_Mobile.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile
{
    public partial class App : Application
    {
        private AppViewModel ViewModel => vm ?? (vm = BindingContext as AppViewModel);
        private AppViewModel vm;
        public static INavigation GlobalNavigation { get; private set; }
        public App()
        {
            BindingContext = new AppViewModel();
            InitializeComponent();

            var rootPage = new NavigationPage(new LoginPage());
            GlobalNavigation = rootPage.Navigation;

            MainPage = rootPage;
            MainPage.SetBinding(NavigationPage.BarBackgroundColorProperty, new Binding("BarBgColor"));

        }

        protected async override void OnStart()
        {
            //if (await ViewModel.CheckValidToken())
            //{
            //    await GlobalNavigation.PushAsync(new TabPage());
            //    var loginAndRegisterPages = GlobalNavigation.NavigationStack.Where(n => n.GetType() == typeof(LoginPage) || n.GetType() == typeof(RegisterPage)).ToList();
            //    foreach (var page in loginAndRegisterPages)
            //    {
            //        GlobalNavigation.RemovePage(page);
            //    }
            //    return;
            //}
        }

        protected override void OnSleep()
        {
        }

        protected async override void OnResume()
        {
            //if (await ViewModel.CheckValidToken())
            //{
            //    await GlobalNavigation.PushAsync(new TabPage());
            //    var loginAndRegisterPages = GlobalNavigation.NavigationStack.Where(n => n.GetType() == typeof(LoginPage) || n.GetType() == typeof(RegisterPage)).ToList();
            //    foreach (var page in loginAndRegisterPages)
            //    {
            //        GlobalNavigation.RemovePage(page);
            //    }
            //    return;
            //}
        }
    }
}
