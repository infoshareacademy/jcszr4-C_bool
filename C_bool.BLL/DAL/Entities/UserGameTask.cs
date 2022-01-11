using System;

namespace C_bool.BLL.DAL.Entities
{
    public class UserGameTask : Entity
    {
        public int UserId { get; set; }
        public int GameTaskId { get; set; }
        public virtual User User { get; set; }
        public virtual GameTask GameTask { get; set; }
        public bool IsDone { get; set; }
        public DateTime DoneOn { get; set; }

        public UserGameTask()
        {
            
        }

        public UserGameTask(User user, GameTask gameTask)
        {
            User = user;
            GameTask = gameTask;
            IsDone = false;
        }

    }
}