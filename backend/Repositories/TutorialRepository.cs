using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace healthy_lifestyle_web_app.Repositories
{
    public class TutorialRepository: ITutorialRepository
    {
        private readonly ApplicationContext _context;

        public TutorialRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Tutorial>> GetAllAsync()
        {
            return await _context.Tutorials.ToListAsync();
        }

        public async Task<List<Tutorial>> FilterTutorialsAsync(string name)
        {
            return await _context.Tutorials
                .Where(t => t.Title.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<bool> PostAsync(Tutorial tutorial)
        {
            try
            {
                _context.Tutorials.Add(tutorial);
                await _context.SaveChangesAsync();
                return true;
            } 
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string title)
        {
            Tutorial? tutorial = _context.Tutorials.FirstOrDefault(t => t.Title.ToLower() == title.ToLower());

            if (tutorial == null)
            {
                return false;
            }

            try
            {
                _context.Tutorials.Remove(tutorial);
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
