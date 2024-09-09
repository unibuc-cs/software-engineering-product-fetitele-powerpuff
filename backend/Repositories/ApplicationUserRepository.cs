using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserRepository(ApplicationContext applicationContext, UserManager<ApplicationUser> userManager)
        {
            _applicationContext = applicationContext;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _applicationContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return false;
            }

            // Get the user's foods
            var foods = await _applicationContext.Foods.Where(f => f.ApplicationUserId == id).ToListAsync();
            if (foods != null && foods.Any())
            {
                // Delete the user's foods (only those that are private to the user)
                _applicationContext.Foods.RemoveRange(foods);
            }

            _applicationContext.Users.Remove(user);
            await _applicationContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            var user = await _applicationContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return false;
            }

            // Get the user's foods and delete them before deleting the user
            var foods = await _applicationContext.Foods.Where(f => f.ApplicationUserId == user.Id).ToListAsync();
            if (foods != null && foods.Any())
            {
                _applicationContext.Foods.RemoveRange(foods);
            }

            _applicationContext.Users.Remove(user);
            await _applicationContext.SaveChangesAsync();
            return true;
        }
    }
}
