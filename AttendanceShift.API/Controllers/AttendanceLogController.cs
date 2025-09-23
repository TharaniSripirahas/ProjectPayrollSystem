using AttendanceShift.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AttendanceShift.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceLogController : ControllerBase
    {
        private readonly IAttendanceLogService _service;

        public AttendanceLogController(IAttendanceLogService service)
        {
            _service = service;
        }

      
        [HttpGet]
        public async Task<ActionResult<ApiResult<AttendanceLogDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

     
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResult<AttendanceLogDto>>> Get(long id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.ResponseCode == 0)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<AttendanceLogDto>>> Create([FromBody] AttendanceLogCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResult<AttendanceLogDto>
                {
                    ResponseCode = 0,
                    Message = "Validation failed.",
                    ErrorDesc = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage))
                });
            }

            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResult<AttendanceLogDto>>> Update(long id, [FromBody] AttendanceLogDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResult<AttendanceLogDto>
                {
                    ResponseCode = 0,
                    Message = "Validation failed."
                });
            }

            var result = await _service.UpdateAsync(id, dto);
            if (result.ResponseCode == 0)
                return NotFound(result);

            return Ok(result);
        }

 
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResult<bool>>> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.ResponseCode == 0)
                return NotFound(result);

            return Ok(result);
        }
    }
}
