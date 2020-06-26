using Middle_Manager_Mobile.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_Mobile.Helpers
{
    public class UserStorage : IUserStorage, INotifyPropertyChanged
    {
        private const string MobileAccessTokenKey = "MobileToken";
        private const string SessionTokenKey = "SessionToken";
        private const string HasSessionExpiredKey = "SessionExpired";
        private const string AppPasscodeKey = "AppPasscode";
        private const string IsLoggedInKey = "IsLoggedIn";
        private const string IsConnectedKey = "IsConnectedKey";
        private const string UserNameKey = "UserName";
        private const string UserRoleKey = "UserRole";
        private const string UserIdKey = "UserId";
        private const string OrganisationIdKey = "OrganisationId";



        private static UserStorage _userStorage;
        /// <summary>
        /// Gets or sets the current settings. This should always be used
        /// </summary>
        /// <value>The current.</value>
        public static UserStorage Current
        {
            get { return _userStorage ?? (_userStorage = new UserStorage()); }
        }

        public UserStorage()
        {
        }

        public bool HasSessionToken => !string.IsNullOrWhiteSpace(SessionToken);
        public string SessionToken
        {
            get => Preferences.Get(SessionTokenKey, string.Empty);
            set
            {
                Preferences.Set(SessionTokenKey, value);
                OnPropertyChanged();
                SessionDateUpdated = !string.IsNullOrEmpty(value) ? DateTime.UtcNow : (DateTime?)null;
            }
        }

        public bool HasMobileAccessToken => !string.IsNullOrWhiteSpace(MobileAccessToken);
        public string MobileAccessToken
        {
            get => Preferences.Get(MobileAccessTokenKey, string.Empty);
            set
            {
                Preferences.Set(MobileAccessTokenKey, value);
                OnPropertyChanged();
            }
        }

        public bool HasSessionExpired => !(SessionDateUpdated.HasValue && SessionDateUpdated.Value > new DateTime(2015, 1, 1)) ||
                                         (DateTime.UtcNow - SessionDateUpdated.Value).Minutes > 30;
        public DateTime? SessionDateUpdated
        {
            get
            {
                var ticks = Preferences.Get(HasSessionExpiredKey, -1L);

                return ticks > 0
                    ? new DateTime(ticks)
                    : (DateTime?)null;
            }
            private set
            {
                var ticks = value?.Ticks ?? -1;
                Preferences.Set(HasSessionExpiredKey, ticks);
                OnPropertyChanged();
            }
        }

        public Task SetAppPasscode(string passcode) => SecureStorage.SetAsync(AppPasscodeKey, passcode);

        public bool IsLoggedIn
        {
            get => Preferences.Get(IsLoggedInKey, false);
            set
            {
                Preferences.Set(IsLoggedInKey, value);
                OnPropertyChanged();
            }
        }

        public bool HasUserName => !string.IsNullOrWhiteSpace(UserName);
        public string UserName
        {
            get => Preferences.Get(UserNameKey, string.Empty);
            set
            {
                Preferences.Set(UserNameKey, value);
                OnPropertyChanged();
            }
        }

        public bool IsConnected
        {
            get => Preferences.Get(IsConnectedKey, true);
            set
            {
                Preferences.Set(IsConnectedKey, value);
                OnPropertyChanged();
            }
        }

        public int UserId
        {
            get => Preferences.Get(UserIdKey, 0);
            set
            {
                Preferences.Set(UserIdKey, value);
                OnPropertyChanged();
            }
        }

        public int OrganisationId
        {
            get => Preferences.Get(OrganisationIdKey, 0);
            set
            {
                Preferences.Set(OrganisationIdKey, value);
                OnPropertyChanged();
            }
        }

        public UserRole UserRole
        {
            get => (UserRole)Preferences.Get(UserRoleKey, 0);
            set
            {
                Preferences.Set(UserRoleKey, (int)value);
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}
