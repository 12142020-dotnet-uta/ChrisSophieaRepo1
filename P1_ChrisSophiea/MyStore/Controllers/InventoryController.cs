using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Models;
using MyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public InventoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            InventoryVM inventoryVM = new InventoryVM()
            {
                Inventories = _db.Inventory.Include(i => i.Store1).Include(i => i.Item1),
                Stores = _db.Store
            };
            return View(inventoryVM);            
        }

        public IActionResult Create(int? id)
        {
            CreateInventoryVM inventoryVM = new CreateInventoryVM()
            {
                Inventory = new Inventory(),
                StoreSelectList = _db.Store.Select(i => new SelectListItem
                {
                    Text = i.StoreAddress,
                    Value = i.StoreId.ToString()
                }),
                ItemSelectList = _db.Item.Select(i => new SelectListItem
                {
                    Text = i.ItemName,
                    Value = i.ItemId.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return View(inventoryVM);
            }
            else
            {
                inventoryVM.Inventory = _db.Inventory.Find(id);
                if (inventoryVM.Inventory == null) { return NotFound(); }
            }
            return View(inventoryVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateInventoryVM inventoryVM)
        {
            //&& 
            var existingInventory = _db.Inventory.AsNoTracking().FirstOrDefault(u => u.Store1Id == inventoryVM.Inventory.Store1Id && u.Item1Id == inventoryVM.Inventory.Item1Id);
            if (ModelState.IsValid && existingInventory==null)
            {
                _db.Inventory.Add(inventoryVM.Inventory);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            inventoryVM.StoreSelectList = _db.Store.Select(i => new SelectListItem
            {
                Text = i.StoreAddress,
                Value = i.StoreId.ToString()
            });
            inventoryVM.ItemSelectList = _db.Item.Select(i => new SelectListItem
            {
                Text = i.ItemName,
                Value = i.ItemId.ToString()
            });

            return View(inventoryVM);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Inventory.Include(x=>x.Store1).Include(x => x.Item1).FirstOrDefault(x=>x.InventoryId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Inventory obj)
        {
            if (ModelState.IsValid)
            {
                _db.Inventory.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }


        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Inventory inventory = _db.Inventory.Include(a => a.Store1).Include(x=>x.Item1).FirstOrDefault(a => a.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Inventory.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Inventory.Remove(obj);
            _db.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}
