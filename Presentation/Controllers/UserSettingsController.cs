using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class UserSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserSettings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserSettings.Include(u => u.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSetting = await _context.UserSettings
                .Include(u => u.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSetting == null)
            {
                return NotFound();
            }

            return View(userSetting);
        }

        // GET: UserSettings/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedAt,UpdatedAt,TimeZone,ApplicationUserId")] UserSetting userSetting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", userSetting.ApplicationUserId);
            return View(userSetting);
        }

        // GET: UserSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSetting = await _context.UserSettings.FindAsync(id);
            if (userSetting == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", userSetting.ApplicationUserId);
            return View(userSetting);
        }

        // POST: UserSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedAt,UpdatedAt,TimeZone,ApplicationUserId")] UserSetting userSetting)
        {
            if (id != userSetting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSettingExists(userSetting.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", userSetting.ApplicationUserId);
            return View(userSetting);
        }

        // GET: UserSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSetting = await _context.UserSettings
                .Include(u => u.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userSetting == null)
            {
                return NotFound();
            }

            return View(userSetting);
        }

        // POST: UserSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userSetting = await _context.UserSettings.FindAsync(id);
            _context.UserSettings.Remove(userSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSettingExists(int id)
        {
            return _context.UserSettings.Any(e => e.Id == id);
        }
    }
}
