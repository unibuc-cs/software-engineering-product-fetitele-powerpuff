namespace healthy_lifestyle_web_app.Models
{
    public class DayFoodCaloriesModel
    {
        public DateOnly Date { get; set; }
        public double Calories { get; set; }

        public DayFoodCaloriesModel(DateOnly date, double calories) 
        {
            Date = date;
            Calories = calories;
        }
    }
}
