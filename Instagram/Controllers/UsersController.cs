using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data;
using Instagram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Controllers
{
    public class UsersController : Controller
    {
        private readonly InstagramContext _context;

        public UsersController(InstagramContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Users
        public async Task<IActionResult> Index(string searchString, int? page, string currentFilter)
        {
            //SORTING
            var instagramContext = from s in _context.MyUsers
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
                instagramContext = instagramContext.Where(s => s.UserName.ToUpper().Contains(searchString)
                                       || s.Name.ToUpper().Contains(searchString)
                                       || s.FamilyName.ToUpper().Contains(searchString));
            }
            int pageSize = 3;
            return View(await PaginatedList<User>.CreateAsync(instagramContext.AsNoTracking(), page ?? 1, pageSize));
        }
        
        [Authorize]
        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.MyUsers
                .Include(u => u.Posts)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}