using Loans.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.LoansDto;

namespace Loans.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanTypeController : ControllerBase
    {
        private readonly ILoanTypeService _service;

        public LoanTypeController(ILoanTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<LoanTypeDto>>> GetAll()
        {
            var result = new ApiResult<LoanTypeDto>();
            try
            {
                result = await _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving loan types.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResult<LoanTypeDto>>> GetById(long id)
        {
            var result = new ApiResult<LoanTypeDto>();
            try
            {
                result = await _service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving loan type.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<LoanTypeDto>>> Create([FromBody] CreateLoanTypeDto dto)
        {
            var result = new ApiResult<LoanTypeDto>();
            try
            {
                result = await _service.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error creating loan type.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResult<LoanTypeDto>>> Update(long id, [FromBody] UpdateLoanTypeDto dto)
        {
            var result = new ApiResult<LoanTypeDto>();
            try
            {
                if (id != dto.LoanTypeId)
                {
                    result.ResponseCode = 0;
                    result.Message = "Id mismatch between route and body";
                    return Ok(result);
                }

                result = await _service.UpdateAsync(dto);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error updating loan type.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResult<bool>>> Delete(long id, [FromQuery] long modifiedBy)
        {
            var result = new ApiResult<bool>();
            try
            {
                result = await _service.DeleteAsync(id, modifiedBy);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error deleting loan type.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }
    }
}
