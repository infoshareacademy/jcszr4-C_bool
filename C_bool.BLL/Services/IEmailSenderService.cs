using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;

namespace C_bool.BLL.Services
{
    public interface IEmailSenderService
    {
        Task SendCheckPhotoEmail(UserGameTask userGameTask, Message message);
    }
}