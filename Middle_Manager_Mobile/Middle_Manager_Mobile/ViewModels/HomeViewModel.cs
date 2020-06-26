using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Services;
using Middle_Manager_Mobile.Services.Interfaces;
using SharedClassLibrary;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Helpers;
using SharedClassLibrary.Models;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;


namespace Middle_Manager_Mobile.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IUserStorage _userStorage;
        private readonly IUserService _userService;
        private readonly ITaskInstanceService _taskInstanceService;

        public ObservableCollection<Grouping<string, TaskInstance>> UsersTasksGrouped { get; set; }
        public CalendarEventCollection CalendarInlineEvents { get; set; } = new CalendarEventCollection();

        private string _taskInstanceCount;
        public string TaskInstanceCount
        {
            get { return _taskInstanceCount; }
            set
            {
                _taskInstanceCount = value;
                OnPropertyChanged();
            }
        }

        public string Key => "Key";

        public HomeViewModel()
        {
            _userStorage = UserStorage.Current;
            _userService = new UserService();
            _taskInstanceService = new TaskInstanceService();

            UsersTasksGrouped = new ObservableCollection<Grouping<string, TaskInstance>>();
        }

        public async Task<ObservableCollection<Grouping<string, TaskInstance>>> GetAllUserTaskInstance()
        {
            var userTasks = await _taskInstanceService.GetUserTaskInstance(_userStorage.UserId);
            var userLocationTasks = await _taskInstanceService.GetUsersLocationTaskInstances(_userStorage.UserId);
            var combinedTaskInstances = userLocationTasks.Concat(userTasks);

            TaskInstanceCount = combinedTaskInstances.Count().ToString();

            var sortedGroups = from taskInstance in combinedTaskInstances
                               orderby taskInstance.ExpectedCompletionTime
                               group taskInstance by taskInstance.ExpectedCompletionTime.HasValue ? taskInstance.ExpectedCompletionTime.Value.ToShortDateString() : DateTime.Today.Date.ToShortDateString()
                         into taskInstanceDateGroup
                               select new Grouping<string, TaskInstance>(taskInstanceDateGroup.Key.ToString(), taskInstanceDateGroup);

            //create a new collection of groups
            return new ObservableCollection<Grouping<string, TaskInstance>>(sortedGroups);
        }

        public void SetCalendarEvents()
        {
            var userTaskInstances = UsersTasksGrouped.SelectMany(t => t.ToList());
            //tempo just wiping old data wont requery every time in future.
            CalendarInlineEvents.Clear();
            foreach (var taskInstance in userTaskInstances)
            {
                CalendarInlineEvents.Add(
                    new CalendarInlineEvent()
                    {
                        StartTime = taskInstance.ExpectedCompletionTime.Value.AddHours(-1).ToLocalTime(),
                        EndTime = taskInstance.ExpectedCompletionTime.Value.ToLocalTime(),
                        Subject = taskInstance.TaskTemplate.Title,
                        Color = taskInstance.TargetType == Enums.TaskTarget.Location ? Color.LawnGreen : Color.Blue
                    });
            }

            return;
        }

    }
}

