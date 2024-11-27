using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationContext _context;

        public RecipeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllAsync()
        {
            return await _context.Recipes.Include(r => r.RecipeFoods).ToListAsync();
        }

        public async Task<bool> PostAsync(Recipe recipe)
        {
            try
            {
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteRecipeByNameAsync(string recipeName)
        {
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Name == recipeName);

            if (recipe == null)
            {
                return false;  
            }

            
            var recipeFoods = await _context.RecipeFoods
                .Where(rf => rf.RecipeId == recipe.Id)
                .ToListAsync();
            _context.RecipeFoods.RemoveRange(recipeFoods); 

            
            _context.Recipes.Remove(recipe);

            await _context.SaveChangesAsync();
            return true; 
        }

       
        public async Task<List<Recipe>> FilterRecipesByNameAsync(string name)
        {
            return await _context.Recipes
                .Include(r => r.RecipeFoods) 
                .Where(r => r.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<List<Recipe>> FilterRecipesByNutrientsAsync(
      int? minCalories, int? maxCalories,
      int? minProteins, int? maxProteins,
      int? minCarbs, int? maxCarbs,
      int? minFats, int? maxFats)
        {
            
            var query = _context.Recipes
                .Include(r => r.RecipeFoods) 
                .ThenInclude(rf => rf.Food) 
                .AsQueryable();

        

            if (minCalories.HasValue)
            {
                query = query.Where(r => r.RecipeFoods
                    .Sum(rf => rf.Grams * rf.Food.Calories / 100.0) >= minCalories.Value);
            }

            if (maxCalories.HasValue)
            {
                query = query.Where(r => r.RecipeFoods
                    .Sum(rf => rf.Grams * rf.Food.Calories / 100.0) <= maxCalories.Value);
            }

            if (minProteins.HasValue)
            {
                query = query.Where(r => r.RecipeFoods
                    .Sum(rf => rf.Grams * rf.Food.Proteins / 100.0) >= minProteins.Value);
            }

            if (maxProteins.HasValue)
            {
                query = query.Where(r => r.RecipeFoods
                    .Sum(rf => rf.Grams * rf.Food.Proteins / 100.0) <= maxProteins.Value);
            }

            if (minCarbs.HasValue)
            {
                query = query.Where(r => r.RecipeFoods
                    .Sum(rf => rf.Grams * rf.Food.Carbohydrates / 100.0) >= minCarbs.Value);
            }

            if (maxCarbs.HasValue)
            {
                query = query.Where(r => r.RecipeFoods
                    .Sum(rf => rf.Grams * rf.Food.Carbohydrates / 100.0) <= maxCarbs.Value);
            }

            if (minFats.HasValue)
            {
                query = query.Where(r => r.RecipeFoods
                    .Sum(rf => rf.Grams * rf.Food.Fats / 100.0) >= minFats.Value);
            }

            if (maxFats.HasValue)
            {
                query = query.Where(r => r.RecipeFoods
                    .Sum(rf => rf.Grams * rf.Food.Fats / 100.0) <= maxFats.Value);
            }

            
            var filteredRecipes = await query.ToListAsync();

            return filteredRecipes;
        }



    }
}
