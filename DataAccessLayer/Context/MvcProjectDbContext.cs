using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class MvcProjectDbContext :IdentityDbContext<ApplicationUser>
    {
        public MvcProjectDbContext(DbContextOptions <MvcProjectDbContext> options) : base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server = . ; Database = MvcProject ; Trusted_Connection = true ; TrustServerCertificate = true ");

        public DbSet<Department> Departments { get; set;  }
        public DbSet<Employee> employees { get; set; }
        //public DbSet<T> Items { get; set; }
    
    }

}

