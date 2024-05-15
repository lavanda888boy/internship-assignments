using Hospital.Application.Auth.Commands;
using Hospital.Presentation.Filters;
using Hospital.Presentation.Requests.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Register")]
        [ServiceFilter(typeof(ModelValidationFilter))]
        public async Task<IActionResult> RegisterUser(RegisterUserDto user)
        {
            _logger.LogInformation("New user registration: {Name} {Surname}...", user.Name, user.Surname);

            var command = new RegisterNewUser(user.Name, user.Surname, user.Email, user.Password, user.Role);
            var accessToken = await _mediator.Send(command);

            _logger.LogInformation("User: {Name} {Surname} was successfully registered", user.Name, user.Surname);

            return Ok(accessToken);
        }

        [HttpPost("Login")]
        [ServiceFilter(typeof(ModelValidationFilter))]
        public async Task<IActionResult> LoginUser(LoginUserDto user)
        {
            _logger.LogInformation("New user login: {Email}...", user.Email);

            var command = new LoginUser(user.Email, user.Password);
            var accessToken = await _mediator.Send(command);

            _logger.LogInformation("User: {Email} succesfully logged in", user.Email);

            return Ok(accessToken);
        }
    }
}
