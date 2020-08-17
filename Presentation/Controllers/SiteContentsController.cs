﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Persistence;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SiteContentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SiteContentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SiteContents
        public async Task<IActionResult> Index()
        {
            return View(await _context.SiteContents.ToListAsync());
        }

        // GET: SiteContents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteContent = await _context.SiteContents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteContent == null)
            {
                return NotFound();
            }

            return View(siteContent);
        }

        // GET: SiteContents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SiteContents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LargeHeader,BlurgUnderLargerHeader,Header1,Header1Content,Header1CodeSnip,Header2,Header2Content,Header3,Header3Content,FinalHeader,FinalHeaderContent,WhatWeCollect,PrivacyPolicy,Documentation")] SiteContent siteContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siteContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siteContent);
        }

        // GET: SiteContents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteContent = await _context.SiteContents.FindAsync(id);
            if (siteContent == null)
            {
                return NotFound();
            }
            return View(siteContent);
        }

        // POST: SiteContents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LargeHeader,BlurgUnderLargerHeader,Header1,Header1CodeSnip,Header1Content,Header2,Header2Content,Header3,Header3Content,FinalHeader,FinalHeaderContent,WhatWeCollect,PrivacyPolicy,Documentation")] SiteContent siteContent)
        {
            if (id != siteContent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteContentExists(siteContent.Id))
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
            return View(siteContent);
        }

        // GET: SiteContents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteContent = await _context.SiteContents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteContent == null)
            {
                return NotFound();
            }

            return View(siteContent);
        }

        // POST: SiteContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siteContent = await _context.SiteContents.FindAsync(id);
            _context.SiteContents.Remove(siteContent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteContentExists(int id)
        {
            return _context.SiteContents.Any(e => e.Id == id);
        }
    }
}
