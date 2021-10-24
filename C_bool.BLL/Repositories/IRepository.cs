using System.Collections.Generic;

namespace C_bool.BLL.Repositories
{
    public interface IRepository<T>
    {
        public void Add(T row);
        public void Delete(T row);
        public void Update(T oldRow, T newRow);
        public T SearchById(string searchId);
        public List<T> SearchByName(string searchName);

    }
}
