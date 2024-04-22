using healthy_lifestyle_web_app.Repositories;
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
        public async Task<IActionResult> GetApplicationUsers()
        {
            return Ok(await _applicationUserRepository.GetAllAsync());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            // Verifică dacă utilizatorul curent este administrator
            if (!User.IsInRole("Admin"))
            {
                return Forbid("Only admins can delete users.");
            }

            // Apelăm metoda de ștergere a utilizatorului din repository
            if (await _applicationUserRepository.DeleteAsync(id))
            {
                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {
            if (!User.IsInRole("Admin"))
            {
                return Forbid("Only admins can delete users.");
            }

            var user = await _applicationUserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (await _applicationUserRepository.DeleteByEmailAsync(email))
            {
                return Ok();
            }

            return BadRequest("Failed to delete user");
        }

    }
}
