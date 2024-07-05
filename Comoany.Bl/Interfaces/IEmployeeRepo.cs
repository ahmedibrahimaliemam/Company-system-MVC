using Company.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comoany.Bl.Interfaces
{
    public interface IEmployeeRepo:IgenericRepo<Employee>
    {
        public IQueryable<Employee> SearchByName(string name);

    }
}
