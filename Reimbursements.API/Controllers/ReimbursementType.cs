using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using Reimbursements.Core.Interfaces;

namespace Reimbursements.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReimbursementTypeController : ControllerBase
    {
        private readonly IReimbursementTypeService _service;

        public ReimbursementTypeController(IReimbursementTypeService service)
        {
            _service = service;
        }

        // GET: api/ReimbursementType
        [HttpGet]
        public async Task<ActionResult<ApiResponse<ReimbursementTypeDto>>> GetAll()
        {
            var response = new ApiResponse<ReimbursementTypeDto>();
            try
            {
                var list = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = list.ToList();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch reimbursement types.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

        // GET: api/ReimbursementType/{id}
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<ReimbursementTypeDto>>> GetById(long id)
        {
            var response = new ApiResponse<ReimbursementTypeDto>();
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Reimbursement Type with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(entity);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving reimbursement type.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // POST: api/ReimbursementType
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ReimbursementTypeDto>>> Create([FromBody] ReimbursementTypeDto dto)
        {
            var response = new ApiResponse<ReimbursementTypeDto>();

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
                response.Message = "Reimbursement Type created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating reimbursement type.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // PUT: api/ReimbursementType/{id}
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<ReimbursementTypeDto>>> Update(long id, [FromBody] ReimbursementTypeDto dto)
        {
            var response = new ApiResponse<ReimbursementTypeDto>();

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                return BadRequest(response);
            }

            if (id != dto.TypeId)
            {
                response.ResponseCode = 0;
                response.Message = "ID mismatch.";
                return BadRequest(response);
            }

            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (updated == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Reimbursement Type with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Reimbursement Type updated successfully.";
                response.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating reimbursement type.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // DELETE: api/ReimbursementType/{id}
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<ReimbursementTypeDto>>> Delete(long id)
        {
            var response = new ApiResponse<ReimbursementTypeDto>();

            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Reimbursement Type with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Reimbursement Type deleted successfully.";
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting reimbursement type.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
