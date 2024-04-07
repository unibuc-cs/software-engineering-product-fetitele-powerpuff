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
