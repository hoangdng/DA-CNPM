using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SemanticWeb.Data;
using SemanticWeb.ViewModels;

namespace SemanticWeb.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context, UserManager<CustomUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Users
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            //var users = await _userManager.Users.ToListAsync();
            var users = from user in _userManager.Users select user;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(user => user.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(user => user.Name);
                    break;
                case "Date":
                    users = users.OrderBy(users => users.DateJoined);
                    break;
                case "date_desc":
                    users = users.OrderByDescending(users => users.DateJoined);
                    break;
                default:
                    users = users.OrderBy(user => user.Name);
                    break;
            }
            return View(await users.AsNoTracking().ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var currentUser = await _userManager.FindByIdAsync(id);
            var posts = await _context.Posts.Where(p => p.UserID == id).ToListAsync();
            UserViewModel userViewModel = new UserViewModel()
            {
                Username = currentUser.UserName,
                Phone = currentUser.PhoneNumber,
                Name = currentUser.Name,
                DateJoined = currentUser.DateJoined,
                Posts = posts
            };
            return View(userViewModel);
        }

        // GET: Users/Lock/5
        public async Task<ActionResult> LockUser(string id)
        {
            var userToLock = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
            await _userManager.SetLockoutEnabledAsync(userToLock, true);
            await _userManager.SetLockoutEndDateAsync(userToLock, DateTime.Today.AddYears(100));
            return View("Index", _userManager.Users.ToList());
        }
        public async Task<ActionResult> UnlockUser(string id)
        {
            var userToLock = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
            await _userManager.SetLockoutEnabledAsync(userToLock, false);
            await _userManager.SetLockoutEndDateAsync(userToLock, DateTime.Now);
            return View("Index", _userManager.Users.ToList());
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToDelete = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            return View(userToDelete);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(userToDelete);
            return RedirectToAction("Index");
        }

    }
}