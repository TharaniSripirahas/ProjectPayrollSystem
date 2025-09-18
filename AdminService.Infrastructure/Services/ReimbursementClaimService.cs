using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;

namespace AdminService.Infrastructure.Services
{
    public class ReimbursementClaimService : IReimbursementClaimService
    {
        private readonly PayrollDbContext _context;

        public ReimbursementClaimService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReimbursementClaimDto>> GetAllAsync()
        {
            return await _context.ReimbursementClaims
                .Include(r => r.Employee)
                .Include(r => r.Type)
                .Select(r => new ReimbursementClaimDto
                {
                    ClaimId = r.ClaimId,
                    EmployeeId = r.EmployeeId,
                    EmployeeName = r.Employee != null ? r.Employee.FirstName + " " + r.Employee.LastName : null,
                    TypeId = r.TypeId,
                    TypeName = r.Type != null ? r.Type.TypeName : null,
                    ClaimDate = r.ClaimDate,
                    Amount = r.Amount,
                    Document = r.Document,
                    ApprovedBy = r.ApprovedBy,
                    ApprovedAt = r.ApprovedAt,
                    PayrollCycleId = r.PayrollCycleId
                })
                .ToListAsync();
        }

        public async Task<ReimbursementClaimDto?> GetByIdAsync(long claimId)
        {
            var claim = await _context.ReimbursementClaims
                .Include(r => r.Employee)
                .Include(r => r.Type)
                .FirstOrDefaultAsync(r => r.ClaimId == claimId);

            if (claim == null) return null;

            return new ReimbursementClaimDto
            {
                ClaimId = claim.ClaimId,
                EmployeeId = claim.EmployeeId,
                EmployeeName = claim.Employee != null ? claim.Employee.FirstName + " " + claim.Employee.LastName : null,
                TypeId = claim.TypeId,
                TypeName = claim.Type != null ? claim.Type.TypeName : null,
                ClaimDate = claim.ClaimDate,
                Amount = claim.Amount,
                Document = claim.Document,
                ApprovedBy = claim.ApprovedBy,
                ApprovedAt = claim.ApprovedAt,
                PayrollCycleId = claim.PayrollCycleId
            };
        }

        public async Task<ReimbursementClaimDto> CreateAsync(CreateReimbursementClaimDto dto)
        {
            var claim = new ReimbursementClaim
            {
                EmployeeId = dto.EmployeeId,
                TypeId = dto.TypeId,
                ClaimDate = dto.ClaimDate,
                Amount = dto.Amount,
                Document = dto.Document,
                ApprovedBy = dto.ApprovedBy,
                PayrollCycleId = dto.PayrollCycleId,
                ApprovedAt = dto.ApprovedAt,
                CreatedBy = 1, 
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1
            };

            _context.ReimbursementClaims.Add(claim);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(claim.ClaimId) ?? throw new Exception("Failed to retrieve created claim.");
        }

        public async Task<ReimbursementClaimDto?> UpdateAsync(long claimId, UpdateReimbursementClaimDto dto)
        {
            var claim = await _context.ReimbursementClaims.FindAsync(claimId);
            if (claim == null) return null;

            claim.EmployeeId = dto.EmployeeId;
            claim.TypeId = dto.TypeId;
            claim.ClaimDate = dto.ClaimDate;
            claim.Amount = dto.Amount;
            claim.Document = dto.Document;
            claim.ApprovedBy = dto.ApprovedBy;
            claim.ApprovedAt = dto.ApprovedAt;
            claim.PayrollCycleId = dto.PayrollCycleId;
            claim.LastModifiedBy = 1; // TODO: replace with logged-in user
            claim.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(claim.ClaimId);
        }

        public async Task<bool> DeleteAsync(long claimId)
        {
            var claim = await _context.ReimbursementClaims.FindAsync(claimId);
            if (claim == null) return false;

            _context.ReimbursementClaims.Remove(claim);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
