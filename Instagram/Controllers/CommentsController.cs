using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Instagram.Data;
using Instagram.Models;
using Microsoft.AspNetCore.Authorization;

namespace Instagram.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly InstagramContext _context;

        public CommentsController(InstagramContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var instagramContext = _context.Comments.Include(c => c.Post).Include(c => c.User);
            return View(await instagramContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Post)
                .Include(c => c.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID");
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,Content")] Comment comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(comment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", comment.PostID);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", comment.PostID);
            return View(comment);
        }

        // POST: Comments/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditComment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CommentToUpdate = await _context.Comments.SingleOrDefaultAsync(c => c.ID == id);
            if (await TryUpdateModelAsync<Comment>(
                CommentToUpdate,
                "",
                c => c.Content))
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
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", CommentToUpdate.PostID);
            return View(CommentToUpdate);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Post)
                .Include(c => c.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (comment == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (comment == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.ID == id);
        }
    }
}
