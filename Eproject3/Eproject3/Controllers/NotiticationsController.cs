using Eproject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eproject3.Controllers
{
    public class NotiticationsController : Controller
    {
        private readonly eProject3Context _context;
        public NotiticationsController(eProject3Context context)
        {
            _context = context;
        }

        // GET: NotiticationsController
        public JsonResult GetNoti()
        {
            return Json(_context.Notifications.Where(x=>x.SendFor ==User.Identity.Name).ToList());
        }

        // GET: NotiticationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NotiticationsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotiticationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NotiticationsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotiticationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NotiticationsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotiticationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
