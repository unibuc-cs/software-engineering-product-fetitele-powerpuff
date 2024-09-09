using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Repositories;
using System.Runtime.CompilerServices;

namespace healthy_lifestyle_web_app.Services
{
    public class CreateDaysService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IDayRepository _dayRepository;

        public CreateDaysService(IProfileRepository profileRepository, IDayRepository dayRepository)
        {
            _profileRepository = profileRepository;
            _dayRepository = dayRepository;
        }

        // Create a day based on the current date for every profile
        // Runs every day at midnight
        public async Task<bool> CreateDays()
        {
            try
            {
                List<Profile> profiles = await _profileRepository.GetAllAsync();
                foreach (Profile profile in profiles)
                {
                    await _dayRepository.PostDayAsync(profile);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Create a day based on the current date for a new user
        // Otherwise it would only be created at midnight
        public async Task<bool> CreateDayForNewUser(Profile profile)
        {
            if (await _dayRepository.PostDayAsync(profile))
            {
                return true;
            }
            return false;
        }
    }
}
