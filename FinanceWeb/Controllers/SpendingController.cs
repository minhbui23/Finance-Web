using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Finance.DataAccess.DBContext;
using Finance.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Finance_Web.Controllers
{
    public class SpendingController : Controller
    {
        private readonly ApplicationDBContext _db;
        public SpendingController(ApplicationDBContext db){
            _db=db;
        }
        public IActionResult Index(){
            List<Spending> List_Spendings = _db.Spendings.ToList();
            return View(List_Spendings);
        }

        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public IActionResult Create(Spending obj){
            if(ModelState.IsValid){
                _db.Spendings.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id){
            if(id == null || id ==0){
                return NotFound();
            }
            Spending? spendingFromDb = _db.Spendings.Find(id);
            if(spendingFromDb == null){
                return NotFound();
            }
            return View(spendingFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Spending obj){
            if(ModelState.IsValid){
                _db.Spendings.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id){
            if(id == null || id == -0) {
                return NotFound();
            }
            Spending? spendingFromDb = _db.Spendings.Find(id);
            if(spendingFromDb == null) {
                return NotFound();
            }
            return View(spendingFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id){
            Spending? obj = _db.Spendings.Find(id);
            if(obj == null){
                return NotFound();
            }
            _db.Spendings.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}