using healthy_lifestyle_web_app.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeFoodController : ControllerBase
    {
        private readonly IRecipeFoodRepository _recipeFoodRepository;

        public RecipeFoodController(IRecipeFoodRepository recipeFoodRepository)
        {
            _recipeFoodRepository = recipeFoodRepository;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddFoodToRecipe(string recipeName, string foodName, int grams)
        {
            if (grams <= 0)
            {
                return BadRequest("Error adding food to recipe: grams must be > 0");
            }
            if (await _recipeFoodRepository.AddFoodToRecipe(recipeName, foodName, grams))
            {
                return Ok("Successfully added food to recipe");
            }
            return BadRequest("Error adding food to recipe");
        }
    }
}
