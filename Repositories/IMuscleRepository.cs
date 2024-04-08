using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IMuscleRepository
    {
        public Task<List<Muscle>> GetAllAsync();
        public Task<bool> PostAsync(Muscle muscle);
        public Task<bool> DeleteAsync(Muscle muscle);
    }
}
