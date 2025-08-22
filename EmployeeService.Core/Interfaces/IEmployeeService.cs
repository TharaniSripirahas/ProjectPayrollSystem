using Payroll.Common.DTOs;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> GetByIdAsync(long id);
       // Task<bool> CreateAsync(EmployeeDto dto);
        Task<bool> UpdateAsync(long id, EmployeeDto dto);
        Task<bool> DeleteAsync(long id);
        Task<ApiResult<EmployeeDto>> CreateEmployeeAsync(EmployeeDto dto);
    }
    public interface IDesignationService
    {
        Task<List<DesignationDto>> GetAllAsync();
        Task<DesignationDto?> GetByIdAsync(long id);
        Task<DesignationDto> CreateAsync(DesignationDto dto);
        Task<bool> UpdateAsync(long id, DesignationDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public interface ISkillService
    {
        Task<List<SkillDto>> GetAllAsync();
        Task<SkillDto?> GetByIdAsync(long id);
        Task<SkillDto> CreateAsync(SkillDto dto);
        Task<bool> UpdateAsync(long id, SkillDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public interface IEmployeeSkillService
    {
        Task<List<EmployeeSkillDto>> GetAllAsync();
        Task<EmployeeSkillDto?> GetByIdAsync(long id);
        Task<EmployeeSkillDto> CreateAsync(EmployeeSkillDto dto);
        Task<bool> UpdateAsync(long id, EmployeeSkillDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public interface IEmployeeTypeService
    {
        Task<List<EmployeeTypeDto>> GetAllAsync();
        Task<EmployeeTypeDto?> GetByIdAsync(long id);
        Task<EmployeeTypeDto> CreateAsync(EmployeeTypeDto req);
        Task<bool> UpdateAsync(long id, EmployeeTypeDto req);
        Task<bool> DeleteAsync(long id);
    }
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto?> GetByIdAsync(long id);
        Task<DepartmentDto> CreateAsync(DepartmentDto dto);
        Task<bool> UpdateAsync(long id, DepartmentDto dto);
        Task<bool> DeleteAsync(long id);
    }


}
