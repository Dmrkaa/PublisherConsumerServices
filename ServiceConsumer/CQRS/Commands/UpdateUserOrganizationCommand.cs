using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ServiceConsumer.Data;
namespace ServiceConsumer.CQRS.Commands
{
    public class UpdateUserOrganizationCommand : IRequest<int>
    {
        public Guid UserID { get; set; }
        public Guid OrganizationID { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserOrganizationCommand, int>
        {
            private readonly IDataContext _context;
            public UpdateUserCommandHandler(IDataContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateUserOrganizationCommand command, CancellationToken cancellationToken)
            {
                var users = await _context.GetUserById(command.UserID);
                var user = users.FirstOrDefault();
                var organization = _context.GetOrganizationsById(command.OrganizationID);
                if (user == null || organization == null)
                {
                    return -1;
                }
                else
                {
                    user.OrganizationID = command.OrganizationID;
                    return await _context.SaveChanges();
                }
            }
        }
    }
}
