using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollComponentController : ControllerBase
    {
        private readonly IPayrollComponentService _service;

        public PayrollComponentController(IPayrollComponentService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PayrollComponentDto>>> GetAll()
        {
            var response = new ApiResponse<PayrollComponentDto>();
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
                response.Message = "Failed to fetch payroll components.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

   
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayrollComponentDto>>> GetById(long id)
        {
            var response = new ApiResponse<PayrollComponentDto>();
            try
            {
                var component = await _service.GetByIdAsync(id);
                if (component == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Payroll component with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(component);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving payroll component.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

    
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PayrollComponentDto>>> Create([FromBody] CreatePayrollComponentDto dto)
        {
            var response = new ApiResponse<PayrollComponentDto>();

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
                response.Message = "Payroll component created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating payroll component.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

     
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayrollComponentDto>>> Update(long id, [FromBody] UpdatePayrollComponentDto dto)
        {
            var response = new ApiResponse<PayrollComponentDto>();

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
                    response.Message = $"Payroll component with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payroll component updated successfully.";
                response.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating payroll component.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<PayrollComponentDto>>> Delete(long id)
        {
            var response = new ApiResponse<PayrollComponentDto>();

            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Payroll component with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Payroll component deleted successfully.";
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting payroll component.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
