using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middle_Manager_API.Data.Interfaces
{
    public interface IRepositoryWrapper
    {
        IAuthRepository Auth { get; }
        IPhotoRepository Photo { get; }
        IUserRepository AppUsers { get; }
        ITaskTemplateRepository TaskTemplate { get; }
        ITaskInstanceRepository TaskInstance { get; }
        ILocationRepository Location { get; }
        ILocationUserRepository LocationUser { get; }
        Task<bool> SaveAsync(string savingEntity);
    }
}
