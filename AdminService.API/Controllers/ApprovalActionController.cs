using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApprovalActionController : ControllerBase
    {
        private readonly IApprovalActionService _service;

        public ApprovalActionController(IApprovalActionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<ApprovalActionDto>>> GetAll()
        {
            var response = new ApiResponse<ApprovalActionDto>();
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
                response.Message = "Failed to fetch approval actions.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalActionDto>>> GetById(long id)
        {
            var response = new ApiResponse<ApprovalActionDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Approval action not found.";
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
                response.Message = "Error retrieving approval action.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ApprovalActionDto>>> Create([FromBody] CreateApprovalActionDto dto)
        {
            var response = new ApiResponse<ApprovalActionDto>();
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
                response.Message = "Approval action created successfully.";
                response.ResponseData.Add(created);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating approval action.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalActionDto>>> Update(long id, [FromBody] UpdateApprovalActionDto dto)
        {
            var response = new ApiResponse<ApprovalActionDto>();
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
                    response.Message = "Approval action not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Approval action updated successfully.";
                response.ResponseData.Add(updated);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating approval action.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalActionDto>>> Delete(long id)
        {
            var response = new ApiResponse<ApprovalActionDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = "Approval action not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Approval action deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting approval action.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
