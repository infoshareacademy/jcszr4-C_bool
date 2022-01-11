using System.Collections.Generic;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using C_bool.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C_bool.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUserService _userService;
        private static List<User> _searchUser;
        private static SearchUsersModel _searchUsersModel;

        public UserController(IRepository<User> userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        // GET: UserController
        [Authorize]
        public ActionResult Index()
        {
            var model = _userRepository.GetAll();
            
            return View(model);
        }

        // GET: UserController/SearchUsers
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

        // POST: UserController/SearchUsers
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
                    return RedirectToAction(nameof(Index));
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

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            var model = _userRepository.GetById(id);
            return View(model);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _userService.AddUser(model);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _userRepository.GetById(id);
            return View(model);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _userRepository.Update(model);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _userRepository.GetById(id);
            return View(model);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User model)
        {
            
            try
            {
                model = _userRepository.GetById(model.Id);
                _userRepository.Delete(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
