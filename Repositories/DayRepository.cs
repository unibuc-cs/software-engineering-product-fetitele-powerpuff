using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace healthy_lifestyle_web_app.Repositories
{
    public class DayRepository: IDayRepository
    {
        private readonly ApplicationContext _context;

        public DayRepository(ApplicationContext context)
        {
            _context = context;
        }   

        public async Task<List<Day>> GetAllAsync()
        {
            return await _context.Days.Include(d => d.DayPhysicalActivities)
                .Include(d => d.DayFoods).ToListAsync();
        }

        // Get by profileId
        public async Task<List<Day>> GetByUserAsync(int id)
        { 
            return await _context.Days.Where(d => d.ProfileId == id)
                .Include(d => d.DayPhysicalActivities)
                .Include(d => d.DayFoods).ToListAsync();
        }

        public async Task<List<Day>> GetAfterDateByProfileAsync(int profileId, DateOnly date)
        {
            return await _context.Days.Where(d => d.ProfileId == profileId && d.Date >= date)
                .Include(d => d.DayPhysicalActivities)
                .Include(d => d.DayFoods).ToListAsync();
        }

        public async Task<Day?> GetCurrentDayAsync(int id)
        {
            List<Day> days =  await _context.Days.Where(d => d.ProfileId == id)
                .Include(d => d.DayPhysicalActivities).Include(d => d.DayFoods).ToListAsync();

            DateOnly currentDay = DateOnly.FromDateTime(DateTime.Today);

            return days.FirstOrDefault(d => d.Date == currentDay);
        }

        public async Task<Day?> GetByDateAsync(int id, DateOnly date)
        {
            return await _context.Days
                .Include(d => d.DayFoods).Include(d => d.DayPhysicalActivities)
                .FirstOrDefaultAsync(d => d.ProfileId == id && d.Date == date);
        }

        public async Task<bool> PostDayAsync(int id)
        {
            Day day = new(id, DateOnly.FromDateTime(DateTime.Today));
            
            try
            {
                _context.Days.Add(day);
                await _context.SaveChangesAsync();
            }
            catch (DbException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> PutFoodAsync(Day day, Food food, int grams)
        {
            try
            {
                _context.DayFoods.Add(new(day.ProfileId, day.Date, food.Id, grams));
                await _context.SaveChangesAsync();
            }
            catch (DbException)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> PutPhysicalActivityAsync(Day day, PhysicalActivity activity, int minutes)
        {
            try
            {
                _context.DayPhysicalActivities.Add(new(day.ProfileId, day.Date, activity.Id, minutes));
                await _context.SaveChangesAsync();
            }
            catch (DbException)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateGramsAsync(Day day, int foodId, int grams)
        {
            DayFood? dayfood = day.DayFoods.FirstOrDefault(f => f.FoodId == foodId);
            if(dayfood == null)
            {
                return false;
            }

            try
            {
                dayfood.Grams = grams;
                await _context.SaveChangesAsync();
            }
            catch(DbException)
            {
                return false;
            }
            
            return true;
        }

        public async Task<bool> UpdateMinutesAsync(Day day, int activityId, int minutes)
        {
            DayPhysicalActivity? activity = day.DayPhysicalActivities
                                .FirstOrDefault(f => f.PhysicalActivityId == activityId);
            if (activity == null)
            {
                return false;
            }

            try
            {
                activity.Minutes = minutes;
                await _context.SaveChangesAsync();
            }
            catch (DbException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteFoodAsync(Day day, int foodId)
        {
            DayFood? dayFood = day.DayFoods.FirstOrDefault(d => d.FoodId == foodId);
            if(dayFood == null)
            {  
                return false; 
            }

            try
            {
                day.DayFoods.Remove(dayFood);
                await _context.SaveChangesAsync();
            }
            catch(DbException)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeletePhysicalActivityAsync(Day day, int activityId)
        {
            DayPhysicalActivity? dayPhysicalActivity = day.DayPhysicalActivities
                                .FirstOrDefault(d => d.PhysicalActivityId == activityId);
            if (dayPhysicalActivity == null)
            {
                return false;
            }

            try
            {
                day.DayPhysicalActivities.Remove(dayPhysicalActivity);
                await _context.SaveChangesAsync();
            }
            catch (DbException)
            {
                return false;
            }
            return true;
        }
    }
}
