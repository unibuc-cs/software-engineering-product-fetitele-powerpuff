using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Repositories;

namespace healthy_lifestyle_web_app.Services
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;


        public ApplicationUserService(IApplicationUserRepository applicationUserRepository, IProfileRepository profileRepository)
        {
            _userRepository = applicationUserRepository;
            _profileRepository = profileRepository;
        }

        public async Task<Entities.Profile?> GetUserProfileByEmail(string email)
        {
            ApplicationUser? currentUser = await _userRepository.GetByEmailAsync(email);
            if (currentUser == null)
            {
                return null;
            }

            Entities.Profile? profile = await _profileRepository.GetByApplicationUserIdAsync(currentUser.Id);
            if (profile == null)
            {
                return null;
            }

            return profile;
        }
    }
}
