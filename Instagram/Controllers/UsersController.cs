using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Instagram.Data;
using Instagram.Models;

namespace Instagram.Controllers
{
    public class UsersController : Controller
    {
        private readonly InstagramContext _context;

        public UsersController(InstagramContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string searchString, int? page, string currentFilter)
        {
			//SORTING
			var instagramContext = from s in _context.Users
								   select s;
			instagramContext = instagramContext.OrderBy(s => s.Name + s.FamilyName);
			//SEARCHING
			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}
			ViewData["CurrentFilter"] = searchString;
			if (!String.IsNullOrEmpty(searchString))
			{
				searchString = searchString.ToUpper();
				instagramContext = instagramContext.Where(s => s.Username.ToUpper().Contains(searchString)
									   || s.Name.ToUpper().Contains(searchString)
									   || s.FamilyName.ToUpper().Contains(searchString));
			}
			int pageSize = 3;
			return View(await PaginatedList<User>.CreateAsync(instagramContext.AsNoTracking(), page ?? 1, pageSize));
			//return View(await instagramContext.AsNoTracking().ToListAsync());
		}

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Posts)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Name,FamilyName")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(user);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyUsername(string username)
        {
            if (!_context.VerifyUsername(username))
            {
                return Json($"Username {username.ToLowerInvariant()} is already in use.");
            }

            return Json(true);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var UserToUpdate = await _context.Users.SingleOrDefaultAsync(u => u.ID == id);
            if (await TryUpdateModelAsync<User>(
                UserToUpdate,
                "",
                u => u.Username, u => u.Password, u => u.Name, u => u.FamilyName))
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
            return View(UserToUpdate);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
