using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.PayslipsReportingDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneratedReportController : ControllerBase
    {
        private readonly IGeneratedReportService _service;

        public GeneratedReportController(IGeneratedReportService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<ActionResult<ApiResponse<GeneratedReportDto>>> GetAll()
        {
            var response = new ApiResponse<GeneratedReportDto>();
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
                response.Message = "Failed to fetch reports.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

      
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResponse<GeneratedReportDto>>> GetById(long id)
        {
            var response = new ApiResponse<GeneratedReportDto>();
            try
            {
                var report = await _service.GetByIdAsync(id);
                if (report == null)
                {
                    response.ResponseCode = 0;
                    response.Message = "Report not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(report);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error retrieving report.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

      
        [HttpPost]
        public async Task<ActionResult<ApiResponse<GeneratedReportDto>>> Create([FromBody] CreateGeneratedReportDto dto)
        {
            var response = new ApiResponse<GeneratedReportDto>();
            if (!ModelState.IsValid)
            {
                response.ResponseCode = 0;
                response.Message = "Validation failed.";
                response.ErrorDesc = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(response);
            }

            try
            {
                var created = await _service.CreateAsync(dto);
                response.ResponseCode = 1;
                response.Message = "Report created successfully.";
                response.ResponseData.Add(created);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error occurred while creating report.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

      
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResponse<GeneratedReportDto>>> Update(long id, [FromBody] UpdateGeneratedReportDto dto)
        {
            var response = new ApiResponse<GeneratedReportDto>();
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
                    response.Message = "Report not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Report updated successfully.";
                response.ResponseData.Add(updated);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error updating report.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }

    
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResponse<GeneratedReportDto>>> Delete(long id)
        {
            var response = new ApiResponse<GeneratedReportDto>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    response.ResponseCode = 0;
                    response.Message = "Report not found.";
                    return NotFound(response);
                }

                response.ResponseCode = 1;
                response.Message = "Report deleted successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "Error deleting report.";
                response.ErrorDesc = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
