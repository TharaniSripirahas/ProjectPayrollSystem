using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollCycleController : ControllerBase
    {
        private readonly IPayrollCycleService _service;

        public PayrollCycleController(IPayrollCycleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PayrollCycleDto>>> GetAll()
        {
            var response = new ApiResponse<PayrollCycleDto>();
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
                response.Message = "Failed to fetch payroll cycles.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }


        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayrollCycleDto>>> GetById(long id)
        {
            var response = new ApiResponse<PayrollCycleDto>();
            try
            {
                var cycle = await _service.GetByIdAsync(id);
                if (cycle == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Payroll Cycle with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(cycle);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving payroll cycle.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse<PayrollCycleDto>>> Create([FromBody] CreatePayrollCycleDto dto)
        {
            var response = new ApiResponse<PayrollCycleDto>();

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
                response.Message = "Payroll cycle created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating payroll cycle.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

 
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayrollCycleDto>>> Update(long id, [FromBody] UpdatePayrollCycleDto dto)
        {
            var response = new ApiResponse<PayrollCycleDto>();

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
                    response.Message = $"Payroll Cycle with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payroll cycle updated successfully.";
                response.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating payroll cycle.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }


        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayrollCycleDto>>> Delete(long id)
        {
            var response = new ApiResponse<PayrollCycleDto>();

            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Payroll Cycle with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payroll cycle deleted successfully.";
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting payroll cycle.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
