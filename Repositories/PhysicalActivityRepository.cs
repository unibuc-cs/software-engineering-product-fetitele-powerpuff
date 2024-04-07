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
