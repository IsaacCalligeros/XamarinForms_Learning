using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Middle_Manager_API.Data.Interfaces;
using Middle_Manager_API.Helpers;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Services.Photos
{
    public class PhotoService : ServiceBaseHelper, IPhotoService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;

        public PhotoService(IRepositoryWrapper repo,
            IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _repo = repo;
            _mapper = mapper;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<PhotosForReturnDto> CreatePhoto(string userName, int userId, PhotoForCreationDto photoForCreationDto)
        {
            var userFromRepo = await _repo.AppUsers.GetUser(userId);
            var uploadResult = new ImageUploadResult();
            if (photoForCreationDto.File.Length > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(photoForCreationDto.FileName, new MemoryStream(photoForCreationDto.File)),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                };
                uploadResult = _cloudinary.Upload(uploadParams);
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            if (!userFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;

            userFromRepo.Photos.Add(photo);

            if (await _repo.SaveAsync(userName))
            {
                var photoToReturn = _mapper.Map<PhotosForReturnDto>(photo);
                return photoToReturn;
            }

            return null;

        }

    }
}
