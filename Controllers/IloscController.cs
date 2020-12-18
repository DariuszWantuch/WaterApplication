using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WaterApplication.Data;
using WaterApplication.Models;

namespace WaterApplication.Controllers
{
    public class IloscController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;  

        public IloscController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;          
        }

        // GET: Ilosc
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ilosc.Include(i => i.Aktywnosc).Include(i => i.User);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ilosc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilosc = await _context.Ilosc
                .Include(i => i.Aktywnosc)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilosc == null)
            {
                return NotFound();
            }

            return View(ilosc);
        }

        // GET: Ilosc/Create
        public IActionResult Create()
        {
            ViewData["AktywnoscId"] = new SelectList(_context.Aktywnosc, "Name", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Ilosc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ammount,UserId,AktywnoscId")] Ilosc ilosc)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ilosc.UserId = userId;
            if (ModelState.IsValid)
            {               
                _context.Add(ilosc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AktywnoscId"] = new SelectList(_context.Aktywnosc, "Id", "Id", ilosc.AktywnoscId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ilosc.UserId);
            return View(ilosc);
        }

        // GET: Ilosc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilosc = await _context.Ilosc.FindAsync(id);
            if (ilosc == null)
            {
                return NotFound();
            }
            ViewData["AktywnoscId"] = new SelectList(_context.Aktywnosc, "Id", "Id", ilosc.AktywnoscId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ilosc.UserId);
            return View(ilosc);
        }

        // POST: Ilosc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ammount,UserId,AktywnoscId")] Ilosc ilosc)
        {
            if (id != ilosc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilosc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IloscExists(ilosc.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AktywnoscId"] = new SelectList(_context.Aktywnosc, "Id", "Id", ilosc.AktywnoscId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ilosc.UserId);
            return View(ilosc);
        }

        // GET: Ilosc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilosc = await _context.Ilosc
                .Include(i => i.Aktywnosc)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilosc == null)
            {
                return NotFound();
            }

            return View(ilosc);
        }

        // POST: Ilosc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilosc = await _context.Ilosc.FindAsync(id);
            _context.Ilosc.Remove(ilosc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IloscExists(int id)
        {
            return _context.Ilosc.Any(e => e.Id == id);
        }
    }
}
