using EmployeeService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Helpers;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using static Payroll.Common.Enums.AppEnums;

namespace EmployeeService.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly PayrollDbContext _context;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IEncryptionHelper _encryptionHelper;


        public EmployeeService(PayrollDbContext context, IPasswordHelper passwordHelper, IEncryptionHelper encryptionHelper)
        {
            _context = context;
            _passwordHelper = passwordHelper;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Include(e => e.EmployeeType)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    UserName = e.UserName,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Gender = e.Gender,
                    DateOfBirth = e.DateOfBirth,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null ? e.Department.DepartmentName : null,
                    DesignationId = e.DesignationId,
                    DesignationName = e.Designation != null ? e.Designation.DesignationName : null,
                    EmployeeTypeId = e.EmployeeType != null ? e.EmployeeType.EmployeeTypeId : 0,
                    EmployeeTypeName = e.EmployeeType != null ? e.EmployeeType.TypeName : null,
                    SkillLevel = e.SkillLevel,
                    TechnologyTags = e.TechnologyTags,
                    EmploymentType = e.EmploymentType,
                    JoinDate = e.JoinDate,
                    ExitDate = e.ExitDate ?? DateOnly.FromDateTime(DateTime.UtcNow),
                    BankName = e.BankName,
                    BankAccountNumber = _encryptionHelper.DecryptStringFromBytes(e.BankAccountNumber),                    
                    IfscCode = e.IfscCode,
                    PfNumber = e.PfNumber,
                    EsiNumber = e.EsiNumber,
                    CreatedBy = e.CreatedBy,
                    CreatedOn = e.CreatedOn,
                    LastModifiedBy = e.LastModifiedBy,
                    LastModifiedOn = e.LastModifiedOn,
                    RecordStatus = (RecordStatus)e.RecordStatus,
                    Password = "[PROTECTED]"
                })
                .ToListAsync();
        }

        // Get Employee by ID
        public async Task<EmployeeDto?> GetByIdAsync(long id)
        {
            var e = await _context.Employees
                .Include(x => x.Department)
                .Include(x => x.Designation)
                .Include(x => x.EmployeeType)
                .FirstOrDefaultAsync(x => x.EmployeeId == id);

            if (e == null) return null;

            return new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                UserName = e.UserName,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department?.DepartmentName,
                DesignationId = e.DesignationId,
                DesignationName = e.Designation?.DesignationName,
                EmployeeTypeId = e.EmployeeType?.EmployeeTypeId ?? 0,
                EmployeeTypeName = e.EmployeeType?.TypeName,
                SkillLevel = e.SkillLevel,
                TechnologyTags = e.TechnologyTags,
                EmploymentType = e.EmploymentType,
                JoinDate = e.JoinDate,
                ExitDate = e.ExitDate,
                BankName = e.BankName,
                BankAccountNumber = _encryptionHelper.DecryptStringFromBytes(e.BankAccountNumber),
                IfscCode = e.IfscCode,
                PfNumber = e.PfNumber,
                EsiNumber = e.EsiNumber,
                CreatedBy = e.CreatedBy,
                CreatedOn = e.CreatedOn,
                LastModifiedBy = e.LastModifiedBy,
                LastModifiedOn = e.LastModifiedOn,
                RecordStatus = (RecordStatus)e.RecordStatus,
                Password = string.Empty
            };
        }

        // Update Employee
        public async Task<bool> UpdateAsync(long id, EmployeeDto dto)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return false;

            emp.UserName = dto.UserName;
            emp.FirstName = dto.FirstName;
            emp.LastName = dto.LastName;
            emp.Email = dto.Email;
            emp.PhoneNumber = dto.PhoneNumber;
            emp.Gender = dto.Gender;
            emp.DateOfBirth = dto.DateOfBirth;
            emp.DepartmentId = dto.DepartmentId;
            emp.DesignationId = dto.DesignationId;
            emp.SkillLevel = dto.SkillLevel;
            emp.TechnologyTags = dto.TechnologyTags;
            emp.EmploymentType = dto.EmploymentType;
            emp.JoinDate = dto.JoinDate;
            emp.ExitDate = dto.ExitDate;
            emp.BankName = dto.BankName;
            emp.BankAccountNumber = _encryptionHelper.EncryptStringToBytes(dto.BankAccountNumber);
            emp.IfscCode = dto.IfscCode;
            emp.PfNumber = dto.PfNumber;
            emp.EsiNumber = dto.EsiNumber;
            emp.LastModifiedBy = dto.LastModifiedBy;
            emp.LastModifiedOn = DateTime.UtcNow;
            emp.RecordStatus = (int)dto.RecordStatus;

            if (!string.IsNullOrWhiteSpace(dto.Password))
                emp.PasswordHash = _passwordHelper.HashPassword(dto.Password);

            _context.Employees.Update(emp);
            return await _context.SaveChangesAsync() > 0;
        }

        // Delete Employee
        public async Task<bool> DeleteAsync(long id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return false;

            _context.Employees.Remove(emp);
            return await _context.SaveChangesAsync() > 0;
        }

        // Create Employee 
        public async Task<ApiResult<EmployeeDto>> CreateEmployeeAsync(EmployeeDto dto)
        {
            var result = new ApiResult<EmployeeDto>();

            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                result.ResponseCode = 0;
                result.Message = "Employee creation failed.";
                result.ErrorDesc = "Password cannot be null or empty.";
                return result;
            }

            try
            {
                var emp = new Employee
                {
                    UserName = dto.UserName,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    DepartmentId = dto.DepartmentId,
                    DesignationId = dto.DesignationId,
                    SkillLevel = dto.SkillLevel,
                    TechnologyTags = dto.TechnologyTags,
                    EmploymentType = dto.EmploymentType,
                    JoinDate = dto.JoinDate,
                    ExitDate = dto.ExitDate,
                    BankName = dto.BankName,
                    BankAccountNumber = _encryptionHelper.EncryptStringToBytes(dto.BankAccountNumber),
                    IfscCode = dto.IfscCode,
                    PfNumber = dto.PfNumber,
                    EsiNumber = dto.EsiNumber,
                    CreatedBy = dto.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    LastModifiedBy = dto.LastModifiedBy,
                    LastModifiedOn = dto.LastModifiedOn,
                    RecordStatus = (int)dto.RecordStatus,
                    PasswordHash = _passwordHelper.HashPassword(dto.Password)
                };

                _context.Employees.Add(emp);
                await _context.SaveChangesAsync();

                var empWithRelations = await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.Designation)
                    .Include(e => e.EmployeeType)
                    .FirstOrDefaultAsync(e => e.EmployeeId == emp.EmployeeId);

                if (empWithRelations != null)
                {
                    dto.EmployeeId = empWithRelations.EmployeeId;
                    dto.DepartmentName = empWithRelations.Department?.DepartmentName;
                    dto.DesignationName = empWithRelations.Designation?.DesignationName;
                    dto.EmployeeTypeName = empWithRelations.EmployeeType?.TypeName;
                    dto.CreatedOn = empWithRelations.CreatedOn;
                    dto.LastModifiedOn = empWithRelations.LastModifiedOn;
                    dto.Password = string.Empty;
                }

                result.ResponseCode = 1;
                result.Message = "Employee created successfully.";
                result.ResponseData = new List<EmployeeDto> { dto };
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Employee creation failed.";
                result.ErrorDesc = ex.Message;
            }

            return result;
        }
    }
}
