using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeStatutoryDetailController : ControllerBase
    {
        private readonly IEmployeeStatutoryDetailService _service;

        public EmployeeStatutoryDetailController(IEmployeeStatutoryDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.EmployeeStatutoryDetailDto>>> GetAll()
        {
            var response = new ApiResponse<DeductionsComplianceDto.EmployeeStatutoryDetailDto>();
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
                response.Message = "Failed to fetch employee statutory details.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.EmployeeStatutoryDetailDto>>> GetById(long id)
        {
            var response = new ApiResponse<DeductionsComplianceDto.EmployeeStatutoryDetailDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Employee statutory detail with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(item);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving employee statutory detail.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.EmployeeStatutoryDetailDto>>> Create(
            [FromBody] DeductionsComplianceDto.CreateEmployeeStatutoryDetailDto dto)
        {
            var response = new ApiResponse<DeductionsComplianceDto.EmployeeStatutoryDetailDto>();

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
                response.Message = "Employee statutory detail created successfully.";
                response.ResponseData.Add(created);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating employee statutory detail.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.EmployeeStatutoryDetailDto>>> Update(
            long id, [FromBody] DeductionsComplianceDto.UpdateEmployeeStatutoryDetailDto dto)
        {
            var response = new ApiResponse<DeductionsComplianceDto.EmployeeStatutoryDetailDto>();

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
                    response.Message = $"Employee statutory detail with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Employee statutory detail updated successfully.";
                response.ResponseData.Add(updated);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating employee statutory detail.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.EmployeeStatutoryDetailDto>>> Delete(long id)
        {
            var response = new ApiResponse<DeductionsComplianceDto.EmployeeStatutoryDetailDto>();

            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Employee statutory detail with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Employee statutory detail deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting employee statutory detail.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
