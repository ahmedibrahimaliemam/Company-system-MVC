using Comoany.Bl.Interfaces;
using Company.DL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comoany.Bl.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyContext dbContext;

        public IEmployeeRepo EmployeeRepo { get ; set ; }
        public IDepartmentRepo DepartmentRepo { get; set ; }
        public UnitOfWork(CompanyContext DbContext)
        {
            EmployeeRepo=new EmployeeRepo(DbContext);
            DepartmentRepo=new DepartmentRepo(DbContext);
            dbContext = DbContext;
        }

        public async Task<int>Complete()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
