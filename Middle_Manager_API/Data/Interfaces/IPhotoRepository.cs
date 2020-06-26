using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middle_Manager_API.Data.Interfaces
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        Task<Photo> GetPhoto(int id);
    }
}
