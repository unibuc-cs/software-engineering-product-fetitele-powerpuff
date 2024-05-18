using AutoMapper;
using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class PhysicalActivityRepository: IPhysicalActivityRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public PhysicalActivityRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PhysicalActivity>> GetAllAsync()
        {
            return (await _context.PhysicalActivities
                .Include(p => p.Muscles).ToListAsync());
        }

        public async Task<PhysicalActivity?> GetByIdAsync(int id)
        {
            return await _context.PhysicalActivities
                        .Include(p => p.Muscles)
                        .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PhysicalActivity?> GetByNameAsync(string name)
        {
            return await _context.PhysicalActivities
                        .Include(p => p.Muscles)
                        .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
        }

        public async Task<List<PhysicalActivity>?> GetByMuscleAsync(string muscleName)
        {
            muscleName = char.ToUpper(muscleName[0]) + muscleName.Substring(1).ToLower();
            Muscle? muscle = await _context.Muscles.FirstOrDefaultAsync(m => m.Name == muscleName);
            if (muscle == null)
                return null;

            List<PhysicalActivity> physicalActivities = await _context.PhysicalActivities
                                            .Include(p => p.Muscles).ToListAsync();

            return physicalActivities.FindAll(p => p.Muscles.Contains(muscle));
        }

        public async Task<bool> PostAsync(PhysicalActivity physicalActivity)
        {
            try
            {
                _context.PhysicalActivities.Add(physicalActivity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(PhysicalActivity physicalActivity)
        {
            PhysicalActivity? p = await _context.PhysicalActivities.FirstOrDefaultAsync(x => 
                                                        x.Name == physicalActivity.Name);

            if (p == null)
            {
                return false;
            }

            _context.PhysicalActivities.Remove(p);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
