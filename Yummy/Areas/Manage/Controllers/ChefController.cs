using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yummy.DAL;
using Yummy.Helpers;
using Yummy.Models;

namespace Yummy.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ChefController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public ChefController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var chefs = _context.Chefs.ToList();
            return View(chefs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Chef chef)
        {
            if (!ModelState.IsValid)
                return View();

            chef.Image = FileManager.Save(_env.WebRootPath,"uploads/chefs",chef.ImageFile);

            _context.Chefs.Add(chef);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
