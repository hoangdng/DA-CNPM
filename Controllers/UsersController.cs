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
using PetWeb.Data;

namespace PetWeb.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;

        public UsersController(UserManager<CustomUser> userManager)
        {
            _userManager = userManager;
        }
        // GET: Users
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {

            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
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