using AttendanceShift.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AttendanceShift.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveTypeDto>>> GetAll()
        {
            var leaveTypes = await _leaveTypeService.GetAllAsync();
            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDto>> GetById(long id)
        {
            var leaveType = await _leaveTypeService.GetByIdAsync(id);
            if (leaveType == null) return NotFound();
            return Ok(leaveType);
        }

        [HttpPost]
        public async Task<ActionResult<LeaveTypeDto>> Create([FromBody] LeaveTypeDto dto)
        {
            var created = await _leaveTypeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.LeaveTypeId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LeaveTypeDto>> Update(long id, [FromBody] LeaveTypeDto dto)
        {
            var updated = await _leaveTypeService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _leaveTypeService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
