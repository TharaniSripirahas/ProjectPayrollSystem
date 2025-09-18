using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeProjectMappingController : ControllerBase
    {
        private readonly IEmployeeProjectMappingService _service;

        public EmployeeProjectMappingController(IEmployeeProjectMappingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<EmployeeProjectMappingDto>>> GetAll()
        {
            var response = new ApiResponse<EmployeeProjectMappingDto>();
            try
            {
                var items = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = items.ToList();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch employee-project mappings.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmployeeProjectMappingDto>>> GetById(long id)
        {
            var response = new ApiResponse<EmployeeProjectMappingDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Mapping not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(item);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving mapping.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmployeeProjectMappingDto>>> Create([FromBody] EmployeeProjectMappingDto dto)
        {
            var response = new ApiResponse<EmployeeProjectMappingDto>();

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
                response.Message = "Mapping created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating mapping.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmployeeProjectMappingDto>>> Update(long id, [FromBody] EmployeeProjectMappingDto dto)
        {
            var response = new ApiResponse<EmployeeProjectMappingDto>();

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                return BadRequest(response);
            }

            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (updated == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Mapping not found.";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "Mapping updated successfully.";
                    response.ResponseData.Add(updated);
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating mapping.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmployeeProjectMappingDto>>> Delete(long id)
        {
            var response = new ApiResponse<EmployeeProjectMappingDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (deleted)
                {
                    response.ResponseCode = 1;
                    response.Message = "Mapping deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Mapping not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting mapping.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
