using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IProfileRepository
    {
        public Task<List<Profile>> GetAllAsync();
        public Task<Profile?> GetByApplicationUserIdAsync(string applicationUserId);
        public Task<bool> PostAsync(Profile profile);
        public Task<bool> PutAsync(int id, PostProfileDTO profileDTO);
        public Task<bool> PutNameAsync(int id, string newName);
        public Task<bool> PutBirthdateAsync(int id, DateOnly newBirthdate);
        public Task<bool> PutWeightAsync(int id, double newWeight);
        public Task<bool> PutHeightAsync(int id, double newHeight);
        public Task<bool> PutGoalAsync(int id, Goal newGoal);
        public Task<bool> DeleteAsync(int id);
    }
}
