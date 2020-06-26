using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Data.Repositories
{
    public class LocationUserRepository : Repository<LocationUser>, ILocationUserRepository
    {
        private readonly DataContext _context;

        public LocationUserRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
