namespace healthy_lifestyle_web_app.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
    }
}
