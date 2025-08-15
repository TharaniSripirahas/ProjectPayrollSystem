using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.DatabaseContext
{
    public class PayrollDbContext : DbContext
    {
        public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<UserRole> UserRoles { get; set; } = null!;

        public DbSet<Employees> Employees { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employees>()
                        .Ignore(e => e.UserRoles);
        }
    }
}
