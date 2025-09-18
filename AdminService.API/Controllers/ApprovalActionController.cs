using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApprovalActionController : ControllerBase
    {
        private readonly IApprovalActionService _service;

        public ApprovalActionController(IApprovalActionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApprovalActionDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApprovalActionDto>> GetById(long id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ApprovalActionDto>> Create(CreateApprovalActionDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ActionId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApprovalActionDto>> Update(long id, UpdateApprovalActionDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
