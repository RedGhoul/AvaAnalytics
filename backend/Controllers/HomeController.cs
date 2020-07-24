using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using SharpCounter.Data;
using SharpCounter.Enities;
using SharpCounter.ViewModels;

namespace SharpCounter.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WhatWeCollect()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult StatusPage()
        {
            return View();
        }

        public IActionResult Documentation()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
