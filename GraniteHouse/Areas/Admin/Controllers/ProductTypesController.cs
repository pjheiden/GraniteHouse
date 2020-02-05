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
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }


        // GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }


        // Post Create action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                //              _db.ProductTypes.Add(productTypes);
                _db.Add(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);

        }


        // GET Edit Action Method
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            ProductTypes productType = await _db.ProductTypes.FindAsync(Id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }


        // POST Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, ProductTypes productType)
        {
            if (Id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }




        // GET Details Action Method
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            ProductTypes productType = await _db.ProductTypes.FindAsync(Id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }


        // POST Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int Id, ProductTypes productType)
        {
            if (Id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }





        // GET Edit TEST **************************
        public async Task<IActionResult> EditTest(int Id)
        {


            ProductTypes tempProduct = new ProductTypes();
            tempProduct.Id = 100;
            tempProduct.Name = "Steel Stunner";
            return View(tempProduct);


            //if (Id == null)
            //{
            //    return NotFound();
            //}

            //ProductTypes productType = await _db.ProductTypes.FindAsync(Id);
            //if (productType == null)
            //{
            //    return NotFound();
            //}
            //return View(productType);
        }


        // POST Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTest(int Id, ProductTypes productType)
        {
            if (Id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productType);
        }





        // Post Create action Method
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ProductTypes productTypes)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //              _db.ProductTypes.Add(productTypes);
        //        _db.Add(productTypes);
        //        await _db.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(productTypes);

        //}

        public IActionResult DeleteTest(int Id)
        {
            ProductTypes productType = _db.ProductTypes.Find(Id);
            _db.Remove(productType);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }




        // GET Delete Action Method
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            ProductTypes productType = await _db.ProductTypes.FindAsync(Id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }


        // POST Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var productType = await _db.ProductTypes.FindAsync(Id);
            _db.ProductTypes.Remove(productType);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




    }
}