using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IPhysicalActivityRepository
    {
        public Task<List<PhysicalActivity>> GetAllAsync();
        public Task<bool> PostAsync(PhysicalActivity physicalActivity);
        public Task<bool> DeleteAsync(PhysicalActivity physicalActivity);
    }
}
