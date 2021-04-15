using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using SharpCounter.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> DemoSite()
        {

            WebSites webSites = await _context.WebSites
                .FirstOrDefaultAsync(m => m.DemoSite == true);
            if (webSites == null)
            {
                return NotFound();
            }
            return View(webSites);
        }

        public IActionResult Index()
        {
            return View(_context.SiteContents.FirstOrDefault());
        }

        public IActionResult WhatWeCollect()
        {
            return View(_context.SiteContents.FirstOrDefault());
        }

        public IActionResult PrivacyPolicy()
        {
            return View(_context.SiteContents.FirstOrDefault());
        }
        public IActionResult StatusPage()
        {
            return View(_context.SiteContents.FirstOrDefault());
        }

        public IActionResult Documentation()
        {
            return View(_context.SiteContents.FirstOrDefault());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
