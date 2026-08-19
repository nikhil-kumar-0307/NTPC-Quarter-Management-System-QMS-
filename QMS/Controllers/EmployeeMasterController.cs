using QMS.Data;
using QMS.Data.Models;
using QMS.Models.DTOs;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Web;
using System.Globalization;

namespace QMS.Controllers
{
    public class EmployeeMasterController : Controller
    {
        private readonly QMSDbContext _db = new QMSDbContext();
        private static readonly string[] QuarterTypes = { "A", "B", "C" };
        private static readonly string[] StatusOptions = { "Active", "Empty", "Retained", "Agency" };
        private static readonly string[] BloodGroups = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };

        // GET: EmployeeMaster
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "EmployeeMaster";
            var employees = _db.EmployeeMasters.OrderBy(e => e.EmployeeName).ToList();
            return View(employees);
        }

        // GET: EmployeeMaster/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "EmployeeMasterCreate";
            PopulateDropdowns();
            return View(new EmployeeMasterDto());
        }

        // POST: EmployeeMaster/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeMasterDto model)
        {
            ViewBag.ActiveMenu = "EmployeeMasterCreate";

            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(model);
            }

            var employee = new EmployeeMaster
            {
                EmployeeName = model.EmployeeName,
                EmployeeNo = model.EmployeeNo,
                Department = model.Department,
                Designation = model.Designation,
                EmailId = model.EmailId,
                MobileNo = model.MobileNo,
                IntercomResidence = model.IntercomResidence,
                IntercomOffice = model.IntercomOffice,
                DateOfBirth = model.DateOfBirth,
                DateOfRetirement = model.DateOfBirth.AddYears(60),
                BloodGroup = model.BloodGroup,
                QuarterNo = model.QuarterNo,
                QuarterType = model.QuarterType,
                Status = model.Status,
                CreatedAt = DateTime.Now
            };

            if (model.Photo != null && model.Photo.ContentLength > 0)
            {
                employee.PhotoPath = SavePhoto(model.Photo);
            }

            _db.EmployeeMasters.Add(employee);
            _db.SaveChanges();
            TempData["Success"] = "Employee added successfully.";
            return RedirectToAction("Index");
        }

        // GET: EmployeeMaster/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.ActiveMenu = "EmployeeMaster";

            if (id == null)
                return HttpNotFound();

            var employee = _db.EmployeeMasters.Find(id.Value);
            if (employee == null)
                return HttpNotFound();

            var model = new EmployeeMasterDto
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                EmployeeNo = employee.EmployeeNo,
                Department = employee.Department,
                Designation = employee.Designation,
                EmailId = employee.EmailId,
                MobileNo = employee.MobileNo,
                IntercomResidence = employee.IntercomResidence,
                IntercomOffice = employee.IntercomOffice,
                DateOfBirth = employee.DateOfBirth,
                BloodGroup = employee.BloodGroup,
                ExistingPhotoPath = employee.PhotoPath,
                QuarterNo = employee.QuarterNo,
                QuarterType = employee.QuarterType,
                Status = employee.Status
            };

            PopulateDropdowns();
            return View(model);
        }

        // POST: EmployeeMaster/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeMasterDto model)
        {
            ViewBag.ActiveMenu = "EmployeeMaster";

            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(model);
            }

            var employee = _db.EmployeeMasters.Find(model.Id);
            if (employee == null)
                return HttpNotFound();

            employee.EmployeeName = model.EmployeeName;
            employee.EmployeeNo = model.EmployeeNo;
            employee.Department = model.Department;
            employee.Designation = model.Designation;
            employee.EmailId = model.EmailId;
            employee.MobileNo = model.MobileNo;
            employee.IntercomResidence = model.IntercomResidence;
            employee.IntercomOffice = model.IntercomOffice;
            employee.DateOfBirth = model.DateOfBirth;
            employee.DateOfRetirement = model.DateOfBirth.AddYears(60);
            employee.BloodGroup = model.BloodGroup;
            employee.QuarterNo = model.QuarterNo;
            employee.QuarterType = model.QuarterType;
            employee.Status = model.Status;

            if (model.Photo != null && model.Photo.ContentLength > 0)
            {
                // Remove the old photo file, if any, before saving the new one
                DeletePhotoFile(employee.PhotoPath);
                employee.PhotoPath = SavePhoto(model.Photo);
            }
            // else: keep the existing PhotoPath untouched

            _db.SaveChanges();
            TempData["Success"] = "Employee updated successfully.";
            return RedirectToAction("Index");
        }

        // POST: EmployeeMaster/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var employee = _db.EmployeeMasters.Find(id);
            if (employee == null)
            {
                TempData["Error"] = "Employee not found.";
                return RedirectToAction("Index");
            }

            DeletePhotoFile(employee.PhotoPath);

            _db.EmployeeMasters.Remove(employee);
            _db.SaveChanges();

            TempData["Success"] = $"Employee '{employee.EmployeeName}' deleted successfully.";
            return RedirectToAction("Index");
        }

        private string SavePhoto(HttpPostedFileBase photo)
        {
            var folder = Server.MapPath("~/Content/uploads/employeemaster");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
            photo.SaveAs(Path.Combine(folder, fileName));
            return "/Content/uploads/employeemaster/" + fileName;
        }

        private void DeletePhotoFile(string photoPath)
        {
            if (string.IsNullOrEmpty(photoPath))
                return;

            try
            {
                var fullPath = Server.MapPath("~" + photoPath);
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
            }
            catch
            {
                // Non-critical: if the file can't be removed (e.g. locked, already gone),
                // we don't want to block the database update/delete.
            }
        }

        private void PopulateDropdowns()
        {
            ViewBag.QuarterTypes = new SelectList(QuarterTypes);
            ViewBag.StatusOptions = new SelectList(StatusOptions);
            ViewBag.BloodGroups = new SelectList(BloodGroups);
        }

        // GET: EmployeeMaster/ImportExcel
        [HttpGet]
        public ActionResult ImportExcel()
        {
            ViewBag.ActiveMenu = "EmployeeMasterImport";
            return View();
        }

        // POST: EmployeeMaster/ImportExcel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportExcel(HttpPostedFileBase excelFile)
        {
            ViewBag.ActiveMenu = "EmployeeMasterImport";

            if (excelFile == null || excelFile.ContentLength == 0)
            {
                ModelState.AddModelError("", "Please select an Excel file to upload.");
                return View();
            }

            var ext = Path.GetExtension(excelFile.FileName).ToLowerInvariant();
            if (ext != ".xlsx" && ext != ".xls")
            {
                ModelState.AddModelError("", "Only .xlsx or .xls files are allowed.");
                return View();
            }

            var result = new ExcelImportResultDto();

            using (var stream = new MemoryStream())
            {
                excelFile.InputStream.CopyTo(stream);

                using (var package = new ExcelPackage(stream))
                {
                    if (!package.Workbook.Worksheets.Any())
                    {
                        ModelState.AddModelError("", "The uploaded file has no worksheets.");
                        return View();
                    }

                    var worksheet = package.Workbook.Worksheets.First();
                    int rowCount = worksheet.Dimension?.Rows ?? 0;

                    if (rowCount < 2)
                    {
                        ModelState.AddModelError("", "The Excel file has no data rows.");
                        return View();
                    }

                    // Expected column order in row 1 (header row, ignored):
                    // 1 EmployeeName | 2 EmployeeNo | 3 Department | 4 Designation | 5 EmailId
                    // 6 MobileNo | 7 IntercomResidence | 8 IntercomOffice | 9 DateOfBirth
                    // 10 BloodGroup | 11 QuarterNo | 12 QuarterType | 13 Status

                    var seenInFile = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    for (int row = 2; row <= rowCount; row++)
                    {
                        string employeeName = worksheet.Cells[row, 1].Text.Trim();
                        string employeeNo = worksheet.Cells[row, 2].Text.Trim();

                        if (string.IsNullOrWhiteSpace(employeeName) && string.IsNullOrWhiteSpace(employeeNo))
                            continue; // fully blank row, skip silently

                        result.TotalRows++;

                        try
                        {
                            string department = worksheet.Cells[row, 3].Text.Trim();
                            string designation = worksheet.Cells[row, 4].Text.Trim();
                            string emailId = worksheet.Cells[row, 5].Text.Trim();
                            string mobileNo = worksheet.Cells[row, 6].Text.Trim();
                            string intercomResidence = worksheet.Cells[row, 7].Text.Trim();
                            string intercomOffice = worksheet.Cells[row, 8].Text.Trim();
                            string bloodGroup = worksheet.Cells[row, 10].Text.Trim();
                            string quarterNo = worksheet.Cells[row, 11].Text.Trim();
                            string quarterType = worksheet.Cells[row, 12].Text.Trim();
                            string status = worksheet.Cells[row, 13].Text.Trim();

                            if (string.IsNullOrWhiteSpace(employeeName)) throw new Exception("Employee Name is required.");
                            if (string.IsNullOrWhiteSpace(employeeNo)) throw new Exception("Employee No is required.");
                            if (string.IsNullOrWhiteSpace(department)) throw new Exception("Department is required.");
                            if (string.IsNullOrWhiteSpace(designation)) throw new Exception("Designation is required.");
                            if (string.IsNullOrWhiteSpace(quarterNo)) throw new Exception("Quarter No is required.");
                            if (string.IsNullOrWhiteSpace(quarterType)) throw new Exception("Quarter Type is required.");
                            if (string.IsNullOrWhiteSpace(status)) throw new Exception("Status is required.");

                            // Read the date directly from the cell instead of a culture-formatted string
                            DateTime dob = ParseExcelDate(worksheet.Cells[row, 9]);

                            if (!QuarterTypes.Contains(quarterType, StringComparer.OrdinalIgnoreCase))
                                throw new Exception($"Invalid Quarter Type '{quarterType}'. Allowed: {string.Join(", ", QuarterTypes)}.");

                            if (!StatusOptions.Contains(status, StringComparer.OrdinalIgnoreCase))
                                throw new Exception($"Invalid Status '{status}'. Allowed: {string.Join(", ", StatusOptions)}.");

                            if (!seenInFile.Add(employeeNo))
                                throw new Exception($"Duplicate Employee No '{employeeNo}' within the file (only first occurrence processed).");

                            var existing = _db.EmployeeMasters
                                .FirstOrDefault(e => e.EmployeeNo == employeeNo);

                            if (existing != null)
                            {
                                existing.EmployeeName = employeeName;
                                existing.Department = department;
                                existing.Designation = designation;
                                existing.EmailId = emailId;
                                existing.MobileNo = mobileNo;
                                existing.IntercomResidence = intercomResidence;
                                existing.IntercomOffice = intercomOffice;
                                existing.DateOfBirth = dob;
                                existing.DateOfRetirement = dob.AddYears(60);
                                existing.BloodGroup = bloodGroup;
                                existing.QuarterNo = quarterNo;
                                existing.QuarterType = quarterType;
                                existing.Status = status;

                                result.Updated++;
                            }
                            else
                            {
                                _db.EmployeeMasters.Add(new EmployeeMaster
                                {
                                    EmployeeName = employeeName,
                                    EmployeeNo = employeeNo,
                                    Department = department,
                                    Designation = designation,
                                    EmailId = emailId,
                                    MobileNo = mobileNo,
                                    IntercomResidence = intercomResidence,
                                    IntercomOffice = intercomOffice,
                                    DateOfBirth = dob,
                                    DateOfRetirement = dob.AddYears(60),
                                    BloodGroup = bloodGroup,
                                    QuarterNo = quarterNo,
                                    QuarterType = quarterType,
                                    Status = status,
                                    CreatedAt = DateTime.Now
                                });

                                result.Inserted++;
                            }
                        }
                        catch (Exception ex)
                        {
                            result.Skipped++;
                            result.Errors.Add($"Row {row}: {ex.Message}");
                        }
                    }

                    _db.SaveChanges();
                }
            }

            return View("ImportResult", result);
        }

        /// <summary>
        /// Reads a Date of Birth value from an Excel cell reliably, regardless of
        /// whether the cell is a true Excel date (numeric) or plain text, and
        /// regardless of the server's regional/culture settings.
        /// </summary>
        private DateTime ParseExcelDate(ExcelRange cell)
        {
            if (cell.Value is DateTime dt)
                return dt;

            if (cell.Value is double oaDate)
                return DateTime.FromOADate(oaDate);

            string text = cell.Text?.Trim();
            if (string.IsNullOrWhiteSpace(text))
                throw new Exception("Date of Birth is required.");

            string[] formats =
            {
                "MM/dd/yyyy", "M/d/yyyy", "dd/MM/yyyy", "d/M/yyyy",
                "yyyy-MM-dd", "dd-MM-yyyy", "MM-dd-yyyy"
            };

            if (DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime parsed))
                return parsed;

            if (DateTime.TryParse(text, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out parsed))
                return parsed;

            throw new Exception($"Invalid Date of Birth '{text}'.");
        }

        // GET: EmployeeMaster/DownloadTemplate
        [HttpGet]
        public ActionResult DownloadTemplate()
        {
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Employees");
                string[] headers =
                {
                    "EmployeeName", "EmployeeNo", "Department", "Designation", "EmailId",
                    "MobileNo", "IntercomResidence", "IntercomOffice", "DateOfBirth",
                    "BloodGroup", "QuarterNo", "QuarterType", "Status"
                };

                for (int i = 0; i < headers.Length; i++)
                    sheet.Cells[1, i + 1].Value = headers[i];

                sheet.Cells[2, 1].Value = "John Doe";
                sheet.Cells[2, 2].Value = "EMP001";
                sheet.Cells[2, 3].Value = "Engineering";
                sheet.Cells[2, 4].Value = "Manager";
                sheet.Cells[2, 5].Value = "john.doe@example.com";
                sheet.Cells[2, 6].Value = "9876543210";
                sheet.Cells[2, 7].Value = "101";
                sheet.Cells[2, 8].Value = "202";
                sheet.Cells[2, 9].Value = "01/15/1985";
                sheet.Cells[2, 10].Value = "O+";
                sheet.Cells[2, 11].Value = "12";
                sheet.Cells[2, 12].Value = "A";
                sheet.Cells[2, 13].Value = "Active";

                sheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                return File(stream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "EmployeeMaster_Template.xlsx");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}