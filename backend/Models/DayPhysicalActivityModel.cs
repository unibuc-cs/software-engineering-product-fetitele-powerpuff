namespace healthy_lifestyle_web_app.Models
{
    public class DayPhysicalActivityModel
    {
        public DateOnly Date {  get; set; }
        public string ActivityName { get; set; }
        public int Minutes { get; set; }
    }
}
