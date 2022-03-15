using System;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServicePublisher.Models;
using Microsoft.Extensions.Logging;

namespace ServicePublisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBus _busService;
        private readonly ILogger<UserController> _logger;
        public UserController(IBus busService, ILogger<UserController> logger)
        {
            _logger = logger;
            _busService = busService;
        }
        
        [HttpPost]
        public async Task<IActionResult> SendPersonInfo(Shared.Models.UserSharedModel person)
        {
            if (ModelState.IsValid)
            {
                Uri uri = new Uri("rabbitmq://localhost/test-queue");
                var endPoint = await _busService.GetSendEndpoint(uri);
                await endPoint.Send(person);
                _logger.LogInformation($"Object sended:Name: {person.Name},Lastname: {person.LastName}, Email: {person.Email}");
                return Ok(value: new { value = $"Object sended:Name: { person.Name},Lastname: { person.LastName}, Email: { person.Email}" });
            }
            else
            {
                _logger.LogError($"Error:Data object is not valid!");
                return BadRequest(error: new { error = "Data object is not valid!" });
            }
        }

    }
}
