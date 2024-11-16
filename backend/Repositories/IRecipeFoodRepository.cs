namespace healthy_lifestyle_web_app.Repositories
{
    public interface IRecipeFoodRepository
    {
        public Task<bool> AddOrUpdateFoodInRecipe(string recipeName, string foodName, int grams);
        public Task<bool> RemoveFoodFromRecipe(string recipeName, string foodName); 
    }
}
