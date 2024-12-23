using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface ITutorialRepository
    {
        public Task<List<Tutorial>> GetAllAsync();
        public Task<bool> PostAsync(Tutorial tutorial);
        public Task<bool> DeleteAsync(string title);
    }
}
