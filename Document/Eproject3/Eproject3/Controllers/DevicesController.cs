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
using ClosedXML.Excel;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;

namespace Eproject3.Controllers
{
	public class DevicesController : Controller
	{
		private readonly eProject3Context db;

		public DevicesController(eProject3Context db, Lab lab, Supplier supplier)
		{
			this.db = db;
		}

		[Authorize(Roles = "admin, constructor, maintainer")]
		public IActionResult Index()
		{
			var model = db.Devices.Include(x => x.Supplier).Include(c => c.Labs).ToList();
			return View(model);

		}

		[Authorize(Roles = "admin")]
		public IActionResult Create()
		{
			ViewData["LabsId"] = new SelectList(db.Labs, "LabsId", "LabsName");
			ViewData["Supplier_ID"] = new SelectList(db.Suppliers, "SupplierId", "SupplierName");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Create(Device device, IFormFile file)
		{

			Device newDevice = new Device();
		
			
				newDevice.DevicesId = device.DevicesId;
				newDevice.DeviceName = device.DeviceName;
				newDevice.DeviceType = device.DeviceType;
				newDevice.LabsId = device.LabsId;
				newDevice.SupplierId = device.SupplierId;
				newDevice.DateMaintance = DateTime.Now;
				newDevice.Status = "Active";
				newDevice.DeviceImg = "";
				if (file != null)
				{
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
					TempData["AlertMessage"]= "Successfully";
					ViewData["LabsId"] = new SelectList(db.Labs, "LabsId", "LabsName", device.LabsId);
					ViewData["Supplier_ID"] = new SelectList(db.Suppliers, "SupplierId", "SupplierName", device.SupplierId);
					return RedirectToAction("Index");
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View("Error!");
		}

		[Authorize(Roles = "admin, constructor, maintainer")]
		public IActionResult Details(int id)
		{
			var result = db.Devices.Where(d => d.DevicesId.Equals(id)).Include(x => x.Supplier).Include(c => c.Labs).FirstOrDefault();
			return View(result);
		}

		[HttpGet]
		[Authorize(Roles = "admin, maintainer")]
		public IActionResult EditDevices(int id)
		{
			var model = db.Devices.Where(d => d.DevicesId.Equals(id)).FirstOrDefault();
			ViewData["LabsId"] = new SelectList(db.Labs, "LabsId", "LabsName", model.LabsId);
			ViewData["Supplier_ID"] = new SelectList(db.Suppliers, "SupplierId", "SupplierName", model.SupplierId);
			return View(model);
		}
		[HttpPost]
		[Authorize(Roles = "admin, maintainer")]
		public IActionResult EditDevices(Device device, IFormFile file)
		{
			var model = db.Devices.Where(d => d.DevicesId.Equals(device.DevicesId)).FirstOrDefault();
			if (model != null)
			{
				model.DeviceName = device.DeviceName;
				model.DeviceType = device.DeviceType;
				model.Status = "Active";
				model.DateMaintance = device.DateMaintance;
				if (file != null)
				{
					var filePath = Path.Combine("wwwroot/images", file.FileName);
					var stream = new FileStream(filePath, FileMode.Create);
					file.CopyToAsync(stream);
					model.DeviceImg = "/images/" + file.FileName;
				}
				// model.DeviceImg=device.DeviceImg ;
				model.SupplierId = device.SupplierId;
				model.LabsId = device.LabsId;
				db.Devices.Update(model);
				db.SaveChanges();
				TempData["AlertMessage"]= "Successfully";
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "can not update device");
			}
			return View(device);
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public IActionResult DeleteDevice(int id)
		{
			var result = db.Devices.FirstOrDefault(d => d.DevicesId.Equals(id));
			if (result != null)
			{
				db.Devices.Remove(result);
				db.SaveChanges();
				TempData["AlertMessage"]= "Can not Delete the device";
				return RedirectToAction("Index");
			}
			else
			{
				return NoContent();
			}
		}

		public IActionResult ExportExcel()
		{
			DataTable dt = new DataTable("Report");
			dt.Columns.AddRange(new DataColumn[6] { new DataColumn("DeviceName"),
											new DataColumn("DeviceType"),
											new DataColumn("DateMaintance"),
											new DataColumn("Supplier"),
											new DataColumn("Labs"),
											new DataColumn("Status"),

			});

			var devices = from device in db.Devices.Include(x => x.Supplier).Include(c => c.Labs).Take(10)
						  select device;

			foreach (var device in devices)
			{
				dt.Rows.Add(device.DeviceName, device.DeviceType, device.DateMaintance, device.Supplier.SupplierName, device.Labs.LabsName, device.Status);
			}

			using (XLWorkbook wb = new XLWorkbook())
			{
				wb.Worksheets.Add(dt);
				using (MemoryStream stream = new MemoryStream())
				{
					wb.SaveAs(stream);
					return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Report_Devices_{DateTime.Now.ToString("dd/MM/yyyy")}.xlsx");
				}
			}
		}

		public FileResult ExportPDF()
		{
			List<object> devices = (from device in db.Devices.Include(x => x.Supplier).Include(c => c.Labs).ToList().Take(10)
									select new[] {
									  device.DeviceName,
									  device.DeviceType,
									  device.DateMaintance.ToString("dd/MM/yyyy"),
									  device.Supplier.SupplierName,
									  device.Labs.LabsName,
									  device.Status,
									  }).ToList<object>();

			//Building an HTML string.
			StringBuilder sb = new StringBuilder();

			//Table start.
			sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border: 1px solid gray;font-family: Arial; width: 100%;'>");

			//Building the Header row.
			sb.Append("<tr>");
			sb.Append("<th style='background-color: gray;border: 1px solid gray; text-align: center'>Device Name</th>");
			sb.Append("<th style='background-color: gray;border: 1px solid gray; text-align: center'>Type</th>");
			sb.Append("<th style='background-color: gray;border: 1px solid gray; text-align: center'>Date</th>");
			sb.Append("<th style='background-color: gray;border: 1px solid gray; text-align: center'>Suplier</th>");
			sb.Append("<th style='background-color: gray;border: 1px solid gray; text-align: center'>Lab</th>");
			sb.Append("<th style='background-color: gray;border: 1px solid gray; text-align: center'>Status</th>");
			sb.Append("</tr>");

			//Building the Data rows.
			for (int i = 0; i < devices.Count; i++)
			{
				string[] device = (string[])devices[i];
				sb.Append("<tr>");
				for (int j = 0; j < device.Length; j++)
				{
					//Append data.
					sb.Append("<td style='border: 1px solid gray'>");
					sb.Append(device[j]);
					sb.Append("</td>");
				}
				sb.Append("</tr>");
			}

			//Table end.
			sb.Append("</table>");

			using (MemoryStream stream = new MemoryStream())
			{
				StringReader sr = new StringReader(sb.ToString());
				Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
				PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
				pdfDoc.Open();
				XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
				pdfDoc.Close();
				return File(stream.ToArray(), "application/pdf", $"Report_Devices_{DateTime.Now.ToString("dd/MM/yyyy")}.pdf");
			}
		}
	}
}