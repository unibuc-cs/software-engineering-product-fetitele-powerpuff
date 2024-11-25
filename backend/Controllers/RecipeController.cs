using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRecipes()
        {
            List<Recipe> recipes = await _recipeRepository.GetAllAsync();
            List<GetRecipeDTO> recipeDTOs = new List<GetRecipeDTO>();
            for (int i = 0; i < recipes.Count; i++)
            {
                recipeDTOs.Add(_mapper.Map<GetRecipeDTO>(recipes[i]));
            }
            return Ok(recipeDTOs);
        }

        // Add a new recipe
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PostRecipe(PostRecipeDTO recipe)
        {
            if (await _recipeRepository.PostAsync(_mapper.Map<Recipe>(recipe)))
            {
                return Ok("Recipe added successfully");
            }
            return BadRequest("Error adding recipe: recipe already in the database");
        }


        [HttpDelete("{recipeName}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRecipe(string recipeName)
        {
            var result = await _recipeRepository.DeleteRecipeByNameAsync(recipeName);

            if (result)
            {
                return Ok("Recipe deleted successfully.");
            }
            return BadRequest("Error deleting recipe: Recipe may not exist.");
        }

    }
}
