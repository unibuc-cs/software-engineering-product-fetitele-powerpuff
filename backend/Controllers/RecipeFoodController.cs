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
        public async Task<IActionResult> AddOrUpdateFoodToRecipe(string recipeName, string foodName, int grams)
        {
            if (grams <= 0)
            {
                return BadRequest("Error: grams must be greater than zero.");
            }

          
            var result = await _recipeFoodRepository.AddOrUpdateFoodInRecipe(recipeName, foodName, grams);

            if (result)
            {
                return Ok("Food added/updated successfully.");
            }

            return BadRequest("Error adding/updating food to recipe.");
        }



        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveFoodFromRecipe(string recipeName, string foodName)
        {
            var isRemoved = await _recipeFoodRepository.RemoveFoodFromRecipe(recipeName, foodName);

            if (isRemoved)
            {
                return Ok("Successfully removed food from recipe.");
            }

            return BadRequest("Error removing food from recipe: food may not exist in the recipe.");
        }



    }
}