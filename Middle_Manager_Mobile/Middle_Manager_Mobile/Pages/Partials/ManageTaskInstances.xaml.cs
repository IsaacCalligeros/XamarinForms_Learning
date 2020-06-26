using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Middle_Manager_Mobile.ViewModels;
using SharedClassLibrary;
using SharedClassLibrary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Pages.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageTaskInstances : ContentPage
    {
        private TaskInstanceViewModel ViewModel => vm ?? (vm = BindingContext as TaskInstanceViewModel);
        private TaskInstanceViewModel vm;

        public ObservableCollection<TaskInstance> TaskInstanceList { get; set; }

        public ManageTaskInstances()
        {
            InitializeComponent();
            BindingContext = new TaskInstanceViewModel();
        }

        protected override async void OnAppearing()
        {
            ViewModel.TaskInstanceList = await ViewModel.GetOrganisationTaskInstance();
            TaskInstanceListItem.ItemsSource = ViewModel.TaskInstanceList;
        }

        private void AddTaskInstance_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateTaskInstance(null, true));
        }

        private void EditTaskInstance(object sender, EventArgs e)
        {
            var taskInstance = (TaskInstance)((Button)sender).BindingContext;
            Navigation.PushAsync(new CreateTaskInstance(taskInstance, true));
        }

        private async void DeleteTaskInstance(object sender, EventArgs e)
        {
            var taskInstance = (TaskInstance)((Button)sender).BindingContext;
            var confirmed = await DisplayAlert(SharedResources.Confirm,
                String.Format(SharedResources.DeleteItemConfirmation, taskInstance.TaskTemplate.Title),
                SharedResources.Yes, SharedResources.No);
            if (confirmed)
            {
                await ViewModel.DeleteTaskInstance(taskInstance.TaskInstanceId);
                ViewModel.TaskInstanceList = await ViewModel.GetOrganisationTaskInstance();
                TaskInstanceListItem.ItemsSource = ViewModel.TaskInstanceList;
            }
            else
            {
                //Nothing
            }
        }
    }
}


