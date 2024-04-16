using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using healthy_lifestyle_web_app.Controllers;

namespace healthy_lifestyle_web_app.Entities
{
    public class DayFood
    {
        [Key]
        [Column(Order = 0)]
        public int ProfileId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateOnly Date { get; set; }

        [Key]
        [Column(Order = 2)]
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int Grams { get; set; }

        public DayFood(int profileId, DateOnly date, int foodId, int grams)
        {
            ProfileId = profileId;
            Date = date;
            FoodId = foodId;
            Grams = grams;
        }
    }
}
