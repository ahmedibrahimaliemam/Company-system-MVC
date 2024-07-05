using Comoany.Bl.Interfaces;
using Company.DL.Contexts;
using Company.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comoany.Bl.Repositories
{
    public class DepartmentRepo : GenericRepo<Department>, IDepartmentRepo
    {
        public DepartmentRepo(CompanyContext companyContext):base(companyContext)
        {
            
        }
    }
}
