using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.SecurityAccessDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _service;

        public AuditLogController(IAuditLogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<AuditLogDto>>> GetAll()
        {
            var result = new ApiResult<AuditLogDto>();
            try
            {
                var list = await _service.GetAllAsync();
                result.ResponseCode = 1;
                result.Message = "Audit logs retrieved successfully.";
                result.ResponseData = list.ToList();
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving audit logs.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResult<AuditLogDto>>> GetById(long id)
        {
            var result = new ApiResult<AuditLogDto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    result.ResponseCode = 0;
                    result.Message = "Audit log not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Audit log retrieved successfully.";
                result.ResponseData.Add(item);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving audit log.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<AuditLogDto>>> Create(CreateAuditLogDto dto)
        {
            var result = new ApiResult<AuditLogDto>();
            try
            {
                var created = await _service.CreateAsync(dto);
                result.ResponseCode = 1;
                result.Message = "Audit log created successfully.";
                result.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error creating audit log.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResult<AuditLogDto>>> Update(long id, UpdateAuditLogDto dto)
        {
            var result = new ApiResult<AuditLogDto>();
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (updated == null)
                {
                    result.ResponseCode = 0;
                    result.Message = "Audit log not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Audit log updated successfully.";
                result.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error updating audit log.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResult<string>>> Delete(long id)
        {
            var result = new ApiResult<string>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    result.ResponseCode = 0;
                    result.Message = "Audit log not found.";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Audit log deleted successfully.";
                result.ResponseData.Add("Deleted");
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error deleting audit log.";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }
    }
}
