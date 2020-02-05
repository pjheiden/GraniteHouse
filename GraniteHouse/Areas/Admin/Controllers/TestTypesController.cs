using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestTypesController : Controller
    {
private readonly ApplicationDbContext _db;

        public TestTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.TestTypes.ToList());
        }


        // GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }


        // Post Create action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestTypes testTypes)
        {
            if (ModelState.IsValid)
            {
                //              _db.ProductTypes.Add(productTypes);
                _db.Add(testTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(testTypes);

        }


        // GET Edit Action Method
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testTypes = _db.TestTypes.Find(id);
            if (testTypes == null)
            {
                return NotFound();
            }
            return View(testTypes);
        }


        // Post Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int code, int specialtag, TestTypes testTypes)
        {
            if (id != testTypes.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //              _db.ProductTypes.Add(productTypes);
                _db.Update(testTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(testTypes);

        }


    }
}