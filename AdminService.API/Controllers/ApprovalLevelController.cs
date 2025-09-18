using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApprovalLevelController : ControllerBase
    {
        private readonly IApprovalLevelService _service;

        public ApprovalLevelController(IApprovalLevelService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<ApprovalLevelDto>>> GetAll()
        {
            var response = new ApiResponse<ApprovalLevelDto>();
            try
            {
                var list = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = list.ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch approval levels.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalLevelDto>>> GetById(long id)
        {
            var response = new ApiResponse<ApprovalLevelDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Approval level not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(item);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving approval level.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ApprovalLevelDto>>> Create([FromBody] CreateApprovalLevelDto dto)
        {
            var response = new ApiResponse<ApprovalLevelDto>();
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
                response.Message = "Approval level created successfully.";
                response.ResponseData.Add(created);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating approval level.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalLevelDto>>> Update(long id, [FromBody] UpdateApprovalLevelDto dto)
        {
            var response = new ApiResponse<ApprovalLevelDto>();
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
                    response.Message = "Approval level not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Approval level updated successfully.";
                response.ResponseData.Add(updated);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating approval level.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalLevelDto>>> Delete(long id)
        {
            var response = new ApiResponse<ApprovalLevelDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = "Approval level not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Approval level deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting approval level.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
