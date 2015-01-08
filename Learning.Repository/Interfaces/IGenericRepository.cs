using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Data
{
    public interface IGenericRepository<T> where T: class
    {
        IQueryable<T> GetAll();
        T GetById(object Id);
        void Create(T t);
        void Update(T t);
        void Delete(T t);
        void Delete(int Id);
        
    }
}
