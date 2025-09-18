using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

     
        [HttpGet]
        public async Task<ActionResult<ApiResult<LeaveTypeDto>>> GetAll()
        {
            var result = new ApiResult<LeaveTypeDto>();
            try
            {
                var leaveTypes = await _leaveTypeService.GetAllAsync();
                result.ResponseCode = 1;
                result.Message = "Leave types retrieved successfully";
                result.ResponseData = leaveTypes;
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Failed to fetch leave types";
                result.ErrorDesc = ex.Message;
            }

            return Ok(result);
        }


        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResult<LeaveTypeDto>>> Get(long id)
        {
            var result = new ApiResult<LeaveTypeDto>();
            try
            {
                var leaveType = await _leaveTypeService.GetByIdAsync(id);
                if (leaveType == null)
                {
                    result.ResponseCode = 0;
                    result.Message = "Leave type not found";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Leave type retrieved successfully";
                result.ResponseData.Add(leaveType);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving leave type";
                result.ErrorDesc = ex.Message;
            }

            return Ok(result);
        }

       
        [HttpPost]
        public async Task<ActionResult<ApiResult<LeaveTypeDto>>> Create([FromBody] LeaveTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResult<LeaveTypeDto>
                {
                    ResponseCode = 0,
                    Message = "Validation failed",
                    ErrorDesc = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage))
                });
            }

            var result = new ApiResult<LeaveTypeDto>();
            try
            {
                var created = await _leaveTypeService.CreateAsync(dto);
                result.ResponseCode = 1;
                result.Message = "Leave type created successfully";
                result.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error creating leave type";
                result.ErrorDesc = ex.Message;
            }

            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResult<LeaveTypeDto>>> Update(long id, [FromBody] LeaveTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResult<LeaveTypeDto>
                {
                    ResponseCode = 0,
                    Message = "Validation failed"
                });
            }

            var result = new ApiResult<LeaveTypeDto>();
            try
            {
                var updated = await _leaveTypeService.UpdateAsync(id, dto);
                if (updated == null)
                {
                    result.ResponseCode = 0;
                    result.Message = "Leave type not found";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Leave type updated successfully";
                result.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error updating leave type";
                result.ErrorDesc = ex.Message;
            }

            return Ok(result);
        }

   
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResult<bool>>> Delete(long id)
        {
            var result = new ApiResult<bool>();
            try
            {
                var deleted = await _leaveTypeService.DeleteAsync(id);
                if (!deleted)
                {
                    result.ResponseCode = 0;
                    result.Message = "Leave type not found";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Leave type deleted successfully";
                result.ResponseData.Add(true);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error deleting leave type";
                result.ErrorDesc = ex.Message;
            }

            return Ok(result);
        }
    }
}
