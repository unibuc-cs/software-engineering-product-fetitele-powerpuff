namespace healthy_lifestyle_web_app.Entities
{
    public class Muscle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PhysicalActivity> PhysicalActivities { get; set; } = new List<PhysicalActivity>();
    }
}
