using blog_project.Models;
using blog_project.Models.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace blog_project.Controllers
{
    [Authorize]
    [Route("user")]
    public class UserController : Controller
    {
        private UserRepo _userRepo;
        private BlogRepo _blogRepo;
        // GET: UserController
        public UserController(IConfiguration configuration)
        {
            _userRepo = new UserRepo(configuration);
            _blogRepo = new BlogRepo(configuration);
        }
        
        [Route("account")]
        public ActionResult profilePage()
        {
            User _user = fetchUser();
            EditUserModel profile = new EditUserModel
            {
                userName = _user.userName,
                firstName = _user.firstName,
                lastName = _user.lastName,
                picture = _user.picture,
                email = _user.email,
            }; 
            List<Blog> userBlogs = _blogRepo.getUserBlogs(_user.id);
            ViewData["userBlogs"]=userBlogs;
            return View(profile);
        }

        [Route("account/edit")]
        [HttpGet]
        public ActionResult editProfile()
        {
            User _user = fetchUser();
            EditUserModel profile = new EditUserModel
            {
                userName = _user.userName,
                firstName = _user.firstName,
                lastName = _user.lastName,
                email = _user.email,
            };
            return View(profile);
        }


        [Route("account/edit")]
        [HttpPost]
        public ActionResult editProfile(EditUserModel editModel)
        {
            User user = fetchUser();
            user.userName = editModel.userName;
            user.firstName = editModel.firstName;
            user.lastName = editModel.lastName;
            user.email = editModel.email;
            _userRepo.UpdateUser(user);
            return RedirectToAction("account","user");
        }

        private User fetchUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string username = identity.FindFirst("userName").Value;
            User user = _userRepo.GetUser(username);
            return user;
        }
    }
}
