using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IApplicationUserRepository
    {
        public Task<List<ApplicationUser>> GetAllAsync();

        public Task<ApplicationUser?> GetByEmailAsync(string email);
    }
}
