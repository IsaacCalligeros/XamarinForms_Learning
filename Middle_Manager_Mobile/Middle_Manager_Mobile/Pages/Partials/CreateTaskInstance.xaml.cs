using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.ViewModels;
using SharedClassLibrary;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_Mobile.Pages.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTaskInstance : ContentPage
    {
        public event EventHandler nonEvent = (s, e) => { };
        private TaskInstanceViewModel ViewModel => vm ?? (vm = BindingContext as TaskInstanceViewModel);
        private TaskInstanceViewModel vm;
        private readonly IUserStorage _userStorage;
        private bool isAdminEdit = false;
        private bool isNewInstance = true;

        public CreateTaskInstance(TaskInstance taskInstance = null, bool canEdit = false)
        {
            _userStorage = UserStorage.Current;
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);
            if (taskInstance != null)
            {
                isNewInstance = false;
                ViewModel.EditableTaskInstance = taskInstance;
                CreateButton.Text = "Update";
            }
            else
            {
                isNewInstance = true;
                ViewModel.EditableTaskInstance = new TaskInstance();
                ViewModel.EditableTaskInstance.ExpectedCompletionTime = DateTime.Today.AddDays(1);
            }
            isAdminEdit = canEdit;
        }

        protected override async void OnAppearing()
        {
            if (!ViewModel.AppearingRan)
            {
                ViewModel.TaskTemplateList = await ViewModel.GetTaskTemplates();
                TaskTemplatePicker.ItemsSource = ViewModel.TaskTemplateList;
                ImageUploadControl.IsVisible = ViewModel.SelectedTaskTemplate?.ValueTypeEnum == TaskValue.Photo;

                ViewModel.UserList = await ViewModel.GetUsers();
                AssignedToUserPicker.ItemsSource = ViewModel.UserList;

                ViewModel.LocationList = await ViewModel.GetLocations();
                AssignedToLocationPicker.ItemsSource = ViewModel.LocationList;

                if (!isNewInstance)
                {
                    if (ViewModel.EditableTaskInstance.AssignedToId.HasValue)
                    {
                        //This is disgusting... has to be some clean way.
                        AssignedToUserPicker.SelectedIndex = AssignedToUserPicker.ItemsSource.IndexOf
                        (ViewModel.UserList.SingleOrDefault(id =>
                            id.UserId == ViewModel.EditableTaskInstance.AssignedToId.Value));
                    }

                    TaskTemplatePicker.SelectedIndex = TaskTemplatePicker.ItemsSource.IndexOf
                    (ViewModel.TaskTemplateList.SingleOrDefault(id =>
                        id.TaskTemplateId == ViewModel.EditableTaskInstance.TaskTemplateId));
                    ViewModel.SelectedTaskTemplate = (TaskTemplate)TaskTemplatePicker.SelectedItem;

                    if (ViewModel.EditableTaskInstance.LocationId.HasValue)
                    {
                        //This is disgusting... has to be some clean way.
                        AssignedToLocationPicker.SelectedIndex = AssignedToLocationPicker.ItemsSource.IndexOf
                        (ViewModel.LocationList.SingleOrDefault(id =>
                            id.LocationId == ViewModel.EditableTaskInstance.LocationId.Value));
                    }
                }
                ViewModel.AppearingRan = true;
            }

            if (isAdminEdit == false)
            {
                //Kind of messy, overriding event handler to do nothing.
                titleEntry.IsReadOnly = true;
                TaskTemplatePicker.IsEnabled = false;
                TaskTemplatePicker.IsEnabled = false;
                TaskValueTypePicker.IsEnabled = true;
                AssignedToUserPicker.IsEnabled = false;
                AssignedToLocationPicker.IsEnabled = false;
                ExpectedCompletionTimePicker.IsEnabled = false;
                IsRecurring.IsVisible = false;
                RecurrenceTypePicker.IsVisible = false;
                RecurrenceEndDate.IsVisible = false;

            }
        }



        async void OnCreateButtonClicked(object sender, EventArgs e)
        {

            if (ViewModel.EditableTaskInstance != null)
            {
                await ViewModel.CreateTaskInstance();
            }
            else
            {
                //This is awful, need to find to actual entity in xaml and pull that through or something.
                var task = new TaskInstance
                {
                    Title = titleEntry.Text,
                    TaskTemplateId = ((TaskTemplate)TaskTemplatePicker.SelectedItem).TaskTemplateId,
                    TargetType = (TaskTarget)TaskValueTypePicker.SelectedItem,
                    AssignedToId = AssignedToUserPicker.SelectedItem == null ? null : ((UserForDetailedDto)AssignedToUserPicker.SelectedItem)?.UserId,
                    LocationId = AssignedToLocationPicker.SelectedItem == null ? null : ((Location)AssignedToLocationPicker.SelectedItem)?.LocationId,
                    ExpectedCompletionTime = ((DatePicker)ExpectedCompletionTimePicker).Date,
                    Recurring = ((CheckBox)IsRecurring).IsChecked,
                    RecurrenceType = (RecurrenceType)RecurrenceTypePicker?.SelectedItem,
                    RecurrenceEndDate = ((DatePicker)RecurrenceEndDate).Date,
                };
                var res = await ViewModel.CreateTaskInstance(task);

            }

            //if (ImageUploadControl.MediaFile != null)
            //{
            //    var photoForCreationDto = new PhotoForCreationDto()
            //    {
            //        File = ImageUploadControl.ImageBytes,
            //        FileName = "NewFile",
            //        OwnerType = OwnerType.TaskInstance,
            //        OwnerId = res.TaskInstanceId,
            //        Description = String.Empty,
            //        Url = String.Empty,
            //        PublicId = String.Empty
            //    };

            //    await ViewModel.AddPhoto(photoForCreationDto);
            //}

            await Navigation.PopAsync();

        }

        private void TaskTargetTypeChanged(object sender, EventArgs e)
        {
            switch ((TaskTarget)((Picker)sender).SelectedItem)
            {
                case TaskTarget.User:
                    AssignedToUserPicker.IsVisible = true;
                    AssignedToLocationPicker.IsVisible = false;
                    break;
                case TaskTarget.Location:
                    AssignedToLocationPicker.IsVisible = true;
                    AssignedToUserPicker.IsVisible = false;
                    break;
                default:
                    break;
            }
        }

        private void AssignedToUserPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewModel.EditableTaskInstance.AssignedToId = AssignedToUserPicker.SelectedItem == null ? null : ((UserForDetailedDto)AssignedToUserPicker.SelectedItem)?.UserId;
        }

        private void AssignedToLocationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewModel.EditableTaskInstance.LocationId = AssignedToLocationPicker.SelectedItem == null ? null : ((Location)AssignedToLocationPicker.SelectedItem)?.LocationId;
        }
        public void TaskTemplatePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Method call every time when picker selection changed.
            ViewModel.SelectedTaskTemplate = (TaskTemplate)TaskTemplatePicker.SelectedItem;
            ImageUploadControl.IsVisible = ViewModel.SelectedTaskTemplate.ValueTypeEnum == TaskValue.Photo;
            ViewModel.EditableTaskInstance.TaskTemplateId = ((TaskTemplate)TaskTemplatePicker.SelectedItem).TaskTemplateId;
        }
    }
}