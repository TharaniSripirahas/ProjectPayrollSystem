using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxDeclarationController : ControllerBase
    {
        private readonly ITaxDeclarationService _service;

        public TaxDeclarationController(ITaxDeclarationService service)
        {
            _service = service;
        }

        
        [HttpGet]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.TaxDeclarationDto>>> GetAll()
        {
            var response = new ApiResponse<DeductionsComplianceDto.TaxDeclarationDto>();
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
                response.Message = "Failed to fetch tax declarations.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }


        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.TaxDeclarationDto>>> GetById(long id)
        {
            var response = new ApiResponse<DeductionsComplianceDto.TaxDeclarationDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Tax declaration with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(item);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving tax declaration.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

      
        [HttpPost]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.TaxDeclarationDto>>> Create(
            [FromBody] DeductionsComplianceDto.CreateTaxDeclarationDto dto)
        {
            var response = new ApiResponse<DeductionsComplianceDto.TaxDeclarationDto>();

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
                response.Message = "Tax declaration created successfully.";
                response.ResponseData.Add(created);

                return CreatedAtAction(nameof(GetById), new { id = created.DeclarationId }, response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating tax declaration.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }

        
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.TaxDeclarationDto>>> Update(
            long id, [FromBody] DeductionsComplianceDto.UpdateTaxDeclarationDto dto)
        {
            var response = new ApiResponse<DeductionsComplianceDto.TaxDeclarationDto>();

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
                    response.Message = $"Tax declaration with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Tax declaration updated successfully.";
                response.ResponseData.Add(updated);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating tax declaration.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.TaxDeclarationDto>>> Delete(long id)
        {
            var response = new ApiResponse<DeductionsComplianceDto.TaxDeclarationDto>();

            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Tax declaration with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Tax declaration deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting tax declaration.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
