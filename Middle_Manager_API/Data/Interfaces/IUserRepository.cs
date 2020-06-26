using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_API.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsers(int organisationId, UserRole role);
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetLocationUsers(int locationId);
    }
}
