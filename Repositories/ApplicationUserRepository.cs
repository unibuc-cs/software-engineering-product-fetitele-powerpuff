using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class ApplicationUserRepository: IApplicationUserRepository
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

            _applicationContext.Users.Remove(user);
            await _applicationContext.SaveChangesAsync();
            return true;
        }
    }
}
