using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using SharedClassLibrary.Dtos;

namespace Middle_Manager_Mobile.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<PhotosForReturnDto> GetPhoto(int photoId);
        Task<PhotosForReturnDto> AddPhoto(PhotoForCreationDto photoForCreationDto);
        //Task<bool> SetMainPhoto(int userId, int photoId);
        Task<bool> DeletePhoto(int photoId);
    }
}
