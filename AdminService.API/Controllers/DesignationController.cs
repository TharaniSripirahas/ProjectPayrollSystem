using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _service;

        public DesignationController(IDesignationService service)
        {
            _service = service;
        }

        // Get All Designations
        [HttpGet]
        public async Task<ActionResult<ApiResponse<DesignationDto>>> GetAll()
        {
            var response = new ApiResponse<DesignationDto>();

            try
            {
                var list = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = list;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch designations.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        // Get Designation by ID
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<DesignationDto>>> GetById(long id)
        {
            var response = new ApiResponse<DesignationDto>();

            try
            {
                var designation = await _service.GetByIdAsync(id);
                if (designation == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Designation not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(designation);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving designation.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        // Create Designation
        [HttpPost]
        public async Task<ActionResult<ApiResponse<DesignationDto>>> Create([FromBody] DesignationDto dto)
        {
            var response = new ApiResponse<DesignationDto>();

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                response.ErrorDesc = string.Join("; ",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(response);
            }

            try
            {
                var created = await _service.CreateAsync(dto);
                response.ResponseCode = 1;
                response.Message = "Designation created successfully.";
                response.ResponseData.Add(created);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating designation.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }


        // Update Designation
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<DesignationDto>>> Update(long id, [FromBody] DesignationDto dto)
        {
            var response = new ApiResponse<DesignationDto>();

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
                    response.Message = "Designation updated successfully.";
                    response.ResponseData.Add(dto);
                    return Ok(response);
                }

                response.ResponseCode = 0;
                response.Message = "Designation not found.";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating designation.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        // Delete Designation
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<DesignationDto>>> Delete(long id)
        {
            var response = new ApiResponse<DesignationDto>();

            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (deleted)
                {
                    response.ResponseCode = 1;
                    response.Message = "Designation deleted successfully.";
                    return Ok(response);
                }

                response.ResponseCode = 0;
                response.Message = "Designation not found.";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting designation.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
