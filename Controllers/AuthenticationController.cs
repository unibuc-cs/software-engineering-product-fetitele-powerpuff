using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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

            // Verificam daca exista deja utilizatorul cu acest email
            if(await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "This email is already used" });
            }

            var user = await _authenticationService.CreateUser(model);

            // Verifica daca a fost creat
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Register failed" });

            // Creaza rolurile daca nu exista
            await _authenticationService.CreateRoles();

            // Atribuie rolurile utilizatorului
            await _authenticationService.AssignUserRole(user);

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

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
            // Daca exista utilizatorul si e validata parola
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);

                // Creaza claims pt utilizatorul continand numele, un identificator unic si rolurile sale
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Creeaza un token bazat pe claims ale utilizatorului
                // Folosit pentru a avea acces la anumite metode
                var token = _authenticationService.GetToken(claims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

    }
}
