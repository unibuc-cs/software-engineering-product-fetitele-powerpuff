using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
using healthy_lifestyle_web_app.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly CreateDaysService _createDaysService;
        private readonly IMapper _mapper;

        public ProfilesController(IProfileRepository profileRepository,
                IApplicationUserRepository applicationUserRepository, IWeightEvolutionRepository weightEvolutionRepository, 
                CreateDaysService createDaysService, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _applicationUserRepository = applicationUserRepository;
            _weightEvolutionRepository = weightEvolutionRepository;
            _createDaysService = createDaysService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _profileRepository.GetAllAsync());
        }

        // What the user will see when they look at their profile
        [HttpGet("user-profile")]
        [Authorize]
        public async Task<IActionResult> GetByApplicationUserId()
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
            return Ok(_mapper.Map<GetProfileDTO>(profile));
        }

        [HttpPost]
        [Authorize]
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

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow.Date);
            if (profileDTO.Birthdate > currentDate)
            {
                return BadRequest("Invalid birthdate");
            }
            if (profileDTO.Weight < 0)
            {
                return BadRequest("Invalid weight");
            }
            if (profileDTO.Height < 0)
            {
                return BadRequest("Invalid height");
            }

            Entities.Profile profile = _mapper.Map<Entities.Profile>(profileDTO);
            profile.ApplicationUserId = user.Id;

            if (await _profileRepository.PostAsync(profile))
            {
                if (await _weightEvolutionRepository.PostAsync(profile.Id, profile.Weight))
                {
                    if (await _createDaysService.CreateDayForNewUser(profile))
                    {
                        return Ok("Profile created successfully");
                    }
                    return BadRequest("Error creating day");
                }
                return BadRequest("Error creating weight evolution");
            }
            return BadRequest("Profile already exists");
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(PostProfileDTO profileDTO)
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

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow.Date);
            if (profileDTO.Birthdate > currentDate)
            {
                return BadRequest("Invalid birthdate");
            }
            if (profileDTO.Weight < 0)
            {
                return BadRequest("Invalid weight");
            }
            if (profileDTO.Height < 0)
            {
                return BadRequest("Invalid height");
            }

            bool weightChange = false;
            if (profile.Weight != profileDTO.Weight)
            {
                weightChange = true;
            }

            if (await _profileRepository.PutAsync(profile.Id, profileDTO))
            {
                // If the weight has been changed, record it in the weight evolution
                if (weightChange)
                {
                    if (await _weightEvolutionRepository.PostAsync(profile.Id, profileDTO.Weight))
                    {
                        return Ok("Profile updated successfully");
                    }
                    else { return BadRequest("Error updating weight in weight evolution"); }
                }
                return Ok("Profile updated successfully");
            }
            return BadRequest();
        }

        [HttpPut("change-name/{newName}")]
        [Authorize]
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
        [Authorize]
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

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow.Date);
            if (newBirthdate > currentDate)
            {
                return BadRequest("Invalid birthdate");
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
        [Authorize]
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

            if (newWeight < 0)
            {
                return BadRequest("Invalid weight");
            }

            Entities.Profile? profile = await _profileRepository.GetByApplicationUserIdAsync(user.Id);
            if (profile == null)
            {
                return NotFound("No profile with this application user id");
            }

            bool weightChange = false;
            if (profile.Weight != newWeight)
            {
                weightChange = true;
            }

            if (await _profileRepository.PutWeightAsync(profile.Id, newWeight))
            {
                // If the weight has been changed, record it in the weight evolution
                if (weightChange)
                {
                    if (await _weightEvolutionRepository.PostAsync(profile.Id, newWeight))
                    {
                        return Ok("Weight updated successfully");
                    }
                    else { return BadRequest("Error updating weight in weight evolution"); }
                }
                return Ok("Weight updated successfully");
            }
            return BadRequest("Error updating weight in profile");
        }

        [HttpPut("change-height/{newHeight}")]
        [Authorize]
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

            if (newHeight < 0)
            {
                return BadRequest("Invalid height");
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
        [Authorize]
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
        [Authorize]
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
        [Authorize(Roles = "admin")]
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
