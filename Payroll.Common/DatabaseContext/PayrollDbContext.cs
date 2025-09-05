using Microsoft.EntityFrameworkCore;
using Payroll.Common.Models;

namespace Payroll.Common.DatabaseContext
{
    public class DbContextPayrollProject : DbContext
    {
        public DbContextPayrollProject(DbContextOptions<DbContextPayrollProject> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<Employees> Employees { get; set; } = null!;
        public DbSet<EmployeeType> EmployeeTypes { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Designation> Designations { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; } = null!;

        public DbSet<Shift> Shifts { get; set; } = null!;
        public DbSet<LeaveType> LeaveTypes { get; set; } = null!;
        public DbSet<LeaveRequest> LeaveRequests { get; set; } = null!;
        public DbSet<AttendanceLog> AttendanceLogs { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employees>()
            .Ignore(e => e.UserRoles);

            modelBuilder.Entity<EmployeeType>().ToTable("EmployeeType");
            modelBuilder.Entity<EmployeeType>().HasKey(e => e.EmployeeTypeId);

            // PostgreSQL-specific types
            modelBuilder.Entity<Employees>()
                .Property(e => e.BankAccountNumber)
                .HasColumnType("bytea");

            modelBuilder.Entity<Employees>()
                .Property(e => e.DateOfBirth)
                .HasColumnType("date");

            modelBuilder.Entity<Employees>()
                .Property(e => e.JoinDate)
                .HasColumnType("date");

            modelBuilder.Entity<Employees>()
                .Property(e => e.ExitDate)
                .HasColumnType("date");

            // Relationships
            modelBuilder.Entity<Employees>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employees>()
                .HasOne(e => e.Designation)
                .WithMany()
                .HasForeignKey(e => e.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employees>()
                .HasOne(e => e.EmployeeType)
                .WithMany()
                .HasForeignKey(e => e.EmploymentType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Designation>()
                .HasOne(d => d.Department)
                .WithMany(dep => dep.Designations)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
