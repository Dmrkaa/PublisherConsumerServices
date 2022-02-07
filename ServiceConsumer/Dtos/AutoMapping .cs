using AutoMapper;
using ServiceConsumer.Models;
using System.Collections.Generic;
using System.Linq;

namespace ServiceConsumer.Dtos
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDto>();
            CreateMap<Organization, OrganizationDto>();
        }
    }
}
