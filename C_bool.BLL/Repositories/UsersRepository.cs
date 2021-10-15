using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using C_bool.BLL.Models.Users;


namespace C_bool.BLL.Repositories
{
    public sealed class UsersRepository : BaseRepository<User>
    {
        public override List<User> Repository { get; protected set; }
        public override string FileName { get; } = "users.json";

        public UsersRepository()
        {
            Repository = new List<User>();
        }

        public override User SearchById(string searchId)
        {
            return (from user in Repository let id = searchId where user.Id == id select user).First();
        }

        public override List<User> SearchByName(string searchName)
        {
            return (from user in Repository
                let name = searchName
                where user.LastName == name || user.FirstName == name
                select user).ToList();
        }
    }
}