using System.Collections.Generic;

namespace C_bool.BLL.Repositories
{
    public interface IRepository<T>
    {
        public void Add(T row);
        public void AddRange(List<T> rows);
        public void Delete(T row);
        public void Update(T row);
        public T SearchById(string searchId);
        public List<T> GetAll();

    }
}
