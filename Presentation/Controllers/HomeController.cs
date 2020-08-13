using Microsoft.AspNetCore.Mvc;
using Persistence;
using SharpCounter.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;

namespace SharpCounter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
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
