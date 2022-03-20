using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;
using FluentEmail.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace C_bool.BLL.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IFluentEmail _email;
        private readonly IRepository<User> _userRepository;

        public EmailSenderService(IFluentEmail email, IRepository<User> userRepository)
        {
            _email = email;
            _userRepository = userRepository;
        }

        public async Task SendCheckPhotoEmail(UserGameTask userGameTask, string title, string body)
        {
            var receiver = _userRepository.GetById(userGameTask.GameTask.CreatedById);
            var newEmail = _email
                .SetFrom("cbool.contact@gmail.com", "C_bool Team")
                .To(receiver.Email, receiver.UserName)
                .Subject(title)
                .Body(body, true);

            await newEmail.SendAsync();

        }
    }
}