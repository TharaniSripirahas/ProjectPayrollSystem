using Microsoft.AspNetCore.Mvc;
using NotificationsApproval.Core.Interfaces;
using Payroll.Common.NonEntities;

namespace NotificationsApproval.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        // Get All Notifications
        [HttpGet]
        public async Task<ActionResult<ApiResponse<NotificationsApprovalDto.NotificationDto>>> GetAll()
        {
            var response = new ApiResponse<NotificationsApprovalDto.NotificationDto>();
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
                response.Message = "Failed to fetch notifications.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        // Get Notification by ID
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<NotificationsApprovalDto.NotificationDto>>> GetById(long id)
        {
            var response = new ApiResponse<NotificationsApprovalDto.NotificationDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Notification not found.";
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
                response.Message = "Error retrieving notification.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        // Create Notification
        [HttpPost]
        public async Task<ActionResult<ApiResponse<NotificationsApprovalDto.NotificationDto>>> Create(
            [FromBody] NotificationsApprovalDto.CreateNotificationDto dto)
        {
            var response = new ApiResponse<NotificationsApprovalDto.NotificationDto>();

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
                response.Message = "Notification created successfully.";
                response.ResponseData.Add(created);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating notification.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        // Update Notification
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<NotificationsApprovalDto.NotificationDto>>> Update(
            long id, [FromBody] NotificationsApprovalDto.UpdateNotificationDto dto)
        {
            var response = new ApiResponse<NotificationsApprovalDto.NotificationDto>();

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
                    response.Message = "Notification not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Notification updated successfully.";
                response.ResponseData.Add(updated);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating notification.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        // Delete Notification
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<NotificationsApprovalDto.NotificationDto>>> Delete(long id)
        {
            var response = new ApiResponse<NotificationsApprovalDto.NotificationDto>();

            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = "Notification not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Notification deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting notification.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
