using System.ComponentModel.DataAnnotations;

namespace healthy_lifestyle_web_app.Entities
{
    public class Tutorial
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public double Grams { get; set; }

        public double Calories { get; set; }

        public double Carbohydrates { get; set; }

        public double Proteins { get; set; }
        public double Fats { get; set; }
        public string VideoLink { get; set; }
    }

}