using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetWeb.Data;
using PetWeb.Models;

namespace PetWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.ToListAsync());
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public PartialViewResult FilterIndex(IFormCollection collection)
        {
            var filteredPosts = _context.Posts.Select(p => p);
            //Filtered by posted date
            var duration = collection.ContainsKey("duration") == false ? "all" : collection["duration"][0];
            switch (duration)
            {
                case "today":
                    filteredPosts = filteredPosts.Where(p => p.PostedDate.Date == DateTime.Now.Date);
                    break;
                case "3days":
                    filteredPosts = filteredPosts.Where(p => p.PostedDate.Date.AddDays(3) >= DateTime.Now.Date);
                    break;
                case "week":
                    filteredPosts = filteredPosts.Where(p => p.PostedDate.Date.AddDays(7) >= DateTime.Now.Date);
                    break;
                default:
                    break;
            }

            //Filter by category
            var category = collection["category"].ToArray();
            if (category.Length != 0)
                filteredPosts = filteredPosts.Where(p => category.Contains(p.Category.Name));

            //Filter by animal
            var animal = collection["animal"].ToArray();
            if (animal.Length != 0)
                filteredPosts = filteredPosts.Where(p => animal.Contains(p.Animal.Name));

            //Filter by area
            var area = collection.ContainsKey("area") == false ? "all" : collection["area"][0];
            if (area != "all")
                filteredPosts = filteredPosts.Where(p => p.City.Name == area);


            return PartialView("NewsFeedPartial", filteredPosts.ToList());
        
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
