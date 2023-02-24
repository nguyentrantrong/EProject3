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
    public class DevicesController : Controller
    {
        private readonly eProject3Context db;

        public DevicesController(eProject3Context db, Lab lab, Supplier supplier)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var model = db.Devices.Include(x=>x.Supplier).Include(c => c.Labs).ToList();
            // 
            return Json(model, Json)
        }
        public IActionResult Create()
        {
            ViewData["LabsId"] = new SelectList(db.Labs, "LabsId", "LabsName");
            ViewData["Supplier_ID"] = new SelectList(db.Suppliers, "Supplier_ID", "SupplierName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Device device, IFormFile file) 
        {
                Device newDevice = new Device();
                newDevice.DevicesId = device.DevicesId;
                newDevice.DeviceName = device.DeviceName;
                newDevice.DeviceType = device.DeviceType;
                newDevice.LabsId = device.LabsId;
                newDevice.Supplier_ID = device.Supplier_ID;
                newDevice.DateMaintance = device.DateMaintance;
                newDevice.Status = device.Status;
                newDevice.DeviceImg = "";
                if(file != null){
                    var filePath = Path.Combine("wwwroot/images", file.FileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    file.CopyToAsync(stream);
                    newDevice.DeviceImg = "/images/" + file.FileName;
                    // string fileName = Path.GetFileName(file.FileName);
                    // string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/", fileName);
                    // using (var stream = new FileStream(filePath, FileMode.Create))
                    // {
                    //     await file.CopyToAsync(stream);
                    // }
                    // newDevice.DeviceImg = "images/" + fileName;
                }
                db.Devices.Add(newDevice);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            // if(ModelState.IsValid){
            // }
                ViewData["LabsId"] = new SelectList(db.Labs , "LabsId", "LabsName", device.LabsId);
                ViewData["Supplier_ID"] = new SelectList(db.Suppliers , "Supplier_ID", "SupplierName", device.Supplier_ID);
                return View(device);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        public IActionResult Details(string id )
        {
            var result = db.Devices.Where(d => d.DevicesId.Equals(id)).Include(x=>x.Supplier).Include(c => c.Labs).FirstOrDefault();
            return View(result);
        }
        [HttpGet]
        public IActionResult EditDevices(string id){
            var model = db.Devices.Where(d => d.DevicesId.Equals(id)).FirstOrDefault();
            ViewData["LabsId"] = new SelectList(db.Labs , "LabsId", "LabsName", model.LabsId);
                ViewData["Supplier_ID"] = new SelectList(db.Suppliers , "Supplier_ID", "SupplierName", model.Supplier_ID);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditDevices(Device device, IFormFile file){
            var model = db.Devices.Where(d => d.DevicesId.Equals(device.DevicesId)).FirstOrDefault();
            if(model != null){
                model.DeviceName=device.DeviceName;
                model.DeviceType=device.DeviceType;
                model.Status=device.Status;
                model.DateMaintance=device.DateMaintance;
                if(file != null){
                    var filePath = Path.Combine("wwwroot/images", file.FileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    file.CopyToAsync(stream);
                    model.DeviceImg = "/images/" + file.FileName;
                    // string fileName = Path.GetFileName(file.FileName);
                    // string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/", fileName);
                    // using (var stream = new FileStream(filePath, FileMode.Create))
                    // {
                    //     await file.CopyToAsync(stream);
                    // }
                    // newDevice.DeviceImg = "images/" + fileName;
                }
                // model.DeviceImg=device.DeviceImg ;
                model.Supplier_ID=device.Supplier_ID;
                model.LabsId=device.LabsId;
                db.Devices.Update(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }else{
                ModelState.AddModelError(string.Empty,"can not update device");
            }
            return View(device);
        }
        public IActionResult DeleteDevice(string deviceId){
            var result = db.Devices.Where(d => d.DevicesId.Equals(deviceId)).FirstOrDefault();
            db.Devices.Remove(result);
            db.SaveChanges();
            return View("Index");
        }
    }
}