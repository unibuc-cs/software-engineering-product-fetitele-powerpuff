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

        // Basic get, post and put functionalities

        public FoodRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Food>> GetAllAsync()
        {
            return (await _context.Foods.ToListAsync());
        }


        public async Task<Food?> GetByIdAsync(int id)
        {
            return await _context.Foods.FirstOrDefaultAsync(f => f.Id == id);
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

        public async Task<bool> UpdateAsync(Food food)
        {
            try
            {
                _context.Entry(food).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Food food)
        {
            try
            {
                _context.Foods.Remove(food);
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
