using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Infrastructure.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DbContextPayrollProject _context;

        public DepartmentService(DbContextPayrollProject context)
        {
            _context = context;
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            return await _context.Departments
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    Description = d.Description,
                    ManagerId = d.ManagerId,
                    CreatedBy = d.CreatedBy,
                    CreatedOn = d.CreatedOn,
                    LastModifiedBy = d.LastModifiedBy,
                    LastModifiedOn = d.LastModifiedOn,
                    RecordStatus = d.RecordStatus
                }).ToListAsync();
        }

        public async Task<DepartmentDto?> GetByIdAsync(long id)
        {
            var entity = await _context.Departments.FindAsync(id);
            if (entity == null) return null;

            return new DepartmentDto
            {
                DepartmentId = entity.DepartmentId,
                DepartmentName = entity.DepartmentName,
                Description = entity.Description,
                ManagerId = entity.ManagerId,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<DepartmentDto> CreateAsync(DepartmentDto dto)
        {
            var entity = new Department
            {
                DepartmentName = dto.DepartmentName,
                Description = dto.Description,
                ManagerId = dto.ManagerId,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = dto.RecordStatus,

                LastModifiedBy = dto.CreatedBy,
                LastModifiedOn = DateTime.UtcNow
            };

            _context.Departments.Add(entity);
            await _context.SaveChangesAsync();

            return new DepartmentDto
            {
                DepartmentName = entity.DepartmentName,
                Description = entity.Description,
                ManagerId = entity.ManagerId,
                DepartmentId = entity.DepartmentId,
                CreatedOn = entity.CreatedOn,
                LastModifiedOn = entity.LastModifiedOn,
                LastModifiedBy = entity.LastModifiedBy,
                RecordStatus = entity.RecordStatus,

            };
        }

        public async Task<bool> UpdateAsync(long id, DepartmentDto dto)
        {
            var entity = await _context.Departments.FindAsync(id);
            if (entity == null) return false;

            entity.DepartmentName = dto.DepartmentName;
            entity.Description = dto.Description;
            entity.ManagerId = dto.ManagerId;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            _context.Departments.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.Departments.FindAsync(id);
            if (entity == null) return false;

            _context.Departments.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
