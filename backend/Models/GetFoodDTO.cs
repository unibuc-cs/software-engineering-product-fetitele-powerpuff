namespace healthy_lifestyle_web_app.Models
{
    public class GetFoodDTO
    {
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Carbohydrates { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }

        public bool Public { get; set; }

    }
}
