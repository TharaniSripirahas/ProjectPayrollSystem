using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _service;

        public ShiftController(IShiftService service)
        {
            _service = service;
        }

      
        [HttpGet]
        public async Task<ActionResult<ApiResponse<ShiftDto>>> GetAll()
        {
            var response = new ApiResponse<ShiftDto>();
            try
            {
                var shifts = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = shifts;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch shifts.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }


        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<ShiftDto>>> Get(long id)
        {
            var response = new ApiResponse<ShiftDto>();
            try
            {
                var shift = await _service.GetByIdAsync(id);
                if (shift == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Shift not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(shift);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving shift.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ShiftDto>>> Create([FromBody] ShiftDto dto)
        {
            var response = new ApiResponse<ShiftDto>();

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
                response.Message = "Shift created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating shift.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

       
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<ShiftDto>>> Update(long id, [FromBody] ShiftDto dto)
        {
            var response = new ApiResponse<ShiftDto>();

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
                    response.Message = "Shift not found.";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "Shift updated successfully.";
                    response.ResponseData.Add(updated);
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating shift.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<ShiftDto>>> Delete(long id)
        {
            var response = new ApiResponse<ShiftDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (deleted)
                {
                    response.ResponseCode = 1;
                    response.Message = "Shift deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Shift not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting shift.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
