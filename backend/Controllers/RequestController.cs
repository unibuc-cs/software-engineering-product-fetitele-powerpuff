using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;
using healthy_lifestyle_web_app.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Request = healthy_lifestyle_web_app.Entities.Request;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // Users can create requests to make private foods public
    // An admin can approve or deny the request

    public class RequestController : ControllerBase
    {

        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;
        private readonly IFoodRepository _foodRepository;
        private readonly IApplicationUserRepository _userRepository;

        public RequestController(IRequestRepository requestRepository, IMapper mapper,
            IFoodRepository foodRepository, IApplicationUserRepository applicationUserRepository)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
            _foodRepository = foodRepository;
            _userRepository = applicationUserRepository;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllRequests()
        {
            List<Request> requests = await _requestRepository.GetAllAsync();
            List<GetRequestDTO> requestsDTO = new List<GetRequestDTO>();

            foreach (var request in requests)
            {
                requestsDTO.Add(_mapper.Map<GetRequestDTO>(request));
            }

            return Ok(requestsDTO);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            Request? request = await _requestRepository.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound("Request not found");
            }

            GetRequestDTO requestDTO = _mapper.Map<GetRequestDTO>(request);
            return Ok(requestDTO);
        }

        [HttpPost("{foodName}")]
        [Authorize]
        // User creates a request for a food
        public async Task<IActionResult> CreateRequest(string foodName)
        {
            if (!User.IsInRole("admin"))
            {
                string userEmail = User.Identity.Name;
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User not authenticated");
                }

                Food? food = await _foodRepository.GetByNameAsync(foodName);
                if (food == null)
                {
                    return NotFound("Food not found");
                }

                ApplicationUser? user = await _userRepository.GetByEmailAsync(userEmail);
                if (user == null)
                {
                    return NotFound("No user with this email");
                }

                // Must be a food created by the user
                if (food.ApplicationUserId != user.Id)
                {
                    return BadRequest("You are not allowed to create a request for this food");
                }

                Request request = new Request { FoodId = food.Id };
                if (await _requestRepository.CreateAsync(request))
                {
                    return Ok("Request created successfully");
                }
                return BadRequest("Failed to create request");
            }
            else
            {
                return BadRequest("Only normal users can create requests.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        // Approve a request
        public async Task<IActionResult> UpdateRequest(int id)
        {
            Request? existingRequest = await _requestRepository.GetByIdAsync(id);
            if (existingRequest == null)
            {
                return NotFound("Request not found");
            }

            Food? food = await _foodRepository.GetByIdAsync(existingRequest.FoodId);
            if (food == null)
            {
                return NotFound("Food not found");
            }

            // Food becomes visible to all users
            food.Public = true;
            food.ApplicationUserId = null;
            food.ApplicationUser = null;

            if (await _foodRepository.UpdateAsync(food))
            {
                if (await _requestRepository.DeleteAsync(existingRequest.Id))
                {
                    return Ok("Request approved successfully");
                }

                food.Public = false;
                await _foodRepository.UpdateAsync(food);
            }

            return BadRequest("Failed to update request");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        // Deny a request
        public async Task<IActionResult> DeleteRequest(int id)
        {
            if (await _requestRepository.DeleteAsync(id))
            {
                return Ok("Request denied successfully");
            }
            return NotFound("Request not found");
        }
    }
}