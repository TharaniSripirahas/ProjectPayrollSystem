using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayslipNotificationController : ControllerBase
    {
        private readonly IPayslipNotificationService _service;

        public PayslipNotificationController(IPayslipNotificationService service)
        {
            _service = service;
        }

    
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PayslipNotificationDto>>> GetAll()
        {
            var response = new ApiResponse<PayslipNotificationDto>();
            try
            {
                var list = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = list.ToList();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch payslip notifications.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

      
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayslipNotificationDto>>> GetById(long id)
        {
            var response = new ApiResponse<PayslipNotificationDto>();
            try
            {
                var notification = await _service.GetByIdAsync(id);
                if (notification == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Payslip notification with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(notification);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving payslip notification.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse<PayslipNotificationDto>>> Create([FromBody] CreatePayslipNotificationDto dto)
        {
            var response = new ApiResponse<PayslipNotificationDto>();

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
                response.Message = "Payslip notification created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating payslip notification.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayslipNotificationDto>>> Update(long id, [FromBody] UpdatePayslipNotificationDto dto)
        {
            var response = new ApiResponse<PayslipNotificationDto>();

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
                    response.Message = $"Payslip notification with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payslip notification updated successfully.";
                response.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating payslip notification.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

      
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayslipNotificationDto>>> Delete(long id)
        {
            var response = new ApiResponse<PayslipNotificationDto>();

            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Payslip notification with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payslip notification deleted successfully.";
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting payslip notification.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
