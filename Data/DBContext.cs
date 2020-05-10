using Microsoft.EntityFrameworkCore;
using MVCCoreTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCoreTutorial.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Employee> employee { get; set; }
        public DbSet<Department> department { get; set; }
        public DbSet<Sites> sites { get; set; }
        public DbSet<SiteUser> siteUser { get; set; }
        public DbSet<UserRole> userRole { get; set; }
    }
}
