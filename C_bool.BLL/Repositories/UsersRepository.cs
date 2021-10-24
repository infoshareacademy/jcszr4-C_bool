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
            return (from user in Repository
                where user.LastName.ToLower().Contains(searchName.ToLower()) || user.FirstName.ToLower().Contains(searchName.ToLower())
                select user).ToList();
        }
    }
}