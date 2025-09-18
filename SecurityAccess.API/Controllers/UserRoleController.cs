using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using SecurityAccess.Core.Interfaces;
using static Payroll.Common.NonEntities.SecurityAccessDto;

namespace SecurityAccess.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _service;

        public UserRoleController(IUserRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<UserRoleDto>>> GetAll()
        {
            var result = new ApiResult<UserRoleDto>();
            try
            {
                var roles = await _service.GetAllAsync();
                result.ResponseCode = 1;
                result.Message = "User roles retrieved successfully.";
                result.ResponseData = roles.ToList();
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving user roles.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<UserRoleDto>>> GetById(long id)
        {
            var result = new ApiResult<UserRoleDto>();
            try
            {
                var role = await _service.GetByIdAsync(id);
                if (role == null)
                {
                    result.ResponseCode = 0;
                    result.Message = "User role not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "User role retrieved successfully.";
                result.ResponseData.Add(role);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving user role.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<UserRoleDto>>> Create(CreateUserRoleDto dto)
        {
            var result = new ApiResult<UserRoleDto>();
            try
            {
                var created = await _service.CreateAsync(dto);
                result.ResponseCode = 1;
                result.Message = "User role created successfully.";
                result.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error creating user role.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<UserRoleDto>>> Update(long id, UpdateUserRoleDto dto)
        {
            var result = new ApiResult<UserRoleDto>();
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (updated == null)
                {
                    result.ResponseCode = 0;
                    result.Message = "User role not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "User role updated successfully.";
                result.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error updating user role.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<string>>> Delete(long id)
        {
            var result = new ApiResult<string>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    result.ResponseCode = 0;
                    result.Message = "User role not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "User role deleted successfully.";
                result.ResponseData.Add("Deleted");
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error deleting user role.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }
    }
}
