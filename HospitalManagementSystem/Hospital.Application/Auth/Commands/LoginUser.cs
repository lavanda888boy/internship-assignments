using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Application.Auth.Commands
{
    public record LoginUser(string Email, string Password) : IRequest<string>;

    public class LoginUserHandler : IRequestHandler<LoginUser, string>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtGenerationService _jwtGenerationService;

        public LoginUserHandler(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IJwtGenerationService jwtGenerationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerationService = jwtGenerationService;
        }

        public async Task<string> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UserLoginException("Invalid email or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                throw new UserLoginException("Invalid email or password");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var token = _jwtGenerationService.GenerateAccessToken(userClaims);

            return token;
        }
    }
}
