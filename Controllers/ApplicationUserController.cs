using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserController(ApplicationContext applicationContext, UserManager<ApplicationUser> userManager)
        {
            _applicationContext = applicationContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicationUsers()
        {
            return Ok(await _userManager.Users.ToListAsync());
        }
    }
}
