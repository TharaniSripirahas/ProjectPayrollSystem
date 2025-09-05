using AttendanceShift.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AttendanceShift.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetAll()
        {
            var shifts = await _shiftService.GetAllAsync();
            return Ok(shifts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftDto>> GetById(long id)
        {
            var shift = await _shiftService.GetByIdAsync(id);
            if (shift == null) return NotFound();
            return Ok(shift);
        }

        [HttpPost]
        public async Task<ActionResult<ShiftDto>> Create([FromBody] ShiftDto dto)
        {
            var created = await _shiftService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ShiftId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShiftDto>> Update(long id, [FromBody] ShiftDto dto)
        {
            var updated = await _shiftService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _shiftService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}