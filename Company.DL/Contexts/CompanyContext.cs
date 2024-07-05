using Company.DL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DL.Contexts
{
    public  class CompanyContext:IdentityDbContext<ApplicationModel>
    {


        public CompanyContext( DbContextOptions<CompanyContext> Options) : base(Options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=. ;Database=CompanyDb ; Trusted_Connection=true ");
        //}

        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }
    }
}
