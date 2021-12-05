using System.Collections.Generic;
using C_bool.BLL.Enums;
using C_bool.BLL.Models;


namespace C_bool.BLL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> SearchByName(List<User> users, string name);
        List<User> SearchByEmail(List<User> users, string email);
        List<User> SearchByGender(List<User> users, Gender gender);
        List<User> SearchActive(List<User> users);
        List<User> OrderByPoints(List<User> users, bool isDescending);
        void AddFileDataToRepository();
        void AddUser(User user);
        public void AddPoints(User user);
    }
}