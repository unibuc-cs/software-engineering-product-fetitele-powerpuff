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
    }
}
