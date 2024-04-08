using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public ProfilesController(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _profileRepository.GetAllAsync());
        }

        // What the user will see when they look at their profile
        [HttpGet("{applicationUserId}")]
        public async Task<IActionResult> GetByApplicationUserId(string applicationUserId)
        {
            Entities.Profile? profile = await _profileRepository.GetByApplicationUserIdAsync(applicationUserId);
            if (profile == null)
            {
                return NotFound("No profile with this application user id");
            }
            return Ok(_mapper.Map<ProfileDTO>(profile));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProfileDTO profileDTO)
        {
            if (await _profileRepository.PostAsync(_mapper.Map<Entities.Profile>(profileDTO)))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("name{id}/{newName}")]
        public async Task<IActionResult> PutName(int id, string newName)
        {
            if (await _profileRepository.PutNameAsync(id, newName))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("birthdate{id}/{newBirthdate}")]
        public async Task<IActionResult> PutBirthdate(int id, DateOnly newBirthdate)
        {
            if (await _profileRepository.PutBirthdateAsync(id, newBirthdate))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("weight{id}/{newWeight}")]
        public async Task<IActionResult> PutWeight(int id, double newWeight)
        {
            if (await _profileRepository.PutWeightAsync(id, newWeight))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("height{id}/{newHeight}")]
        public async Task<IActionResult> PutHeight(int id, double newHeight)
        {
            if (await _profileRepository.PutHeightAsync(id, newHeight))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("goal{id}/{newGoal}")]
        public async Task<IActionResult> PutGoal(int id, Goal newGoal)
        {
            if (await _profileRepository.PutGoalAsync(id, newGoal))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _profileRepository.DeleteAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
