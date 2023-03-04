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

        public IActionResult Index()
        {
            var model = db.Slots.Include(x => x.Lab).Include(c => c.Admins).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["LabList"] = new SelectList(db.Labs, "LabsId", "LabsName");
            ViewData["StaffList"] = new SelectList(db.Admins.Where(a => a.Role == "staff"), "Id", "AdminName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Slot s)
        {
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
        public IActionResult Edit(int id)
        {
            var s = db.Slots.SingleOrDefault(c => c.SlotId.Equals(id));
            ViewData["LabList"] = new SelectList(db.Labs, "LabsId", "LabsName");
            ViewData["StaffList"] = new SelectList(db.Admins.Where(a => a.Role == "staff"), "Id", "AdminName");
            return View(s);
        }

        [HttpPost]
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