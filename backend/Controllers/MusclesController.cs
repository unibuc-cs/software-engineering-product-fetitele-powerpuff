using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Only an admin can use the methods in this controller

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusclesController : ControllerBase
    {
        private readonly IMuscleRepository _muscleRepository;
        private readonly IMapper _mapper;

        public MusclesController(IMuscleRepository muscleRepository, IMapper mapper)
        {
            _muscleRepository = muscleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetMuscles()
        {
            List<Muscle> muscles = await _muscleRepository.GetAllAsync();
            List<GetMuscleDTO> musclesDTO = new List<GetMuscleDTO>();

            for (int i = 0; i < muscles.Count; i++)
            {
                musclesDTO.Add(_mapper.Map<GetMuscleDTO>(muscles[i]));
            }

            return Ok(musclesDTO);

        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PostMuscle(PostDeleteMuscleDTO muscle)
        {
            if (await _muscleRepository.PostAsync(_mapper.Map<Muscle>(muscle)))
            {
                return Ok("Muscle added successfully");
            }
            return BadRequest("Muscle already in the database");
        }

        [HttpDelete("{name}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteMuscle(String name)
        {
            PostDeleteMuscleDTO muscle = new PostDeleteMuscleDTO(name);
            if (await _muscleRepository.DeleteAsync(_mapper.Map<Muscle>(muscle)))
            {
                return Ok("Muscle deleted successfully");
            } 
            return NotFound("No muscle with this name");
        }
    }
}
