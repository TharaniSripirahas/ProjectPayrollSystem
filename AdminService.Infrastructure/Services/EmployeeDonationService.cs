using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.Infrastructure.Services
{
    public class EmployeeDonationService : IEmployeeDonationService
    {
        private readonly PayrollDbContext _context;

        public EmployeeDonationService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDonationDto>> GetAllAsync()
        {
            return await _context.EmployeeDonations
                .Include(d => d.Employee)
                .Include(d => d.Fund)
                .Include(d => d.PayrollCycle) 
                .Select(d => new EmployeeDonationDto
                {
                    DonationId = d.DonationId,
                    EmployeeId = d.EmployeeId,
                    EmployeeName = d.Employee != null ? $"{d.Employee.FirstName} {d.Employee.LastName}" : null,
                    FundId = d.FundId,
                    FundName = d.Fund != null ? d.Fund.FundName : null,
                    Amount = d.Amount,
                    DonationDate = d.DonationDate,
                    Cause = d.Cause,
                    PayrollCycleId = d.PayrollCycleId,
                    PayrollCycleName = d.PayrollCycle != null ? d.PayrollCycle.PayrollCycleName : null,
                    CreatedBy = d.CreatedBy,
                    CreatedOn = d.CreatedOn,
                    LastModifiedBy = d.LastModifiedBy,
                    LastModifiedOn = d.LastModifiedOn,
                    RecordStatus = d.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<EmployeeDonationDto?> GetByIdAsync(long donationId)
        {
            return await _context.EmployeeDonations
                .Include(d => d.Employee)
                .Include(d => d.Fund)
                .Include(d => d.PayrollCycle) 
                .Where(d => d.DonationId == donationId)
                .Select(d => new EmployeeDonationDto
                {
                    DonationId = d.DonationId,
                    EmployeeId = d.EmployeeId,
                    EmployeeName = d.Employee != null ? $"{d.Employee.FirstName} {d.Employee.LastName}" : null,
                    FundId = d.FundId,
                    FundName = d.Fund != null ? d.Fund.FundName : null,
                    Amount = d.Amount,
                    DonationDate = d.DonationDate,
                    Cause = d.Cause,
                    PayrollCycleId = d.PayrollCycleId,
                    PayrollCycleName = d.PayrollCycle != null ? d.PayrollCycle.PayrollCycleName : null,
                    CreatedBy = d.CreatedBy,
                    CreatedOn = d.CreatedOn,
                    LastModifiedBy = d.LastModifiedBy,
                    LastModifiedOn = d.LastModifiedOn,
                    RecordStatus = d.RecordStatus
                })
                .FirstOrDefaultAsync();
        }

        public async Task<EmployeeDonationDto> CreateAsync(EmployeeDonationCreateDto dto)
        {
            var now = DateTime.UtcNow;

            var entity = new EmployeeDonation
            {
                EmployeeId = dto.EmployeeId,
                FundId = dto.FundId,
                Amount = dto.Amount,
                DonationDate = dto.DonationDate,
                Cause = dto.Cause,
                PayrollCycleId = dto.PayrollCycleId,
                CreatedBy = dto.CreatedBy,
                CreatedOn = dto.CreatedOn ?? DateTime.UtcNow,
                RecordStatus = dto.RecordStatus,
                LastModifiedBy = dto.CreatedBy,
                LastModifiedOn = now,
            };

            _context.EmployeeDonations.Add(entity);
            await _context.SaveChangesAsync();

            var createdDonation = await _context.EmployeeDonations
                .Include(d => d.Employee)
                .Include(d => d.Fund)
                .Include(d => d.PayrollCycle)
                .FirstOrDefaultAsync(d => d.DonationId == entity.DonationId);

            if (createdDonation == null)
                throw new Exception("Donation not created.");

            return new EmployeeDonationDto
            {
                DonationId = createdDonation.DonationId,
                EmployeeId = createdDonation.EmployeeId,
                EmployeeName = createdDonation.Employee != null
                    ? $"{createdDonation.Employee.FirstName} {createdDonation.Employee.LastName}"
                    : null,
                FundId = createdDonation.FundId,
                FundName = createdDonation.Fund != null ? createdDonation.Fund.FundName : null,
                Amount = createdDonation.Amount,
                DonationDate = createdDonation.DonationDate,
                Cause = createdDonation.Cause,
                PayrollCycleId = createdDonation.PayrollCycleId,
                PayrollCycleName = createdDonation.PayrollCycle?.PayrollCycleName,
                CreatedBy = createdDonation.CreatedBy,
                CreatedOn = createdDonation.CreatedOn,
                LastModifiedBy = createdDonation.LastModifiedBy,
                LastModifiedOn = createdDonation.LastModifiedOn,
                RecordStatus = createdDonation.RecordStatus
            };
        }

        public async Task<EmployeeDonationDto?> UpdateAsync(EmployeeDonationUpdateDto dto)
        {
            var entity = await _context.EmployeeDonations.FindAsync(dto.DonationId);
            if (entity == null) return null;

            entity.EmployeeId = dto.EmployeeId;
            entity.FundId = dto.FundId;
            entity.Amount = dto.Amount;
            entity.DonationDate = dto.DonationDate;
            entity.Cause = dto.Cause;
            entity.PayrollCycleId = dto.PayrollCycleId;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            _context.EmployeeDonations.Update(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.DonationId);
        }

        public async Task<bool> DeleteAsync(long donationId)
        {
            var entity = await _context.EmployeeDonations.FindAsync(donationId);
            if (entity == null) return false;

            _context.EmployeeDonations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
