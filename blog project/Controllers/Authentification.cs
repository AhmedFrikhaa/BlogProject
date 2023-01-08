using blog_project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace blog_project.Controllers
{
    public class Authentification : Controller
    {

        private UserRepo userRepo;

        public Authentification(IConfiguration configuration)
        {
            userRepo = new UserRepo(configuration);
        }

        [Route("/login")]
        [HttpGet]
        public IActionResult login()
        {
            
            return View();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> loginAsync(LoginModel login)
        {   
            User user = userRepo.getUserByEmail(login.email);
            if (user == null)
            {
                ModelState.AddModelError("email", "There is not a user with this email");
                return View();
            }
            else
            {
                if(login.password != user.password)
                {
                    ModelState.AddModelError("password", "Please Verify Your Password");
                    return View();
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim("userName",user.userName )
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(2),
                    };
                    
                    await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);
                    return RedirectToAction("getBlogs", "blog");
                }
            }
            
        }

        [Route("/signup")]
        [HttpGet]
        public IActionResult signUp()
        {
            return View();
        }

        [Route("/signup")]
        [HttpPost]
        public IActionResult signUp(RegistrationModel registration)
        {
            User user = new User(registration.userName, registration.firstName, registration.lastName, registration.picture, registration.email, registration.password);
            userRepo.AddUser(user);
            return RedirectToAction("login");
        }

        public async Task<IActionResult> signOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login");
        }


    }
}
