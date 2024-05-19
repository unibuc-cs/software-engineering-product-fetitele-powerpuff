using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace healthy_lifestyle_web_app.Entities
{
    public class Day
    {
        [Key]
        [Column(Order = 0)]
        public int ProfileId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateOnly Date { get; set; }

        [JsonIgnore]
        public Profile? Profile { get; set; }
        public int Calories { get; set; }

        public ICollection<DayFood> DayFoods { get; set; }
        public ICollection<DayPhysicalActivity> DayPhysicalActivities { get; set; }

        public Day(int profileId, DateOnly date, int calories)
        {
            ProfileId = profileId;
            Date = date;
            Calories = calories;
            DayFoods = new List<DayFood>();
            DayPhysicalActivities = new List<DayPhysicalActivity>();
        }
    }
}
