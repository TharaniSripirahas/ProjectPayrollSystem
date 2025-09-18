using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceMetricController : ControllerBase
    {
        private readonly IPerformanceMetricService _service;

        public PerformanceMetricController(IPerformanceMetricService service)
        {
            _service = service;
        }

        
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PerformanceMetricDto>>> GetAll()
        {
            var response = new ApiResponse<PerformanceMetricDto>();
            try
            {
                var metrics = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = metrics.ToList();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch performance metrics.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

        
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<PerformanceMetricDto>>> GetById(long id)
        {
            var response = new ApiResponse<PerformanceMetricDto>();
            try
            {
                var metric = await _service.GetByIdAsync(id);
                if (metric == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Performance metric not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(metric);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving performance metric.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PerformanceMetricDto>>> Create([FromBody] PerformanceMetricDto dto)
        {
            var response = new ApiResponse<PerformanceMetricDto>();

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
                response.Message = "Performance metric created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating performance metric.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

       
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<PerformanceMetricDto>>> Update(long id, [FromBody] PerformanceMetricDto dto)
        {
            var response = new ApiResponse<PerformanceMetricDto>();

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
                    response.Message = "Performance metric not found.";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "Performance metric updated successfully.";
                    response.ResponseData.Add(updated);
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating performance metric.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<PerformanceMetricDto>>> Delete(long id)
        {
            var response = new ApiResponse<PerformanceMetricDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (deleted)
                {
                    response.ResponseCode = 1;
                    response.Message = "Performance metric deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Performance metric not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting performance metric.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
