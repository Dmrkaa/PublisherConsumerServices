using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ServiceConsumer.Data;
using ServiceConsumer.Models;
namespace ServiceConsumer.CQRS.Commands
{
    public class AddUserCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }

        public class AddUserCommandHandler : IRequestHandler<AddUserCommand, string>
        {
            private readonly IDataContext _context;
            public AddUserCommandHandler(IDataContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(AddUserCommand command, CancellationToken cancellationToken)
            {
                return await SaveUser(command);
            }

            private async Task<string> SaveUser(AddUserCommand command)
            {
                var person = new User();
                person.Name = command.Name;
                person.LastName = command.LastName;
                person.MiddleName = command.MiddleName;
                person.Email = command.Email;
                _context.Users.Add(person);
                await _context.SaveChanges();
                return $"User added: {person.Name} {person.LastName} {person.Email}";
            }
        }
    }
}
