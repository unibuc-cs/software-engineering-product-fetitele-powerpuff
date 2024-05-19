using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Models
{
    public class GetPhysicalActivityDTO
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public ICollection<GetMuscleDTO> muscles { get; set; } = new List<GetMuscleDTO>();
    }
}
