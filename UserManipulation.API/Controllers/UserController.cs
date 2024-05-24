using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManipulation.API.Controllers.Common;
using UserManipulation.Application.Common.Interfaces.MessageBrokers;
using UserManipulation.Application.Features.Users.GetUsers.Queries;

namespace UserManipulation.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class UserController : UMControllerBase
    {
        private readonly ISender _sender;
        private readonly IMessageProducer _messageProducer;
        public UserController(ISender sender, IMessageProducer messageProducer)
        {
            _sender = sender;
            _messageProducer = messageProducer;
        }

        [MapToApiVersion("1.0")]
        [HttpGet("GetUserList")]
        [Authorize]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserList()
        {
            var query = new GetUserListQuery();
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("SendUser")]
        [Authorize]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendUser()
        {
            var query = new GetFirstUserQuery();
            var user = await _sender.Send(query);
            _messageProducer.SendMessage(user);
            return Ok("Ok");
        }

    }
}
