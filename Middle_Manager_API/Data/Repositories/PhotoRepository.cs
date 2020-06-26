using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middle_Manager_API.Data.Repositories
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        private readonly DataContext _context;
        public PhotoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.PhotoId == id);
            return photo;
        }
    }
}
