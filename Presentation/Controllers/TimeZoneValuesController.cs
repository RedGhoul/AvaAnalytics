using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Presentation.Models;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TimeZoneValuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeZoneValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TimeZoneValues
        public async Task<IActionResult> Index()
        {
            return View(await _context.TimeZoneValues.ToListAsync());
        }

        // GET: TimeZoneValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeZoneValue = await _context.TimeZoneValues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeZoneValue == null)
            {
                return NotFound();
            }

            return View(timeZoneValue);
        }

        // GET: TimeZoneValues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TimeZoneValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text")] TimeZoneValue timeZoneValue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeZoneValue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeZoneValue);
        }

        // GET: TimeZoneValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeZoneValue = await _context.TimeZoneValues.FindAsync(id);
            if (timeZoneValue == null)
            {
                return NotFound();
            }
            return View(timeZoneValue);
        }

        // POST: TimeZoneValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text")] TimeZoneValue timeZoneValue)
        {
            if (id != timeZoneValue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeZoneValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeZoneValueExists(timeZoneValue.Id))
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
            return View(timeZoneValue);
        }

        // GET: TimeZoneValues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeZoneValue = await _context.TimeZoneValues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeZoneValue == null)
            {
                return NotFound();
            }

            return View(timeZoneValue);
        }

        // POST: TimeZoneValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeZoneValue = await _context.TimeZoneValues.FindAsync(id);
            _context.TimeZoneValues.Remove(timeZoneValue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeZoneValueExists(int id)
        {
            return _context.TimeZoneValues.Any(e => e.Id == id);
        }
    }
}
