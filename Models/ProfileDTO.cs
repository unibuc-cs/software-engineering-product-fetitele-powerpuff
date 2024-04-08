using healthy_lifestyle_web_app.Entities;
using System.Text.Json.Serialization;

namespace healthy_lifestyle_web_app.Models
{
    public class ProfileDTO
    {
        public string Name { get; set; }
        public DateOnly Birthdate { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Goal Goal { get; set; }
    }
}
