using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace healthy_lifestyle_web_app.Entities
{
    public class WeightEvolution
    {
        // Composite primary key EvolutionId and ProfileId
        // To automatically generate EvolutionId
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EvolutionId { get; set; }
        public int ProfileId { get; set; }
        public Profile? Profile { get; set; }
        public double Weight { get; set; }
        public DateOnly Date { get; set; }

        public WeightEvolution(int profileId, double weight, DateOnly date)
        {
            ProfileId = profileId;
            Weight = weight;
            Date = date;
        }
    }
}
