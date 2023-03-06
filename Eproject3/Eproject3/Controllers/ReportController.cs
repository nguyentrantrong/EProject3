using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Eproject3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Data;
using System.Reflection;

namespace Eproject3.Controllers
{
    public class ReportController : Controller 
    {
        private readonly eProject3Context db;

        public ReportController(eProject3Context db, Lab lab, Supplier supplier)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var model = db.Devices.Include(x => x.Supplier).Include(c => c.Labs).ToList();
            return View(model);

        }

        public IActionResult ExportExcel()
        {
            DataTable dt = new DataTable("Report");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("DeviceName"),
                                            new DataColumn("DeviceType"),
                                            new DataColumn("Supplier"),
                                            new DataColumn("Labs"),
                                            new DataColumn("Status"),

            });

            var devices = from device in db.Devices.Include(x => x.Supplier).Include(c => c.Labs).Take(10)
                            select device;

            foreach (var device in devices)
            {
                dt.Rows.Add(device.DeviceName, device.DeviceType, device.Supplier.SupplierName, device.Labs.LabsName, device.Status);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Report_{DateTime.Now.ToString("dd/MM/yyyy")}.xlsx");
                }
            }
        }

    }
}
