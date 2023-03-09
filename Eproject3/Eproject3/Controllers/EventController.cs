using Microsoft.AspNetCore.Mvc;
using Eproject3.Data;
using Eproject3.Models.ViewModels;
using Eproject3.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Eproject3.Controllers
{
    public class EventController : Controller
    {
        private readonly IDAL _dal;
        private readonly eProject3Context db;


        public EventController(IDAL dal, eProject3Context db) 
        {
            _dal = dal;
            this.db = db;

        }

        // GET: Event
        [Authorize(Roles = "admin, constructor, maintainer")]
        public IActionResult Index()
        {
            var model = _dal.GetEvents().ToList();
            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"];
            }
            return View(model);
        }

        // GET: Event/Details/5
        [Authorize(Roles = "admin, constructor, maintainer")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _dal.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        //GET: Event/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View(new EventViewModel(db.Labs.ToList()));
        }

        // POST: Event/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create(EventViewModel vm, IFormCollection form)
        {
            try
            {
                _dal.CreateEvent(form);
                TempData["Alert"] = "Success! You created a new event for: " + form["Event.Name"];
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["Alert"] = "An error occurred: " + ex.Message;
                return View(vm);
            }
        }

        // GET: Event/Edit/5
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _dal.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }
            var vm = new EventViewModel(@event, db.Labs.Find((int)id));
            return View(vm);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id, IFormCollection form)
        {
            try
            {
                _dal.UpdateEvent(form);
                TempData["Alert"] = "Success! You modified an event for: " + form["Event.Name"];
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Alert"] = "An error occurred: " + ex.Message;
                var vm = new EventViewModel(_dal.GetEvent(id), db.Labs.Find(id));
                return View(vm);
            }
        }

        // GET: Event/Delete/5
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = _dal.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _dal.DeleteEvent(id);
            TempData["Alert"] = "You deleted an event.";
            return RedirectToAction(nameof(Index));
        }
    }
}