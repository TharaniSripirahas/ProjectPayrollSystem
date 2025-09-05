using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>> GetAll()
        {
            var response = new ApiResponse<DepartmentDto>
            {
                ResponseCode = -1,
                Message = string.Empty,
                ErrorDesc = string.Empty,
                ResponseData = new List<DepartmentDto>()
            };

            try
            {
                var Department = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = Department;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch employees.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>> GetById(long id)
        {
            var response = new ApiResponse<DepartmentDto>
            {
                ResponseCode = -1,
                Message = string.Empty,
                ErrorDesc = string.Empty,
                ResponseData = new List<DepartmentDto>()
            };

            try
            {
                var Department = await _service.GetByIdAsync(id);
                if (Department == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Department not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(Department);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving employee.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>> Create([FromBody] DepartmentDto dto)
        {
            var response = new ApiResponse<DepartmentDto>
            {
                ResponseCode = -1,
                Message = string.Empty,
                ErrorDesc = string.Empty,
                ResponseData = new List<DepartmentDto>()
            };

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
                var createdDepartment = await _service.CreateAsync(dto);
                if (createdDepartment != null)
                {
                    response.ResponseCode = 1;
                    response.Message = "Department created successfully.";
                    response.ResponseData.Add(createdDepartment);
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Failed to create department.";
                }

            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating employee.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Update(long id, [FromBody] DepartmentDto dto)
        {
            var response = new ApiResponse<DepartmentDto>
            {
                ResponseCode = -1,
                Message = string.Empty,
                ErrorDesc = string.Empty,
                ResponseData = new List<DepartmentDto>()
            };

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                return BadRequest(response);
            }

            try
            {
                var success = await _service.UpdateAsync(id, dto);
                if (success)
                {
                    response.ResponseCode = 1;
                    response.Message = "Employee updated successfully.";
                    response.ResponseData.Add(dto);
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Employee not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating employee.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(long id)
        {
            var response = new ApiResponse<DepartmentDto>
            {
                ResponseCode = -1,
                Message = string.Empty,
                ErrorDesc = string.Empty,
                ResponseData = new List<DepartmentDto>()
            };

            try
            {
                var success = await _service.DeleteAsync(id);
                if (success)
                {
                    response.ResponseCode = 1;
                    response.Message = "Employee deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Employee not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting employee.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

    }
}
