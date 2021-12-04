using System;
using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.Models;
using C_bool.BLL.Models.User;


namespace C_bool.BLL.Repositories
{
    public sealed class UsersRepository : BaseRepository<User>, IUserRepository
    {
        public override string FileName { get; } = "users.json";

        public UsersRepository()
        {
            AddFileDataToRepository();
        }

        public List<User> SearchByFirstName(List<User> users, string firstName) => 
            users.Where(u => u.FirstName.ToLower().Contains(firstName.ToLower())).ToList();

        public List<User> SearchByLastName(List<User> users, string lastName) => 
            users.Where(u => u.LastName.ToLower().Contains(lastName.ToLower())).ToList();

        public List<User> SearchByEmail(List<User> users, string email) => 
            users.Where(u => u.Email.Equals(email)).ToList();

        public List<User> SearchByGender(List<User> users, Gender gender) => users.Where(u => u.Gender == gender).ToList();

        public List<User> SearchActive(List<User> users) => users.Where(u => u.IsActive == true).ToList();

        public List<User> OrderByPoints(List<User> users, bool isDescending) => isDescending
            ? users.OrderByDescending(u => u.Points).ToList()
            : users.OrderBy(u => u.Points).ToList();

        public void AddUser(User user)
        {
            user.Id = Guid.NewGuid().ToString().Replace("-", "");
            user.CreatedOn = DateTime.Now;
            Add(user);
        }

        //TODO should be implemented by sum of points from all completed GameTasks 
        private void AddPoints(string userId, int pointsToAdd)
        {
            var random = new Random();
            foreach (var user in Repository)
            {
                user.Points = random.Next(0, 1000);
            }
        }
    }
}