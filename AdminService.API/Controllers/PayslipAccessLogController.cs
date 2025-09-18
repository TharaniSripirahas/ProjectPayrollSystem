using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.PayslipsReportingDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayslipAccessLogController : ControllerBase
    {
        private readonly IPayslipAccessLogService _service;

        public PayslipAccessLogController(IPayslipAccessLogService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PayslipAccessLogDto>>> GetAll()
        {
            var response = new ApiResponse<PayslipAccessLogDto>();
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
                response.Message = "Failed to fetch payslip access logs.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

      
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayslipAccessLogDto>>> GetById(long id)
        {
            var response = new ApiResponse<PayslipAccessLogDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Payslip access log with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(item);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving payslip access log.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

  
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PayslipAccessLogDto>>> Create([FromBody] CreatePayslipAccessLogDto dto)
        {
            var response = new ApiResponse<PayslipAccessLogDto>();

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
                response.Message = "Payslip access log created successfully.";
                response.ResponseData.Add(created);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating payslip access log.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }

   
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayslipAccessLogDto>>> Update(long id, [FromBody] UpdatePayslipAccessLogDto dto)
        {
            var response = new ApiResponse<PayslipAccessLogDto>();

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
                    response.Message = $"Payslip access log with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payslip access log updated successfully.";
                response.ResponseData.Add(updated);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating payslip access log.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayslipAccessLogDto>>> Delete(long id)
        {
            var response = new ApiResponse<PayslipAccessLogDto>();

            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Payslip access log with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payslip access log deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting payslip access log.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
