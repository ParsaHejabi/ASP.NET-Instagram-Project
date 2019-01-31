using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Instagram.Data;
using Instagram.Models;
using System.IO;

namespace Instagram.Controllers
{
    public class PostsController : Controller
    {
        private readonly InstagramContext _context;

        public PostsController(InstagramContext context)
        {
            _context = context;
        }

        // GET: Posts
		public async Task<IActionResult> Index(int? page)
		{
			var instagramContext = from s in _context.Posts.Include(p => p.User)
								   select s;
			instagramContext = instagramContext.OrderByDescending(s => s.PostTime);
			int pageSize = 4;
			return View(await PaginatedList<Post>.CreateAsync(instagramContext.AsNoTracking(), page ?? 1, pageSize));
		}

		// GET: Posts/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Username");
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Caption,Image")] PostViewModel postViewModel)
        {
            Post post = null;
            try
            {
                if (postViewModel == null || postViewModel.Image == null || postViewModel.Image.Length == 0)
                {
                    return Content("Image is not selected!");
                }

                var ext = Path.GetExtension(postViewModel.Image.FileName).ToLowerInvariant();

                if (!_extensions.Keys.Contains(ext))
                {
                    return Content("File selected is not an image!");
                }

                var uniqueFileName = GetUniqueFileName(postViewModel.Image.FileName, postViewModel.UserID);
                var uploadFolder = Path.Combine(Path.GetTempPath(), "InstagramImages");
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await postViewModel.Image.CopyToAsync(stream);
                }

                post = new Post
                {
                    UserID = postViewModel.UserID,
                    Caption = postViewModel.Caption,
                    ImagePath = filePath
                };

                if (ModelState.IsValid)
                {
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["UserID"] = new SelectList(_context.Users, "ID", "Username", post.UserID);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(post);
        }

        private static readonly IDictionary<string, string> _extensions = new Dictionary<string, string>()
        {
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png", "image/png" }
        };

        private string GetUniqueFileName(string fileName, int UserID)
        {
            fileName = Path.GetFileName(fileName);
            return UserID.ToString() + Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 5)
                      + Path.GetExtension(fileName);
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
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var PostToUpdate = await _context.Posts.SingleOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Post>(
                PostToUpdate,
                "",
                s => s.Caption))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(PostToUpdate);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (System.IO.File.Exists(post.ImagePath))
            {
                System.IO.File.Delete(post.ImagePath);
            }

            try
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.ID == id);
        }
    }
}
