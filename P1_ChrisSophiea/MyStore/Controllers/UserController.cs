using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Models;
using MyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string searchString)
        {

            var users = from u in _db.ApplicationUser select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.FullName.Contains(searchString) || s.Email.Contains(searchString));
            }
            //IEnumerable<ApplicationUser> = _db.users;
            return View(users);
        }

        public IActionResult ViewPurchases(string id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<Purchase> Purchases;

            if (id == null && User.IsInRole(WC.AdminRole))
            {
                Purchases = _db.Purchase.Include(i => i.Store1).Include(i => i.Customer1);
            }
            else if (id != null && User.IsInRole(WC.AdminRole))
            {
                Purchases = _db.Purchase.Include(i => i.Store1).Include(i=>i.ItemPurchase).Include(i => i.Customer1).Where(x => x.Customer1Id == id);
            }
            else
            {
                Purchases = _db.Purchase.Include(i => i.Store1).Include(i => i.ItemPurchase).Include(i => i.Customer1).Where(x => x.Customer1Id == claim.Value);
            }

            foreach(Purchase p in Purchases)
            {
                p.ItemPurchase = _db.ItemPurchase.Include(i=>i.Item1Item).Where(i => i.PurchasesPurchaseId == p.PurchaseId).ToList();
            }

            UserPurchaseVM userPurchaseVM = new UserPurchaseVM
            {
                UserSelectList = _db.ApplicationUser.Select(i => new SelectListItem
                {
                    Text = i.FullName,
                    Value = i.Id.ToString()
                }),
                StoreList = _db.Store,
                PurchaseList = Purchases
            };

            return View(userPurchaseVM);
        }

    }
}
