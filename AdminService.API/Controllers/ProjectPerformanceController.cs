using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectPerformanceController : ControllerBase
    {
        private readonly IProjectPerformanceService _service;

        public ProjectPerformanceController(IProjectPerformanceService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<ActionResult<ApiResponse<ProjectPerformanceDto>>> GetAll()
        {
            var response = new ApiResponse<ProjectPerformanceDto>();
            try
            {
                var list = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = list;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch project performance data.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

    
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<ProjectPerformanceDto>>> GetById(long id)
        {
            var response = new ApiResponse<ProjectPerformanceDto>();
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Project performance not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(entity);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving project performance.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProjectPerformanceDto>>> Create([FromBody] ProjectPerformanceDto dto)
        {
            var response = new ApiResponse<ProjectPerformanceDto>();

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
                response.Message = "Project performance created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating project performance.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }


        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<ProjectPerformanceDto>>> Update(long id, [FromBody] ProjectPerformanceDto dto)
        {
            var response = new ApiResponse<ProjectPerformanceDto>();

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
                    response.Message = "Project performance not found.";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "Project performance updated successfully.";
                    response.ResponseData.Add(updated);
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating project performance.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

 
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<ProjectPerformanceDto>>> Delete(long id)
        {
            var response = new ApiResponse<ProjectPerformanceDto>();
            try
            {
                var success = await _service.DeleteAsync(id);
                if (success)
                {
                    response.ResponseCode = 1;
                    response.Message = "Project performance deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Project performance not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting project performance.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
