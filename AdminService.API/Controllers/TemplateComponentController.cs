using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplateComponentController : ControllerBase
    {
        private readonly ITemplateComponentService _service;

        public TemplateComponentController(ITemplateComponentService service)
        {
            _service = service;
        }

  
        [HttpGet]
        public async Task<ActionResult<ApiResponse<TemplateComponentDto>>> GetAll()
        {
            var response = new ApiResponse<TemplateComponentDto>();
            try
            {
                var items = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = items.ToList();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch template components.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

    
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<TemplateComponentDto>>> GetById(long id)
        {
            var response = new ApiResponse<TemplateComponentDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Template component not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(item);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving template component.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

    
        [HttpPost]
        public async Task<ActionResult<ApiResponse<TemplateComponentDto>>> Create([FromBody] TemplateComponentDto dto)
        {
            var response = new ApiResponse<TemplateComponentDto>();

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
                response.Message = "Template component created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating template component.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

     
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<TemplateComponentDto>>> Update(long id, [FromBody] TemplateComponentDto dto)
        {
            var response = new ApiResponse<TemplateComponentDto>();

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
                    response.Message = "Template component not found.";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "Template component updated successfully.";
                    response.ResponseData.Add(updated);
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating template component.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

      
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<TemplateComponentDto>>> Delete(long id)
        {
            var response = new ApiResponse<TemplateComponentDto>();
            try
            {
                var success = await _service.DeleteAsync(id);
                if (success)
                {
                    response.ResponseCode = 1;
                    response.Message = "Template component deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Template component not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting template component.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
