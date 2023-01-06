using blog_project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog_project.Controllers
{
    
    
    public class UserController : Controller
    {
        private UserRepo _userRepo;
        // GET: UserController
        public UserController(IConfiguration configuration)
        {
            _userRepo = new UserRepo(configuration);
        }
        [Route("/user/getusers")]
        
        public ActionResult getUsers()
        {
            return View(_userRepo.GetAllUsers());
        }
        /*
        [Route("/user/{username}", Name = "getUser")]
        // GET: UserController/Details/5
        public ActionResult Details(string username)
        {
            return View(_userRepo.GetUser(username));
        }

        // GET: UserController/Create
        [Route("/create", Name = "create")]
        public ActionResult Create()
        {
            user u = new user();
            _userRepo.AddUser(u);
            return View(u);
        }

        // POST: UserController/Create
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

        [Route("/edit/{username}", Name = "edit")]
        // GET: UserController/Edit/5
        public ActionResult Edit(string username)
        {
            user u = _userRepo.GetUser(username);
            _userRepo.UpdateUser(u);
            return View(u);
        }

        // POST: UserController/Edit/5
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
        [Route("/delete/{username}", Name = "delete")]
        // GET: UserController/Delete/5
        public ActionResult Delete(string username)
        {
            user u = _userRepo.GetUser(username);
            _userRepo.DeleteUser(u);
            return View(u);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
