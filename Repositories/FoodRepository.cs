using System.Threading.Tasks;
using AutoMapper;
using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using Microsoft.EntityFrameworkCore;

namespace healthy_lifestyle_web_app.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public FoodRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Food>> GetAllAsync()
        {
            return (await _context.Foods.ToListAsync());
        }

        public async Task<Food?> GetByNameAsync(string name)
        {
            return await _context.Foods
                        .FirstOrDefaultAsync(f => f.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> PostAsync(Food food)
        {
            try
            {
                _context.Foods.Add(food);
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
