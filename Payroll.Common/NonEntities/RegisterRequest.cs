using System.ComponentModel.DataAnnotations;

namespace Payroll.Common.NonEntities
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public long RoleId { get; set; }

        [Required]
        public long EmployeeId { get; set; }

        public long CreatedBy { get; set; }
    }
}
