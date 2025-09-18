using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanRepaymentController : ControllerBase
    {
        private readonly ILoanRepaymentService _service;

        public LoanRepaymentController(ILoanRepaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<LoanRepaymentDto>>> GetAll()
        {
            var result = new ApiResult<LoanRepaymentDto>();
            try
            {
                var repayments = await _service.GetAllAsync();
                result.ResponseCode = 1;
                result.Message = "Repayments retrieved successfully";
                result.ResponseData.AddRange(repayments);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving repayments";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResult<LoanRepaymentDto>>> GetById(long id)
        {
            var result = new ApiResult<LoanRepaymentDto>();
            try
            {
                var repayment = await _service.GetByIdAsync(id);
                if (repayment == null)
                {
                    result.ResponseCode = 2;
                    result.Message = "Repayment not found";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Repayment retrieved successfully";
                result.ResponseData.Add(repayment);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving repayment";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<LoanRepaymentDto>>> Create([FromBody] LoanRepaymentCreateDto dto)
        {
            var result = new ApiResult<LoanRepaymentDto>();
            try
            {
                var created = await _service.CreateAsync(dto);
                result.ResponseCode = 1;
                result.Message = "Repayment created successfully";
                result.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error creating repayment";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResult<LoanRepaymentDto>>> Update(long id, [FromBody] LoanRepaymentUpdateDto dto)
        {
            var result = new ApiResult<LoanRepaymentDto>();
            try
            {
                if (id != dto.RepaymentId)
                {
                    result.ResponseCode = 0;
                    result.Message = "RepaymentId mismatch";
                    return BadRequest(result);
                }

                var updated = await _service.UpdateAsync(dto);
                if (updated == null)
                {
                    result.ResponseCode = 2;
                    result.Message = "Repayment not found";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Repayment updated successfully";
                result.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error updating repayment";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResult<bool>>> Delete(long id)
        {
            var result = new ApiResult<bool>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    result.ResponseCode = 2;
                    result.Message = "Repayment not found";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Repayment deleted successfully";
                result.ResponseData.Add(true);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error deleting repayment";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }
    }
}
