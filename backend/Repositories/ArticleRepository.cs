using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationContext _context;

        public ArticleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<bool> AddArticleAsync(Article article)
        {
            try
            {
                _context.Articles.Add(article);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<Article> GetByTitleAsync(string title)
        {
            return await _context.Articles.FirstOrDefaultAsync(a => a.Title == title);
        }



        public async Task<bool> DeleteArticleAsync(Article article)
        {
            try
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}