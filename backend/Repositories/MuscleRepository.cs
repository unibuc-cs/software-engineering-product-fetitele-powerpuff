using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class MuscleRepository : IMuscleRepository
    {
        private readonly ApplicationContext _context;

        // Basic get, post and delete functionalities

        public MuscleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Muscle>> GetAllAsync()
        {
            return (await _context.Muscles.ToListAsync());
        }

        public async Task<bool> PostAsync(Muscle muscle)
        {
            try
            {
                _context.Muscles.Add(muscle);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Muscle muscle)
        {
            Muscle? m = await _context.Muscles.FirstOrDefaultAsync(x => x.Name == muscle.Name);

            if (m == null)
            {
                return false;
            }

            _context.Muscles.Remove(m);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
