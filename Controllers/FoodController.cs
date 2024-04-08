using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Repositories;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

  
        [HttpPost("add")]
        public async Task<IActionResult> AddFood([FromBody] Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

      
            var result = await _foodRepository.AddFood(food);

            return Ok(result);
        }

        [HttpPost("admin/add")]
        public async Task<IActionResult> AddFoodAsAdmin([FromBody] Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

       
            var result = await _foodRepository.AddFoodAsAdmin(food);

            return Ok(result);
        }
    }
}
