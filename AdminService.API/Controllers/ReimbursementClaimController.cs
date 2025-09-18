using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReimbursementClaimController : ControllerBase
    {
        private readonly IReimbursementClaimService _service;

        public ReimbursementClaimController(IReimbursementClaimService service)
        {
            _service = service;
        }

        
        [HttpGet]
        public async Task<ActionResult<ApiResponse<ReimbursementClaimDto>>> GetAll()
        {
            var response = new ApiResponse<ReimbursementClaimDto>();
            try
            {
                var list = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = list.ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ReimbursementClaimDto>
                {
                    ResponseCode = 0,
                    Message = "Failed to fetch reimbursement claims.",
                    ErrorDesc = ex.Message
                });
            }
        }

    
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<ReimbursementClaimDto>>> GetById(long id)
        {
            var claim = await _service.GetByIdAsync(id);

            if (claim == null)
            {
                return NotFound(new ApiResponse<ReimbursementClaimDto>
                {
                    ResponseCode = 0,
                    Message = $"Reimbursement Claim with ID {id} not found."
                });
            }

            return Ok(new ApiResponse<ReimbursementClaimDto>
            {
                ResponseCode = 1,
                Message = "Success",
                ResponseData = new List<ReimbursementClaimDto> { claim }
            });
        }

   
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ReimbursementClaimDto>>> Create([FromBody] CreateReimbursementClaimDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);

                return Ok(new ApiResponse<ReimbursementClaimDto>
                {
                    ResponseCode = 1,
                    Message = "Reimbursement claim created successfully.",
                    ResponseData = new List<ReimbursementClaimDto> { created }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ReimbursementClaimDto>
                {
                    ResponseCode = 0,
                    Message = "Error creating reimbursement claim.",
                    ErrorDesc = ex.Message
                });
            }
        }

        
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<ReimbursementClaimDto>>> Update(long id, [FromBody] UpdateReimbursementClaimDto dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto);

                if (updated == null)
                {
                    return NotFound(new ApiResponse<ReimbursementClaimDto>
                    {
                        ResponseCode = 0,
                        Message = $"Reimbursement Claim with ID {id} not found."
                    });
                }

                return Ok(new ApiResponse<ReimbursementClaimDto>
                {
                    ResponseCode = 1,
                    Message = "Reimbursement claim updated successfully.",
                    ResponseData = new List<ReimbursementClaimDto> { updated }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ReimbursementClaimDto>
                {
                    ResponseCode = 0,
                    Message = "Error updating reimbursement claim.",
                    ErrorDesc = ex.Message
                });
            }
        }


        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<ReimbursementClaimDto>>> Delete(long id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);

                if (!success)
                {
                    return NotFound(new ApiResponse<ReimbursementClaimDto>
                    {
                        ResponseCode = 0,
                        Message = $"Reimbursement Claim with ID {id} not found."
                    });
                }

                return Ok(new ApiResponse<ReimbursementClaimDto>
                {
                    ResponseCode = 1,
                    Message = "Reimbursement claim deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<ReimbursementClaimDto>
                {
                    ResponseCode = 0,
                    Message = "Error deleting reimbursement claim.",
                    ErrorDesc = ex.Message
                });
            }
        }
    }
}
