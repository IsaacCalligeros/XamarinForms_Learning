using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AutoMapper;
using CloudinaryDotNet;
using System.Threading.Tasks;
using System.Security.Claims;
using CloudinaryDotNet.Actions;
using System.Linq;
using Middle_Manager_API.Controllers;
using Middle_Manager_API.Helpers;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using System.IO;
using Middle_Manager_API.Services.Photos;

namespace MiddleManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotosController : BaseController
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;
        public PhotosController(IRepositoryWrapper repo,
            IMapper mapper,
         IOptions<CloudinarySettings> cloudinaryConfig,
         IPhotoService photoService)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repo = repo;
            _photoService = photoService;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("GetPhoto")]
        public async Task<PhotosForReturnDto> GetPhoto(int photoId)
        {
            var photoFromRepo = await _repo.Photo.GetPhoto(photoId);

            var photo = _mapper.Map<PhotosForReturnDto>(photoFromRepo);
            return photo;
        }

        [HttpPost("AddPhoto")]
        public async Task<PhotosForReturnDto> AddPhoto(PhotoForCreationDto photoForCreationDto)
        {
            return await _photoService.CreatePhoto(UserDetails.Name, UserDetails.Id, photoForCreationDto);
        }

        // [HttpPost("SetMainPhoto")]
        // public async Task<bool> SetMainPhoto(int userId, int photoId)
        // {
        //     var userDetails = IsCurrentUser(userId, mobileAccessToken);
        //     
        //         return false;

        //     var user = await _repo.User.GetUser(userId);

        //     if (!user.Photos.Any(p => p.PhotoId == photoId))
        //         return false;

        //     var photoFromRepo = await _repo.Photo.GetPhoto(photoId);
        //     if (photoFromRepo.IsMain)
        //         return false;

        //     var currentMainPhoto = await _repo.Photo.GetMainUserPhoto(userId);
        //     currentMainPhoto.IsMain = false;
        //     photoFromRepo.IsMain = true;
        //     if (await _repo.SaveAsync(userDetails.Name))
        //         return true;

        //     return false;
        // }

        [HttpDelete("DeletePhoto")]
        public async Task<bool> DeletePhoto(int photoId)
        {
            var user = await _repo.AppUsers.GetUser(UserDetails.Id);

            if (!user.Photos.Any(p => p.PhotoId == photoId))
                return false;

            var photoFromRepo = await _repo.Photo.GetPhoto(photoId);
            if (photoFromRepo.IsMain)
            {
                return false;
            }
            if (photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);
                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    _repo.Photo.Delete(photoFromRepo);
                }
            }

            if (photoFromRepo.PublicId == null)
            {
                _repo.Photo.Delete(photoFromRepo);
            }

            if (await _repo.SaveAsync(UserDetails.Name))
                return true;

            return false;
        }
    }
}