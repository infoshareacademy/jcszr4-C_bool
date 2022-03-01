using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;

namespace C_bool.BLL.Services
{
    public interface IMessagingService
    {
        public Message GetMessageById(int id);
        public Message GetMessageByParentId(int id);
        public List<Message> GetUserMessages(int id);
        public void Add(Place place, Message message);
        public void Add(UserGameTask userGameTask, Message message);
        public void Add(User user, Message message);
        public void Add(GameTask gameTask, Message message);
        public Message GetParentMessage(Message message);
        public List<Message> GetRootMessages(Message message);
        public void MarkAsRead(Message message, bool isViewed);
        public void Update(Message message);
        public void Delete(Message message);
    }
}
