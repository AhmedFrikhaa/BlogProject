using blog_project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace blog_project.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private blogRepo _blogRepo;
        public BlogController(IConfiguration configuration)
        {
            _blogRepo = new blogRepo(configuration);
        }

        // GET: BlogController
        [Route("/index", Name = "index")]
        public ActionResult getBlogs()
        {
            return View(_blogRepo.GetAllBlogs());
        }
        [Route("/blog/{id}", Name = "getBlog")]
        // GET: BlogController/Details/5
        public ActionResult Details(int id)
        {
            return View(_blogRepo.getBlog(id));
        }
        [Route("/create", Name = "create")]
        // GET: BlogController/Create
        public ActionResult Create()
        {
            blog b = new blog();
            _blogRepo.AddBlog(b);
            return View(b);
        }

        // POST: BlogController/Create
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

        [Route("/edit/{id}", Name= "edit")]
        // GET: BlogController/Edit/5
        public ActionResult Edit(int id)
        {
            blog b = _blogRepo.getBlog(id);
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
            blog b = _blogRepo.getBlog(id);
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
