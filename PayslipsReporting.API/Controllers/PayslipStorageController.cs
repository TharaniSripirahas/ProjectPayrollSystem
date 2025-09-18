using Microsoft.AspNetCore.Mvc;
using PayslipsReporting.Core.Interfaces;
using Payroll.Common.NonEntities;

namespace PayslipsReporting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayslipStorageController : ControllerBase
    {
        private readonly IPayslipStorageService _service;

        public PayslipStorageController(IPayslipStorageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PayslipsReportingDto.PayslipDto>>> GetAll()
        {
            var response = new ApiResponse<PayslipsReportingDto.PayslipDto>();
            try
            {
                var list = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Payslips fetched successfully.";
                response.ResponseData = list.ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error fetching payslips.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PayslipsReportingDto.PayslipDto>>> GetById(long id)
        {
            var response = new ApiResponse<PayslipsReportingDto.PayslipDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Payslip not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payslip fetched successfully.";
                response.ResponseData.Add(item);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error fetching payslip.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PayslipsReportingDto.PayslipDto>>> Create(
            PayslipsReportingDto.CreatePayslipDto dto)
        {
            var response = new ApiResponse<PayslipsReportingDto.PayslipDto>();
            try
            {
                var created = await _service.CreateAsync(dto);
                response.ResponseCode = 1;
                response.Message = "Payslip created successfully.";
                response.ResponseData.Add(created);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating payslip.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<PayslipsReportingDto.PayslipDto>>> Update(
            long id,
            PayslipsReportingDto.UpdatePayslipDto dto)
        {
            var response = new ApiResponse<PayslipsReportingDto.PayslipDto>();
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (updated == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Payslip not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payslip updated successfully.";
                response.ResponseData.Add(updated);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating payslip.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(long id)
        {
            var response = new ApiResponse<string>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = "Payslip not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payslip deleted successfully.";
                response.ResponseData.Add("Deleted");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting payslip.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
