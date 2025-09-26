using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.DeductionsComplianceDto;

namespace AdminService.Infrastructure.Services
{
    public class EmployeeStatutoryDetailService : IEmployeeStatutoryDetailService
    {
        private readonly PayrollDbContext _context;

        public EmployeeStatutoryDetailService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeStatutoryDetailDto>> GetAllAsync()
        {
            return await _context.EmployeeStatutoryDetails
                .Include(e => e.Employee)                 
                .Include(e => e.StatutoryDeduction)       
                .Select(e => new EmployeeStatutoryDetailDto
                {
                    DetailsId = e.DetailsId,
                    EmployeeId = e.EmployeeId,
                    DeductionId = e.DeductionId,
                    AccountNumber = e.AccountNumber,
                    AccountDetails = e.AccountDetails,
                    IsApplicable = e.IsApplicable,
                    EmployeeName = e.Employee.FirstName + " " + e.Employee.LastName,
                    DeductionName = e.StatutoryDeduction.DeductionName,
                    RecordStatus = e.RecordStatus
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EmployeeStatutoryDetailDto?> GetByIdAsync(long detailsId)
        {
            var entity = await _context.EmployeeStatutoryDetails
                .Include(e => e.Employee)
                .Include(e => e.StatutoryDeduction)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.DetailsId == detailsId);

            if (entity == null) return null;

            return new EmployeeStatutoryDetailDto
            {
                DetailsId = entity.DetailsId,
                EmployeeId = entity.EmployeeId,
                DeductionId = entity.DeductionId,
                AccountNumber = entity.AccountNumber,
                AccountDetails = entity.AccountDetails,
                IsApplicable = entity.IsApplicable,
                EmployeeName = entity.Employee.FirstName + " " + entity.Employee.LastName,
                DeductionName = entity.StatutoryDeduction.DeductionName,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<EmployeeStatutoryDetailDto> CreateAsync(CreateEmployeeStatutoryDetailDto dto)
        {
            var entity = new EmployeeStatutoryDetail
            {
                EmployeeId = dto.EmployeeId,
                DeductionId = dto.DeductionId,
                AccountNumber = dto.AccountNumber,
                AccountDetails = dto.AccountDetails,
                IsApplicable = dto.IsApplicable,
                CreatedBy = dto.CreatedBy > 0 ? dto.CreatedBy : 1,
                CreatedOn = dto.CreatedOn ?? DateTime.UtcNow,
                RecordStatus = dto.RecordStatus > 0 ? dto.RecordStatus : 1
            };

            _context.EmployeeStatutoryDetails.Add(entity);
            await _context.SaveChangesAsync();

            var savedEntity = await _context.EmployeeStatutoryDetails
                .Include(e => e.Employee)
                .Include(e => e.StatutoryDeduction)
                .FirstOrDefaultAsync(e => e.DetailsId == entity.DetailsId);

            if (savedEntity == null)
                throw new Exception("Error retrieving created entity.");

            return new EmployeeStatutoryDetailDto
            {
                DetailsId = savedEntity.DetailsId,
                EmployeeId = savedEntity.EmployeeId,
                EmployeeName = savedEntity.Employee.FirstName + " " + savedEntity.Employee.LastName, 
                DeductionId = savedEntity.DeductionId,
                DeductionName = savedEntity.StatutoryDeduction.DeductionName,                      
                AccountNumber = savedEntity.AccountNumber,
                AccountDetails = savedEntity.AccountDetails,
                IsApplicable = savedEntity.IsApplicable,
                RecordStatus = savedEntity.RecordStatus
            };
        }

        public async Task<EmployeeStatutoryDetailDto?> UpdateAsync(long detailsId, UpdateEmployeeStatutoryDetailDto dto)
        {
            var entity = await _context.EmployeeStatutoryDetails.FindAsync(detailsId);
            if (entity == null) return null;

            entity.EmployeeId = dto.EmployeeId;
            entity.DeductionId = dto.DeductionId;
            entity.AccountNumber = dto.AccountNumber;
            entity.AccountDetails = dto.AccountDetails;
            entity.IsApplicable = dto.IsApplicable;
            entity.RecordStatus = dto.RecordStatus;
            entity.LastModifiedBy = dto.LastModifiedBy ?? 1;
            entity.LastModifiedOn = dto.LastModifiedOn ?? DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.DetailsId);
        }

        public async Task<bool> DeleteAsync(long detailsId)
        {
            var entity = await _context.EmployeeStatutoryDetails.FindAsync(detailsId);
            if (entity == null) return false;

            _context.EmployeeStatutoryDetails.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
