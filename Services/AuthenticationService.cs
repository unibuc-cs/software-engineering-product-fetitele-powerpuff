using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace healthy_lifestyle_web_app.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync(Roles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            
            if (!await _roleManager.RoleExistsAsync(Roles.User))
                await _roleManager.CreateAsync(new IdentityRole(Roles.User));
        }

        public async Task<ApplicationUser?> CreateUser(RegisterModel model)
        {
            ApplicationUser user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                System.Diagnostics.Debug.WriteLine("---------------------------------------------------------------------------------------");
                foreach (IdentityError error in result.Errors)
                    System.Diagnostics.Debug.WriteLine($"{error.Description} ({error.Code})");
                return null;
            }

            return user;
        }

        public async Task AssignUserRole(ApplicationUser user)
        {
            if (await _roleManager.RoleExistsAsync(Roles.User))
                await _userManager.AddToRoleAsync(user, Roles.User);
        }

        public async Task<ApplicationUser?> PromoteUserToAdmin(ApplicationUser applicationUser)
        {
            // Check if the admin role exists
            if (!await _roleManager.RoleExistsAsync(Roles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            await _userManager.AddToRoleAsync(applicationUser, Roles.Admin);
            return applicationUser;
        }

        public JwtSecurityToken GetToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
