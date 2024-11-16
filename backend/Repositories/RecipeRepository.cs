﻿using healthy_lifestyle_web_app.ContextModels;
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
    }
}
