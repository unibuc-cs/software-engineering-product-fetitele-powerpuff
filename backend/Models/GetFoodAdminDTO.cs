namespace healthy_lifestyle_web_app.Models
{
    public class GetFoodAdminDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Carbohydrates { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public bool Public { get; set; }
        public string? ApplicationUserId { get; set; }
    }
}
