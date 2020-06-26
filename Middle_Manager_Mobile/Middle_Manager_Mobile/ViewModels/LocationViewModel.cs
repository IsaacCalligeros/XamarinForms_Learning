using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.Services;
using SharedClassLibrary.Models;
using Xamarin.Forms;

namespace Middle_Manager_Mobile.ViewModels
{
    public class LocationViewModel : ViewModelBase, IDisposable
    {
        private readonly IUserStorage _userStorage;
        private readonly LocationService _locationService;

        public LocationViewModel()
        {
            _userStorage = UserStorage.Current;
            _locationService = new LocationService();
        }

        public async Task<bool> CreateLocation(Location location)
        {
            return await _locationService.CreateLocation(location);
        }

        public async Task<bool> UpdateLocation(Location location)
        {
            return await _locationService.UpdateLocation(location);
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await _locationService.GetLocations();
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }
        #endregion
    }
}
