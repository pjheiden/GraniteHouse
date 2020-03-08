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


        // Get
        public IActionResult ConfirmAppointment(int? id)
        {

                ShoppingCartVM.Appointments = _db.Appointments.FirstOrDefault(a => a.Id == id);
                ShoppingCartVM.Products = _db.ProductsSelectedForAppointment.Where(p => p.AppointmentId == id).Select(p => p.Products).Include(p => p.ProductTypes).ToList();
                return View(ShoppingCartVM);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ConfirmAppointment")]
        public IActionResult ConfirmAppointmentPost()
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            ShoppingCartVM.Appointments.AppointmentDate = ShoppingCartVM.Appointments.AppointmentDate
                .AddHours(ShoppingCartVM.Appointments.AppointmentTime.Hour)
                .AddMinutes(ShoppingCartVM.Appointments.AppointmentTime.Minute);

            Appointments appointments = ShoppingCartVM.Appointments;
            _db.Appointments.Add(appointments);
            _db.SaveChanges();

            int appointmentId = appointments.Id;

            foreach (var productId in lstShoppingCart)
            {
                _db.ProductsSelectedForAppointment.Add(new ProductsSelectedForAppointment() { AppointmentId = appointmentId, ProductId = productId });
            }
            _db.SaveChanges();
            lstShoppingCart = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

            return RedirectToAction("Index");
            //      return View(ShoppingCartVM);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            ShoppingCartVM.Appointments.AppointmentDate = ShoppingCartVM.Appointments.AppointmentDate
                .AddHours(ShoppingCartVM.Appointments.AppointmentTime.Hour)
                .AddMinutes(ShoppingCartVM.Appointments.AppointmentTime.Minute);

            Appointments appointments = ShoppingCartVM.Appointments;
            _db.Appointments.Add(appointments);
            _db.SaveChanges();

            int appointmentId = appointments.Id;

            foreach (var productId in lstShoppingCart)
            {
                _db.ProductsSelectedForAppointment.Add(new ProductsSelectedForAppointment() { AppointmentId = appointmentId, ProductId = productId });
            }
            _db.SaveChanges();
            lstShoppingCart = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

            return RedirectToAction("ConfirmAppointment", "ShoppingCart", new { Id = appointmentId });
            //      return View(ShoppingCartVM);
        }


        //[HttpPost, ActionName("Remove")]
        //[ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstShoppingCart.Count > 0)
            {
                if (lstShoppingCart.Contains(id))
                {
                    lstShoppingCart.Remove(id);

                    HttpContext.Session.Set<List<int>>("ssShoppingCart", lstShoppingCart);
                }



            }

            return RedirectToAction(nameof(Index));



            //List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            //if (lstShoppingCart == null)
            //{
            //    lstShoppingCart = new List<int>();

            //}
            //lstShoppingCart.Add(id);
            //HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

            //return RedirectToAction(nameof(Index));

        }


    }
}