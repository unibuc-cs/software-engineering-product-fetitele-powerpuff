using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace healthy_lifestyle_web_app.Entities
{
    public class RecipeFood
    {
        [Key]
        [Column(Order = 0)]
        public int RecipeId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int FoodId { get; set; }

        public int Grams { get; set; }

        public RecipeFood(int recipeId, int foodId, int grams)
        {
            RecipeId = recipeId;
            FoodId = foodId;
            Grams = grams;
        }   
    }
}
