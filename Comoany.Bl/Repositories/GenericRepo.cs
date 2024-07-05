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
    public class GenericRepo<T>:IgenericRepo<T> where T : class
    {
        private readonly CompanyContext DbContext;
        public GenericRepo(CompanyContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public async Task Add(T item)
        {
           await DbContext.Set<T>().AddAsync(item);
            
        }

        public void Delete(T item)
        {
            DbContext.Set<T>().Remove(item);
            
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T)==typeof(Employee))
            {
                return (IEnumerable<T>) await DbContext.Set<Employee>().Include(e=>e.Department).ToListAsync();
            }
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public void Update(T department)
        {
            DbContext.Update(department);
            
        }
    }
}
