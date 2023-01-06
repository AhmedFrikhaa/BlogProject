using blog_project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace blog_project.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private BlogRepo _blogRepo;
        private UserRepo _userRepo;
        public BlogController(IConfiguration configuration)
        {
            _blogRepo = new BlogRepo(configuration);
            _userRepo = new UserRepo(configuration);
        }

        // GET: BlogController
        [Route("/index", Name = "index")]
        public ActionResult getBlogs()
        {
            return View(_blogRepo.GetAllBlogs());
        }
        
        





        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [Route("create")]
        [HttpPost]
        public ActionResult Create(CreateBlog blogCreate)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                string username =identity.FindFirst("userName").Value;
                User user = _userRepo.GetUser(username);
                Blog blog = new Blog
                {
                    title = blogCreate.title,
                    description = blogCreate.description,
                    image = blogCreate.image,
                    userId = user.id,
                };
                _blogRepo.AddBlog(blog);
            }
            return View();
        }






     
        [Route("/blog/{id}", Name = "getBlog")]
        // GET: BlogController/Details/5
        public ActionResult Details(int id)
        {
            return View(_blogRepo.getBlog(id));
        }






        [Route("/edit/{id}", Name= "edit")]
        // GET: BlogController/Edit/5
        public ActionResult Edit(int id)
        {
            Blog b = _blogRepo.getBlog(id);
            _blogRepo.updateBlog(b);
            return View(b);
        }

        // POST: BlogController/Edit/5
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

        [Route("/delete/{id}", Name = "delete")]
        // GET: BlogController/Delete/5
        public ActionResult Delete(int id)
        {
            Blog b = _blogRepo.getBlog(id);
            _blogRepo.deleteBlog(b);
            return View(b);
        }

        // POST: BlogController/Delete/5
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
        }
    }
}
