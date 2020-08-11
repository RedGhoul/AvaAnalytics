using Microsoft.AspNetCore.Mvc;
using SharpCounter.ViewModels;
using System;
using System.Diagnostics;

namespace SharpCounter.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            ViewBag.CodeSnip = @"<script defer src=""https://sharp-counter.experimentsinthedeep.com/js/prod/count.min.js""" + Environment.NewLine +
                               @"data-sharpcounter=""https://sharp-counter.experimentsinthedeep.com/api/Interaction/Count""" + Environment.NewLine +
                               @"data-sharpcounter-apikey=""<YOUR-API-KEY>"" ></script>" + Environment.NewLine;
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
