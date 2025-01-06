using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorialController : ControllerBase
    {
        private readonly ITutorialRepository _tutorialRepository;
        private readonly IMapper _mapper;

        public TutorialController(ITutorialRepository tutorialRepository, IMapper mapper)
        {
            _tutorialRepository = tutorialRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTutorials()
        {
            return Ok(await _tutorialRepository.GetAllAsync());
        }

        [HttpGet("filter")]
        [Authorize]
        public async Task<IActionResult> FilterTutorilals([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name parameter is required.");
            }

            var tutorials = await _tutorialRepository.FilterTutorialsAsync(name);
            if (!tutorials.Any())
            {
                return NotFound("No tutorials found matching the given input.");
            }
            var tutorialDTOs = tutorials.Select(t => _mapper.Map<TutorialDTO>(t)).ToList();
            return Ok(tutorialDTOs);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PostTutorial(TutorialDTO tutorial)
        {
            if (tutorial.Calories <= 0)
            {
                return BadRequest("Calories must be greater than 0");
            }
            if (tutorial.Grams < 0 || tutorial.Carbohydrates < 0 || tutorial.Proteins < 0 || tutorial.Fats < 0)
            {
                return BadRequest("Grams, carbs, proteins and fats cannot have negative values");
            }

            if (await _tutorialRepository.PostAsync(_mapper.Map<Tutorial>(tutorial)))
            {
                return Ok("Tutorial added successfully");
            } 
            return BadRequest("Failed to add tutorial. A tutorial with the same name might already exist");
        }

        [HttpDelete("{title}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTutorial(string title)
        {
            if (await _tutorialRepository.DeleteAsync(title))
            {
                return Ok("Tutorial deleted successfully");
            }
            return BadRequest("Failed to delete tutorial. A tutorial with this name might not exist");
        }
    }
}
