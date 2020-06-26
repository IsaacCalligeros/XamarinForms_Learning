using Middle_Manager_API.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;


namespace Middle_Manager_API.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        //TODO: GET FEEDBACK
        //Based off of https://code-maze.com/net-core-web-development-part4/#comments
        //Implemented an IRepository and IRepo Factory with Unit of work, most of what i read
        //Said this wa redundant in core, however saving on each repo is a pain in the ass
        //also DI each repo is annoying as well, not sure how heavy this will be and if it locks
        //Me in... get some feedback sometime.

        //Try make this all generic

        private DataContext _dataContext;
        private IAuthRepository _auth;
        private IPhotoRepository _photo;
        private IUserRepository _appUsers;
        private ITaskTemplateRepository _taskTemplate;
        private ILocationRepository _location;
        private ITaskInstanceRepository _taskInstance;
        private ILocationUserRepository _locationUser;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RepositoryWrapper(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public IAuthRepository Auth
        {
            get
            {
                if (_auth == null)
                {
                    _auth = new AuthRepository(_dataContext);
                }

                return _auth;
            }
        }

        public IPhotoRepository Photo
        {
            get
            {
                if (_photo == null)
                {
                    _photo = new PhotoRepository(_dataContext);
                }

                return _photo;
            }
        }

        public IUserRepository AppUsers
        {
            get
            {
                if (_appUsers == null)
                {
                    _appUsers = new UserRepository(_dataContext);
                }

                return _appUsers;
            }
        }

        public ITaskTemplateRepository TaskTemplate
        {
            get
            {
                if (_taskTemplate == null)
                {
                    _taskTemplate = new TaskTemplateRepository(_dataContext);
                }

                return _taskTemplate;
            }
        }

        public ILocationRepository Location
        {
            get
            {
                if (_location == null)
                {
                    _location = new LocationRepository(_dataContext);
                }

                return _location;
            }
        }

        public ITaskInstanceRepository TaskInstance
        {
            get
            {
                if (_taskInstance == null)
                {
                    _taskInstance = new TaskInstanceRepository(_dataContext);
                }

                return _taskInstance;
            }
        }

        public ILocationUserRepository LocationUser
        {
            get
            {
                if (_locationUser == null)
                {
                    _locationUser = new LocationUserRepository(_dataContext);
                }

                return _locationUser;
            }
        }

        public async Task<bool> SaveAsync(string savingEntity)
        {
            addAuditableFields(_dataContext, savingEntity);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        private void addAuditableFields(DataContext data, string savingEntity)
        {
            var entries = data.ChangeTracker
                .Entries()
                .Where(e => e.Entity is Auditables && (e.State == EntityState.Added || e.State == EntityState.Modified));

            //var userId1 = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUserName = !string.IsNullOrEmpty(savingEntity)
                ? savingEntity
                : "Anonymous";

            foreach (var entityEntry in entries)
            {
                ((Auditables)entityEntry.Entity).DateUpdated = DateTime.UtcNow;
                ((Auditables)entityEntry.Entity).UpdatedBy = currentUserName;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Auditables)entityEntry.Entity).DateCreated = DateTime.UtcNow;
                    ((Auditables)entityEntry.Entity).CreatedBy = currentUserName;
                }
            }
        }
    }
}
