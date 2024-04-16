using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace healthy_lifestyle_web_app.Entities
{
    public class DayPhysicalActivity
    {
        [Key]
        [Column(Order = 0)]
        public int ProfileId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateOnly Date { get; set; }

        [Key]
        [Column(Order = 2)]
        public int PhysicalActivityId { get; set; }
        public PhysicalActivity PhysicalActivity { get; set; }

        public int Minutes { get; set; }

        public DayPhysicalActivity(int profileId, DateOnly date, int physicalActivityId, int minutes)
        {
            ProfileId = profileId;
            Date = date;
            PhysicalActivityId = physicalActivityId;
            Minutes = minutes;
        }
    }
}
