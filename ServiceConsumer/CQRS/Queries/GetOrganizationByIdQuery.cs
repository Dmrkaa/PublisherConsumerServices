using MediatR;
using ServiceConsumer.Data;
using ServiceConsumer.Models;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ServiceConsumer.CQRS.Queries
{
    public class GetOrganizationByIdQuery : IRequest<Organization>
    {
        public int OrganizationID { get; set; }
        public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, Organization>
        {
            private readonly IDataContext _context;            
            public GetOrganizationByIdQueryHandler(IDataContext context)
            {
                _context = context;
            }
            public async Task<Organization> Handle(GetOrganizationByIdQuery query, CancellationToken cancellationToken)
            {
                return await _context.GetOrganizationsById(query.OrganizationID);
            }
        }
    }
}

