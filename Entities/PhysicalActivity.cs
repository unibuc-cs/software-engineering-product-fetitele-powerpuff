﻿using System.Text.Json.Serialization;

namespace healthy_lifestyle_web_app.Entities
{
    public enum Muscle
    {
        Legs,
        Arms
    }

    public class PhysicalActivity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ICollection<Muscle> Muscles { get; set; }
    }
}
