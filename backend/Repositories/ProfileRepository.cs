using AutoMapper;
using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationContext _context;

        // Basic get, post, put and delete functionalities

        public ProfileRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Profile>> GetAllAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Entities.Profile?> GetByApplicationUserIdAsync(string applicationUserId)
        {
            return await _context.Profiles.FirstOrDefaultAsync(p => p.ApplicationUserId == applicationUserId);
        }

        public async Task<bool> PostAsync(Entities.Profile profile)
        {
            try
            {
                _context.Profiles.Add(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> PutAsync(int id, PostProfileDTO profileDTO)
        {
            Entities.Profile? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return false;
            }
            profile.Name = profileDTO.Name;
            profile.Birthdate = profileDTO.Birthdate;
            profile.Weight = profileDTO.Weight;
            profile.Height = profileDTO.Height;
            profile.Goal = profileDTO.Goal;
            try
            {
                _context.Profiles.Update(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> PutNameAsync(int id, string newName)
        {
            Entities.Profile? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return false;
            }
            profile.Name = newName;
            try
            {
                _context.Profiles.Update(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> PutBirthdateAsync(int id, DateOnly mewBirthdate)
        {
            Entities.Profile? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return false;
            }
            profile.Birthdate = mewBirthdate;
            try
            {
                _context.Profiles.Update(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> PutWeightAsync(int id, double newWeight)
        {
            Entities.Profile? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return false;
            }
            profile.Weight = newWeight;
            try
            {
                _context.Profiles.Update(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> PutHeightAsync(int id, double newHeight)
        {
            Entities.Profile? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return false;
            }
            profile.Height = newHeight;
            try
            {
                _context.Profiles.Update(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> PutGoalAsync(int id, Goal newGoal)
        {
            Entities.Profile? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return false;
            }
            profile.Goal = newGoal;
            try
            {
                _context.Profiles.Update(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Entities.Profile? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return false;
            }
            try
            {
                _context.Profiles.Remove(profile);
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
