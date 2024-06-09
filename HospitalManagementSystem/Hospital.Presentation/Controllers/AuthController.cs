using Hospital.Application.Auth.Commands;
using Hospital.Presentation.Filters;
using Hospital.Presentation.Requests.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        [ServiceFilter(typeof(ModelValidationFilter))]
        public async Task<IActionResult> RegisterUser(RegisterUserDto user)
        {
            var command = new RegisterNewUser(user.Name, user.Surname, user.Email, user.Password, user.Role);
            var accessToken = await _mediator.Send(command);

            return Ok(accessToken);
        }

        [HttpPost("Login")]
        [ServiceFilter(typeof(ModelValidationFilter))]
        public async Task<IActionResult> LoginUser(LoginUserDto user)
        {
            var command = new LoginUser(user.Email, user.Password);
            var accessToken = await _mediator.Send(command);

            return Ok(accessToken);
        }
    }
}
