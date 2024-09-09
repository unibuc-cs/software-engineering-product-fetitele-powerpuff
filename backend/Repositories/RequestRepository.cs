using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationContext _context;

        // Basic get, post and delete functionalities

        public RequestRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Request>> GetAllAsync()
        {
            return await _context.Requests.ToListAsync();
        }

        public async Task<Request?> GetByIdAsync(int id)
        {
            return await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> CreateAsync(Request request)
        {
            try
            {
                _context.Requests.Add(request);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
            if (request == null)
            {
                return false;
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}