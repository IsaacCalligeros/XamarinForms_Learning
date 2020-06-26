using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.ViewModels;
using SharedClassLibrary;
using SharedClassLibrary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Pages.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageTaskTemplates : ContentPage
    {
        private ManageTaskTemplatesViewModel ViewModel => vm ?? (vm = BindingContext as ManageTaskTemplatesViewModel);
        private ManageTaskTemplatesViewModel vm;

        public ObservableCollection<TaskTemplate> TaskTemplatesList { get; set; }

        public ManageTaskTemplates()
        {
            InitializeComponent();
            BindingContext = new ManageTaskTemplatesViewModel();
        }

        protected override async void OnAppearing()
        {
            ViewModel.TaskTemplatesList = await ViewModel.GetTaskTemplates();
            TaskTemplateList.ItemsSource = ViewModel.TaskTemplatesList;
        }

        private void AddTaskTemplate_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateTask());
        }

        private void EditTaskTemplate(object sender, EventArgs e)
        {
            var taskTemplate = (TaskTemplate)((Button)sender).BindingContext;
            Navigation.PushAsync(new CreateTask(taskTemplate));
        }

        private async void DeleteTaskTemplate(object sender, EventArgs e)
        {
            var taskTemplate = (TaskTemplate)((Button)sender).BindingContext;
            var confirmed = await DisplayAlert(SharedResources.Confirm,
                String.Format(SharedResources.DeleteItemConfirmation, taskTemplate.Title),
                SharedResources.Yes, SharedResources.No);
            if (confirmed)
            {
                await ViewModel.DeleteTaskTemplate(taskTemplate.TaskTemplateId);
                ViewModel.TaskTemplatesList = await ViewModel.GetTaskTemplates();
                TaskTemplateList.ItemsSource = ViewModel.TaskTemplatesList;
            }
            else
            {
                //Nothing
            }
        }
    }
}
