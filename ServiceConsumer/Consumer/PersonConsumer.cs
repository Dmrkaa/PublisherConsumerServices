using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServiceConsumer.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServiceConsumer.Data;
using ServiceConsumer.CQRS.Commands;
using System;
using System.Data.SqlClient;
using Shared.Models;

namespace ServiceConsumer.Consumer
{
    public class UserConsumer : IConsumer<UserSharedModel>
    {
        private IMediator _mediator;
        private ILogger _logger;
        public UserConsumer(IMediator mediator, ILogger<UserConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserSharedModel> context)
        {
            try
            {
                AddPersonCommand addPersonCommand = CreateCommand(context.Message);

                _logger.LogInformation($"Object recieve. Name: {context.Message.Name}, LastName: {context.Message.LastName}, MiddleName: {context.Message.MiddleName}, Email: {context.Message.Email}");
                string sendResult = await _mediator.Send(addPersonCommand);
                _logger.LogInformation($"{sendResult}");
            }
            catch (SqlException e)
            {
                _logger.LogError($"Write to DB is not complete: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message}");
            }

        }

        private AddPersonCommand CreateCommand(UserSharedModel message)
        {
            AddPersonCommand cmd = new AddPersonCommand();
            cmd.ID = System.Guid.NewGuid();
            cmd.Name = message.Name;
            cmd.LastName = message.LastName;
            cmd.MiddleName = message.MiddleName;
            cmd.Email = message.Email;
            return cmd;
        }

    }
}
