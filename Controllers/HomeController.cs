using System;
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

        public async Task<IActionResult> ReloadIndex(IFormCollection collection)
        {
            var posts = _context.Posts.Select(p => p);
            switch (collection.ToArray()[0].Value.ToString())
            {
                case "today":
                    posts = posts.Where(p => p.PostedDate.Date == DateTime.Now.Date);
                    break;
                case "3days":
                    posts = posts.Where(p => p.PostedDate.Date.AddDays(3) >= DateTime.Now.Date);
                    break;
                case "week":
                    posts = posts.Where(p => p.PostedDate.Date.AddDays(7) >= DateTime.Now.Date);
                    break;
                default:
                    break;
            }
            return View("Index", await posts.ToListAsync());
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> Subscribe()
        {
            string email = HttpContext.Request.Form["sub_email"];
            if (!_context.Subscribers.Any(s => s.Email == email))
            {
                Subscriber s = new Subscriber
                {
                    Email = email
                };
                _context.Subscribers.Add(s);
                await _context.SaveChangesAsync();
                return View();
            }
            else
            {
                return View();
            }
        }

    
        public async Task<IActionResult> Unsubscribe()
        {
            string mailid = HttpContext.Request.Query["mailid"].ToString();
            var mail = await _context.Subscribers.FirstOrDefaultAsync(m => m.Email == mailid);
            _context.Subscribers.Remove(mail);
            await _context.SaveChangesAsync();
            return View();
        }


    }
        
}
