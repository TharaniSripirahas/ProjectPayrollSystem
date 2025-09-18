using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApprovalWorkflowController : ControllerBase
    {
        private readonly IApprovalWorkflowService _service;

        public ApprovalWorkflowController(IApprovalWorkflowService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<ApprovalWorkflowDto>>> GetAll()
        {
            var response = new ApiResponse<ApprovalWorkflowDto>();
            try
            {
                var list = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = list.ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch workflows.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalWorkflowDto>>> GetById(long id)
        {
            var response = new ApiResponse<ApprovalWorkflowDto>();
            try
            {
                var workflow = await _service.GetByIdAsync(id);
                if (workflow == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Workflow not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(workflow);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving workflow.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ApprovalWorkflowDto>>> Create([FromBody] CreateApprovalWorkflowDto dto)
        {
            var response = new ApiResponse<ApprovalWorkflowDto>();
            try
            {
                var created = await _service.CreateAsync(dto);
                response.ResponseCode = 1;
                response.Message = "Workflow created successfully.";
                response.ResponseData.Add(created);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating workflow.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalWorkflowDto>>> Update(long id, [FromBody] UpdateApprovalWorkflowDto dto)
        {
            var response = new ApiResponse<ApprovalWorkflowDto>();
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (updated == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Workflow not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Workflow updated successfully.";
                response.ResponseData.Add(updated);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating workflow.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<ApprovalWorkflowDto>>> Delete(long id)
        {
            var response = new ApiResponse<ApprovalWorkflowDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = "Workflow not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Workflow deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting workflow.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
