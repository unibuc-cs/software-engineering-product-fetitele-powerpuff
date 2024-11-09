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

        public async Task<bool> AddFoodToRecipe(string recipeName, string foodName, int grams)
        {
            Recipe? recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Name == recipeName);
            Food? food = await _context.Foods.FirstOrDefaultAsync(f => f.Name == foodName);

            if (recipe == null || food == null) 
            {
                return false;
            }

            try
            {
                _context.RecipeFoods.Add(new RecipeFood(recipe.Id, food.Id, grams));
                await _context.SaveChangesAsync();
                return true;
            }
            catch(DbUpdateException)
            {
                return false;
            }
        }
    }
}
