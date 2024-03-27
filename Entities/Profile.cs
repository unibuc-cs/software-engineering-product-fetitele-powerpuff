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
        public ICollection<Goal> Goals { get; set; }
    }
}
