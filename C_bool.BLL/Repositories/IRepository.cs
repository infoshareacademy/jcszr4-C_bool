using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace C_bool.BLL.Repositories
{
    public interface IRepository<T> where T : class
    {
        public void AddFileDataToRepository();

        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Remove(T entity);
    }
}
