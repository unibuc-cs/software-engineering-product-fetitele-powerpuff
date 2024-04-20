using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IWeightEvolutionRepository _weightEvolutionRepository;
        private readonly IMapper _mapper;

        public ProfilesController(IProfileRepository profileRepository, 
                IApplicationUserRepository applicationUserRepository, IWeightEvolutionRepository weightEvolutionRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _applicationUserRepository = applicationUserRepository;
            _mapper = mapper;
            _weightEvolutionRepository = weightEvolutionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _profileRepository.GetAllAsync());
        }

        // What the user will see when they look at their profile
        [HttpGet("user-profile")]
        public async Task<IActionResult> GetByApplicationUserId()
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return NotFound("No user logged in");
            }

            ApplicationUser? user = await _applicationUserRepository.GetByEmailAsync(email);
            if(user == null)
            {
                return NotFound("No user found");
            }

            Entities.Profile? profile = await _profileRepository.GetByApplicationUserIdAsync(user.Id);
            if (profile == null)
            {
                return NotFound("No profile with this application user id");
            }
            return Ok(_mapper.Map<GetProfileDTO>(profile));
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostProfileDTO profileDTO)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return NotFound("No user logged in");
            }

            ApplicationUser? user = await _applicationUserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound("No user found");
            }

            Entities.Profile profile = _mapper.Map<Entities.Profile>(profileDTO);
            profile.ApplicationUserId = user.Id;

            if (await _profileRepository.PostAsync(profile))
            {
                if (await _weightEvolutionRepository.PostAsync(profile.Id, profile.Weight))
                {
                    return Ok("Profile created successfully");
                }
            }
            return BadRequest("Profile already exists");
        }

        [HttpPut("change-name/{newName}")]
        public async Task<IActionResult> PutName(string newName)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return NotFound("No user logged in");
            }

            ApplicationUser? user =  await _applicationUserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound("No user found");
            }

            Entities.Profile? profile = await _profileRepository.GetByApplicationUserIdAsync(user.Id);
            if (profile == null)
            {
                return NotFound("No profile with this application user id");
            }

            if (await _profileRepository.PutNameAsync(profile.Id, newName))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("change-birthday/{newBirthdate}")]
        public async Task<IActionResult> PutBirthdate(DateOnly newBirthdate)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return NotFound("No user logged in");
            }

            ApplicationUser? user = await _applicationUserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound("No user found");
            }

            Entities.Profile? profile = await _profileRepository.GetByApplicationUserIdAsync(user.Id);
            if (profile == null)
            {
                return NotFound("No profile with this application user id");
            }

            if (await _profileRepository.PutBirthdateAsync(profile.Id, newBirthdate))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("change-weight/{newWeight}")]
        public async Task<IActionResult> PutWeight(double newWeight)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return NotFound("No user logged in");
            }

            ApplicationUser? user = await _applicationUserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound("No user found");
            }

            Entities.Profile? profile = await _profileRepository.GetByApplicationUserIdAsync(user.Id);
            if (profile == null)
            {
                return NotFound("No profile with this application user id");
            }

            if (await _profileRepository.PutWeightAsync(profile.Id, newWeight))
            {
                if (await _weightEvolutionRepository.PostAsync(profile.Id, newWeight))
                {
                    return Ok("Weight updated successfully");
                }
                else { return BadRequest("Error updating weight in weight evolution"); }
            }
            return BadRequest("Error updating weight in profile");
        }

        [HttpPut("change-height/{newHeight}")]
        public async Task<IActionResult> PutHeight(double newHeight)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return NotFound("No user logged in");
            }

            ApplicationUser? user = await _applicationUserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound("No user found");
            }

            Entities.Profile? profile = await _profileRepository.GetByApplicationUserIdAsync(user.Id);
            if (profile == null)
            {
                return NotFound("No profile with this application user id");
            }

            if (await _profileRepository.PutHeightAsync(profile.Id, newHeight))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("change-goal/{newGoal}")]
        public async Task<IActionResult> PutGoal(Goal newGoal)
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return NotFound("No user logged in");
            }

            ApplicationUser? user = await _applicationUserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound("No user found");
            }

            Entities.Profile? profile = await _profileRepository.GetByApplicationUserIdAsync(user.Id);
            if (profile == null)
            {
                return NotFound("No profile with this application user id");
            }

            if (await _profileRepository.PutGoalAsync(profile.Id, newGoal))
            {
                return Ok();
            }
            return BadRequest();
        }

        // A user can delete their own profile
        [HttpDelete]
        public async Task<IActionResult> DeleteUserProfile()
        {
            string? email = User.Identity.Name;
            if (email == null)
            {
                return NotFound("No user logged in");
            }

            ApplicationUser? user = await _applicationUserRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound("No user found");
            }

            Entities.Profile? profile = await _profileRepository.GetByApplicationUserIdAsync(user.Id);
            if (profile == null)
            {
                return NotFound("No profile with this application user id");
            }

            if (await _profileRepository.DeleteAsync(profile.Id))
            {
                return Ok();
            }
            return NotFound("No profile found");
        }

        // An admin can delete any profile by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _profileRepository.DeleteAsync(id))
            {
                return Ok();
            }
            return NotFound("No profile found");
        }
    }
}
