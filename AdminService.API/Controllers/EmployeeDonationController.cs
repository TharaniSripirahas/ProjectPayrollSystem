using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeDonationController : ControllerBase
    {
        private readonly IEmployeeDonationService _service;

        public EmployeeDonationController(IEmployeeDonationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDonationDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<EmployeeDonationDto>> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDonationDto>> Create(EmployeeDonationCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.DonationId }, result);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<EmployeeDonationDto>> Update(long id, EmployeeDonationUpdateDto dto)
        {
            if (id != dto.DonationId) return BadRequest("ID mismatch");

            var result = await _service.UpdateAsync(dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
