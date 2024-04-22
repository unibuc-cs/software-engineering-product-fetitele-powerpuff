using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IWeightEvolutionRepository
    {
        public Task<List<WeightEvolution>> GetByProfileIdAsync(int profileId);
        public Task<bool> PostAsync(int profileId, double weight);
    }
}
