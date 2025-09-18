using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll.Common.NonEntities;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationFundController : ControllerBase
    {
        private readonly IDonationFundService _service;

        public DonationFundController(IDonationFundService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<DonationFundDto>>> GetAll()
        {
            var result = new ApiResult<DonationFundDto>();
            try
            {
                var funds = await _service.GetAllAsync();
                result.ResponseCode = 1;
                result.Message = "Funds retrieved successfully";
                result.ResponseData.AddRange(funds);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving funds";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ApiResult<DonationFundDto>>> GetById(long id)
        {
            var result = new ApiResult<DonationFundDto>();
            try
            {
                var fund = await _service.GetByIdAsync(id);
                if (fund == null)
                {
                    result.ResponseCode = 2;
                    result.Message = "Fund not found";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Fund retrieved successfully";
                result.ResponseData.Add(fund);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving fund";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<DonationFundDto>>> Create([FromBody] DonationFundCreateDto dto)
        {
            var result = new ApiResult<DonationFundDto>();
            try
            {
                var created = await _service.CreateAsync(dto);
                result.ResponseCode = 1;
                result.Message = "Fund created successfully";
                result.ResponseData.Add(created);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error creating fund";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ApiResult<DonationFundDto>>> Update(long id, [FromBody] DonationFundUpdateDto dto)
        {
            var result = new ApiResult<DonationFundDto>();
            try
            {
                if (id != dto.FundId)
                {
                    result.ResponseCode = 0;
                    result.Message = "FundId mismatch";
                    return BadRequest(result);
                }

                var updated = await _service.UpdateAsync(dto);
                if (updated == null)
                {
                    result.ResponseCode = 2;
                    result.Message = "Fund not found";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Fund updated successfully";
                result.ResponseData.Add(updated);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error updating fund";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ApiResult<bool>>> Delete(long id)
        {
            var result = new ApiResult<bool>();
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    result.ResponseCode = 2;
                    result.Message = "Fund not found";
                    return NotFound(result);
                }

                result.ResponseCode = 1;
                result.Message = "Fund deleted successfully";
                result.ResponseData.Add(true);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error deleting fund";
                result.ErrorDesc = ex.Message;
            }
            return Ok(result);
        }
    }
}
