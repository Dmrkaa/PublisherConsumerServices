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
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {
        public Guid OrganizationID { get; set; }
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;
            public GetUsersQueryHandler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IEnumerable<User>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
            {
                return await _context.GetAllUsers();
            }
        }
    }
}

