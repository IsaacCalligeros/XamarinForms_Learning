using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Pages.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAudit : ContentPage
    {
        public CreateAudit()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
        }
    }
}