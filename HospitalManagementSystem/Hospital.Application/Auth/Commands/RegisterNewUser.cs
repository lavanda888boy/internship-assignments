using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Hospital.Application.Auth.Commands
{
    public record RegisterNewUser(string Name, string Surname, string Email, string Password, string Role) 
        : IRequest<string>; 

    public class RegisterNewUserHandler : IRequestHandler<RegisterNewUser, string>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtGenerationService _jwtGenerationService;

        public RegisterNewUserHandler(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, IJwtGenerationService jwtGenerationService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtGenerationService = jwtGenerationService;
        }

        public async Task<string> Handle(RegisterNewUser request, CancellationToken cancellationToken)
        {
            var newUser = new IdentityUser
            {
                UserName = request.Name + request.Surname,
                Email = request.Email,
            };
            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {
                var roleExists = await _roleManager.RoleExistsAsync(request.Role);
                
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(request.Role));
                }
                
                await _userManager.AddToRoleAsync(newUser, request.Role);

                var newClaims = new List<Claim>()
                {
                    new(ClaimTypes.Name, request.Name),
                    new(ClaimTypes.Surname, request.Surname),
                    new(ClaimTypes.Email, request.Email),
                    new(ClaimTypes.Role, request.Role)
                };

                await _userManager.AddClaimsAsync(newUser, newClaims);

                var token = _jwtGenerationService.GenerateAccessToken(new List<Claim>()
                {
                    new(ClaimTypes.Email, request.Email),
                    new(ClaimTypes.Role, request.Role)
                });

                return await Task.FromResult(token);
            }
            else
            {
                throw new UserRegistrationException("An error occured in the process of user registration");
            }
        }
    }
}
