using System.Collections.Generic;
using C_bool.BLL.Models.Places;

namespace C_bool.BLL.Repositories
{
    public interface IRepository<T>
    {
        public void Add(T row);
        public void Delete(T row);
        public void Delete(string id);
        public void Update(T oldRow, T newRow);
        public T SearchById(string searchId);

        List<T> GetAll();
    }
}
