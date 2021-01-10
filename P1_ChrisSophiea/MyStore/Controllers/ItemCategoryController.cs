using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    public class ItemCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ItemCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ItemCategory> objList = _db.ItemCategory;
            return View(objList);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItemCategory obj)
        {
            var existingCategory = _db.ItemCategory.AsNoTracking().FirstOrDefault(i => i.CategoryName == obj.CategoryName);
            if (ModelState.IsValid && existingCategory==null)
            {
                _db.ItemCategory.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ItemCategory.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ItemCategory obj)
        {
            var existingCategory = _db.ItemCategory.AsNoTracking().FirstOrDefault(i => i.CategoryName == obj.CategoryName);
            if (ModelState.IsValid && existingCategory == null)
            {
                _db.ItemCategory.Update(obj);
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
            var obj = _db.ItemCategory.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.ItemCategory.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.ItemCategory.Remove(obj);
            _db.SaveChanges();


            return RedirectToAction("Index");
        }
    }

}

