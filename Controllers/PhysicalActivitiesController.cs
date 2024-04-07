using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
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

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            PhysicalActivity? physicalActivity = await _physicalActivityRepository.GetByNameAsync(name);
            if (physicalActivity == null)
            {
                return NotFound("No activity with this name");
            }
            return Ok(_mapper.Map<GetPhysicalActivityDTO>(physicalActivity));
        }

        [HttpGet("target-{muscleName}")]
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


        [HttpPost]
        public async Task<IActionResult> PostPhysicalActivity(PostPhysicalActivityDTO physicalActivity)
        {
            if (await _physicalActivityRepository.PostAsync(_mapper.Map<PhysicalActivity>(physicalActivity)))
            {
                return Ok();
            }
            return BadRequest("Physical activity already in the database");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePhysicalActivity(DeletePhysicalActivity physicalActivity)
        {
            if (await _physicalActivityRepository.DeleteAsync(_mapper.Map<PhysicalActivity>(physicalActivity)))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
