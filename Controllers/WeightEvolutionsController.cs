using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
using healthy_lifestyle_web_app.Services;
using Microsoft.AspNetCore.Mvc;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightEvolutionsController : ControllerBase
    {
        private readonly IWeightEvolutionRepository _weightEvolutionRepository;
        private readonly IApplicationUserService _userService;
        private readonly IMapper _mapper;

        public WeightEvolutionsController(IWeightEvolutionRepository weightEvolutionRepository, IApplicationUserService applicationUserService, IMapper mapper)
        {
            _weightEvolutionRepository = weightEvolutionRepository;
            _userService = applicationUserService;
            _mapper = mapper;
        }

        // Get the weight evolution of the user logged in
        [HttpGet]
        public async Task<IActionResult> GetByProfile()
        {
            // Find the name (email) of the user that is logged in
            string? email = User.Identity.Name;
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

            // Get the weight evolutions of that profile
            List<WeightEvolution> weightEvolutions = await _weightEvolutionRepository.GetByProfileIdAsync(profile.Id);
            List<GetWeightEvolutionDTO> getWeightEvolutionDTOs = new List<GetWeightEvolutionDTO>();

            foreach (WeightEvolution weightEvolution in weightEvolutions)
            {
                getWeightEvolutionDTOs.Add(_mapper.Map<GetWeightEvolutionDTO>(weightEvolution));
            }

            return Ok(getWeightEvolutionDTOs);
        }
    }
}
