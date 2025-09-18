using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Form16Controller : ControllerBase
    {
        private readonly IForm16Service _service;

        public Form16Controller(IForm16Service service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.Form16Dto>>> GetAll()
        {
            var response = new ApiResponse<DeductionsComplianceDto.Form16Dto>();
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
                response.Message = "Failed to fetch Form16 records.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

      
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.Form16Dto>>> GetById(long id)
        {
            var response = new ApiResponse<DeductionsComplianceDto.Form16Dto>();
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Form16 with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(item);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving Form16 record.";
                response.ErrorDesc = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.Form16Dto>>> Create(
            [FromBody] DeductionsComplianceDto.CreateForm16Dto dto)
        {
            var response = new ApiResponse<DeductionsComplianceDto.Form16Dto>();

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
                response.Message = "Form16 created successfully.";
                response.ResponseData.Add(created);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error creating Form16 record.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.Form16Dto>>> Update(
            long id, [FromBody] DeductionsComplianceDto.UpdateForm16Dto dto)
        {
            var response = new ApiResponse<DeductionsComplianceDto.Form16Dto>();

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
                    response.Message = $"Form16 with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Form16 updated successfully.";
                response.ResponseData.Add(updated);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating Form16 record.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }

      
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<DeductionsComplianceDto.Form16Dto>>> Delete(long id)
        {
            var response = new ApiResponse<DeductionsComplianceDto.Form16Dto>();

            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = $"Form16 with ID {id} not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Form16 deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting Form16 record.";
                response.ErrorDesc = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
