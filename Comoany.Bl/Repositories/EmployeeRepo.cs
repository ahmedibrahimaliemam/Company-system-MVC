using Comoany.Bl.Interfaces;
using Company.DL.Contexts;
using Company.DL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comoany.Bl.Repositories
{
    public class EmployeeRepo : GenericRepo<Employee> , IEmployeeRepo
    {
        private readonly CompanyContext companyContext;

        public EmployeeRepo(CompanyContext companyContext):base(companyContext)
        {
            this.companyContext = companyContext;
        }

        public IQueryable<Employee> SearchByName(string name)
        {
            return companyContext.Set<Employee>().Where((ele) => ele.name.ToLower().Contains(name.ToLower()));
        }
    }
}
