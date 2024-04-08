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
        public DateOnly Birthdate { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Goal Goal { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Day>? Days { get; set;}
    }
}
