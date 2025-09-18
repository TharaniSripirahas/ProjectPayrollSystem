using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.NonEntities
{
    public class SalaryComponentDto
    {
        public long ComponentId { get; set; }
        public string ComponentName { get; set; } = string.Empty;
        public long ComponentType { get; set; }
        public int? IsTaxable { get; set; }
        public int? IsStatutory { get; set; }
        public string? Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }


    public class SalaryTemplateDto
    {
        public long TemplateId { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public long EmployeeTypeId { get; set; }
        public string? Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }

    public class TemplateComponentDto
    {
        public long TemplateComponentId { get; set; }
        public long TemplateId { get; set; }
        public long ComponentId { get; set; }
        public long CalculationType { get; set; }
        public decimal? Value { get; set; }
        public decimal? MaxLimit { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }

    public class EmpSalaryStructureDto
    {
        public long StructureId { get; set; }
        public long EmployeeName { get; set; }
        public long TemplateId { get; set; }
        public decimal BasicSalary { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public int? IsCurrent { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }
}
