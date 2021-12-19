using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace C_bool.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: UserController
        public ActionResult Index()
        {
            var model = _userRepository.GetAll();
            return View(model);
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

            _userRepository.AddUser(model);

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
