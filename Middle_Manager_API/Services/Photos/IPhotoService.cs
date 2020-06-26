using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedClassLibrary.Dtos;

namespace Middle_Manager_API.Services.Photos
{
    public interface IPhotoService
    {
        public Task<PhotosForReturnDto> CreatePhoto(string userName, int userId,
            PhotoForCreationDto photoForCreationDto);
    }
}
