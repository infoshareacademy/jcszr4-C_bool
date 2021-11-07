using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.Models;


namespace C_bool.BLL.Repositories
{
    public sealed class UsersRepository : BaseRepository<User>, IRepository<User>
    {
        public override List<User> Repository { get; protected set; }
        public override string FileName { get; } = "users.json";

        public UsersRepository()
        {
            Repository = new List<User>();
        }

        public List<User> SearchByName(string searchName)
        {
            var searchNameLowerCase = searchName.ToLower();
            return Repository.Where(user =>
                    user.LastName.ToLower().Contains(searchNameLowerCase) ||
                    user.LastName.ToLower().Contains(searchNameLowerCase)).Select(user => user).ToList();
        }

        public List<User> OrderByPoints(bool isDescending) => isDescending
            ? Repository.OrderByDescending(u => u.Points).ToList()
            : Repository.OrderBy(u => u.Points).ToList();
    }
}