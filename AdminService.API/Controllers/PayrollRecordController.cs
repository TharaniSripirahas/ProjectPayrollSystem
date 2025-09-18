using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollRecordController : ControllerBase
    {
        private readonly IPayrollRecordService _service;

        public PayrollRecordController(IPayrollRecordService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PayrollRecordDto>>> GetAll()
        {
            var response = new ApiResponse<PayrollRecordDto>();
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
                response.Message = "Failed to fetch payroll records.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

       
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayrollRecordDto>>> GetById(long id)
        {
            var response = new ApiResponse<PayrollRecordDto>();
            try
            {
                var record = await _service.GetByIdAsync(id);
                if (record == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Payroll record with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(record);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving payroll record.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

     
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PayrollRecordDto>>> Create([FromBody] CreatePayrollRecordDto dto)
        {
            var response = new ApiResponse<PayrollRecordDto>();

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
                response.Message = "Payroll record created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating payroll record.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }


        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayrollRecordDto>>> Update(long id, [FromBody] UpdatePayrollRecordDto dto)
        {
            var response = new ApiResponse<PayrollRecordDto>();

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                return BadRequest(response);
            }

            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (updated == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Payroll record with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payroll record updated successfully.";
                response.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating payroll record.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

       
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayrollRecordDto>>> Delete(long id)
        {
            var response = new ApiResponse<PayrollRecordDto>();

            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Payroll record with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payroll record deleted successfully.";
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting payroll record.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
