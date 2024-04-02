using System.Text.Json.Serialization;

namespace healthy_lifestyle_web_app.Entities
{
    public enum Goal
    {
        Lose,
        Gain,
        Maintain
    }

    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Goal Goals { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Day>? Days { get; set;}
    }
}
