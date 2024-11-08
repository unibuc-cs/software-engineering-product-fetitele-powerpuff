using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Models
{
    public class GetDayDTO
    {
        public DateOnly Date {  get; set; }
        public int Calories { get; set; }
        public int WaterIntake { get; set; }
        public ICollection<DayFoodDTO>? DayFoods { get; set; }
        public ICollection<DayPhysicalActivityDTO>? DayPhysicalActivities { get; set; }
    }
}
