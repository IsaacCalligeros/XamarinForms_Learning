using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Services.Interfaces;
using Refit;
using SharedClassLibrary.Dtos;

namespace Middle_Manager_Mobile.Services
{
    public class PhotoService : BaseService, IPhotoService
    {
        public async Task<PhotosForReturnDto> GetPhoto(int photoId)
        {
            return await MMApi.GetPhoto(photoId);
        }
        public async Task<PhotosForReturnDto> AddPhoto(PhotoForCreationDto photoForCreationDto)
        {
            return await MMApi.AddPhoto(photoForCreationDto);
        }
        //public async Task<bool> SetMainPhoto(int photoId)
        //{
        //    return await MMApi.SetMainPhoto(userId, photoId);
        //}
        public async Task<bool> DeletePhoto(int photoId)
        {
            return await MMApi.DeletePhoto(photoId);
        }
    }
}
