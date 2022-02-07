using AutoMapper;
using ServiceConsumer.Models;
using System;
using System.Collections.Generic;
namespace ServiceConsumer.Dtos
{
    public class OrganizationDto
    {
        public string Name { get; set; }
        public List<UserDto> Users { get; set; }

        public class MapProfile : Profile
        {
            public MapProfile()
            {
                CreateMap<Organization, OrganizationDto>();

                CreateMap<UserDto, OrganizationDto>();

            }
        }

    }
}
