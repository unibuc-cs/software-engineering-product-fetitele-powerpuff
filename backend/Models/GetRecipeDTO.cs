using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Models
{
    public class GetRecipeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RecipeFood> RecipeFoods { get; set; }
    }
}
