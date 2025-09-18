using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApprovalRequestController : ControllerBase
    {
        private readonly IApprovalRequestService _service;

        public ApprovalRequestController(IApprovalRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<ApprovalRequestDto>>> GetAll()
        {
            var response = new ApiResponse<ApprovalRequestDto>();
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
                response.Message = "Failed to fetch approval requests.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalRequestDto>>> GetById(long id)
        {
            var response = new ApiResponse<ApprovalRequestDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Approval request not found.";
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
                response.Message = "Error retrieving approval request.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ApprovalRequestDto>>> Create([FromBody] CreateApprovalRequestDto dto)
        {
            var response = new ApiResponse<ApprovalRequestDto>();
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
                response.Message = "Approval request created successfully.";
                response.ResponseData.Add(created);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating approval request.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalRequestDto>>> Update(long id, [FromBody] UpdateApprovalRequestDto dto)
        {
            var response = new ApiResponse<ApprovalRequestDto>();
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
                    response.Message = "Approval request not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Approval request updated successfully.";
                response.ResponseData.Add(updated);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating approval request.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalRequestDto>>> Delete(long id)
        {
            var response = new ApiResponse<ApprovalRequestDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = "Approval request not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Approval request deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting approval request.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
