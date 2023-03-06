using Eproject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

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
            var model = db.Labs.Include(x=>x.Devices).Include(x => x.Slots).ThenInclude(x => x.Admins).ToList();
            // item.Devices.Count();
            return View(model);
        }

        // GET: LabsController1/Details/5
        public ActionResult DevicesList(int id)
        {
            //Labs và Devices là quan hệ 1 nhiều , 1 Labs có nhiều Devices
            //vd: Xuất ra view 1 lab A sẽ trả ra đc list Devices của Lab A (dựa vào Include)
            var lab = db.Labs.Where(lab => lab.LabsId == id).Include(lab=>lab.Devices).ThenInclude(lab => lab.Supplier).FirstOrDefault();
            return View(lab);
        }

        // GET: LabsController1/Create
        public ActionResult Create()
        {
            ViewData["TeacherList"] = new SelectList(db.Admins.Where(a => a.Role == "staff"), "Id", "AdminName");
            return View();     
        }

        // POST: LabsController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lab lab)
        {
            var model = db.Labs.Where(c =>c.LabsName.Equals(lab.LabsName)).FirstOrDefault();
            try
            {
                if(model == null){
                    db.Labs.Add(lab);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Lab " + lab.LabsName + " already exists");
                }
            }
            catch
            {
                return View(lab);
                ModelState.AddModelError(string.Empty, "Lab " + lab.LabsName + " already exists");
            }
            return View();
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
