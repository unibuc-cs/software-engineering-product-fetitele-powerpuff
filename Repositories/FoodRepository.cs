using System.Threading.Tasks;
using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationContext _context;

        public FoodRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Food> AddFood(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return food;
        }

       
        public async Task<Food> AddFoodAsAdmin(Food food)
        {
           
            food.Public = true;
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return food;
        }
    }
}
