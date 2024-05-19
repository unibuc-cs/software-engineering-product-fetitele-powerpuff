using Microsoft.AspNetCore.Mvc;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Repositories;
using AutoMapper;
using healthy_lifestyle_web_app.Models;
using Microsoft.AspNetCore.Authorization;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IMapper _mapper;

        public FoodController(IFoodRepository foodRepository, IMapper mapper, IApplicationUserRepository applicationUserRepository)
        {
            _foodRepository = foodRepository;
            _applicationUserRepository = applicationUserRepository;
            _mapper = mapper;
        }

        // This is the information the user will see 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFood()
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return NotFound("No user logged in");
            }

            ApplicationUser? user = await _applicationUserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound("No user found");
            }

            List<Food> foods = await _foodRepository.GetAllAsync();
            List<GetFoodDTO> foodsDTO = new List<GetFoodDTO>();

            for (int i = 0; i < foods.Count; i++)
            {
                // Send only public foods or the user's foods
                if (foods[i].Public || foods[i].ApplicationUserId == user.Id)
                {
                    foodsDTO.Add(_mapper.Map<GetFoodDTO>(foods[i]));
                }
            }

            return Ok(foodsDTO);
        }

        // This is the information the admin will see 
        [HttpGet("for-admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetFoodAdmin()
        {
            List<Food> foods = await _foodRepository.GetAllAsync();
            List<GetFoodAdminDTO> foodsDTO = new List<GetFoodAdminDTO>();

            for (int i = 0; i < foods.Count; i++)
            {
                foodsDTO.Add(_mapper.Map<GetFoodAdminDTO>(foods[i]));
            }

            return Ok(foodsDTO);
        }

        [HttpGet("by-id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            Food? food =  await _foodRepository.GetByIdAsync(id);
            if (food == null)
            {
                return NotFound("No food with this id");
            }
            return Ok(_mapper.Map<GetFoodDTO>(food));
        }

        [HttpGet("{name}")]
        [Authorize]
        public async Task<IActionResult> GetByName(string name)
        {
            Food? food = await _foodRepository.GetByNameAsync(name);

            if (food == null)
            {
                return NotFound("No food with this name");
            }

            string? email = User.Identity.Name;
            if (email == null)
            {
                return NotFound("No user logged in");
            }

            ApplicationUser? user = await _applicationUserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound("No user found");
            }

            if (!(food.Public || food.ApplicationUserId == user.Id)) {
                return NotFound("No food with this name");
            }
            
            return Ok(_mapper.Map<GetFoodDTO>(food));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostFood(PostFoodDTO food)
        {
            if (User.IsInRole("admin"))
            {
                food.Public = true; // Alimentele adăugate de admin sunt publice pentru toți
                food.ApplicationUserId = null;
            }
            else
            {
                food.Public = false; // Alimentele adăugate de utilizatori sunt private
                string? email = User.Identity.Name;

                if (string.IsNullOrEmpty(email))
                {
                    return Forbid("You cannot create food");
                }

                ApplicationUser? user = await _applicationUserRepository.GetByEmailAsync(email);
                if (user == null)
                {
                    return NotFound("No user with this email");
                }

                food.ApplicationUserId = user.Id;
            }

            if (await _foodRepository.PostAsync(_mapper.Map<Food>(food)))
            {
                return Ok("Food added successfully");
            }
            return BadRequest("Food already in the database");
        }
    }
}
