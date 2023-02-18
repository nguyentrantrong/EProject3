using Eproject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eproject3.Controllers
{
    public class LabsController : Controller
    {
        private eProject3Context db;
        public LabsController (eProject3Context db)
        {
            this.db = db;
        }
        // GET: LabsController1
        public ActionResult Index()
        {
            var model = db.Labs.ToList();
            return View(model);
        }

        // GET: LabsController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LabsController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LabsController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lab lab)
        {
            var model = db.Labs.Where(c => c.LabsId == lab.LabsId && c.LabsName.Equals(lab.LabsName)).FirstOrDefault();
            try
            {
                if(model == null){
                    db.Labs.Add(lab);
                    db.SaveChanges();
                }
                else{
                    ModelState.AddModelError(string.Empty, "Lab " + lab.LabsName + " already exists");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LabsController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LabsController1/Edit/5
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

        // GET: LabsController1/Delete/5
        public ActionResult Delete(int id)
        {
            var lab = db.Labs.SingleOrDefault(b=>b.LabsId.Equals(id));
            if(lab != null)
            {
                db.Labs.Remove(lab);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return NoContent();
            }
            return View();
        }

        // POST: LabsController1/Delete/5
    }
}
