﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraniteHouse.Models;
using GraniteHouse.Data;
using Microsoft.EntityFrameworkCore;
using GraniteHouse.Extensions;

namespace GraniteHouse.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;


        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await _db.Products.Include(m => m.ProductTypes).Include(m => m.SpecialTags).ToListAsync();
            return View(productList);
        }

        // GET : Details
        public async Task<IActionResult> Details(int id)
        {
            //          var product = await _db.Products.FindAsync(id);
            var product = await _db.Products.Include(m => m.ProductTypes).Include(m => m.SpecialTags).Where(m => m.Id == id).FirstOrDefaultAsync();

            return View(product);
        }

        // POST : Details
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DetailsPost(int id)
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart == null)
            {
                lstShoppingCart = new List<int>();

            }
            lstShoppingCart.Add(id);
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
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
