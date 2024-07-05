using Company.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comoany.Bl.Interfaces
{
    public  interface IgenericRepo<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task Add(T item);
        public Task<T> GetById(int id);
        public void Update(T item);
        public void Delete(T item);
    }
}
