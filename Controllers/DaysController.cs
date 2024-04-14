using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
using healthy_lifestyle_web_app.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {
        private readonly IDayRepository _dayRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IApplicationUserService _userService;
        private readonly IMapper _mapper;

        public DaysController(IDayRepository dayRepository, IMapper mapper,
                           IFoodRepository foodRepository,
                            IApplicationUserService userService)
        {
            _dayRepository = dayRepository;
            _mapper = mapper;
            _foodRepository = foodRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dayRepository.GetAllAsync());
        }

        [HttpGet("by-user")]
        public async Task<IActionResult> GetByUser()
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

            List<Day> days = await _dayRepository.GetByUserAsync(profile.Id);
            List<GetDayDTO> getDaysDTO = new List<GetDayDTO>();

            foreach (Day day in days)
            {
                getDaysDTO.Add(_mapper.Map<GetDayDTO>(day));
            }

            return Ok(getDaysDTO);
        }

        [HttpGet("current-day")]
        public async Task<IActionResult> GetCurrent()
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

            Day? currentDay = await _dayRepository.GetCurrentDayAsync(profile.Id);
            if (currentDay == null)
            {
                return NotFound("Day doesn't exit");
            }

            Food? food = await _foodRepository.GetByNameAsync(model.foodName);
            if(food == null)
            {
                return NotFound("Food doesn't exist");
            }

            await _dayRepository.PutFoodAsync(currentDay, food, model.grams);

            return Ok();
        }

        [HttpPut("change-grams")]
        public async Task<IActionResult> PutGrams([FromBody] DayFoodGrams model)
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

        [HttpDelete("delete-food")]
        public async Task<IActionResult> DeleteFood()
        
    }
}
