﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBashaBari.Data;
using TBashaBari.Models;
using System.Data.SqlClient;
using TBashaBari.Controllers;

namespace BashaBari.Controllers
{
    public class TenantViewsNotice : Controller
    {
        List<OwnerNotice> _ownernoticelist = new List<OwnerNotice>();

        private readonly ApplicationDbContext _db;

        public TenantViewsNotice(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            FetchOwnerNotice();
            return View(_ownernoticelist);
        }

        //:::::::::::::::::::::::::::::::::::::::::GET_CREATE
        public IActionResult Create()
        {
            return View();
        }
        //::::::::::::::::::::::::::::::::::::::::POST_CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OwnerNotice obj)
        {
            if (ModelState.IsValid)
            {
                _db.OwnerNotice.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //:::::::::::::::::::::::::::::::::::::::GET_EDIT
        public IActionResult Edit(int? id)
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
        //::::::::::::::::::::::::::::::::::::::POST_EDIT
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

        //:::::::::::::::::::::::::::::::::::::::GET_DELETE
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
        //::::::::::::::::::::::::::::::::::::::POST_DELETE
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

        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
        //:::::::::::::::::::::::::::::         custom methods        ::::::::::::::::::::::::::::::::://
        private void FetchOwnerNotice()
        {
            //User.Identity.Name returns current logged in user's email
            string queryString = "SELECT TOP 1000 O.[NoticeText],O.[NoticeTime], C.[OwnerEmail], C.[IsConfirmed]" +
                "FROM TenantConnectsOwner C JOIN  OwnerNotice O " +
                "ON C.[OwnerEmail] = O.[OwnerEmail] " +
                "WHERE C.[IsConfirmed] ='Yes' AND C.[TenantEmail] = '" + User.Identity.Name + "'";
            //to clear the list initially
            if (_ownernoticelist.Count > 0)
            {
                _ownernoticelist.Clear();
            }

            //database operation
            DatabaseConnection obj = new DatabaseConnection();
            obj.DbConnect();
            while (obj.ExeQuery(queryString).Read())
            {
                _ownernoticelist.Add(new OwnerNotice
                {
                   // NoticeId = int.Parse(obj.ExeQuery(queryString)["NoticeId"].ToString()),
                    OwnerEmail = obj.ExeQuery(queryString)["OwnerEmail"].ToString(),
                    NoticeText = obj.ExeQuery(queryString)["NoticeText"].ToString(),
                    NoticeTime = obj.ExeQuery(queryString)["NoticeTime"].ToString(),
                });
            }
            obj.CloseDbConnect();
        }


        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
    }
}
