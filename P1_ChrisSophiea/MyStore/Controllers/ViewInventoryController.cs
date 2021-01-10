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
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    public class ViewInventoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ViewInventoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int id)
        {
            HomeVM homeVM = new HomeVM()
            {
                Inventories = _db.Inventory.Include(x => x.Item1).Include(x => x.Store1).Include(x => x.Item1.ItemCategory).Where(x => x.Store1Id == id),
                ItemCategories = _db.ItemCategory,
            };
            return View(homeVM);
        }

        public IActionResult Detail(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }


            DetailVM detailVM = new DetailVM()
            {
                Inventory = _db.Inventory.Include(i => i.Item1).Include(i => i.Store1).Include(i => i.Item1.ItemCategory).FirstOrDefault(x=>x.InventoryId == id),
                ExistsInCart = false
            };
            Inventory inventory = _db.Inventory.AsNoTracking().Include(i => i.Store1).FirstOrDefault(x => x.InventoryId == id);
            foreach (var item in shoppingCartList)
            {
                if (item.ProductId == inventory.Item1Id)
                {
                    detailVM.ExistsInCart = true;
                }
            }
            return View(detailVM);
        }

        [HttpPost, ActionName("Detail")]
        public IActionResult DetailPost(int id, int qty)
        {
            Inventory inventory = _db.Inventory.AsNoTracking().Include(i => i.Store1).FirstOrDefault(x => x.InventoryId == id);
            int storeid = inventory.Store1Id;

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            foreach (ShoppingCart sc in shoppingCartList)
            {
                if(sc.StoreId != storeid)
                {
                    shoppingCartList = new List<ShoppingCart>();
                }
            }
            shoppingCartList.Add(new ShoppingCart
            {
                ProductId = inventory.Item1Id,
                StoreId = storeid,
                ProductQty = qty
            });
            inventory.InventoryAmount -= qty;
            _db.Inventory.Update(inventory);
            _db.SaveChanges();
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);


            return RedirectToAction("Index", new { id = storeid });
        }

        public IActionResult RemoveFromCart(int id)
        {
            Inventory inventory = new Inventory();
            Inventory i1 = _db.Inventory.AsNoTracking().FirstOrDefault(x => x.InventoryId == id);

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            IEnumerable<Inventory> inventories = _db.Inventory.AsNoTracking().Include(i => i.Store1).Include(i => i.Item1);
            foreach (ShoppingCart sc in shoppingCartList)
            {
                foreach(Inventory i in inventories)
                {
                    if(i.Item1Id == sc.ProductId && i.Store1Id == sc.StoreId && i1.Item1Id == i.Item1Id)
                    {
                        inventory = i;
                    }
                }
                
            }
            
            ShoppingCart shoppingCart = shoppingCartList.Find(x => x.ProductId == inventory.Item1Id);
            Inventory i2 = _db.Inventory.AsNoTracking().FirstOrDefault(x => x.InventoryId == id);

            int returnToStoreId = i2.Store1Id;

            var itemToRemove = shoppingCartList.SingleOrDefault(i => i.ProductId == inventory.Item1Id);
            inventory.InventoryAmount += shoppingCart.ProductQty;

            if (itemToRemove != null)
            {
                shoppingCartList.Remove(itemToRemove);
            }

            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            _db.Update(inventory);
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = returnToStoreId });
        }
    }
}
