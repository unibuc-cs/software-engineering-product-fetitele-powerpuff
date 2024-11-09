namespace healthy_lifestyle_web_app.Repositories
{
    public interface IRecipeFoodRepository
    {
        public Task<bool> AddFoodToRecipe(string recipeName, string foodName, int grams);
    }
}
