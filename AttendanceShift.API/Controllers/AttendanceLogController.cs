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
            public async Task<IActionResult> GetAll()
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(long id)
            {
                var result = await _service.GetByIdAsync(id);
                return Ok(result);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] AttendanceLogDto dto)
            {
                var result = await _service.CreateAsync(dto);
                return Ok(result);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(long id, [FromBody] AttendanceLogDto dto)
            {
                var result = await _service.UpdateAsync(id, dto);
                return Ok(result);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(long id)
            {
                var result = await _service.DeleteAsync(id);
                return Ok(result);
            }
        }
    
}
