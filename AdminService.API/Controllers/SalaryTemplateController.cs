using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryTemplateController : ControllerBase
    {
        private readonly ISalaryTemplateService _service;

        public SalaryTemplateController(ISalaryTemplateService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<ActionResult<ApiResponse<SalaryTemplateDto>>> GetAll()
        {
            var response = new ApiResponse<SalaryTemplateDto>();
            try
            {
                var templates = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = templates.ToList();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch salary templates.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

      
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<SalaryTemplateDto>>> Get(long id)
        {
            var response = new ApiResponse<SalaryTemplateDto>();
            try
            {
                var template = await _service.GetByIdAsync(id);
                if (template == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Salary template not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(template);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving salary template.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

     
        [HttpPost]
        public async Task<ActionResult<ApiResponse<SalaryTemplateDto>>> Create([FromBody] SalaryTemplateDto dto)
        {
            var response = new ApiResponse<SalaryTemplateDto>();

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
                response.Message = "Salary template created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating salary template.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

  
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<SalaryTemplateDto>>> Update(long id, [FromBody] SalaryTemplateDto dto)
        {
            var response = new ApiResponse<SalaryTemplateDto>();

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
                    response.Message = "Salary template not found.";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "Salary template updated successfully.";
                    response.ResponseData.Add(updated);
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating salary template.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

      
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<SalaryTemplateDto>>> Delete(long id)
        {
            var response = new ApiResponse<SalaryTemplateDto>();
            try
            {
                var success = await _service.DeleteAsync(id);
                if (success)
                {
                    response.ResponseCode = 1;
                    response.Message = "Salary template deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Salary template not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting salary template.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
