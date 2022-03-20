using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using C_bool.WebApp.Models;
using C_bool.WebApp.Models.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C_bool.WebApp.Controllers
{
    [Authorize(Roles = "Admin, Moderator")]
    public class AdminPanelController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUserService _userService;
        private readonly IReportService _reportService;
        private static List<User> _searchUser;
        private static SearchUsersModel _searchUsersModel;

        private readonly UserManager<User> _userManager;

        public AdminPanelController(
            IRepository<User> userRepository,
            IUserService userService,
            UserManager<User> userManager, IReportService reportService1)
        {
            _userRepository = userRepository;
            _userService = userService;
            _userManager = userManager;
            _reportService = reportService1;
        }

        public ActionResult UsersList()
        {
            var model = _userRepository.GetAll();

            return View(model);
        }

        public ActionResult SearchUsers()
        {
            var model = _searchUser;

            ViewBag.Name = _searchUsersModel.Name;
            ViewBag.Email = _searchUsersModel.Email;
            ViewBag.Gender = _searchUsersModel.Gender;
            ViewBag.IsActive = _searchUsersModel.IsActive;
            ViewBag.IsDescending = _searchUsersModel.IsDescending;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchUsers(string name, string email, string gender, string isActive, string isDescending)
        {
            _searchUser = _userService.SearchUsers(name, email, gender, isActive, isDescending);

            _searchUsersModel = new SearchUsersModel(name, email, gender, isActive, isDescending);

            try
            {
                if (name == null && email == null && gender == null && isActive == null && isDescending == null)
                {
                    return RedirectToAction(nameof(UsersList));
                }
                else
                {
                    return RedirectToAction(nameof(SearchUsers));
                }

            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Reports(string dateFrom, string dateTo, int limit)
        {
            ViewData["dateFrom"] = dateFrom;
            ViewData["dateTo"] = dateTo;
            var model = new ReportViewModel();
            DateTime dateFromParsed = DateTime.TryParse(dateFrom, out dateFromParsed) ? dateFromParsed : DateTime.MinValue;
            DateTime dateToParsed = DateTime.TryParse(dateTo, out dateToParsed) ? dateToParsed : DateTime.MaxValue;
            limit = 5;
            model.ActiveUsersCount = await _reportService.GetActiveUsersCount();
            model.GameTaskPointsAverage = await _reportService.GetGameTaskPointsAverage();
            model.GameTaskTypeClassification =
                await _reportService.GetGameTaskTypeClassification(dateFromParsed, dateToParsed, limit);
            model.PlaceByGameTasksClassification =
                await _reportService.GetPlaceByGameTasksClassification(dateFromParsed, dateToParsed, limit);
            model.PlaceWithoutGameTasksCount = await _reportService.GetPlacesWithoutGameTasksCount();
            model.UserGameTaskByUsersClassification =
                await _reportService.GetUserGameTaskByUsersClassification(dateFromParsed, dateToParsed, limit);
            model.UserGameTaskDoneCount = await _reportService.GetUserGameTaskDoneCount();
            model.UserGameTaskDoneTimeAverage = await _reportService.GetUserGameTaskDoneTimeAverage();
            model.UserGameTaskMostActiveUsersClassification =
                await _reportService.GetUserGameTaskMostActiveUsersClassification(dateFromParsed, dateToParsed, limit);

            return View(model);
        }

        public ActionResult UserDetails(int id)
        {
            var model = _userRepository.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeUserStatus([FromRoute] int id, [FromBody] UserStatusModel newStatus)
        {

            var parsedNewStatus = bool.Parse(newStatus.NewStatus);
            _userService.ChangeUserStatus(id, parsedNewStatus);

            if (!parsedNewStatus)
            {
                return Json(new
                {
                    success = true,
                    responseText = "Użytkownik został zablokowany"
                });
            }

            return Json(new
            {
                success = true,
                responseText = "Użytkownik został aktywowany"
            });
        }
    }
}