using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using Reimbursements.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursements.Infrastructure.Services
{
    public class ReimbursementTypeService : IReimbursementTypeService
    {
        private readonly PayrollDbContext _context;

        public ReimbursementTypeService(PayrollDbContext context)
        {
            _context = context;
        }

        // Get all
        public async Task<IEnumerable<ReimbursementTypeDto>> GetAllAsync()
        {
            return await _context.ReimbursementTypes
                .Where(r => r.RecordStatus == 0)
                .Select(r => new ReimbursementTypeDto
                {
                    TypeId = r.TypeId,
                    TypeName = r.TypeName,
                    Description = r.Description,
                    MaxAmountPerMonth = r.MaxAmountPerMonth,
                    IsClaim = r.IsClaim,
                    RequiresDocument = r.RequiresDocument,
                    RecordStatus = r.RecordStatus
                })
                .ToListAsync();
        }

        // Get by Id
        public async Task<ReimbursementTypeDto?> GetByIdAsync(long typeId)
        {
            var type = await _context.ReimbursementTypes
                .FirstOrDefaultAsync(r => r.TypeId == typeId && r.RecordStatus == 0);

            if (type == null) return null;

            return new ReimbursementTypeDto
            {
                TypeId = type.TypeId,
                TypeName = type.TypeName,
                Description = type.Description,
                MaxAmountPerMonth = type.MaxAmountPerMonth,
                IsClaim = type.IsClaim,
                RequiresDocument = type.RequiresDocument,
                RecordStatus = type.RecordStatus
            };
        }

        //Create new reimbursement type
        public async Task<ReimbursementTypeDto> CreateAsync(ReimbursementTypeDto dto)
        {
            var entity = new ReimbursementType
            {
                TypeName = dto.TypeName,
                Description = dto.Description,
                MaxAmountPerMonth = dto.MaxAmountPerMonth,
                IsClaim = dto.IsClaim,
                RequiresDocument = dto.RequiresDocument,
                CreatedBy = 1, 
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 0
            };

            _context.ReimbursementTypes.Add(entity);
            await _context.SaveChangesAsync();

            dto.TypeId = entity.TypeId;
            dto.RecordStatus = entity.RecordStatus;

            return dto;
        }

        // Update existing reimbursement type
        public async Task<ReimbursementTypeDto?> UpdateAsync(long typeId, ReimbursementTypeDto dto)
        {
            var entity = await _context.ReimbursementTypes.FindAsync(typeId);
            if (entity == null || entity.RecordStatus != 0) return null;

            entity.TypeName = dto.TypeName;
            entity.Description = dto.Description;
            entity.MaxAmountPerMonth = dto.MaxAmountPerMonth;
            entity.IsClaim = dto.IsClaim;
            entity.RequiresDocument = dto.RequiresDocument;
            entity.LastModifiedBy = 1; 
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            dto.TypeId = entity.TypeId;
            dto.RecordStatus = entity.RecordStatus;

            return dto;
        }

        // Delete reimbursement type 
        public async Task<bool> DeleteAsync(long typeId)
        {
            var entity = await _context.ReimbursementTypes.FindAsync(typeId);
            if (entity == null || entity.RecordStatus != 0) return false;

            entity.RecordStatus = 1; 
            entity.LastModifiedBy = 1;
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        //public async Task<bool> ExistsAsync(long typeId)
        //{
        //    return await _context.ReimbursementTypes.AnyAsync(r => r.TypeId == typeId && r.RecordStatus == 0);
        //}
    }
}
