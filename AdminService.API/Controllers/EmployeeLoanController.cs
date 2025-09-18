using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeLoanController : ControllerBase
    {
        private readonly IEmployeeLoanService _service;

        public EmployeeLoanController(IEmployeeLoanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<EmployeeLoanDto>>> GetAll()
        {
            var result = new ApiResult<EmployeeLoanDto>();
            try
            {
                result = await _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving employee loans.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("{loanId:long}")]
        public async Task<ActionResult<ApiResult<EmployeeLoanDto>>> GetById(long loanId)
        {
            var result = new ApiResult<EmployeeLoanDto>();
            try
            {
                result = await _service.GetByIdAsync(loanId);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving employee loan.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<EmployeeLoanDto>>> Create([FromBody] CreateEmployeeLoanDto dto)
        {
            var result = new ApiResult<EmployeeLoanDto>();
            try
            {
                result = await _service.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error creating employee loan.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPut("{loanId:long}")]
        public async Task<ActionResult<ApiResult<EmployeeLoanDto>>> Update(long loanId, [FromBody] UpdateEmployeeLoanDto dto)
        {
            var result = new ApiResult<EmployeeLoanDto>();
            try
            {
                if (loanId != dto.LoanId)
                {
                    result.ResponseCode = 0;
                    result.Message = "Loan ID mismatch between route and body.";
                    return Ok(result);
                }

                result = await _service.UpdateAsync(dto);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error updating employee loan.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpDelete("{loanId:long}")]
        public async Task<ActionResult<ApiResult<bool>>> Delete(long loanId, [FromQuery] long modifiedBy)
        {
            var result = new ApiResult<bool>();
            try
            {
                result = await _service.DeleteAsync(loanId, modifiedBy);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error deleting employee loan.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }
    }
}
