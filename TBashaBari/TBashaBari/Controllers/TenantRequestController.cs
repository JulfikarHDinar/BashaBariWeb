using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBashaBari.Data;
using TBashaBari.Models;

namespace BashaBari.Controllers
{
    public class TenantRequestController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TenantRequestController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<TenantRequest> objList = _db.TenantRequest;
            return View(objList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TenantRequest obj)
        {
            if (ModelState.IsValid)
            {
                _db.TenantRequest.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
            
        }

        //GET_EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.TenantRequest.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST_EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TenantRequest obj)
        {
            if (ModelState.IsValid)
            {
                _db.TenantRequest.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET_DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.TenantRequest.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST_DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(TenantRequest obj)
        {
            if (ModelState.IsValid)
            {
                _db.TenantRequest.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

    }
}
