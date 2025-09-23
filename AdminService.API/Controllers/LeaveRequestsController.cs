using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestsController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        
        [HttpGet]
        public async Task<ActionResult<ApiResponse<LeaveRequestDto>>> GetAll()
        {
            var response = new ApiResponse<LeaveRequestDto>();
            try
            {
                var leaveRequests = await _leaveRequestService.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = leaveRequests;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch leave requests.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

       
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<LeaveRequestDto>>> Get(long id)
        {
            var response = new ApiResponse<LeaveRequestDto>();
            try
            {
                var leaveRequest = await _leaveRequestService.GetByIdAsync(id);
                if (leaveRequest == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Leave request not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(leaveRequest);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving leave request.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

        
        [HttpPost]
        public async Task<ActionResult<ApiResponse<LeaveRequestDto>>> Create([FromBody] LeaveRequestCreateDto dto)
        {
            var response = new ApiResponse<LeaveRequestDto>();

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
                var created = await _leaveRequestService.CreateAsync(dto);
                response.ResponseCode = 1;
                response.Message = "Leave request created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating leave request.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }


        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<LeaveRequestDto>>> Update(long id, [FromBody] LeaveRequestCreateDto dto)
        {
            var response = new ApiResponse<LeaveRequestDto>();

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                return BadRequest(response);
            }

            try
            {
                var updated = await _leaveRequestService.UpdateAsync(id, dto);
                if (updated == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Leave request not found.";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "Leave request updated successfully.";
                    response.ResponseData.Add(updated);
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating leave request.";
                response.ErrorDesc = ex.InnerException?.Message ?? ex.Message; 
            }

            return Ok(response);
        }





        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<LeaveRequestDto>>> Delete(long id)
        {
            var response = new ApiResponse<LeaveRequestDto>();
            try
            {
                var deleted = await _leaveRequestService.DeleteAsync(id);
                if (deleted)
                {
                    response.ResponseCode = 1;
                    response.Message = "Leave request deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Leave request not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting leave request.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
