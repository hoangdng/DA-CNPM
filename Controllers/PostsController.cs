﻿using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetWeb.Data;
using PetWeb.Models;
using PetWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace PetWeb.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CustomUser> _userManager;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<CustomUser> userManager)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _userManager = userManager;
        }

        // GET: Posts
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(string postedDate)
        {
            return View(await _context.Posts.Include(c => c.Category).ToListAsync());
        }

        // GET: Posts/History
        public async Task<IActionResult> ViewHistory()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _context.Posts.Where(post => post.UserID == currentUserId).ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            var animalType = _context.Animals.Where(animal => animal.AnimalId == post.AnimalId).FirstOrDefault();
            var categoryType = _context.Categories.Where(category => category.CategoryId == post.CategoryId).FirstOrDefault();
            var city = _context.Cities.Where(c => c.CityId == post.CityId).FirstOrDefault();
            post.City = city;
            post.Animal = animalType;
            post.Category = categoryType;
            var comments = await _context.Comments.Where(comment => comment.PostId == id).ToListAsync();

            CommentViewModel commentViewModel = new CommentViewModel()
            {
                Post = post,
                Comments = comments

            };

            return View(commentViewModel);
        }

        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,AnimalId,CategoryId,CityId,ImageURL")] PostViewModel vm)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            vm.UserID = currentUserId;
            string stringFileName = UploadFile(vm);
            var animalType = _context.Animals.Where(animal => animal.AnimalId == vm.AnimalId).FirstOrDefault();
            var categoryType = _context.Categories.Where(category => category.CategoryId == vm.CategoryId).FirstOrDefault();
            var city = _context.Cities.Where(c => c.CityId == vm.CityId).FirstOrDefault();
            var post = new Post()
            {
                Title = vm.Title,
                Description = vm.Description,
                PostedDate = vm.PostedDate,
                Status = vm.Status,
                UserID = vm.UserID,
                AnimalId = vm.AnimalId,
                Animal = animalType,
                CategoryId = vm.CategoryId,
                Category = categoryType,
                City = city,
                CityId = vm.CityId,
                ImageURL = stringFileName
            };

            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewHistory));
            }
            return View(vm);
        }

        private string UploadFile(PostViewModel vm)
        {
            string fileName = null;
            if (vm.ImageURL != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.ImageURL.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.ImageURL.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,AnimalId,CategoryId,CityId,Status,ImageURL")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var animalType = _context.Animals.Where(animal => animal.AnimalId == post.AnimalId).FirstOrDefault();
                    var categoryType = _context.Categories.Where(category => category.CategoryId == post.CategoryId).FirstOrDefault();
                    var city = _context.Cities.Where(c => c.CityId == post.CityId).FirstOrDefault();
                    post.City = city;
                    post.Animal = animalType;
                    post.Category = categoryType;
                    post.UserID = currentUserId;
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ViewHistory));
            }
            return View(post);
        }
        // GET: Posts/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewHistory));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        public PartialViewResult AddComment(IFormCollection collection)
        {
            int postId = Convert.ToInt32(collection["postId"][0]);
            var currentPost = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();
            string username = collection["userId"][0];
            Comment newComment = new Comment()
            {
                Content = collection["comment"][0],
                Username = username,
                PostId = currentPost.Id,
                Post = currentPost
            };
            _context.Add(newComment);
            _context.SaveChanges();

            CommentViewModel commentViewModel = new CommentViewModel()
            {
                Post = currentPost,
                Comments = currentPost.Comments

            };

            return PartialView("CommentPartial", commentViewModel);
            //return RedirectToAction("Details", new { id = postId });
        }

        public async Task<ActionResult> ViewSavedPost()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var postIds = await _context.UserPosts.Where(up => up.CustomUserId == currentUserId).Select(up => up.PostId).ToListAsync();
            return View(await _context.Posts.Where(post => postIds.Contains(post.Id)).ToListAsync());
        }


    }
}
