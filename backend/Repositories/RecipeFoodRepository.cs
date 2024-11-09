using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class RecipeFoodRepository : IRecipeFoodRepository
    {
        private readonly ApplicationContext _context;

        public RecipeFoodRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrUpdateFoodInRecipe(string recipeName, string foodName, int grams)
        {
            if (grams <= 0)
            {
                return false; // Valoare invalidă pentru gramaj
            }

            Recipe? recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Name == recipeName);
            Food? food = await _context.Foods.FirstOrDefaultAsync(f => f.Name == foodName);


            if (recipe == null || food == null)
            {
                return false;
            }

            var existingRecipeFood = await _context.RecipeFoods
                .FirstOrDefaultAsync(rf => rf.RecipeId == recipe.Id && rf.FoodId == food.Id);

            if (existingRecipeFood != null)
            {
                existingRecipeFood.Grams = grams;
            }
            else
            {
                _context.RecipeFoods.Add(new RecipeFood(recipe.Id, food.Id, grams));
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> RemoveFoodFromRecipe(string recipeName, string foodName)
        {
      
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Name == recipeName);

            var food = await _context.Foods
                .FirstOrDefaultAsync(f => f.Name == foodName);

         
            if (recipe == null || food == null)
            {
                return false;
            }

   
            var recipeFood = await _context.RecipeFoods
                .FirstOrDefaultAsync(rf => rf.RecipeId == recipe.Id && rf.FoodId == food.Id);

            if (recipeFood == null)
            {
                return false;
            }

            _context.RecipeFoods.Remove(recipeFood);
            await _context.SaveChangesAsync();

            return true;
        }




        
        
    }
}
