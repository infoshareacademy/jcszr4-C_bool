using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace C_bool.BLL.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly IRepository<Message> _messagesRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<GameTask> _gameTasksRepository;
        private readonly IRepository<Place> _placesRepository;
        private readonly IRepository<UserGameTask> _userGameTasksRepository;

        public MessagingService(IRepository<User> usersRepository, IRepository<GameTask> gameTasksRepository, IRepository<Place> placesRepository, IRepository<UserGameTask> userGameTasksRepository, IRepository<Message> messagesRepository)
        {
            _usersRepository = usersRepository;
            _gameTasksRepository = gameTasksRepository;
            _placesRepository = placesRepository;
            _userGameTasksRepository = userGameTasksRepository;
            _messagesRepository = messagesRepository;
        }

        public Message GetMessageById(int id)
        {
            return _messagesRepository.GetAllQueryable().SingleOrDefault(x => x.Id == id);
        }

        public Message GetMessageByParentId(int id)
        {
            return _messagesRepository.GetAllQueryable().SingleOrDefault(x => x.ParentId == id);
        }

        public List<Message> GetUserMessages(int id)
        {
            return _usersRepository.GetAllQueryable().Where(x => x.Id == id)
                    .SelectMany(x => x.Messages).ToList();
        }

        public void Add(Place place, Message message)
        {
            place.Messages.Add(message);
            _placesRepository.Update(place);
        }

        public void Add(UserGameTask userGameTask, Message message)
        {
            userGameTask.Messages.Add(message);
            _userGameTasksRepository.Update(userGameTask);
        }

        public void Add(User user, Message message)
        {
            user.Messages.Add(message);
            _usersRepository.Update(user);
        }

        public void Add(GameTask gameTask, Message message)
        {
            gameTask.Messages.Add(message);
            _gameTasksRepository.Update(gameTask);
        }

        public Message GetParentMessage(Message message)
        {

            return _messagesRepository.GetAllQueryable().SingleOrDefault(x => x.ParentId == message.Id);
        }

        public List<Message> GetRootMessages(Message message)
        {
            return _messagesRepository.GetAllQueryable().Where(x => x.RootId == message.Id).ToList();
        }

        public void MarkAsRead(Message message, bool isViewed)
        {
            message.IsViewed = isViewed;
            _messagesRepository.Update(message);
        }

        public void MarkAllAsRead(int userId)
        {
            var messages = GetUserMessages(userId);
            foreach (var message in messages)
            {
                message.IsViewed = true;
                _messagesRepository.Update(message);
            }
        }

        public void Update(Message message)
        {
            _messagesRepository.Update(message);
        }

        public void Delete(Message message)
        {
            _messagesRepository.Delete(message);
        }

        public void DeleteAll(int userId)
        {
            var messages = GetUserMessages(userId);
            foreach (var message in messages)
            {
                _messagesRepository.Delete(message);
            }
        }
    }
}
