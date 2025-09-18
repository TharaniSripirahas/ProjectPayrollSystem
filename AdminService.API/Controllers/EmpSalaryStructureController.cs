using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpSalaryStructureController : ControllerBase
    {
        private readonly IEmpSalaryStructureService _service;

        public EmpSalaryStructureController(IEmpSalaryStructureService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<EmpSalaryStructureDto>>> GetAll()
        {
            var response = new ApiResponse<EmpSalaryStructureDto>();
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
                response.Message = "Failed to fetch salary structures.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmpSalaryStructureDto>>> Get(long id)
        {
            var response = new ApiResponse<EmpSalaryStructureDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Salary structure not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(item);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving salary structure.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmpSalaryStructureDto>>> Create([FromBody] EmpSalaryStructureDto dto)
        {
            var response = new ApiResponse<EmpSalaryStructureDto>();

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
                response.Message = "Salary structure created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating salary structure.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmpSalaryStructureDto>>> Update(long id, [FromBody] EmpSalaryStructureDto dto)
        {
            var response = new ApiResponse<EmpSalaryStructureDto>();

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
                    response.Message = "Salary structure not found.";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "Salary structure updated successfully.";
                    response.ResponseData.Add(updated);
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating salary structure.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmpSalaryStructureDto>>> Delete(long id)
        {
            var response = new ApiResponse<EmpSalaryStructureDto>();
            try
            {
                var success = await _service.DeleteAsync(id);
                if (success)
                {
                    response.ResponseCode = 1;
                    response.Message = "Salary structure deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Salary structure not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting salary structure.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
