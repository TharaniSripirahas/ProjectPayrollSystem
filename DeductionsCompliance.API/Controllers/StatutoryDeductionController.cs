using DeductionsCompliance.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.DeductionsComplianceDto;

namespace DeductionsCompliance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatutoryDeductionController : ControllerBase
    {
        private readonly IStatutoryDeductionService _service;

        public StatutoryDeductionController(IStatutoryDeductionService service)
        {
            _service = service;
        }

      
        [HttpGet]
        public async Task<ActionResult<ApiResponse<StatutoryDeductionDto>>> GetAll()
        {
            var response = new ApiResponse<StatutoryDeductionDto>();
            try
            {
                var deductions = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = deductions.ToList();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch statutory deductions.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

       
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<StatutoryDeductionDto>>> GetById(long id)
        {
            var response = new ApiResponse<StatutoryDeductionDto>();
            try
            {
                var deduction = await _service.GetByIdAsync(id);
                if (deduction == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Statutory Deduction with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(deduction);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error fetching statutory deduction.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse<StatutoryDeductionDto>>> Create([FromBody] CreateStatutoryDeductionDto dto)
        {
            var response = new ApiResponse<StatutoryDeductionDto>();

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                response.ErrorDesc = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(response);
            }

            try
            {
                var created = await _service.CreateAsync(dto);
                response.ResponseCode = 1;
                response.Message = "Statutory deduction created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating statutory deduction.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }


        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<StatutoryDeductionDto>>> Update(long id, [FromBody] UpdateStatutoryDeductionDto dto)
        {
            var response = new ApiResponse<StatutoryDeductionDto>();

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
                    response.Message = $"Statutory Deduction with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Statutory deduction updated successfully.";
                response.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating statutory deduction.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

   
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<StatutoryDeductionDto>>> Delete(long id)
        {
            var response = new ApiResponse<StatutoryDeductionDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Statutory Deduction with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Statutory deduction deleted successfully.";
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting statutory deduction.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
