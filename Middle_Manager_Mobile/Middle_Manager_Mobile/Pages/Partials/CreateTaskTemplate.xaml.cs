using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.ViewModels;
using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedClassLibrary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_Mobile.Pages.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTask : ContentPage
    {
        private ManageTaskTemplatesViewModel ViewModel => vm ?? (vm = BindingContext as ManageTaskTemplatesViewModel);
        private ManageTaskTemplatesViewModel vm;
        private readonly IUserStorage _userStorage;

        public CreateTask(TaskTemplate taskTemplate = null)
        {
            _userStorage = UserStorage.Current;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            TaskValueTypePicker.ItemsSource = TaskValueType;
            ViewModel.EditableTaskTemplate = taskTemplate;
            if (taskTemplate != null)
                CreateButton.Text = "Update";
        }

        async void OnCreateButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel.EditableTaskTemplate != null)
            {
                //Ugly but just tempo would like to move all CRUD operations onto a ViewModel entitiy.
                var editableTaskTemplate = ViewModel.EditableTaskTemplate;
                editableTaskTemplate.Title = titleEntry.Text;
                editableTaskTemplate.Description = descriptionEntry.Text;
                editableTaskTemplate.ValueTypeEnum = (TaskValue)TaskValueTypePicker.SelectedIndex;
                var isTaskTemplateUpdated = await ViewModel.UpdateTaskTemplate(editableTaskTemplate);
                if(isTaskTemplateUpdated)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    TaskValueTypeWarning.IsVisible = true;
                }
            }
            else
            {

                var task = new TaskTemplate
                {
                    Title = titleEntry.Text,
                    Description = descriptionEntry.Text,
                    ValueTypeEnum = (TaskValue)TaskValueTypePicker.SelectedIndex,
                    OrganisationId = _userStorage.OrganisationId

                };
                var res = await ViewModel.CreateTaskTemplate(task);
                await Navigation.PopAsync();
            }
        }

        public List<string> TaskValueType
        {
            get
            {
                return Enum.GetNames(typeof(TaskValue)).Select(n => n).ToList();
            }
        }
    }
}