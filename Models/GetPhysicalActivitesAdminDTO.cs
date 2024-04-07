using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Models
{
    public class GetPhysicalActivitesAdminDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int calories { get; set; }
        public ICollection<GetMuscleDTO> muscles { get; set; } = new List<GetMuscleDTO>();
    }
}
