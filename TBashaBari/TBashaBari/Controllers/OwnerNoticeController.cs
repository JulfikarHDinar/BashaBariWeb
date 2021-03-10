using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBashaBari.Data;
using TBashaBari.Models;

namespace BashaBari.Controllers
{
    public class OwnerNoticeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OwnerNoticeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string _CurrentUserEmail)
        {

            //_CurrentUserEmail = current logged in user's email id
            IEnumerable<OwnerNotice> objList = _db.OwnerNotice;
            return View(objList);
        }

        //GET_CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST_CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OwnerNotice obj)
        {
            if (ModelState.IsValid) {
                _db.OwnerNotice.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET_EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }

            var obj = _db.OwnerNotice.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST_EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OwnerNotice obj)
        {
            if (ModelState.IsValid)
            {
                _db.OwnerNotice.Update(obj);
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

            var obj = _db.OwnerNotice.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST_DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(OwnerNotice obj)
        {
            if (ModelState.IsValid)
            {
                _db.OwnerNotice.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }



    } 
}
