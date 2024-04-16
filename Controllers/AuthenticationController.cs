using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Services.IAuthenticationService _authenticationService;

        public AuthenticationController(UserManager<ApplicationUser> userManager, Services.IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            // Verifies if there is already a user with this email
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "This email is already used" });
            }

            ApplicationUser? user = await _authenticationService.CreateUser(model);

            // Checks is the user was created
            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Register failed" });
            }

            // Creates user roles if they don't already exist
            await _authenticationService.CreateRoles();

            // Assigns roles to the user
            await _authenticationService.AssignUserRole(user);

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        // Promotes a normal user to admin
        [HttpPut("promote")]
        public async Task<IActionResult> PromoteUserToAdmin(string email)
        {
            // Check if the user exists
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "There is no registered user with this email" });
            }

            // Check if the user is already an admin
            var isAdmin = await _userManager.IsInRoleAsync(user, Roles.Admin);
            if (isAdmin)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = "This user is already an admin" });
            }

            // Add the admin role
            await _authenticationService.PromoteUserToAdmin(user);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Checks if the user with this email exists, then checks if password matches
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);

                // Creates claims for the user containing name (as the email), a unique identifier and their roles
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Creates a token based on the user's claims
                // Can now access certain features depending on the role
                var token = _authenticationService.GetToken(claims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized("Invalid email or password");
        }

    }
}
