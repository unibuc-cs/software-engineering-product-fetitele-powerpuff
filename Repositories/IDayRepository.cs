using healthy_lifestyle_web_app.Entities;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IDayRepository
    {
        public Task<List<Day>> GetAllAsync();

        // Gets all days of a user
        public Task<List<Day>> GetByUserAsync(int id);
        
        // Gets the current day for a user
        public Task<Day?> GetCurrentDayAsync(int id);

        // Get day by user and date
        public Task<Day?> GetByDateAsync(int id,  DateOnly date);

        // Creates a new day for a user
        public Task<bool> PostDayAsync(int id);

        // Add food
        public Task<bool> PutFoodAsync(Day day, Food food, int grams);

        // Add physical activity
        public Task<bool> PutPhysicalActivityAsync(Day day, PhysicalActivity activity, int minutes);

        // Modify grams
        public Task<bool> UpdateGramsAsync(Day day, int foodId, int grams);

        // Modify minutes
        public Task<bool> UpdateMinutesAsync(Day day, int activityId, int minutes);

        // Delete food
        public Task<bool> DeleteFoodAsync(Day day, int foodId);

        // Delete physical activity
        public Task<bool> DeletePhysicalActivityAsync(Day day, int activityId);


    }
}
