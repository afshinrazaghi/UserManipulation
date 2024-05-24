using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManipulation.API.Controllers.Common;
using UserManipulation.Application.DOTs.Auth;
using UserManipulation.Application.Features.Users.Login.Commands;

namespace UserManipulation.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : UMControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public AccountController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("Login")]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = _mapper.Map<LoginCommand>(request);
            var res = await _sender.Send(command);
            if (!string.IsNullOrEmpty(res.Token))
                return Ok(res);
            return Unauthorized();
        }
    }
}
