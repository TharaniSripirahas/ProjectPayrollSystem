using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using SalaryComponent.Core.Interfaces;

namespace SalaryComponent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryComponentController : ControllerBase
    {
        private readonly ISalaryComponentService _service;

        public SalaryComponentController(ISalaryComponentService service)
        {
            _service = service;
        }

        // GET: api/SalaryComponent
        [HttpGet]
        public async Task<ActionResult<ApiResponse<SalaryComponentDto>>> GetAll()
        {
            var response = new ApiResponse<SalaryComponentDto>();
            try
            {
                var components = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Salary components retrieved successfully.";
                response.ResponseData = components.ToList();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch salary components.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // GET: api/SalaryComponent/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<SalaryComponentDto>>> GetById(long id)
        {
            var response = new ApiResponse<SalaryComponentDto>();
            try
            {
                var component = await _service.GetByIdAsync(id);
                if (component == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Salary component not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Salary component retrieved successfully.";
                response.ResponseData.Add(component);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving salary component.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // POST: api/SalaryComponent
        [HttpPost]
        public async Task<ActionResult<ApiResponse<SalaryComponentDto>>> Create([FromBody] SalaryComponentDto dto)
        {
            var response = new ApiResponse<SalaryComponentDto>();

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
                response.Message = "Salary component created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating salary component.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // PUT: api/SalaryComponent/5
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<SalaryComponentDto>>> Update(long id, [FromBody] SalaryComponentDto dto)
        {
            var response = new ApiResponse<SalaryComponentDto>();

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
                    response.Message = "Salary component not found.";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "Salary component updated successfully.";
                    response.ResponseData.Add(updated);
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating salary component.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // DELETE: api/SalaryComponent/5
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<SalaryComponentDto>>> Delete(long id)
        {
            var response = new ApiResponse<SalaryComponentDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (deleted)
                {
                    response.ResponseCode = 1;
                    response.Message = "Salary component deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Salary component not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting salary component.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
