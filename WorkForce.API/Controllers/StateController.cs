using Microsoft.AspNetCore.Mvc;
using WorkForce.API.Mappers;
using WorkForce.API.Models.Responses;
using WorkForce.API.Models.State;
using WorkForce.API.Repositories;

// Controllers/StateController.cs
//using AutoMapper; // Si usas AutoMapper


namespace WorkForce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository _stateRepository;

        public StateController(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }


        [HttpGet("list")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StateResponse>>>> GetActiveStates()
        {
            try
            {
                var states = await _stateRepository.GetActiveStatesAsync();
                return Ok(ApiResponse<IEnumerable<StateResponse>>.SuccessResult(
                    states, $"{states.Count()} states retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<StateResponse>>.ErrorResult(
                    $"Error retrieving states: {ex.Message}"));
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<StateResponse>>> GetStateById(int id)
        {
            try
            {
                var state = await _stateRepository.GetStateByIdAsync(id);

                if (state == null)
                    return NotFound(ApiResponse<StateResponse>.ErrorResult("State not found"));

                return Ok(ApiResponse<StateResponse>.SuccessResult(state, "State retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<StateResponse>.ErrorResult($"Error: {ex.Message}"));
            }
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse<StateResponse>>> CreateState(StateRequest request)
        {
            try
            {
                var response = await _stateRepository.CreateStateAsync(request);
                return CreatedAtAction(nameof(GetStateById), new { id = response.Id },
                    ApiResponse<StateResponse>.SuccessResult(response, "State created successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<StateResponse>.ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<StateResponse>.ErrorResult($"Error: {ex.Message}"));
            }
        }

    }
}