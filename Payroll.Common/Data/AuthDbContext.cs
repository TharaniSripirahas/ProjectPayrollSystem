using Microsoft.EntityFrameworkCore;
using Payroll.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.Data
{
    public class AuthDbContext
    {
        public class AuthDbContextData : DbContext
        {
            public AuthDbContextData(DbContextOptions<AuthDbContextData> options) : base(options) { }
            public DbSet<User> Userdb { get; set; } = null!;
            public DbSet<UserRole> UserRoledb { get; set; } = null!;

        }
    }
}

