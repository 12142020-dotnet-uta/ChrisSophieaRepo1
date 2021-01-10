using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Models;
using MyStore.Models.ViewModels;
using MyStore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Inventory> inventories = _db.Inventory.Include(x => x.Store1).Include(x => x.Item1);
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<Inventory> inventoriesInCart = new List<Inventory>();
            foreach (ShoppingCart sc in shoppingCartList)
            {
                foreach(Inventory i in inventories)
                {
                    if(sc.ProductId==i.Item1Id && sc.StoreId == i.Store1Id)
                    {
                        inventoriesInCart.Add(i);
                    }
                }
            }

            List<int> itemQtys = new List<int>();
            foreach (ShoppingCart sc in shoppingCartList)
            {
                itemQtys.Add(sc.ProductQty);
            }

            IDictionary<Inventory, int> productList = new Dictionary<Inventory, int>();
            for(int i = 0; i < shoppingCartList.Count(); i++)
            {
                productList.Add(inventoriesInCart[i], itemQtys[i]);
            }
            return View(productList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut(double total)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = User.FindFirstValue(ClaimTypes.Name); 
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            //List<int> productIds = shoppingCartList.Select(i => i.ProductId).ToList();
            int storeId = shoppingCartList.Select(i => i.StoreId).FirstOrDefault();

            Purchase purchase = new Purchase
            {
                Store1Id = storeId,
                Customer1Id = claim.Value,
                PurchaseDate = DateTime.Now,
                TotalPrice = total,
            };
            _db.Purchase.Add(purchase);
            _db.SaveChanges();

            List<int> productIds = new List<int>();
            List<int> qtys = new List<int>();

            for(int i=0;i<shoppingCartList.Count();i++)
            {
                productIds.Add(shoppingCartList[i].ProductId);
                qtys.Add(shoppingCartList[i].ProductQty);
            }

            for(int i=0; i<productIds.Count(); i++)
            {
                ItemPurchase itemPurchase = new ItemPurchase()
                {
                    PurchasesPurchaseId = purchase.PurchaseId,
                    Item1ItemId = productIds[i],
                    ItemQty = qtys[i],
                };
                _db.ItemPurchase.Add(itemPurchase);
                _db.SaveChanges();
            }

            IEnumerable<Item> productList = _db.Item.Where(i => productIds.Contains(i.ItemId));

            ProductUserVM = new ProductUserVM()
            {
                ApplicationUser = _db.ApplicationUser.FirstOrDefault(i => i.Id == claim.Value),
                ProductList = productList.ToList(),
                PurchaseTotal = total
            };
            HttpContext.Session.Clear();
            return View(ProductUserVM);
        }



        public IActionResult Remove(int id, int qty) {
            Inventory inventory = _db.Inventory.FirstOrDefault(x => x.InventoryId == id);
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            
            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u=>u.ProductId == inventory.Item1Id && u.StoreId == inventory.Store1Id));
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            inventory.InventoryAmount += qty;
            _db.Update(inventory);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
