using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace C_bool.WebApp.Controllers
{
    [Authorize]
    public class MessagingController : Controller
    {
        private readonly ILogger<MessagingController> _logger;
        private readonly IUserService _userService;
        private readonly IMessagingService _messagingService;
        private readonly IMapper _mapper;

        public MessagingController(
            ILogger<MessagingController> logger,
            IMapper mapper,
            IUserService userService, IRepository<User> usersRepository, 
            IMessagingService messagingService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
            _messagingService = messagingService;
        }
        // GET: MessagingController
        public ActionResult Index()
        {
            var user = _userService.GetCurrentUser();
            var model = _messagingService.GetUserMessages(user.Id)
                .OrderByDescending(x => x.CreatedOn)
                .ToList();

            return View(model);
        }

        // GET: MessagingController/Details/5
        public ActionResult Details(int id)
        {
            var model = _messagingService.GetMessageById(id);
            _messagingService.MarkAsRead(model, true);
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var message = _messagingService.GetMessageById(id);
            if (message == null) RedirectToAction(nameof(Index));
            _messagingService.Delete(message);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult DeleteAll()
        {
            var user = _userService.GetCurrentUser();
            _messagingService.DeleteAll(user.Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult MarkAsRead(int id)
        {
            var message = _messagingService.GetMessageById(id);
            if (message == null) return RedirectToAction(nameof(Index));
            _messagingService.MarkAsRead(message, !message.IsViewed);
            return RedirectToAction(nameof(Index));
        }
    }
}
