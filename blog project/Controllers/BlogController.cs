using blog_project.Models;
using blog_project.Models.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
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






     
        [Route("Details/{id}", Name = "getBlog")]
        // GET: BlogController/Details/5
        public ActionResult Details(int id)
        {
            Blog blog = _blogRepo.getBlog(id);
            return View(blog);
        }






        [Route("edit/{id}", Name = "edit")]
        // GET: BlogController/Edit/5
        public ActionResult Edit(int id)
        {
            Blog b = _blogRepo.getBlog(id);
            EditModelBlog b1 = new EditModelBlog
            {
                Id = b.Id,
                description = b.description,
                title = b.title,
                image = b.image,
                theme = b.theme,
            };
            return View(b1);

        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [Route("edit/{id}")]
        public ActionResult Edit(EditModelBlog emb)
        {
            Blog b = _blogRepo.getBlog(@emb.Id);
            b.title = emb.title;
            b.description = emb.description;
            b.theme = emb.theme;
            b.image = emb.image;
            _blogRepo.updateBlog(b);
            return RedirectToAction("getBlogs");
        }

        [Route("delete/{id}", Name = "delete")]
        // GET: BlogController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog b = _blogRepo.getBlog(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }
        private class HttpStatusCodeResult : ActionResult
        {
            private HttpStatusCode badRequest;

            public HttpStatusCodeResult(HttpStatusCode badRequest)
            {
                this.badRequest = badRequest;
            }
        }

        // POST: BlogController/Delete/5
        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            {
                Blog b = _blogRepo.getBlog(id);
                _blogRepo.deleteBlog(b);
                return RedirectToAction("account", "user");

            }
        }
    }
}