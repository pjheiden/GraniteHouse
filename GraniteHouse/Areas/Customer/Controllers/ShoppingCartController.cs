using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Extensions;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }
        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCartVM = new ShoppingCartViewModel()
            {
                Products = new List<Models.Products>()
            };
        }

        // Get Index Shopping Cart
        public IActionResult Index()
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart == null)
            {
                lstShoppingCart = new List<int>();

            }
            foreach (var cartItem in lstShoppingCart)
            {
      //          ShoppingCartVM.Products.Add(_db.Products.Include(m=>m.ProductTypes).Include(m=>m.SpecialTags).FirstOrDefault(m => m.Id == cartItem));

                //ShoppingCartVM.Products.Add(_db.Products
                //    .Include(m => m.ProductTypes)
                //    .Include(m => m.SpecialTags)
                //    .Where(m => m.Id == cartItem)
                //    .First());
                Products prod = _db.Products
                    .Include(m => m.ProductTypes)
                    .Include(m => m.SpecialTags)
                    .Where(m => m.Id == cartItem)
                    .First();

                ShoppingCartVM.Products.Add(prod);
            }
            return View(ShoppingCartVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int itemId, ShoppingCartViewModel sCart)
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            lstShoppingCart.Remove(itemId);
            var product = ShoppingCartVM.Products.FirstOrDefault(p => p.Id == itemId);

            ShoppingCartVM.Products.Remove(product);
            HttpContext.Session.Set<List<int>>("ssShoppingCart", lstShoppingCart);
            return View(ShoppingCartVM);

        }
    }
}