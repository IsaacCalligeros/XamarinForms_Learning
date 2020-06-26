using Microsoft.EntityFrameworkCore;
using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedClassLibrary.Dtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Middle_Manager_API.Data
{
    //IdentityDbContext contains all user tables, inheriting from this allows AddIdentity in Startup.cs
    public class DataContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<User> AppUsers { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<TaskTemplate> TaskTemplates { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationUser> LocationUsers { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<TaskInstance> TaskInstances { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}