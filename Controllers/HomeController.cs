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
using MailKit.Net.Smtp;
using MimeKit;

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
            var posts =  _context.Posts.Select(p => p);
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

        public IActionResult SendEmail()
        {
            string toEmail = HttpContext.Request.Form["sub_email"];
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("huutaibk", "huutaibkdn@gmail.com"));
            message.To.Add(new MailboxAddress("user", toEmail));
            message.Subject = "Subject";

            message.Body = new TextPart("plain")
            {
                Text = "Hello World"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {

                client.Connect("smtp.gmail.com", 465, true);

                //SMTP server authentication if needed
                client.Authenticate("huutaibkdn@gmail.com", "huutai123456");

                client.Send(message);

                client.Disconnect(true);
            }
            return View();
        }


    }
}
