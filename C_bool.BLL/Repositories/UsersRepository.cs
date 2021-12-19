using System;
using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;


namespace C_bool.BLL.Repositories
{
    public sealed class UsersRepository
    {
        private readonly IRepository<User> _repository;
        public string FileName { get; } = "users.json";

        public UsersRepository(IRepository<User> repository)
        {
            _repository = repository;
        }

        public List<User> SearchByName(string name)
        {
            var users = _repository.GetAllQueryable();
            return users.Where(u => u.Name.Equals(name)).ToList();
            //users.Where(u => u.Name.Equals(name)).ToList();
        }
        public List<User> SearchByEmail(List<User> users, string email) =>
            users.Where(u => u.Email.Equals(email)).ToList();

        public List<User> SearchByGender(List<User> users, Gender gender) => users.Where(u => u.Gender == gender).ToList();

        public List<User> SearchActive(List<User> users) => users.Where(u => u.IsActive == true).ToList();

        public List<User> OrderByPoints(List<User> users, bool isDescending) => isDescending
            ? users.OrderByDescending(u => u.Points).ToList()
            : users.OrderBy(u => u.Points).ToList();

        public void AddUser(User user)
        {
            //user.Id = Guid.NewGuid().ToString().Replace("-", "");
            user.CreatedOn = DateTime.Now;
            _repository.Add(user);
        }

        //TODO should be implemented by sum of points from completed GameTasks list 
        public void AddPoints(User user)
        {
            throw new NotImplementedException();
        }
    }
}