using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;

namespace healthy_lifestyle_web_app.Repositories
{
    public interface IDayRepository
    {
        public Task<List<Day>> GetAllAsync();

        // Gets all days of a user
        public Task<List<Day>> GetByUserAsync(int id);

        // Get all days after a given date for a profile
        public Task<List<Day>> GetAfterDateAsync(int profileId, DateOnly date);
        
        // Gets the current day for a user
        public Task<Day?> GetCurrentDayAsync(int id);

        // Get day by user and date
        public Task<Day?> GetByDateAsync(int id, DateOnly date);

        // Gets the number of food calories for a given day of a user
        public Task<double> GetFoodCalories(int id, DateOnly date);

        // Gets the number of physical activity burned calories for a given day of a user
        public Task<double> GetActivityCalories(int profileId, DateOnly date);

        // Get a list of date and food calories for a profile for given days
        public Task<List<DateCaloriesModel>> GetDaysFoodCaloriesAsync(List<Day> days);

        // Get a list of date and activity calories for a profile for given days
        public Task<List<DateCaloriesModel>> GetDaysActivityCaloriesAsync(List<Day> days);

        // Get the average calories for a list of day food calories
        public Task<double> GetAverageCalories(List<DateCaloriesModel> dateCalories);

        // Creates a new day for a user
        public Task<bool> PostDayAsync(Profile profile);

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
