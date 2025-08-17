using EmployeeService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.DTOs;
using Payroll.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Services
{
    public class EmployeeTypeService : IEmployeeTypeService
    {
        private readonly PayrollDbContext _context;

        public EmployeeTypeService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeTypeDto>> GetAllAsync()
        {
            return await _context.EmployeeTypes
                .Select(e => new EmployeeTypeDto
                {
                    EmployeeTypeId = e.EmployeeTypeId,
                    TypeName = e.TypeName,
                    Description = e.Description,
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn,
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn,
                    RecordStatus = e.RecordStatus
                }).ToListAsync();
        }

        public async Task<EmployeeTypeDto?> GetByIdAsync(long id)
        {
            var entity = await _context.EmployeeTypes.FindAsync(id);
            if (entity == null) return null;

            return new EmployeeTypeDto
            {
                EmployeeTypeId = entity.EmployeeTypeId,
                TypeName = entity.TypeName,
                Description = entity.Description,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<EmployeeTypeDto> CreateAsync(EmployeeTypeDto dto)
        {
            var entity = new EmployeeType
            {
                TypeName = dto.TypeName,
                Description = dto.Description,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy = dto.CreatedBy, // set initially
                LastModifiedOn = DateTime.UtcNow, // set initially
                RecordStatus = dto.RecordStatus
            };

            _context.EmployeeTypes.Add(entity);
            await _context.SaveChangesAsync();

            dto.EmployeeTypeId = entity.EmployeeTypeId;
            dto.CreatedOn = entity.CreatedOn;
            dto.LastModifiedBy = entity.LastModifiedBy;
            dto.LastModifiedOn = entity.LastModifiedOn;
            return dto;
        }


        public async Task<bool> UpdateAsync(long id, EmployeeTypeDto dto)
        {
            var entity = await _context.EmployeeTypes.FindAsync(id);
            if (entity == null) return false;

            entity.TypeName = dto.TypeName;
            entity.Description = dto.Description;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = dto.LastModifiedOn;

            _context.EmployeeTypes.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.EmployeeTypes.FindAsync(id);
            if (entity == null) return false;

            _context.EmployeeTypes.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
