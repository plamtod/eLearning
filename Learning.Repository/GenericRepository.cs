using Learning.Domain.Entities;
using Learning.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class, IEntity
    {
        private readonly IDbContext _context;
        private readonly IDbSet<T> entities; 

        public GenericRepository(IDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }


        public virtual IQueryable<T> GetAll()
        {
            return entities;
        }

        public T GetById(object Id)
        {
            return entities.Find(Id);
        }

        public void Create(T t)
        {
            entities.Add(t);
        }

        public void Update(T t)
        {
            T entity = entities.Find(t.Id);
            entity = t;
        }

        public void Delete(T t)
        {
            entities.Remove(t);
        }

        public void Delete(int Id)
        {
            T entity = entities.Find(Id);
            entities.Remove(entity);
        }
    }
}
