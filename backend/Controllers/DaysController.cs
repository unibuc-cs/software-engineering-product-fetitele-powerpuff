using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
using healthy_lifestyle_web_app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {
        private readonly IDayRepository _dayRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IPhysicalActivityRepository _physicalActivityRepository;
        private readonly IApplicationUserService _userService;
        private readonly IMapper _mapper;

        public DaysController(IDayRepository dayRepository, IMapper mapper,
                           IFoodRepository foodRepository, IApplicationUserService userService, 
                           IPhysicalActivityRepository physicalActivityRepository)
        {
            _dayRepository = dayRepository;
            _mapper = mapper;
            _foodRepository = foodRepository;
            _userService = userService;
            _physicalActivityRepository = physicalActivityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dayRepository.GetAllAsync());
        }

        // Returns all days of the user currently logged in
        [HttpGet("by-user")]
        public async Task<IActionResult> GetByUser()
        {
            string? email = User.Identity.Name;  // Find the name (email) of the user that is logged in
            if (email == null)
            {
                return BadRequest("No user logged in");
            }

            // Get the user's profile
            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            // And the days associated with the profile
            List<Day> days = await _dayRepository.GetByUserAsync(profile.Id);
            List<GetDayDTO> getDaysDTO = new List<GetDayDTO>();

            foreach (Day day in days)
            {
                getDaysDTO.Add(_mapper.Map<GetDayDTO>(day));
            }

            return Ok(getDaysDTO);
        }

        // Returns the current day for the user logged in
        [HttpGet("current-day")]
        public async Task<IActionResult> GetCurrentDay()
        {
            string? email = User.Identity.Name;

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            Day? day = await _dayRepository.GetCurrentDayAsync(profile.Id);
            return Ok(_mapper.Map<GetDayDTO>(day));
        }

        // Returns a day by date for the user logged in
        [HttpGet("by-date")]
        public async Task<IActionResult> GetByDate(DateOnly date)
        {
            string? email = User.Identity.Name;

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            Day? day = await _dayRepository.GetByDateAsync(profile.Id, date);
            if(day == null)
            {
                return NotFound("Day doesn't exist");
            }

            return Ok(_mapper.Map<GetDayDTO>(day));
        }

        [HttpGet("food-calories")]
        public async Task<IActionResult> GetFoodCalories(DateOnly date)
        {
            string? email = User.Identity.Name;

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            Day? day = await _dayRepository.GetByDateAsync(profile.Id, date);
            if (day == null)
            {
                return NotFound("Day doesn't exist");
            }

            return Ok(await _dayRepository.GetFoodCalories(profile.Id, date));
        }

        [HttpGet("activity-calories")]
        public async Task<IActionResult> GetActivityCalories(DateOnly date)
        {
            string? email = User.Identity.Name;

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            Day? day = await _dayRepository.GetByDateAsync(profile.Id, date);
            if (day == null)
            {
                return NotFound("Day doesn't exist");
            }

            return Ok(await _dayRepository.GetActivityCalories(profile.Id, date));
        }

        // Get a list of date and food calories for a profile after a given date
        [HttpGet("food-calories-after-date")]
        public async Task<IActionResult> GetFoodCaloriesAfterDate(DateOnly date)
        {
            string? email = User.Identity.Name;

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            // Get a list of days after the given date
            List<Day> days = await _dayRepository.GetAfterDateAsync(profile.Id, date);
            if (days.IsNullOrEmpty())
            {
                return NotFound("No days after the given date");
            }

            // Return a list of dates and food calories for the computed days
            return Ok(await _dayRepository.GetDaysFoodCaloriesAsync(days));
        }

        // Get a list of date and food calories for a profile after a given date
        [HttpGet("activity-calories-after-date")]
        public async Task<IActionResult> GetActivityCaloriesAfterDate(DateOnly date)
        {
            string? email = User.Identity.Name;

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            // Get a list of days after the given date
            List<Day> days = await _dayRepository.GetAfterDateAsync(profile.Id, date);
            if (days.IsNullOrEmpty())
            {
                return NotFound("No days after the given date");
            }

            // Return a list of dates and activity calories for the computed days
            return Ok(await _dayRepository.GetDaysActivityCaloriesAsync(days));
        }

        // Get the average food calories after a given date for a profile
        [HttpGet("average-food-calories")]
        public async Task<IActionResult> GetAverageFoodCalories(DateOnly date)
        {
            string? email = User.Identity.Name;

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            // Get a list of days after the given date
            List<Day> days = await _dayRepository.GetAfterDateAsync(profile.Id, date);
            if (days.IsNullOrEmpty())
            {
                return NotFound("No days after the given date");
            }

            // Get a list of dates and calories for the computed days
            List<DateCaloriesModel> daysCalories = await _dayRepository.GetDaysFoodCaloriesAsync(days);

            return Ok(await _dayRepository.GetAverageCalories(daysCalories));
        }

        // Get the average activity calories after a given date for a profile
        [HttpGet("average-activity-calories")]
        public async Task<IActionResult> GetAverageActivityCalories(DateOnly date)
        {
            string? email = User.Identity.Name;

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            // Get a list of days after the given date
            List<Day> days = await _dayRepository.GetAfterDateAsync(profile.Id, date);
            if (days.IsNullOrEmpty())
            {
                return NotFound("No days after the given date");
            }

            // Get a list of dates and calories for the computed days
            List<DateCaloriesModel> daysCalories = await _dayRepository.GetDaysActivityCaloriesAsync(days);

            return Ok(await _dayRepository.GetAverageCalories(daysCalories));
        }

        // Will eventually change to automatically create a new day for a user at midnight
        // Currently creates (if it doesn't already exist) a new day with the current date for the user
        // // that is logged in
        [HttpPost]
        public async Task<IActionResult> PostDay()
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return BadRequest("No user logged in");
            }

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            if (await _dayRepository.PostDayAsync(profile.Id))
            {
                return Ok("Day created");
            }
            else
            {
                return BadRequest("Unable to create day");
            }
        }

        // Add food and grams in a day
        [HttpPut("add-food")]
        public async Task<IActionResult> PutFood([FromBody] DayFoodModel model)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return BadRequest("No user logged in");
            }

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            Day? day = await _dayRepository.GetByDateAsync(profile.Id, model.Date);
            if (day == null)
            {
                return NotFound("Day doesn't exit");
            }

            Food? food = await _foodRepository.GetByNameAsync(model.FoodName);
            if(food == null)
            {
                return NotFound("Food doesn't exist");
            }

            await _dayRepository.PutFoodAsync(day, food, model.Grams);

            return Ok("Food added successfully to day");
        }

        // Change grams for a food logged in the day given by date
        [HttpPut("change-grams")]
        public async Task<IActionResult> PutGrams([FromBody] DayFoodModel model)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return BadRequest("No user logged in");
            }

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            Day? day = await _dayRepository.GetByDateAsync(profile.Id, model.Date);
            if (day == null)
            {
                return NotFound("Day doesn't exist");
            }

            Food? food = await _foodRepository.GetByNameAsync(model.FoodName);
            if(food == null)
            {
                return NotFound("Food doesn't exist");
            }

            if(await _dayRepository.UpdateGramsAsync(day, food.Id, model.Grams))
            {
                return Ok();
            }
            return BadRequest("Couldn't update grams");
        }

        // Same as add food
        [HttpPut("add-activity")]
        public async Task<IActionResult> PutPhysicalActivity([FromBody] DayPhysicalActivityModel model)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return BadRequest("No user logged in");
            }

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            Day? day = await _dayRepository.GetByDateAsync(profile.Id, model.Date);
            if (day == null)
            {
                return NotFound("Day doesn't exit");
            }

            PhysicalActivity? activity = await _physicalActivityRepository.GetByNameAsync(model.ActivityName);
            if (activity == null)
            {
                return NotFound("Activity doesn't exist");
            }

            await _dayRepository.PutPhysicalActivityAsync(day, activity, model.Minutes);

            return Ok("Physical activity added successfully to day");
        }

        // Same as change grams
        [HttpPut("change-minutes")]
        public async Task<IActionResult> PutMinutes([FromBody] DayPhysicalActivityModel model)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return BadRequest("No user logged in");
            }

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            Day? day = await _dayRepository.GetByDateAsync(profile.Id, model.Date);
            if (day == null)
            {
                return NotFound("Day doesn't exist");
            }

            PhysicalActivity? activity = await _physicalActivityRepository.GetByNameAsync(model.ActivityName);
            if (activity == null)
            {
                return NotFound("Activity doesn't exist");
            }

            if (await _dayRepository.UpdateMinutesAsync(day, activity.Id, model.Minutes))
            {
                return Ok();
            }
            return BadRequest("Couldn't update grams");
        }

        // Removes the given food from the given day by date
        [HttpDelete("delete-food/{date}/{foodName}")]
        public async Task<IActionResult> DeleteFood(DateOnly date, string foodName)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return BadRequest("No user logged in");
            }

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            Day? day = await _dayRepository.GetByDateAsync(profile.Id, date);
            if (day == null)
            {
                return NotFound("Day doesn't exist");
            }

            Food? food = await _foodRepository.GetByNameAsync(foodName);
            if (food == null)
            {
                return NotFound("Activity doesn't exist");
            }

            if (await _dayRepository.DeleteFoodAsync(day, food.Id))
            {
                return Ok();
            }
            return BadRequest("Couldn't remove food");
        }

        // Same as delete food
        [HttpDelete("delete-activity/{date}/{activityName}")]
        public async Task<IActionResult> DeletePhysicalActivity(DateOnly date, string activityName)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return BadRequest("No user logged in");
            }

            Entities.Profile? profile = await _userService.GetUserProfileByEmail(email);
            if (profile == null)
            {
                return NotFound("Profile not found");
            }

            Day? day = await _dayRepository.GetByDateAsync(profile.Id, date);
            if (day == null)
            {
                return NotFound("Day doesn't exist");
            }

            PhysicalActivity? activity = await _physicalActivityRepository.GetByNameAsync(activityName);
            if (activity == null)
            {
                return NotFound("Activity doesn't exist");
            }

            if (await _dayRepository.DeletePhysicalActivityAsync(day, activity.Id))
            {
                return Ok();
            }
            return BadRequest("Couldn't remove physical activity");
        }
    }
}
