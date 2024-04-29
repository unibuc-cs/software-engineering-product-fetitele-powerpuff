namespace healthy_lifestyle_web_app.Entities
{
    public class PhysicalActivity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public ICollection<Muscle> Muscles { get; set; } = new List<Muscle>();
        public ICollection<DayPhysicalActivity>? DayPhysicalActivities { get; set; }
    }
}
