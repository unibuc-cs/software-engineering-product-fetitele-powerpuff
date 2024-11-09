namespace healthy_lifestyle_web_app.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RecipeFood> RecipeFoods { get; set; }
    }
}
