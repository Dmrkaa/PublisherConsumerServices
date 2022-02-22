using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceConsumer.Models;
namespace ServiceConsumer.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>().HasData(
              new Organization[]
              {
                    new Organization { Name="Marvel" },
                    new Organization { Name="Universal" }
              });

            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                    new User {  Name="Tom", LastName="Hohland", Email="abc@gmail.kz", MiddleName="Ivanovich"},
                    new User {  Name="Alice", LastName="Wolf", Email="WolfAlice@outllok.com"}
                });


        }

        public async Task<Organization> GetOrganizationsById(int organizationID)
        {
            return await Organizations.Where(x => x.OrganizationID == organizationID).FirstOrDefaultAsync();
        }

        public async Task<IList<User>> GetUserById(int userID)
        {
            return await Users.Where(x => x.UserID == userID).ToListAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await Users.ToListAsync();
        }

        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

    }
}
