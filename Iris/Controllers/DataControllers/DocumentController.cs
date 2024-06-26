﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Iris.DAL;
using Iris.Filters;
using Iris.Models;
using Iris.BPM;
using Iris.Notification;
using Iris.Services;

namespace Iris.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class DocumentController : Controller
    {
        private readonly IrisDBEntities db;

        private USER_ADMINS loggedAdmin;
        private USER_SCHOOLS loggedSchool;

        private const string UPLOAD_PATH = "~/Uploads/";

        private readonly IUploadService uploadService;

        public DocumentController(IrisDBEntities entities, IUploadService uploadService)
        {
            db = entities;
            this.uploadService = uploadService;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        #region --- ADMIN AREA ---

        #region UPLOAD-DOWNLOAD FILES (ADMIN AREA)

        public ActionResult xUploadData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            // Combos populators
            PopulateSchoolYears();

            return View();
        }

        #region MASTER GRID CRUD FUNCTIONS

        public ActionResult xUpload_Read([DataSourceRequest] DataSourceRequest request, int schoolId = 0)
        {
            List<UploadsViewModel> data = uploadService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult xUpload_Create([DataSourceRequest] DataSourceRequest request, UploadsViewModel data, int schoolId = 0)
        {
            var newdata = new UploadsViewModel();

            if (!(schoolId > 0))
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε σχολή ΕΠΑΣ. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                uploadService.Create(data, schoolId);
                newdata = uploadService.Refresh(data.UPLOAD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult xUpload_Update([DataSourceRequest] DataSourceRequest request, UploadsViewModel data, int schoolId = 0)
        {
            var newdata = new UploadsViewModel();

            if (!(schoolId > 0))
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε σχολή ΕΠΑΣ. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                uploadService.Update(data, schoolId);
                newdata = uploadService.Refresh(data.UPLOAD_ID);
            }

            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult xUpload_Destroy([DataSourceRequest] DataSourceRequest request, UploadsViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteUpload(data.UPLOAD_ID))
                {
                    uploadService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Για να γίνει διαγραφή πρέπει πρώτα να διαγραφούν τα σχετικά αρχεία μεταφόρτωσης");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CHILD GRID (UPLOADED FILEDETAILS)

        public ActionResult UploadFiles_Read([DataSourceRequest] DataSourceRequest request, int uploadId = 0)
        {
            List<UploadsFilesViewModel> data = uploadService.GetFiles(uploadId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFiles_Destroy([DataSourceRequest] DataSourceRequest request, UploadsFilesViewModel data)
        {
            // TODO: ALSO REMOVE UPLOADED FILES FROM SERVER
            if (data != null)
            {
                // First delete the physical file and then the info record. Important!
                DeleteUploadedFile(data.ID);

                uploadService.DeleteFile(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region UPLOAD FORM WITH SAVE-REMOVE ACTIONS

        public ActionResult xUploadForm(int uploadId, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);
            if (!(uploadId > 0))
            {
                string msg = "Άκυρος κωδικός μεταφόρτωσης. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή μεταφόρτωσης.";
                return RedirectToAction("ErrorData", "Document", new { notify = msg });
            }
            ViewData["uploadId"] = uploadId;

            return View();
        }

        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, int uploadId = 0)
        {
            string folder = "";
            string uploadPath = UPLOAD_PATH;
            string subfolder = "";

            List<UploadsFilesViewModel> fileDetails = new List<UploadsFilesViewModel>();

            var upload_info = Common.GetUploadInfo(uploadId);    // returns a tuple with SCHOOL_ID and SCHOOLYEAR_ID
            folder = Common.GetSchoolUsername(upload_info.Item1);
            subfolder = Common.GetSchoolYearText(upload_info.Item2);

            if (!String.IsNullOrEmpty(folder) && !String.IsNullOrEmpty(subfolder))
                uploadPath += folder + "/" + subfolder + "/";
            try
            {
                bool exists = System.IO.Directory.Exists(Server.MapPath(uploadPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(uploadPath));

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        // Some browsers send file names with full path.
                        // We are only interested in the file name.
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            UPLOADS_FILES fileDetail = new UPLOADS_FILES()
                            {
                                FILENAME = fileName.Length > 120 ? fileName.Substring(0, 120) : fileName,
                                EXTENSION = Path.GetExtension(fileName),
                                SCHOOL_USER = folder,
                                SCHOOLYEAR_TEXT = subfolder,
                                UPLOAD_ID = uploadId,
                                ID = Guid.NewGuid()
                            };
                            db.UPLOADS_FILES.Add(fileDetail);
                            db.SaveChanges();

                            var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileDetail.ID + fileDetail.EXTENSION);
                            file.SaveAs(physicalPath);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "Παρουσιάστηκε σφάλμα στη μεταφόρτωση:<br/>" + ex.Message;
                return Content(msg);
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult Remove(string[] fileNames, int uploadId)
        {
            // The parameter of the Remove action must be called "fileNames"
            string folder = "";
            string uploadPath = UPLOAD_PATH;
            string subfolder = "";

            var upload_info = Common.GetUploadInfo(uploadId);    // returns a tuple with SCHOOL_ID and SCHOOLYEAR_ID
            folder = Common.GetSchoolUsername(upload_info.Item1);
            subfolder = Common.GetSchoolYearText(upload_info.Item2);

            if (!String.IsNullOrEmpty(folder) && !String.IsNullOrEmpty(subfolder))
                uploadPath += folder + "/" + subfolder + "/";

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var extension = Path.GetExtension(fileName);

                    Guid file_guid = Common.GetFileGuidFromName(fileName, uploadId);

                    string fileToDelete = file_guid + extension;
                    var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileToDelete);

                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                        DeleteUploadFileRecord(file_guid);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        #endregion

        public FileResult Download(Guid file_id)
        {
            String p = "";
            String f = "";
            string the_path = UPLOAD_PATH;

            var fileinfo = (from d in db.UPLOADS_FILES where d.ID == file_id select d).FirstOrDefault();
            if (fileinfo != null)
            {
                the_path += fileinfo.SCHOOL_USER + "/" + fileinfo.SCHOOLYEAR_TEXT + "/";
                p = fileinfo.ID.ToString() + fileinfo.EXTENSION;
                f = fileinfo.FILENAME;
            }

            return File(Path.Combine(Server.MapPath(the_path), p), System.Net.Mime.MediaTypeNames.Application.Octet, f);
        }

        public ActionResult DeleteUploadFileRecord(Guid file_guid)
        {
            UPLOADS_FILES entity = db.UPLOADS_FILES.Find(file_guid);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.UPLOADS_FILES.Remove(entity);
                db.SaveChanges();
            }
            return Content("");
        }

        public ActionResult DeleteUploadedFile(Guid file_guid)
        {
            string folder = "";
            string uploadPath = UPLOAD_PATH;
            string subfolder = "";
            string extension = "";

            var data = (from d in db.UPLOADS_FILES where d.ID == file_guid select d).FirstOrDefault();
            if (data != null)
            {
                folder = data.SCHOOL_USER;
                subfolder = data.SCHOOLYEAR_TEXT;
                extension = data.EXTENSION;

                if (!String.IsNullOrEmpty(folder) && !String.IsNullOrEmpty(subfolder))
                    uploadPath += folder + "/" + subfolder + "/";

                string fileToDelete = file_guid + extension;
                var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileToDelete);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }

        #endregion UPLOAD-DOWNLOAD FILES (ADMIN AREA)

        #endregion


        #region --- SCHOOL AREA ---

        #region UPLOAD-DOWNLOAD FILES (SCHOOL AREA)

        public ActionResult UploadData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            // Combos populators
            PopulateSchoolYears();

            return View();
        }

        #region MASTER GRID CRUD FUNCTIONS

        public ActionResult Upload_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<UploadsViewModel> data = uploadService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Create([DataSourceRequest] DataSourceRequest request, UploadsViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new UploadsViewModel();

            if (!(schoolId > 0))
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε βρεφονηπιακό σταθμό. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                uploadService.Create(data, schoolId);
                newdata = uploadService.Refresh(data.UPLOAD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Update([DataSourceRequest] DataSourceRequest request, UploadsViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new UploadsViewModel();

            if (!(schoolId > 0))
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε σχολή ΕΠΑΣ. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                uploadService.Update(data, schoolId);
                newdata = uploadService.Refresh(data.UPLOAD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Destroy([DataSourceRequest] DataSourceRequest request, UploadsViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteUpload(data.UPLOAD_ID))
                {
                    uploadService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Για να γίνει διαγραφή πρέπει πρώτα να διαγραφούν τα σχετικά αρχεία μεταφόρτωσης");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        // All related functions are common with those in the admin section
        public ActionResult UploadForm(int uploadId, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);
            if (!(uploadId > 0))
            {
                string msg = "Άκυρος κωδικός μεταφόρτωσης. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή μεταφόρτωσης.";
                return RedirectToAction("ErrorData", "Document", new { notify = msg });
            }
            ViewData["uploadId"] = uploadId;

            return View();
        }

        #endregion UPLOAD-DOWNLOAD FILES (SCHOOL AREA)

        #endregion


        #region POPULATORS

        public void PopulateEidikotites()
        {
            var data = (from d in db.sqlEIDIKOTITES orderby d.ΚΛΑΔΟΣ, d.ΕΙΔΙΚΟΤΗΤΑ select d).ToList();
            ViewData["eidikotites"] = data;
            ViewData["defaultEidikotita"] = data.First().ΚΩΔΙΚΟΣ;
        }

        public void PopulatePeriferiakes()
        {
            var data = (from d in db.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ select d).ToList();
            ViewData["periferiakes"] = data;
        }

        public void PopulateSchoolDomes()
        {
            var data = (from d in db.ΣΥΣ_ΕΚΠΑΙΔΕΥΤΙΚΕΣ_ΔΟΜΕΣ select d).ToList();
            ViewData["schoolDomes"] = data;
        }

        public void PopulateKladoi()
        {
            var kladosTypes = (from k in db.ΣΥΣ_ΚΛΑΔΟΙ select k).ToList();

            ViewData["kladoi"] = kladosTypes;
        }

        public void PopulateGenders()
        {
            var data = (from d in db.ΣΥΣ_ΦΥΛΑ select d).ToList();
            ViewData["genders"] = data;
        }

        public void PopulateSchoolYears()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).ToList();
            ViewData["schoolYears"] = data;
        }

        public void PopulateSchools()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ select d).ToList();
            ViewData["schools"] = data;
        }

        #endregion


        #region GETTERS

        public JsonResult GetSchools()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ
                        where d.ΔΟΜΗ == 1 || d.ΔΟΜΗ == 2
                        orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ
                        select new SchoolsViewModel
                        {
                            ΣΧΟΛΗ_ΚΩΔ = d.ΣΧΟΛΗ_ΚΩΔ,
                            ΕΠΩΝΥΜΙΑ = d.ΕΠΩΝΥΜΙΑ
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchoolYears()
        {
            var data = db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Select(m => new SysSchoolYearViewModel
            {
                SCHOOLYEAR_ID = m.SCHOOLYEAR_ID,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = m.ΣΧΟΛΙΚΟ_ΕΤΟΣ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public USER_ADMINS GetLoginAdmin()
        {
            loggedAdmin = db.USER_ADMINS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FULLNAME;
            return loggedAdmin;
        }

        public USER_SCHOOLS GetLoginSchool()
        {
            loggedSchool = db.USER_SCHOOLS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            int SchoolID = loggedSchool.USER_SCHOOLID ?? 0;
            var _school = (from s in db.sqlUSER_SCHOOL
                           where s.USER_SCHOOLID == SchoolID
                           select new { s.ΕΠΩΝΥΜΙΑ }).FirstOrDefault();

            ViewBag.loggedUser = _school.ΕΠΩΝΥΜΙΑ;
            return loggedSchool;
        }

        #endregion


        #region ERROR PAGES

        public ActionResult Error(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult ErrorData(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        #endregion

    }
}