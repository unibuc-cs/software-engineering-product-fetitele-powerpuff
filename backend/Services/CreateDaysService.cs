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

        public async Task<bool> CreateDays()
        {
            try
            {
                List<Profile> profiles = await _profileRepository.GetAllAsync();
                foreach (Profile profile in profiles)
                {
                    await _dayRepository.PostDayAsync(profile.Id);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
