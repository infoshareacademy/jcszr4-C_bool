using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;
using C_bool.BLL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace C_bool.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserPlace> _userPlaceRepository;
        private readonly IRepository<UserGameTask> _userGameTaskRepository;
        private readonly IRepository<Place> _placeRepository;
        private readonly IRepository<GameTask> _gameTaskRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public UserService(IRepository<User> userRepository, 
            
            IRepository<UserPlace> userPlaceRepository, 
            IRepository<UserGameTask> userGameTaskRepository ,
            IRepository<Place> placeRepository,
            IRepository<GameTask> gameTaskRepository,
            IHttpContextAccessor httpContextAccessor, 
            UserManager<User> userManager)
        { 
            _userRepository = userRepository;
            _userGameTaskRepository = userGameTaskRepository;
            _userPlaceRepository = userPlaceRepository;
            _placeRepository = placeRepository;
            _gameTaskRepository = gameTaskRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public IQueryable<User> GetAllQueryable()
        {
            return _userRepository.GetAllQueryable();
        }

        public int GetCurrentUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public User GetCurrentUser()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
           
            return _userRepository.GetById(userId);
        }

        public async Task<List<string>> GetUserRoles()
        {
            var user = await _userManager.FindByIdAsync(GetCurrentUserId().ToString());
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<List<string>> GetUserRoles(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public int GetRankingPlace()
        {
            var currentUser = GetCurrentUser();
            var users = _userRepository.GetAllQueryable();

            var usersOrderedByPoints = users.OrderByDescending(e => e.Points).Select(e => e.Id).ToList();

            return usersOrderedByPoints.IndexOf(currentUser.Id) + 1;
        }

        public void ChangeUserStatus(int userId)
        {
            var user = _userRepository.GetById(userId);

            user.IsActive = !user.IsActive;

            _userRepository.Update(user);
        }

        public bool AddFavPlace(Place place)
        {
            var userFavPlaces = GetFavPlaces();
            var currentUser = GetCurrentUser();
            if (userFavPlaces.Any(x => x.Id.Equals(place.Id))) return false;
            currentUser.FavPlaces.Add(new UserPlace(currentUser, place));
            _userRepository.Update(currentUser);
            return true;
        }

        public bool RemoveFavPlace(Place place)
        {
            var userFavPlaces = GetFavPlaces();
            var currentUser = GetCurrentUser();
            if (userFavPlaces.Any(x => x.Id.Equals(place.Id)))
            {
                var userplace = _userPlaceRepository.GetAllQueryable().SingleOrDefault(x => x.PlaceId.Equals(place.Id));
                _userPlaceRepository.Delete(userplace);
                return true;
            }
            return false;
        }
        
        public List<Place> GetFavPlaces()
        {
            var usersPlaces = _userPlaceRepository.GetAllQueryable();
            var currentUserId = GetCurrentUserId();

            return usersPlaces.Where(up => up.UserId == currentUserId).Select(up => up.Place).ToList();
        }

        public List<Place> GetPlacesCreatedByUser()
        {
            var userPlaces = _placeRepository.GetAllQueryable();
            var currentUserId = GetCurrentUserId();

            return userPlaces.Where(e => e.CreatedById == currentUserId).ToList();
        }

        public bool AddTaskToUser(GameTask gameTask)
        {
            var currentUser = GetCurrentUser();
            var userGameTasks = GetAllTasks();
            if (userGameTasks.Any(x => x.Id.Equals(gameTask.Id))) return false;
            currentUser.UserGameTasks.Add(new UserGameTask(currentUser, gameTask));
            _userRepository.Update(currentUser);
            return true;
        }

        public List<GameTask> GetAllTasks()
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();
            var currentUserId = GetCurrentUserId();

            return usersGameTasks.Where(ugt => ugt.UserId == currentUserId)
                .Select(ugt => ugt.GameTask).ToList();
        }

        public List<GameTask> GetToDoTasks()
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();
            var currentUserId = GetCurrentUserId();

            return usersGameTasks.Where(ugt => ugt.UserId == currentUserId && !ugt.IsDone)
                .Select(ugt => ugt.GameTask)
                .Where(gt => gt.ValidFrom >= DateTime.UtcNow && gt.IsActive).ToList();
        }

        public List<GameTask> GetToDoTasks(int userId)
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();

            return usersGameTasks.Where(ugt => ugt.UserId == userId && !ugt.IsDone)
                .Select(ugt => ugt.GameTask)
                .Where(gt => gt.ValidFrom >= DateTime.UtcNow && gt.IsActive).ToList();
        }

        public List<GameTask> GetInProgressTasks()
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();
            var currentUserId = GetCurrentUserId();

            return usersGameTasks.Where(ugt => ugt.UserId == currentUserId && !ugt.IsDone)
                .Select(ugt => ugt.GameTask)
                .Where(gt => gt.ValidFrom <= DateTime.UtcNow && gt.ValidThru >= DateTime.UtcNow && gt.IsActive).ToList();
        }

        public List<GameTask> GetInProgressTasks(int userId)
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();

            return usersGameTasks.Where(ugt => ugt.UserId == userId && !ugt.IsDone)
                .Select(ugt => ugt.GameTask)
                .Where(gt => gt.ValidFrom <= DateTime.UtcNow || gt.ValidFrom == DateTime.MinValue)
                .Where(gt => gt.ValidThru >= DateTime.UtcNow || gt.ValidFrom == DateTime.MinValue)
                .Where(gt => gt.IsActive)
                .ToList();
        }

        public List<GameTask> GetDoneTasks()
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();
            var currentUserId = GetCurrentUserId();

            return usersGameTasks.Where(ugt => ugt.UserId == currentUserId && ugt.IsDone).Select(ugt => ugt.GameTask).ToList();
        }

        public List<GameTask> GetDoneTasks(int userId)
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();

            return usersGameTasks.Where(ugt => ugt.UserId == userId && ugt.IsDone).Select(ugt => ugt.GameTask).ToList();
        }

        public List<GameTask> GetTasksToAccept()
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();
            var currentUserId = GetCurrentUserId();

            return usersGameTasks.Where(ugt => ugt.Photo != null && !ugt.IsDone && ugt.GameTask.CreatedById == currentUserId)
                .Select(ugt => ugt.GameTask).ToList();
        }

        public List<GameTask> GetTasksToAccept(int userId)
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();

            return usersGameTasks.Where(ugt => ugt.Photo != null && !ugt.IsDone && ugt.GameTask.CreatedById == userId)
                .Select(ugt => ugt.GameTask).ToList();
        }

        public List<GameTask> GetGameTasksCreatedByUser()
        {
            var userGameTasks = _gameTaskRepository.GetAllQueryable();
            var currentUserId = GetCurrentUserId();

            return userGameTasks.Where(e => e.CreatedById == currentUserId).ToList();
        }

        public List<User> SearchByName(string name)
        {
            var users = _userRepository.GetAllQueryable();
            
            return users.Where(u => u.UserName.Equals(name)).ToList();
        }

        public List<User> SearchByEmail(string email)
        {
            var users = _userRepository.GetAllQueryable();
            
            return users.Where(u => u.Email.Equals(email)).ToList();
        }

        public List<User> SearchByGender(Gender gender)
        {
            var users = _userRepository.GetAllQueryable();
            
            return users.Where(u => u.Gender == gender).ToList();
        }

        public List<User> SearchActive(bool isActive)
        {
            var users = _userRepository.GetAllQueryable();
            
            return users.Where(u => u.IsActive == isActive).ToList();
        }

        public List<User> OrderByPoints(bool isDescending)
        {
            var users = _userRepository.GetAllQueryable();
            
            return isDescending ? users.OrderByDescending(u => u.Points).ToList() : users.OrderBy(u => u.Points).ToList();
        }

        public List<User> SearchUsers(string name, string email, string gender, string isActive, string isDescending)
        {
            var users = _userRepository.GetAllQueryable();

            if (name != null)
            {
                users = users.Where(u => u.UserName.Contains(name));
            }
            if (email != null)
            {
                users = users.Where(u => u.Email.Contains(email));
            }
            if (gender != null)
            {
                users = users.Where(u => u.Gender == Enum.Parse<Gender>(gender));
            }
            if (isActive != null)
            {
                users = users.Where(u => u.IsActive == bool.Parse(isActive));
            }
            if (isDescending != null)
            {
                users = bool.Parse(isDescending) ? users.OrderByDescending(u => u.Points) : users.OrderBy(u => u.Points);
            }

            return users.ToList();
        }

        public bool PostMessage(int userId, Message message)
        {
            var user = _userRepository.GetAllQueryable().SingleOrDefault(x => x.Id == userId);

            if (user == null) return false;
            user.Messages.Add(message);
            _userRepository.Update(user);

            return true;
        }
    }
}