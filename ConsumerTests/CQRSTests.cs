using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceConsumer.Controllers;
using ServiceConsumer.Consumer;
using ServiceConsumer.Models;
using ServiceConsumer.Data;
using ServiceConsumer.CQRS.Queries;
using Xunit;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using System.Data.Entity.Infrastructure;
using ServiceConsumer.CQRS.Commands;

namespace ConsumerTests
{

    public class CunsumerServiceTests
    {
        private readonly Mock<IDataContext> _datacontextMock = new Mock<IDataContext>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();


        public CunsumerServiceTests()
        {
            SeedDataContext();
        }

        List<Organization> _orgs { get; set; }
        List<User> _usr { get; set; }

        [Fact]
        public async Task AddUserTest()
        {

            var users = GetQueryableMockDbSet(_usr);

            AddPersonCommand _sut = new AddPersonCommand();
            AddPersonCommand.AddUserCommandHandler hnd = new AddPersonCommand.AddUserCommandHandler(_datacontextMock.Object);

            _datacontextMock.Setup(c => c.Users).Returns(users);
            int bef = users.Count(x => x == null);
            int befN = users.Count(x => x != null);
            _sut.Name = "xUnit";
            _sut.LastName = "Test";
            _sut.Email = "mm@mm.rr";
            _sut.ID = Guid.NewGuid();
            var result = await hnd.Handle(_sut, new System.Threading.CancellationToken());
            string expected = "User added: xUnit Test mm@mm.rr";
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetUsers_by_OrganizationId()
        {
            var orgsQueryable = _orgs.AsQueryable();
            var mockOrganization = new Mock<DbSet<Organization>>();
            mockOrganization.As<IDbAsyncEnumerable<Organization>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Organization>(orgsQueryable.GetEnumerator()));

            mockOrganization.As<IQueryable<Organization>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncEnumerable<Organization>(orgsQueryable));

            mockOrganization.As<IQueryable<Organization>>().Setup(m => m.Expression).Returns(orgsQueryable.Expression);
            mockOrganization.As<IQueryable<Organization>>().Setup(m => m.ElementType).Returns(orgsQueryable.ElementType);
            mockOrganization.As<IQueryable<Organization>>().Setup(m => m.GetEnumerator()).Returns(orgsQueryable.GetEnumerator());

            _datacontextMock.Setup(c => c.Organizations).Returns(mockOrganization.Object);


            var usrQueryable = _usr.AsQueryable();
            var mockUsers = new Mock<DbSet<User>>();
            mockUsers.As<IDbAsyncEnumerable<User>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<User>(usrQueryable.GetEnumerator()));

            mockUsers.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncEnumerable<User>(usrQueryable));

            mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(usrQueryable.Expression);
            mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(usrQueryable.ElementType);
            mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(usrQueryable.GetEnumerator());

            GetOrganizationByIdQuery _sut = new GetOrganizationByIdQuery();
            GetOrganizationByIdQuery.GetOrganizationByIdQueryHandler hnd = new GetOrganizationByIdQuery.GetOrganizationByIdQueryHandler(_datacontextMock.Object);


            _sut.OrganizationID = Guid.Parse("B35A3C95-B96E-4BD0-9A54-86F282ED9543");

            _datacontextMock.Setup(c => c.GetOrganizationsById(_sut.OrganizationID).Result).Returns(mockOrganization.Object.Where(x => x.OrganizationID == _sut.OrganizationID).FirstOrDefault());
            _datacontextMock.Setup(c => c.GetAllUsers().Result).Returns(mockUsers.Object.ToList());

            var result = await hnd.Handle(_sut, new System.Threading.CancellationToken());
            var expected = _orgs.Where(x => x.OrganizationID == _sut.OrganizationID).FirstOrDefault();

            Assert.Equal(expected, result);
        }



        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var coll = sourceList as ICollection<T>;
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return dbSet.Object;
        }

        private void SeedDataContext()
        {
            _orgs = new List<Organization>();
            _orgs.AddRange(
                new Organization[]
                {
                    new Organization { OrganizationID=Guid.Parse("B35A3C95-B96E-4BD0-9A54-86F282ED9543"),
                        Name="Marvel" },
                    new Organization { OrganizationID=Guid.Parse("1FECB8BF-BF38-41E7-8146-00AD585C2E18"),
                        Name="Universal" },
                    new Organization { OrganizationID=Guid.Parse("962683A0-B3D5-440E-B139-2F794D78F5D6"),
                        Name="DC" }
                });
            _usr = new List<User>();
            _usr.AddRange(
                  new User[]
                {
                    new User { UserID=Guid.NewGuid(), Name="Tom",
                        OrganizationID=Guid.Parse("B35A3C95-B96E-4BD0-9A54-86F282ED9543"),
                        LastName="Hohland",
                        Email="abc@gmail.kz",
                        MiddleName="Ivanovich"},
                    new User { UserID=Guid.NewGuid(), Name="Alice", LastName="Wolf", Email="WolfAlice@outllok.com"}
                });
        }

    }
}
