using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        // GET: api/Skill
        [HttpGet]
        public async Task<ActionResult<ApiResponse<SkillDto>>> GetAll()
        {
            var response = new ApiResponse<SkillDto>
            {
                ResponseCode = -1,
                Message = string.Empty,
                ErrorDesc = string.Empty,
                ResponseData = new List<SkillDto>()
            };

            try
            {
                var skills = await _skillService.GetAllAsync();
                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData = skills;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Failed to fetch skills.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // GET: api/Skill/{id}
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<SkillDto>>> GetById(long id)
        {
            var response = new ApiResponse<SkillDto>
            {
                ResponseCode = -1,
                Message = string.Empty,
                ErrorDesc = string.Empty,
                ResponseData = new List<SkillDto>()
            };

            try
            {
                var skill = await _skillService.GetByIdAsync(id);
                if (skill == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Skill not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(skill);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving skill.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // POST: api/Skill
        [HttpPost]
        public async Task<ActionResult<ApiResponse<SkillDto>>> Create([FromBody] SkillDto dto)
        {
            var response = new ApiResponse<SkillDto>
            {
                ResponseCode = -1,
                Message = string.Empty,
                ErrorDesc = string.Empty,
                ResponseData = new List<SkillDto>()
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
                var created = await _skillService.CreateAsync(dto);
                response.ResponseCode = 1;
                response.Message = "Skill created successfully.";
                response.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating skill.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // PUT: api/Skill/{id}
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<SkillDto>>> Update(long id, [FromBody] SkillDto dto)
        {
            var response = new ApiResponse<SkillDto>
            {
                ResponseCode = -1,
                Message = string.Empty,
                ErrorDesc = string.Empty,
                ResponseData = new List<SkillDto>()
            };

            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                return BadRequest(response);
            }

            try
            {
                var updated = await _skillService.UpdateAsync(id, dto);
                if (!updated)
                {
                    response.ResponseCode = 0;
                    response.Message = "Skill not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Skill updated successfully.";
                response.ResponseData.Add(dto);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating skill.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        // DELETE: api/Skill/{id}
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<SkillDto>>> Delete(long id)
        {
            var response = new ApiResponse<SkillDto>
            {
                ResponseCode = -1,
                Message = string.Empty,
                ErrorDesc = string.Empty,
                ResponseData = new List<SkillDto>()
            };

            try
            {
                var deleted = await _skillService.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = "Skill not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Skill deleted successfully.";
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting skill.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }
    }
}
