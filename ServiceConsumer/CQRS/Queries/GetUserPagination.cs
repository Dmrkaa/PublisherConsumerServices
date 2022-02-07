using MediatR;
using ServiceConsumer.Data;
using ServiceConsumer.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServiceConsumer.Dtos;
using AutoMapper;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace ServiceConsumer.CQRS.Queries
{
    public class GetUserPaginationByIdQuery : IRequest<IEnumerable<OrganizationDto>>
    {
        public Guid OrganizationID { get; set; }
        public class GetUserPaginationByIdQueryHandler : IRequestHandler<GetUserPaginationByIdQuery, IEnumerable<OrganizationDto>>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;
            public GetUserPaginationByIdQueryHandler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IEnumerable<OrganizationDto>> Handle(GetUserPaginationByIdQuery query, CancellationToken cancellationToken)
            {
                var organisation = await _context.GetOrganizationsById(query.OrganizationID);
                var users = await _context.GetAllUsers();
                return BuildResult(organisation, users);
            }


            private List<OrganizationDto> BuildResult(Organization orgs, List<User> users)
            {
                var organizationDtos = _mapper.Map<List<OrganizationDto>>(orgs);
                var userrsDto = _mapper.Map<List<UserDto>>(users);
                return organizationDtos;
            }

        }
    }

}

