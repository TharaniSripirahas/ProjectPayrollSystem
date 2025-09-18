using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.SecurityAccessDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<PermissionDto>>> GetAll()
        {
            var result = new ApiResult<PermissionDto>();
            try
            {
                var list = await _service.GetAllAsync();
                result.ResponseCode = 1;
                result.Message = "Permissions retrieved successfully.";
                result.ResponseData = list.ToList();
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving permissions.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<PermissionDto>>> GetById(long id)
        {
            var result = new ApiResult<PermissionDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    result.ResponseCode = 0;
                    result.Message = "Permission not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Permission retrieved successfully.";
                result.ResponseData.Add(item);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving permission.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<PermissionDto>>> Create(CreatePermissionDto dto)
        {
            var result = new ApiResult<PermissionDto>();
            try
            {
                var created = await _service.CreateAsync(dto);
                result.ResponseCode = 1;
                result.Message = "Permission created successfully.";
                result.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error creating permission.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<PermissionDto>>> Update(long id, UpdatePermissionDto dto)
        {
            var result = new ApiResult<PermissionDto>();
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (updated == null)
                {
                    result.ResponseCode = 0;
                    result.Message = "Permission not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Permission updated successfully.";
                result.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error updating permission.";
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
                    result.Message = "Permission not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Permission deleted successfully.";
                result.ResponseData.Add("Deleted");
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error deleting permission.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }
    }
}
