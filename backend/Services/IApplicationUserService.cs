using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Services
{
    public interface IApplicationUserService
    {
        // Get profile by user email
        public Task<Profile?> GetUserProfileByEmail(string email);
    }
}
