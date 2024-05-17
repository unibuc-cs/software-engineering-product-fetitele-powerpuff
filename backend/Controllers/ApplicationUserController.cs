using healthy_lifestyle_web_app.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        public ApplicationUserController(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetApplicationUsers()
        {
            return Ok(await _applicationUserRepository.GetAllAsync());
        }

        // Un admin poate sa stearga si dupa id si dupa email
        [HttpDelete("dupa-id/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            // Verifică dacă utilizatorul curent este administrator
            if (!User.IsInRole("admin"))
            {
                return Forbid("Only admins can delete users.");
            }

            // Apelăm metoda de ștergere a utilizatorului din repository
            if (await _applicationUserRepository.DeleteAsync(id))
            {
                return Ok("User deleted successfully");
            }

            return NotFound("User not found");
        }

        [HttpDelete("dupa-email/{email}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {
            /*if (!User.IsInRole("admin"))
            {
                return Forbid("Only admins can delete users");
            }*/

            var user = await _applicationUserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (await _applicationUserRepository.DeleteByEmailAsync(email))
            {
                return Ok("User deleted successfully");
            }

            return BadRequest("Failed to delete user");
        }
    }
}
