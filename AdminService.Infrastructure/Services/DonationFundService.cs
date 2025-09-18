using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.Infrastructure.Services
{
    public class DonationFundService : IDonationFundService
    {
        private readonly PayrollDbContext _context;

        public DonationFundService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DonationFundDto>> GetAllAsync()
        {
            return await _context.DonationFunds
                .Select(f => new DonationFundDto
                {
                    FundId = f.FundId,
                    FundName = f.FundName,
                    Description = f.Description,
                    IsActive = f.IsActive,
                    CreatedBy = f.CreatedBy,
                    CreatedOn = f.CreatedOn,
                    LastModifiedBy = f.LastModifiedBy,
                    LastModifiedOn = f.LastModifiedOn,
                    RecordStatus = f.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<DonationFundDto?> GetByIdAsync(long fundId)
        {
            return await _context.DonationFunds
                .Where(f => f.FundId == fundId)
                .Select(f => new DonationFundDto
                {
                    FundId = f.FundId,
                    FundName = f.FundName,
                    Description = f.Description,
                    IsActive = f.IsActive,
                    CreatedBy = f.CreatedBy,
                    CreatedOn = f.CreatedOn,
                    LastModifiedBy = f.LastModifiedBy,
                    LastModifiedOn = f.LastModifiedOn,
                    RecordStatus = f.RecordStatus
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DonationFundDto> CreateAsync(DonationFundCreateDto dto)
        {
            var entity = new DonationFund
            {
                FundName = dto.FundName,
                Description = dto.Description,
                IsActive = dto.IsActive ?? 1,
                CreatedBy = dto.CreatedBy,
                CreatedOn = dto.CreatedOn ?? DateTime.UtcNow,
                RecordStatus = dto.RecordStatus
            };

            _context.DonationFunds.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.FundId) ?? throw new Exception("Fund not created");
        }

        public async Task<DonationFundDto?> UpdateAsync(DonationFundUpdateDto dto)
        {
            var entity = await _context.DonationFunds.FindAsync(dto.FundId);
            if (entity == null) return null;

            entity.FundName = dto.FundName;
            entity.Description = dto.Description;
            entity.IsActive = dto.IsActive;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            _context.DonationFunds.Update(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.FundId);
        }

        public async Task<bool> DeleteAsync(long fundId)
        {
            var entity = await _context.DonationFunds.FindAsync(fundId);
            if (entity == null) return false;

            _context.DonationFunds.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
