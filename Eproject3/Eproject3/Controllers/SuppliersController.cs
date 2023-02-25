using Eproject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eproject3.Controllers{
    public class SuppliersController : Controller{
        private eProject3Context db;
        public SuppliersController (eProject3Context db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var model = db.Suppliers.ToList();
            return View(model);
        }
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public IActionResult Create(Supplier sup){
            var model = db.Suppliers.Where(s => s.SupplierName.Equals(sup.SupplierName)).FirstOrDefault();
            try {
                if(model == null){
                    db.Suppliers.Add(sup);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }else{
                    ModelState.AddModelError(string.Empty,sup.SupplierName + " already exists");
                }
            }catch{
                return View(sup);
            }
            return View();
        }
    }

}
