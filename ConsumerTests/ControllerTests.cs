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
using System.Net;
using ServiceConsumer.Dtos;
using System.Xml.Serialization;
using System.IO;

namespace ConsumerTests
{
    public class ControllerTests
    {
        private readonly Mock<IDataContext> _datacontextMock = new Mock<IDataContext>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        public ControllerTests()
        {
            SeedDataContext();
        }

        List<Organization> _orgs { get; set; }
        List<User> _usr { get; set; }
        [Fact]
        public void GetOrders_WithOrdersInRepo_ReturnsOk()
        {
            //Assert
            OrganizationDto expected = new OrganizationDto()
            {
                Name = "Marvel",
                Users = new List<UserDto>
                {
                    new UserDto()
                    { Name = _usr[0].Name, Email = _usr[0].Email, LastName = _usr[0].LastName, MiddleName = _usr[0].MiddleName }
                }
            };
            var mapperCfg = new MapperConfiguration(cfg
                => cfg.AddProfiles(new Profile[]
                {
                    new AutoMapping()
                }));

            mapperCfg.AssertConfigurationIsValid();
            var mapp = mapperCfg.CreateMapper();
            var controller = new UserController(_mediator.Object, mapp);

            //Act
            var ctoFromController = controller.BuildResult(_orgs[0], _usr);
            //Arrange
            var serializer = new XmlSerializer(typeof(OrganizationDto));
            StringWriter serialized1 = new StringWriter(), serialized2 = new StringWriter(), serialized3 = new StringWriter();
            serializer.Serialize(serialized2, expected);
            serializer.Serialize(serialized3, ctoFromController);

            bool areEqual = serialized2.ToString() == serialized3.ToString();
            Assert.True(areEqual);
            var result = controller.UserAndOrganizationConnect(0, 1);
        }


        private void SeedDataContext()
        {
            _usr = new List<User>();
            _usr.AddRange(
                  new User[]
                {
                    new User { UserID=1, Name="Tom",
                        OrganizationID=1,
                        LastName="Hohland",
                        Email="abc@gmail.kz",
                        MiddleName="Ivanovich"},
                    new User { UserID=2, Name="Alice", LastName="Wolf", Email="WolfAlice@outllok.com"}
                });
            _orgs = new List<Organization>();
            _orgs.AddRange(
                new Organization[]
                {
                    new Organization { OrganizationID=1,
                        Name="Marvel", Users = new List<User> { _usr[0] } },
                    new Organization { OrganizationID=2,
                        Name="Universal" },
                    new Organization { OrganizationID=3,
                        Name="DC" }
                });

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
    }
}
