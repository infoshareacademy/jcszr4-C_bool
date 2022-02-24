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
            //var model = (_userService.GetAllQueryable()
            //        .Where(x => x.Id == user.Id)
            //        .Select(x => x.Messages)
            //        .AsNoTracking()
            //        .SingleOrDefault() ?? new List<Message>())
            //    .OrderByDescending(x => x.CreatedOn)
            //    .ToList();

            var model = _messagingService.GetUserMessages(user.Id)
                .OrderByDescending(x => x.CreatedOn)
                .ToList();

            return View(model);
        }

        // GET: MessagingController/Details/5
        public ActionResult Details(int id)
        {
            var user = _userService.GetCurrentUser();
            var model = _userService.GetAllQueryable().Where(x => x.Id == user.Id).SelectMany(x => x.Messages).SingleOrDefault(x => x.Id == id);
            return View(model);
        }

        // GET: MessagingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessagingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MessagingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MessagingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // POST: MessagingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var message = _messagingService.GetMessageById(id);
            if (message == null) return Json(new { success = false, responseText = "Nie znaleziono wiadomości" });
            _messagingService.Delete(message);
            return Json(new { success = true, responseText = "Wiadomość usunięta" });
        }

        [Authorize]
        [HttpPost]
        public ActionResult MarkAsRead([FromBody] ReturnString request)
        {
            var message = _messagingService.GetMessageById(int.Parse(request.Id));
            if (message == null) return Json(new { success = false, responseText = "Nie znaleziono wiadomości"});
            if (message.IsViewed)
            {
                _messagingService.MarkAsRead(message, false);
                return Json(new { success = true, responseText = "Oznaczono jako nieprzeczytane"});
            }
            _messagingService.MarkAsRead(message, true);
            return Json(new { success = true, responseText = "Oznaczono jako przeczytane" });
        }
    }
}
