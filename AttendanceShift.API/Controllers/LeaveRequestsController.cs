using AttendanceShift.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AttendanceShift.API.Controllers
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _leaveRequestService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _leaveRequestService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeaveRequestDto dto)
        {
            var result = await _leaveRequestService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.LeaveId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] LeaveRequestDto dto)
        {
            var result = await _leaveRequestService.UpdateAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _leaveRequestService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
