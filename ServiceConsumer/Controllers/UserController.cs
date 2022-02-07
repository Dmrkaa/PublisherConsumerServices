using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceConsumer.CQRS.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ServiceConsumer.CQRS.Queries;
using ServiceConsumer.Dtos;
using ServiceConsumer.Models;
using AutoMapper;

namespace ServiceConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;
        private readonly IMapper _mapper;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;
        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Connect user with any organization.
        /// </summary>
        /// <param name="userID">ID of the User</param>
        /// <param name="organizationID">ID of the organization</param>
        [HttpGet("{organizationID}")]
        [HttpPut("{userID}/{organizationID}")]
        public async Task<IActionResult> UserAndOrganizationConnect(Guid userID, Guid organizationID)
        {
            var updateCommand = new UpdateUserOrganizationCommand() { OrganizationID = organizationID, UserID = userID };
            return Ok(await Mediator.Send(updateCommand));
        }



        /// <summary>
        /// Load a Users info by Organization id
        /// </summary>
        /// <param name="id">ID of the needed Organization</param>
        [HttpGet("{organizationID}")]
        public async Task<IActionResult> GetUsersByOrganizationID(Guid organizationID)
        {
            var users = await Mediator.Send(new GetUsersQuery());
            var organization = await Mediator.Send(new GetOrganizationByIdQuery() { OrganizationID = organizationID });
            return Ok(BuildResult(organization, (List<User>)users));
        }

        [NonAction]
        public OrganizationDto BuildResult(Organization orgs, List<User> users)
        {
            var organizationDtos = _mapper.Map<OrganizationDto>(orgs);
            return organizationDtos;
        }
    }
}
