using blog_project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web.Helpers;

namespace blog_project.Controllers
{
    public class Authentification : Controller
    {

        private UserRepo userRepo;
        private IWebHostEnvironment env;
        public Authentification(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            userRepo = new UserRepo(configuration);
            env = hostEnvironment;
        }

        [Route("/login")]
        [HttpGet]
        public IActionResult login()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity!=null && identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
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
                if(!Crypto.VerifyHashedPassword(user.password,login.password))
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
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
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
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity!= null && identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            return View();
        }

        [Route("/signup")]
        [HttpPost]
        public IActionResult signUp(RegistrationModel registration)
        {

            var hash = Crypto.HashPassword(registration.password);
            User user = new User { 
                userName=registration.userName,
                firstName= registration.firstName,
                lastName= registration.lastName,
                email = registration.email,
                password = hash
            };

            if( registration.picture != null && registration.picture.Length > 0)
            {
                var uploadDir = @"media";
                var filename = Guid.NewGuid().ToString()+"-"+registration.picture.FileName;
                var path = Path.Combine(env.WebRootPath, uploadDir, filename);
                registration.picture.CopyToAsync(new FileStream(path, FileMode.Create));
                user.picture ="/"+uploadDir+"/"+filename ;
            }
            else
            {
                user.picture = "/media/profileplaceholder.png";
            }
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
