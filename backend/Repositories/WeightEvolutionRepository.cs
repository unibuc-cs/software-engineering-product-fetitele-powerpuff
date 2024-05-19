using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class WeightEvolutionRepository : IWeightEvolutionRepository
    {
        private readonly ApplicationContext _context;

        public WeightEvolutionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<WeightEvolution>> GetByProfileIdAsync(int profileId)
        {
           return await _context.WeightEvolutions.Where(we => we.ProfileId == profileId).ToListAsync();
        }

        public async Task<WeightEvolution?> GetByProfileIdAndDateAsync(int profileId, DateOnly date)
        {
            var weightEvolutions = await _context.WeightEvolutions.Where(we => we.ProfileId == profileId ).ToListAsync();
            weightEvolutions = weightEvolutions.OrderByDescending(weightEvolutions => weightEvolutions.Date).ToList();
            for (var i = 0; i < weightEvolutions.Count; i++)
            {
                if (date >= weightEvolutions[i].Date)
                {
                    return weightEvolutions[i];
                }
            }
            return null;
        }

        public async Task<bool> PostAsync(int profileId, double weight)
        {
            // Create a weight evolution for a profile for today
            WeightEvolution weightEvolution = new WeightEvolution(profileId, weight, DateOnly.FromDateTime(DateTime.Today));

            try
            {
                _context.WeightEvolutions.Add(weightEvolution);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
