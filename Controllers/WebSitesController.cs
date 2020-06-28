using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SharpCounter.Dapper;
using SharpCounter.Data;
using SharpCounter.Enities;

namespace SharpCounter.Controllers
{
    public class WebSitesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly WebSiteRepo _websiteRepo;

        public WebSitesController(WebSiteRepo WebsiteRepo,ApplicationDbContext context)
        {
            _context = context;
            _websiteRepo = WebsiteRepo;
        }

        // GET: WebSites
        public async Task<IActionResult> Index()
        {
            return View(await _websiteRepo.FindAll());
        }

        // GET: WebSites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webSites = await _context.WebSites
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webSites == null)
            {
                return NotFound();
            }

            return View(webSites);
        }

        // GET: WebSites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebSites/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LinkDomain,CreatedAt,UpdatedAt")] WebSites webSites)
        {
            if (ModelState.IsValid)
            {
                webSites.APIKey = Guid.NewGuid().ToString();
                webSites.UpdatedAt = DateTime.UtcNow;
                webSites.CreatedAt = DateTime.UtcNow;
                _context.Add(webSites);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(webSites);
        }

        // GET: WebSites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webSites = await _context.WebSites.FindAsync(id);
            if (webSites == null)
            {
                return NotFound();
            }
            return View(webSites);
        }

        // POST: WebSites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,APIKey,LinkDomain,CreatedAt,UpdatedAt")] WebSites webSites)
        {
            if (id != webSites.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webSites);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebSitesExists(webSites.Id))
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
            return View(webSites);
        }

        // GET: WebSites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webSites = await _context.WebSites
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webSites == null)
            {
                return NotFound();
            }

            return View(webSites);
        }

        // POST: WebSites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webSites = await _context.WebSites.FindAsync(id);
            _context.WebSites.Remove(webSites);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebSitesExists(int id)
        {
            return _context.WebSites.Any(e => e.Id == id);
        }
    }
}
