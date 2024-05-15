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
        private readonly PasswordHasher<IdentityUser> _passwordHasher;

        public RegisterNewUserHandler(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, IJwtGenerationService jwtGenerationService, 
            PasswordHasher<IdentityUser> passwordHasher)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtGenerationService = jwtGenerationService;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Handle(RegisterNewUser request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.HashPassword(null, request.Password);

            var newUser = new IdentityUser
            {
                UserName = request.Name + " " + request.Surname,
                Email = request.Email,
                PasswordHash = hashedPassword,
            };
            var result = await _userManager.CreateAsync(newUser, hashedPassword);

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
                    new("Name", request.Name),
                    new("Surname", request.Surname),
                    new("Role", request.Role)
                };

                await _userManager.AddClaimsAsync(newUser, newClaims);

                var token = _jwtGenerationService.GenerateAccessToken(newClaims);

                return await Task.FromResult(token);
            }
            else
            {
                throw new UserRegistrationException("An error occured in the process of user registration");
            }
        }
    }
}
