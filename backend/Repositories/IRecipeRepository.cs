using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IRecipeRepository
    {
        public Task<List<Recipe>> GetAllAsync();
        public Task<bool> PostAsync(Recipe recipe);
        public Task<bool> DeleteRecipeByNameAsync(string recipeName);

    }
}
