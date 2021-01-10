using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Models;
using MyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Item> objList = _db.Item.Include(i=>i.ItemCategory);
            return View(objList);
        }

        //GET - Upsert
        public IActionResult Upsert(int? id)
        {
            ItemVM productVM = new ItemVM()
            {
                Item = new Item(),
                CategorySelectList = _db.ItemCategory.Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Item = _db.Item.Find(id);
                if (productVM.Item == null) { return NotFound(); }
            }
            return View(productVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ItemVM productVM)
        {
            var existingItem = _db.Item.AsNoTracking().FirstOrDefault(i => i.ItemName == productVM.Item.ItemName);
            if (ModelState.IsValid && existingItem == null)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productVM.Item.ItemId == 0)
                {
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVM.Item.ItemImage = fileName + extension;
                    _db.Item.Add(productVM.Item);

                }
                else
                {
                    var objFromDb = _db.Item.AsNoTracking().FirstOrDefault(u => u.ItemId == productVM.Item.ItemId);
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        if (objFromDb.ItemImage != null)
                        {
                            var oldFile = Path.Combine(upload, objFromDb.ItemImage);
                            if (System.IO.File.Exists(oldFile))
                            {
                                System.IO.File.Delete(oldFile);
                            }
                        }
                        

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productVM.Item.ItemImage = fileName + extension;

                    }
                    else
                    {
                        productVM.Item.ItemImage = objFromDb.ItemImage;
                    }
                    _db.Item.Update(productVM.Item);

                }
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            productVM.CategorySelectList = _db.Item.Select(i => new SelectListItem
            {
                Text = i.ItemName,
                Value = i.ItemId.ToString()
            });
        
            return View(productVM);
        }

        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Item product = _db.Item.Include(a => a.ItemCategory).FirstOrDefault(a => a.ItemId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Item.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;


            var oldFile = Path.Combine(upload, obj.ItemImage);
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Item.Remove(obj);
            _db.SaveChanges();


            return RedirectToAction("Index");
        }


    }
}
