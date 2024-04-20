namespace healthy_lifestyle_web_app.Models
{
    public class GetRequestDTO
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public GetFoodAdminDTO? GetFoodAdmin { get; set; }
    }
}
