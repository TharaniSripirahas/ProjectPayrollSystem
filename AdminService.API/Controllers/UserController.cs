using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.SecurityAccessDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<UserDto>>> GetAll()
        {
            var result = new ApiResult<UserDto>();
            try
            {
                var users = await _service.GetAllAsync();
                result.ResponseCode = 1;
                result.Message = "Users retrieved successfully.";
                result.ResponseData = users.ToList();
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving users.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<UserDto>>> GetById(long id)
        {
            var result = new ApiResult<UserDto>();
            try
            {
                var user = await _service.GetByIdAsync(id);
                if (user == null)
                {
                    result.ResponseCode = 0;
                    result.Message = "User not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "User retrieved successfully.";
                result.ResponseData.Add(user);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving user.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<UserDto>>> Create(CreateUserDto dto)
        {
            var result = new ApiResult<UserDto>();
            try
            {
                var created = await _service.CreateAsync(dto);
                result.ResponseCode = 1;
                result.Message = "User created successfully.";
                result.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error creating user.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<UserDto>>> Update(long id, UpdateUserDto dto)
        {
            var result = new ApiResult<UserDto>();
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (updated == null)
                {
                    result.ResponseCode = 0;
                    result.Message = "User not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "User updated successfully.";
                result.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error updating user.";
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
                    result.Message = "User not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "User deleted successfully.";
                result.ResponseData.Add("Deleted");
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error deleting user.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }
    }
}
