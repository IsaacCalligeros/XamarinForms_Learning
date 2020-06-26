using Akavache;
using HttpTracer;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Helpers;
using Xamarin.Forms;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_Mobile.Services
{
    public class BaseService
    {
        private static readonly IUserStorage Settings = UserStorage.Current;

        private static readonly MMHttpHandler Tracer = new MMHttpHandler();


        private static readonly HttpClient NativeHttpClient = new HttpClient(Tracer);
        private static ImmApi _mmApi;

        protected static ImmApi MMApi
        {
            get
            {

                NativeHttpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
                NativeHttpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentCulture.ThreeLetterISOLanguageName));

                var timeout = new TimeSpan(0, 2, 30);
                if (NativeHttpClient.Timeout.Ticks != timeout.Ticks)
                {
                    NativeHttpClient.Timeout = timeout;
                }

                if (UserStorage.Current.HasMobileAccessToken)
                {
                    NativeHttpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", UserStorage.Current.MobileAccessToken);
                }


                var apiBaseAddress = new Uri(GetApiBaseAddress());

                var restSettings = new RefitSettings()
                {
                    
                    ContentSerializer = new JsonContentSerializer(new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    })
                };
                if (NativeHttpClient.BaseAddress == null)
                {
                    NativeHttpClient.BaseAddress = apiBaseAddress;
                    _mmApi = RestService.For<ImmApi>(NativeHttpClient, restSettings);
                }
                else if (!NativeHttpClient.BaseAddress.Equals(apiBaseAddress))
                {
                    NativeHttpClient.BaseAddress = apiBaseAddress;
                    _mmApi = RestService.For<ImmApi>(NativeHttpClient, restSettings);
                }
                else if (_mmApi == null)
                {
                    _mmApi = RestService.For<ImmApi>(NativeHttpClient, restSettings);
                }

                return _mmApi;
            }
        }

        private static string GetApiBaseAddress()
        {
            var apiBaseAddress = string.Empty;

#if DEBUG
            if (Device.RuntimePlatform == "iOS")
            {
                //Set Some localhost shit here
            }
            else if (Device.RuntimePlatform == "Android")
            {
                apiBaseAddress = "http://10.0.2.2:5062/api";
            }
#else
            apiBaseAddress = "SetThisLater";
#endif

            return apiBaseAddress;
        }

        protected IBlobCache GetBlobCache()
        {
            return BlobCache.LocalMachine;
        }

        public string SessionToken => Settings.SessionToken;
        public string UserName => Settings.UserName;
        public UserRole UserRole => Settings.UserRole;
        public string MobileAccessToken => Settings.MobileAccessToken;
    }
}
