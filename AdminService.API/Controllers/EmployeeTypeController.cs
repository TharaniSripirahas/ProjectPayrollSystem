using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeTypeController : ControllerBase
    {
        private readonly IEmployeeTypeService _service;

        public EmployeeTypeController(IEmployeeTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<EmployeeTypeDto>>> GetAll()
        {
            var response = new ApiResponse<EmployeeTypeDto>();
            try
            {
                var types = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = types;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch employee types.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmployeeTypeDto>>> Get(long id)
        {
            var response = new ApiResponse<EmployeeTypeDto>();
            try
            {
                var type = await _service.GetByIdAsync(id);
                if (type == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Employee type not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(type);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving employee type.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmployeeTypeDto>>> Create([FromBody] EmployeeTypeDto dto)
        {
            var response = new ApiResponse<EmployeeTypeDto>();

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
                var created = await _service.CreateAsync(dto);
                response.ResponseCode = 1;
                response.Message = "Employee type created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating employee type.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmployeeTypeDto>>> Update(long id, [FromBody] EmployeeTypeDto dto)
        {
            var response = new ApiResponse<EmployeeTypeDto>();

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                return BadRequest(response);
            }

            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (updated)
                {
                    response.ResponseCode = 1;
                    response.Message = "Employee type updated successfully.";
                    response.ResponseData.Add(dto);
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Employee type not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating employee type.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmployeeTypeDto>>> Delete(long id)
        {
            var response = new ApiResponse<EmployeeTypeDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (deleted)
                {
                    response.ResponseCode = 1;
                    response.Message = "Employee type deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Employee type not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting employee type.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
