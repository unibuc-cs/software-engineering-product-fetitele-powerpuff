﻿using System.Threading.Tasks;
using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IFoodRepository
    {
        public Task<List<Food>> GetAllAsync();
        public Task<Food?> GetByIdAsync(int id);
        public Task<Food?> GetByNameAsync(string name);
        public Task<bool> PostAsync(Food food);
        public Task<bool> UpdateAsync(Food food);
        public Task<bool> DeleteAsync(Food food);
    }
}
