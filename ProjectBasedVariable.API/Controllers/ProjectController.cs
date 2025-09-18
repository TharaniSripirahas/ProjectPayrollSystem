using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using ProjectBasedVariable.Core.Interfaces;

namespace ProjectBasedVariable.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> GetAll()
        {
            var response = new ApiResponse<ProjectDto>();
            try
            {
                var projects = await _service.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = projects.ToList();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch projects.";
                response.ErrorDesc = ex.Message;
            }
            return Ok(response);
        }

        // GET: api/Project/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> GetById(long id)
        {
            var response = new ApiResponse<ProjectDto>();
            try
            {
                var project = await _service.GetByIdAsync(id);
                if (project == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Project not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(project);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving project.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // POST: api/Project
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> Create([FromBody] ProjectDto dto)
        {
            var response = new ApiResponse<ProjectDto>();

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
                response.Message = "Project created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating project.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // PUT: api/Project/5
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> Update(long id, [FromBody] ProjectDto dto)
        {
            var response = new ApiResponse<ProjectDto>();

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
                    response.Message = "Project not found.";
                }
                else
                {
                    response.ResponseCode = 1;
                    response.Message = "Project updated successfully.";
                    response.ResponseData.Add(updated);
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating project.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // DELETE: api/Project/5
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> Delete(long id)
        {
            var response = new ApiResponse<ProjectDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (deleted)
                {
                    response.ResponseCode = 1;
                    response.Message = "Project deleted successfully.";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.Message = "Project not found.";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting project.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
