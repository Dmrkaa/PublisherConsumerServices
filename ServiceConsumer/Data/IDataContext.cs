using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceConsumer.Models;

namespace ServiceConsumer.Data
{
    public interface IDataContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        public Task<Organization> GetOrganizationsById(Guid organizationID);
        public Task<IList<User>> GetUserById(Guid userID);
        public Task<List<User>> GetAllUsers();
        Task<int> SaveChanges();



    }
}
