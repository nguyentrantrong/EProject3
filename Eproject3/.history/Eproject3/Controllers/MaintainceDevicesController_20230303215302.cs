using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eproject3.Models;

namespace Eproject3.Controllers
{
    public class MaintainceDevicesController : Controller
    {
        private readonly eProject3Context _context;

        public MaintainceDevicesController(eProject3Context context)
        {
            _context = context;
        }

        // GET: MaintainceDevices
        public async Task<IActionResult> Index()
        {
            var eProject3Context = _context.MaintainceDevices.Include(m => m.Devices).Where(m => m.Step == 1 || m.Step == -99);
            return View(await eProject3Context.ToListAsync());
        }
        public async Task<IActionResult> ListForMaintainer()
        {
            var eProject3Context = _context.MaintainceDevices.Include(m => m.Devices).Where(m => m.Step >= 2);
            return View(await eProject3Context.ToListAsync());
        }

        // GET: MaintainceDevices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MaintainceDevices == null)
            {
                return NotFound();
            }

            var maintainceDevice = await _context.MaintainceDevices
                .Include(m => m.Devices)
                .FirstOrDefaultAsync(m => m.MaintnId == id);
            if (maintainceDevice == null)
            {
                return NotFound();
            }

            return View(maintainceDevice);
        }

        // GET: MaintainceDevices/Create
        public IActionResult Create()
        {
            ViewData["DevicesId"] = new SelectList(_context.Devices, "DevicesId", "DeviceName");
            return View();
        }

        // POST: MaintainceDevices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaintnId,Descriptions,Reason,Date,Creater,DevicesId,Id")] MaintainceDevice maintainceDevice)
        {
            MaintainceDevice newMaintaince = new MaintainceDevice();
            newMaintaince.Descriptions = maintainceDevice.Descriptions;
            newMaintaince.Reason = maintainceDevice.Reason;
            newMaintaince.Date = DateTime.Now;
            newMaintaince.Creater = maintainceDevice.Creater;
            newMaintaince.DevicesId = maintainceDevice.DevicesId;
            newMaintaince.Status= "Pending";
            newMaintaince.Step = 1;
            //newMaintaince.Status = "pending";
            _context.Add(newMaintaince);
            await _context.SaveChangesAsync();
            ViewData["DevicesId"] = new SelectList(_context.Devices, "DevicesId", "DeviceName", maintainceDevice.DevicesId);
            return RedirectToAction(nameof(Index));
            //return View(maintainceDevice);
        }

        // GET: MaintainceDevices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MaintainceDevices == null)
            {
                return NotFound();
            }

            var maintainceDevice = await _context.MaintainceDevices.FindAsync(id);
            if (maintainceDevice == null)
            {
                return NotFound();
            }
            ViewData["DevicesId"] = new SelectList(_context.Devices, "DevicesId", "DeviceName", maintainceDevice.DevicesId);
            return View(maintainceDevice);
        }

        // POST: MaintainceDevices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MaintnId,Descriptions,Reason,Date,Creater,DevicesId,Id")] MaintainceDevice maintainceDevice)
        {
            var model = await _context.MaintainceDevices.FirstOrDefaultAsync(c => c.MaintnId == maintainceDevice.MaintnId);
            model.Descriptions = maintainceDevice.Descriptions;
            model.Reason = maintainceDevice.Reason;
            model.Date = maintainceDevice.Date;
            model.Creater = maintainceDevice.Creater;
            model.DevicesId = maintainceDevice.DevicesId;
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: MaintainceDevices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MaintainceDevices == null)
            {
                return NotFound();
            }

            var maintainceDevice = await _context.MaintainceDevices
                .Include(m => m.Devices)
                .FirstOrDefaultAsync(m => m.MaintnId == id);
            if (maintainceDevice == null)
            {
                return NotFound();
            }

            return View(maintainceDevice);
        }

        // POST: MaintainceDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MaintainceDevices == null)
            {
                return Problem("Entity set 'eProject3Context.MaintainceDevices'  is null.");
            }
            var maintainceDevice = await _context.MaintainceDevices.FindAsync(id);
            if (maintainceDevice != null)
            {
                _context.MaintainceDevices.Remove(maintainceDevice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintainceDeviceExists(int id)
        {
          return _context.MaintainceDevices.Any(e => e.MaintnId == id);
        }
        //browser function
        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            var request = await _context.MaintainceDevices.FirstOrDefaultAsync(req => req.MaintnId == id);
            var req = await _context.Devices.FirstOrDefaultAsync(req => req.DevicesId == id);
            
            if(request != null)
            {
                request.Step++;
                switch (request.Step)
                {
                    case 2:
                        request.Status = "Waiting for Approved";
                        req.Status = "Can not use rightnow";
                        break;
                    case 3:
                        request.Status = "Wainting for Confirm";
                        req.Status = "Can not use rightnow";
                        break;
                    case 4:
                        request.isFinished = true;
                        req.Status = "Active";
                        req.DateMaintance = DateTime.Now;
                        break;
                }
                _context.Update(request);
                _context.Update(req);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DisApprove(int id)
        {
            var request = await _context.MaintainceDevices.FirstOrDefaultAsync(req => req.MaintnId == id);
            var req = await _context.Devices.FirstOrDefaultAsync(req => req.DevicesId == id);
            if(request != null)
            {
                request.Step = -99;
                req.Status = "Deactivate";
                _context.Update(request);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        
    }
}
