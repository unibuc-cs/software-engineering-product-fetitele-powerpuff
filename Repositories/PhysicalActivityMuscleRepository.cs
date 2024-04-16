using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace healthy_lifestyle_web_app.Repositories
{
    public class PhysicalActivityMuscleRepository: IPhysicalActivityMuscleRepository
    {
        private readonly ApplicationContext _context;

        public PhysicalActivityMuscleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> AddMuscleToPhysicalActivity(string muscleName, string activityName)
        {
            Muscle? muscle = await _context.Muscles.FirstOrDefaultAsync(x =>
                                            x.Name == muscleName);

            PhysicalActivity? physicalActivity = await _context.PhysicalActivities.FirstOrDefaultAsync(x => 
                                            x.Name == activityName);  

            if(muscle == null || physicalActivity == null)
            {
                return false;
            }

            muscle.PhysicalActivities.Add(physicalActivity);
            physicalActivity.Muscles.Add(muscle);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteMuscleFromPhysicalActivity(string muscleName, string activityName)
        {
            // Includes the list of associated entities
            Muscle? muscle = await _context.Muscles.Include(x => x.PhysicalActivities)
                                            .FirstOrDefaultAsync(x => x.Name == muscleName);

            PhysicalActivity? physicalActivity = await _context.PhysicalActivities.Include(x => x.Muscles)
                                           .FirstOrDefaultAsync(x =>
                                            x.Name == activityName);

            if (muscle == null || physicalActivity == null)
            {
                return false;
            }

            muscle.PhysicalActivities.Remove(physicalActivity);
            physicalActivity.Muscles.Remove(muscle);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
