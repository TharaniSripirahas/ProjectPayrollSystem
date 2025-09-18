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

        [HttpGet]
        public async Task<ActionResult<ApiResult<EmployeeDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ApiResult<EmployeeDto>
            {
                ResponseCode = 1,
                Message = "Success",
                ResponseData = result
            });
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResult<EmployeeDto>>> Get(long id)
        {
            var employee = await _service.GetByIdAsync(id);

            var result = new ApiResult<EmployeeDto>();
            if (employee == null)
            {
                result.ResponseCode = 0;
                result.Message = "Employee not found.";
                return NotFound(result);
            }

            result.ResponseCode = 1;
            result.Message = "Success";
            result.ResponseData = new List<EmployeeDto> { employee };
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<EmployeeDto>>> Create([FromBody] EmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResult<EmployeeDto>
                {
                    ResponseCode = 0,
                    Message = "Validation failed.",
                    ErrorDesc = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage))
                });
            }

            var result = await _service.CreateEmployeeAsync(dto);
            if (result.ResponseCode == 0)
                return StatusCode(500, result);

            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResult<EmployeeDto>>> Update(long id, [FromBody] EmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResult<EmployeeDto>
                {
                    ResponseCode = 0,
                    Message = "Validation failed.",
                    ErrorDesc = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage))
                });
            }

            var success = await _service.UpdateAsync(id, dto);
            if (!success)
            {
                return NotFound(new ApiResult<EmployeeDto>
                {
                    ResponseCode = 0,
                    Message = "Employee not found."
                });
            }

            return Ok(new ApiResult<EmployeeDto>
            {
                ResponseCode = 1,
                Message = "Employee updated successfully.",
                ResponseData = new List<EmployeeDto> { dto }
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResult<bool>>> Delete(long id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResult<bool>
                {
                    ResponseCode = 0,
                    Message = "Employee not found."
                });
            }

            return Ok(new ApiResult<bool>
            {
                ResponseCode = 1,
                Message = "Employee deleted successfully.",
                ResponseData = new List<bool> { true }
            });
        }
    }
}
