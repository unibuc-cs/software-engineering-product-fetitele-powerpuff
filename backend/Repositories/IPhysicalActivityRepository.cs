using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IPhysicalActivityRepository
    {
        public Task<List<PhysicalActivity>> GetAllAsync();
        public Task<PhysicalActivity?> GetByIdAsync(int id);
        public Task<PhysicalActivity?> GetByNameAsync(string name);
        public Task<List<PhysicalActivity>?> GetByMuscleAsync(string muscleName);
        public Task<bool> PostAsync(PhysicalActivity physicalActivity);
        public Task<bool> DeleteAsync(PhysicalActivity physicalActivity);
    }
}
