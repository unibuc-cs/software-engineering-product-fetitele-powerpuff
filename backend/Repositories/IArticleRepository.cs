using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IArticleRepository
    {
        public Task<bool> AddArticleAsync(Article article);
        public Task<Article> GetByTitleAsync(string title);

        public Task<bool> DeleteArticleAsync(Article article);
    }
}