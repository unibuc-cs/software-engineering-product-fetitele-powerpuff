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
    public class PhysicalActivitiesController : ControllerBase
    {
        private readonly IPhysicalActivityRepository _physicalActivityRepository;
        private readonly IMapper _mapper;

        public PhysicalActivitiesController(IPhysicalActivityRepository physicalActivityRepository, IMapper mapper)
        {
            _physicalActivityRepository = physicalActivityRepository;
            _mapper = mapper;
        }

        // This is the information the user will see (only name and muscles)
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPhysicalActivites()
        {
            List<PhysicalActivity> physicalActivities = await _physicalActivityRepository.GetAllAsync();
            List<GetPhysicalActivityDTO> physicalActivitesDTO = new List<GetPhysicalActivityDTO>();

            for (int i = 0; i < physicalActivities.Count; i++)
            {
                physicalActivitesDTO.Add(_mapper.Map<GetPhysicalActivityDTO>(physicalActivities[i]));
            }

            return Ok(physicalActivitesDTO);
        }

        // This is the information the admin will see (+ id and calories)
        [HttpGet("for-admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetPhysicalActivitesAdmin()
        {
            List<PhysicalActivity> physicalActivities = await _physicalActivityRepository.GetAllAsync();
            List<GetPhysicalActivitesAdminDTO> physicalActivitesDTO = new List<GetPhysicalActivitesAdminDTO>();

            for (int i = 0; i < physicalActivities.Count; i++)
            {
                physicalActivitesDTO.Add(_mapper.Map<GetPhysicalActivitesAdminDTO>(physicalActivities[i]));
            }

            return Ok(physicalActivitesDTO);
        }

        [HttpGet("by-id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            PhysicalActivity? physicalActivity = await _physicalActivityRepository.GetByIdAsync(id);
            if (physicalActivity == null)
            {
                return NotFound("No activity with this id");
            }
            return Ok(_mapper.Map<GetPhysicalActivityDTO>(physicalActivity));
        }

        [HttpGet("{name}")]
        [Authorize]
        public async Task<IActionResult> GetByName(string name)
        {
            PhysicalActivity? physicalActivity = await _physicalActivityRepository.GetByNameAsync(name);
            if (physicalActivity == null)
            {
                return NotFound("No activity with this name");
            }
            return Ok(_mapper.Map<GetPhysicalActivityDTO>(physicalActivity));
        }

        // Returns all activities that target the given muscle
        [HttpGet("target-{muscleName}")]
        [Authorize]
        public async Task<IActionResult> GetByMuscle(string muscleName)
        {
            List<PhysicalActivity>? physicalActivities = await _physicalActivityRepository.GetByMuscleAsync(muscleName);
            if (physicalActivities == null)
            {
                return NotFound("No muscle with this name");
            }

            if(physicalActivities.Count == 0)
            {
                return NotFound("No exercises found");
            }

            List<GetPhysicalActivityDTO> getPhysicalActivitiesDTO = new List<GetPhysicalActivityDTO>();
            foreach(var p in physicalActivities)
            {
                getPhysicalActivitiesDTO.Add(_mapper.Map<GetPhysicalActivityDTO>(p));
            }

            return Ok(getPhysicalActivitiesDTO);
        } 

        // An admin can add a new activity
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PostPhysicalActivity(PostPhysicalActivityDTO physicalActivity)
        {
            if (physicalActivity.calories < 0)
            {
                return BadRequest("Invalid calories");
            }
            if (await _physicalActivityRepository.PostAsync(_mapper.Map<PhysicalActivity>(physicalActivity)))
            {
                return Ok("Physical activity added successfully");
            }
            return BadRequest("Physical activity already in the database");
        }

        // Or delete one
        [HttpDelete("{name}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePhysicalActivity(String name)
        {
            PhysicalActivity physicalActivity = await _physicalActivityRepository.GetByNameAsync(name);
            if (physicalActivity == null)
            {
                return NotFound("No physical activity with this name");
            }
            if (await _physicalActivityRepository.DeleteAsync(physicalActivity))
            {
                return Ok("Physical activity deleted successfully");
            }
            return BadRequest("Error deleting physical activity");
        }
    }
}
