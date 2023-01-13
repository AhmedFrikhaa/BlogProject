using blog_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace blog_project.Controllers
{
    [Authorize]
    [Route("blog")]
    public class BlogController : Controller
    {
        private BlogRepo _blogRepo;
        private UserRepo _userRepo;
        private IWebHostEnvironment env;
        public BlogController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _blogRepo = new BlogRepo(configuration);
            _userRepo = new UserRepo(configuration);
            env = environment;
        }

        // GET: BlogController
        [HttpGet]
        [Route("index", Name = "index")]
        public ActionResult getBlogs()
        {
            List<Blog> blogs = _blogRepo.GetAllBlogs();
            return View(blogs);
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
                string username =identity.FindFirst("userName").Value;
                User user = _userRepo.GetUser(username);
                Blog blog = new Blog
                {
                    title = blogCreate.title,
                    description = blogCreate.description,
                    userId = user.id,
                    date = DateTime.Now,
                    theme = blogCreate.theme
                };
                if (blogCreate.image != null && blogCreate.image.Length > 0)
                {
                    var uploadDir = @"media";
                    var filename = Guid.NewGuid().ToString() + "-" + blogCreate.image.FileName;
                    var path = Path.Combine(env.WebRootPath, uploadDir, filename);
                    blogCreate.image.CopyToAsync(new FileStream(path, FileMode.Create));
                    blog.image = "/" + uploadDir + "/" + filename;
                }
                _blogRepo.AddBlog(blog);
            }
            return RedirectToAction("account","user");
        }






     
        [Route("blog/{id}", Name = "getBlog")]
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
