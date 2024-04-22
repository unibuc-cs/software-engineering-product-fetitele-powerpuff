using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IRequestRepository
    {
        public Task<List<Request>> GetAllAsync();
         public Task<Request> GetByIdAsync(int id);

        public Task<bool> CreateAsync(Request request);
        public Task<bool> DeleteAsync(int id);
    }
}
