using AutoMapper;
using healthy_lifestyle_web_app.ContextModels;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetMuscles()
        {
            List<Muscle> muscles = await _muscleRepository.GetAllAsync();
            List<GetMuscleDTO> musclesDTO = new List<GetMuscleDTO>();

            for(int i = 0; i < muscles.Count; i++)
            {
                musclesDTO.Add(_mapper.Map<GetMuscleDTO>(muscles[i]));
            }

            return Ok(musclesDTO);

        }

        [HttpPost]
        public async Task<IActionResult> PostMuscle(PostDeleteMuscleDTO muscle)
        {
            if(await _muscleRepository.PostAsync(_mapper.Map<Muscle>(muscle)))
            {
                return Ok();
            }
            return BadRequest("Muscle already in the database");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMuscle(PostDeleteMuscleDTO muscle)
        {
            if(await _muscleRepository.DeleteAsync(_mapper.Map<Muscle>(muscle)))
            {
                return Ok();
            } 
            return NotFound();
        }
        
    }
}
