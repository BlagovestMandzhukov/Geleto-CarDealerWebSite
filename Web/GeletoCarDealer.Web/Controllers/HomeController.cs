﻿namespace GeletoCarDealer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using GeletoCarDealer.Data;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly GeletoDbContext db;

        public HomeController(GeletoDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Contacts()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            var viewModel = this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });

            if (statusCode == 404)
            {
                return this.View("NotFound");
            }

            return this.View(viewModel);
        }

    }
}
