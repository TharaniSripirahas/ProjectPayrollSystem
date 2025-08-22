using EmployeeService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.DTOs;

namespace EmployeeService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeSkillController : ControllerBase
    {
        private readonly IEmployeeSkillService _employeeSkillService;

        public EmployeeSkillController(IEmployeeSkillService employeeSkillService)
        {
            _employeeSkillService = employeeSkillService;
        }

      
        [HttpGet]
        public async Task<ActionResult<ApiResponse<EmployeeSkillDto>>> GetAll()
        {
            var response = new ApiResponse<EmployeeSkillDto>
            {
                ResponseCode = 1,
                Message = "Success",
                ResponseData = await _employeeSkillService.GetAllAsync()
            };
            return Ok(response);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmployeeSkillDto>>> Get(long id)
        {
            var response = new ApiResponse<EmployeeSkillDto>();

            var skill = await _employeeSkillService.GetByIdAsync(id);
            if (skill == null)
            {
                response.ResponseCode = 0;
                response.Message = "Employee skill not found.";
                return NotFound(response);
            }

            response.ResponseCode = 1;
            response.Message = "Success";
            response.ResponseData.Add(skill);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmployeeSkillDto>>> Create([FromBody] EmployeeSkillDto dto)
        {
            var response = new ApiResponse<EmployeeSkillDto>();

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
                var created = await _employeeSkillService.CreateAsync(dto);
                response.ResponseCode = 1;
                response.Message = "Employee skill created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating employee skill.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmployeeSkillDto>>> Update(long id, [FromBody] EmployeeSkillDto dto)
        {
            var response = new ApiResponse<EmployeeSkillDto>();

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                return BadRequest(response);
            }

            try
            {
                var updated = await _employeeSkillService.UpdateAsync(id, dto);
                if (!updated)
                {
                    response.ResponseCode = 0;
                    response.Message = "Employee skill not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Employee skill updated successfully.";
                response.ResponseData.Add(dto);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating employee skill.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

      
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<EmployeeSkillDto>>> Delete(long id)
        {
            var response = new ApiResponse<EmployeeSkillDto>();

            try
            {
                var deleted = await _employeeSkillService.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = "Employee skill not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Employee skill deleted successfully.";
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting employee skill.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
