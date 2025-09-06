using EmployeeService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace EmployeeService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }
        private static ApiResponse<T> BuildResponse<T>() where T : class
        {
            return new ApiResponse<T>
            {
                ResponseCode = -1,
                Message = string.Empty,
                ErrorDesc = string.Empty,
                ResponseData = new List<T>()
            };
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 500)]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetAll()
        {
            var response = BuildResponse<EmployeeDto>();
            try
            {
                var employees = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = employees;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch employees.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }


        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 404)]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 500)]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> Get(long id)
        {
            var response = BuildResponse<EmployeeDto>();
            try
            {
                var employee = await _service.GetByIdAsync(id);
                if (employee == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Employee not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(employee);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving employee.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 201)]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 400)]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 500)]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> Create([FromBody] EmployeeDto dto)
        {
            var response = BuildResponse<EmployeeDto>();

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                response.ErrorDesc = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(response);
            }

            try
            {
                var result = await _service.CreateEmployeeAsync(dto);

                if (result.ResponseCode != 1)
                {
                    return StatusCode(500, result);
                }

                var createdEmployeeId = result.ResponseData.FirstOrDefault()?.EmployeeId ?? 0;

                return CreatedAtAction(nameof(Get), new { id = createdEmployeeId }, result);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating employee.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 400)]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 404)]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 500)]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> Update(long id, [FromBody] EmployeeDto dto)
        {
            var response = BuildResponse<EmployeeDto>();


            ModelState.Remove(nameof(EmployeeDto.Password));

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                response.ErrorDesc = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(response);
            }

            try
            {
                var success = await _service.UpdateAsync(id, dto);
                if (!success)
                {
                    response.ResponseCode = 0;
                    response.Message = "Employee not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Employee updated successfully.";
                response.ResponseData.Add(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating employee.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 404)]
        [ProducesResponseType(typeof(ApiResponse<EmployeeDto>), 500)]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> Delete(long id)
        {
            var response = BuildResponse<EmployeeDto>();

            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                {
                    response.ResponseCode = 0;
                    response.Message = "Employee not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Employee deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting employee.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
