using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace healthy_lifestyle_web_app.Services
{
    public interface IAuthenticationService
    {
        public Task CreateRoles();
        public Task<ApplicationUser?> CreateUser(RegisterModel model);
        public Task AssignUserRole(ApplicationUser user);
        public Task<ApplicationUser?> PromoteUserToAdmin(ApplicationUser applicationUser);
        public JwtSecurityToken GetToken(List<Claim> claims);
    }
}
