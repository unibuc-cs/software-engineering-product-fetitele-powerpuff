using System.Threading.Tasks;
using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IFoodRepository
    {
        public Task<Food> AddFood(Food food);
        public Task<Food> AddFoodAsAdmin(Food food);
    }
}
