using Microsoft.AspNetCore.Mvc;
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
namespace Eproject3.Controllers
{
    public class ComplainController : Controller
    {
        private eProject3Context db;

        public ComplainController(eProject3Context _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var result = db.Complains.Include(x => x.IdNavigation).ToList();
            return View(result);
        }
        public IActionResult IndexUser()
        {
            var result = db.Complains.Include(x => x.IdNavigation).ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(db.Admins, "Id", "AdminName");
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult Create(Complain newCPL)
        {

            //var user = db.Users.Where(u=>u.id.Equals(newUser.id)).FirstOrDefault();
            Complain newComplain = new Complain();
            newComplain.Description = newCPL.Description;
            newComplain.Reason = newCPL.Reason;
            newComplain.StatusCp = newCPL.StatusCp;
            newComplain.DateCp = DateTime.Now;
            newComplain.Category = newCPL.Category;
            newComplain.Id = newCPL.Id;
            newComplain.Reply = "";
            db.Complains.Add(newComplain);
            db.SaveChanges();
            ViewData["Id"] = new SelectList(db.Admins, "Id", "AdminName", newCPL.Id);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult EditComplain(int id)
        {
            var model = db.Complains.Where(d => d.ComplainId == id).FirstOrDefault();
            ViewData["Id"] = new SelectList(db.Admins, "Id", "AdminName", model.Id);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditComplain(Complain newCPL)
        {
            var model = db.Complains.Where(d => d.ComplainId == newCPL.ComplainId).FirstOrDefault();

            if (model != null)
            {
                model.Description = newCPL.Description;
                model.Reason = newCPL.Reason;
                model.StatusCp = newCPL.StatusCp;
                model.DateCp = DateTime.Now;
                model.Category = newCPL.Category;
                model.Id = newCPL.Id;
                model.Reply = newCPL.Reply;

                db.Update(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "can not update Complain");
            }
            return View(newCPL);
        }
        [HttpGet]
        public IActionResult DeleteComplain(int id)
        {
            var result = db.Complains.FirstOrDefault(d => d.ComplainId.Equals(id));
            if (result != null)
            {
                db.Complains.Remove(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return NoContent();
            }
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var result = db.Complains.Where(d => d.ComplainId.Equals(id)).FirstOrDefault();
            return View(result);
        }
    }
}
