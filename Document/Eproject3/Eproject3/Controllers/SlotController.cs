using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Eproject3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.AspNetCore.Authorization;

namespace Eproject3.Controllers
{
    public class SlotController : Controller
    {
        private readonly eProject3Context db;
        public SlotController(eProject3Context db)
        {
            this.db = db;
        }

        [Authorize(Roles = "admin, constructor, maintainer")]
        public IActionResult Index()
        {
            var model = db.Slots.Include(x => x.Lab).Include(c => c.Admins).ToList();
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["LabList"] = new SelectList(db.Labs, "LabsId", "LabsName");
            ViewData["ConstructorList"] = new SelectList(db.Admins.Where(a => a.Role == "constructor"), "Id", "AdminName");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]    
        public async Task<IActionResult> Create([FromForm] Slot s)
        {
            var isExist = db.Slots.Any(sl => (sl.AdminsId == s.AdminsId && sl.Slot1.Equals(s.Slot1) && sl.Day == s.Day && sl.LabId == s.LabId) || (sl.AdminsId == s.AdminsId && sl.Slot1.Equals(s.Slot1) && sl.Day == s.Day));
       
            if (isExist)
            {
                ModelState.AddModelError(string.Empty, "Account Is Exists In System");
                ViewData["LabList"] = new SelectList(db.Labs, "LabsId", "LabsName");
                ViewData["ConstructorList"] = new SelectList(db.Admins.Where(a => a.Role == "constructor"), "Id", "AdminName");
                return View();
            }
            Slot newSlot = new Slot();
            newSlot.Day = s.Day;
            newSlot.Slot1 = s.Slot1;
            newSlot.LabId = s.LabId;
            newSlot.AdminsId = s.AdminsId;
            db.Slots.Add(newSlot);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var s = db.Slots.SingleOrDefault(c => c.SlotId.Equals(id));
            ViewData["LabList"] = new SelectList(db.Labs, "LabsId", "LabsName");
            ViewData["ConstructorList"] = new SelectList(db.Admins.Where(a => a.Role == "constructor"), "Id", "AdminName");
            return View(s);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(Slot slot)
        {
            var model = db.Slots.Where(d => d.SlotId.Equals(slot.SlotId)).FirstOrDefault();
            if (model != null)
            {
                model.Day = slot.Day;
                model.Slot1 = slot.Slot1;
                model.LabId = slot.LabId;
                model.AdminsId = slot.AdminsId;
                db.Slots.Update(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Cannot Update Slot!");
            }
            return View(model);
        }
    }
}