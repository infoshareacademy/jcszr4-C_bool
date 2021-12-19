using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.DAL.Entities;

namespace C_bool.BLL.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        public void Add(T entity);
        public void AddRange(IEnumerable<T> entities);
        public void Delete(T entity);
        public void Delete(int id);
        public void Update(T entity);
        public T GetById(int id);
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllQueryable();
    }
}
