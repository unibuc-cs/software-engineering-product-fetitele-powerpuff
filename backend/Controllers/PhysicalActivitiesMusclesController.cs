using healthy_lifestyle_web_app.Repositories;
using Microsoft.AspNetCore.Mvc;

// Only an admin can use this methods, used to associate muscles with physical activities

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicalActivitiesMusclesController : ControllerBase
    {
        private readonly IPhysicalActivityMuscleRepository _physicalActivityMuscleRepository;

        public PhysicalActivitiesMusclesController(IPhysicalActivityMuscleRepository physicalActivityMuscleRepository)
        {
            _physicalActivityMuscleRepository = physicalActivityMuscleRepository;
        }

        [HttpPut]
        public async Task<IActionResult> PutMuscleToPhysicalActivity(string muscleName, string activityName)
        {
            if(await _physicalActivityMuscleRepository.AddMuscleToPhysicalActivity(muscleName, activityName))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMuscleToPhysicalActivity(string muscleName, string activityName)
        {
            if (await _physicalActivityMuscleRepository.DeleteMuscleFromPhysicalActivity(muscleName, activityName))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
