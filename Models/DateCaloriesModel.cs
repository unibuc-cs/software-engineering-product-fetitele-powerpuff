namespace healthy_lifestyle_web_app.Models
{
    public class DateCaloriesModel
    {
        public DateOnly Date { get; set; }
        public double Calories { get; set; }

        public DateCaloriesModel(DateOnly date, double calories) 
        {
            Date = date;
            Calories = calories;
        }
    }
}
