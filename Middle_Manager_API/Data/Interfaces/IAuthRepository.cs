using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middle_Manager_API.Data.Interfaces
{
    public interface IAuthRepository : IRepository<User>
    {
        Task<bool> UserExists(string username);
    }
}
