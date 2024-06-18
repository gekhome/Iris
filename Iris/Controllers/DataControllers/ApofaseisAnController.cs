using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using Iris.ServicesApofaseis;

namespace Iris.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class ApofaseisAnController : ControllerUnit
    {
        private USER_ADMINS loggedAdmin;
        private readonly IrisDBEntities db;

        private readonly IAnathesiInitialAnService anathesiInitialAnService;
        private readonly IAnathesiSupplementAnService anathesiSupplementAnService;
        private readonly IAnathesiCancelAnService anathesiCancelAnService;
        private readonly IAnathesiRevokeAnService anathesiRevokeAnService;
        private readonly IAnathesiModifyAnService anathesiModifyAnService;
        private readonly IAnathesiModifyAnAKService anathesiModifyAnAKService;

        private readonly IApofasiInitialAnService apofasiInitialAnService;
        private readonly IApofasiSupplementAnService apofasiSupplementAnService;
        private readonly IApofasiCancelAnService apofasiCancelAnService;
        private readonly IApofasiRevokeAnService apofasiRevokeAnService;
        private readonly IApofasiCorrectAnService apofasiCorrectAnService;
        private readonly IApofasiModifyAnService apofasiModifyAnService;
        private readonly IApofasiModifyAnAKService apofasiModifyAnAKService;

        public ApofaseisAnController(IrisDBEntities entities, IAnathesiInitialAnService anathesiInitialAnService,
            IAnathesiSupplementAnService anathesiSupplementAnService, IAnathesiCancelAnService anathesiCancelAnService,
            IAnathesiRevokeAnService anathesiRevokeAnService, IAnathesiModifyAnService anathesiModifyAnService,
            IAnathesiModifyAnAKService anathesiModifyAnAKService, IApofasiInitialAnService apofasiInitialAnService,
            IApofasiSupplementAnService apofasiSupplementAnService, IApofasiCancelAnService apofasiCancelAnService,
            IApofasiRevokeAnService apofasiRevokeAnService, IApofasiCorrectAnService apofasiCorrectAnService,
            IApofasiModifyAnService apofasiModifyAnService, IApofasiModifyAnAKService apofasiModifyAnAKService) : base(entities)
        {
            db = entities;

            this.anathesiInitialAnService = anathesiInitialAnService;
            this.anathesiSupplementAnService = anathesiSupplementAnService;
            this.anathesiCancelAnService = anathesiCancelAnService;
            this.anathesiRevokeAnService = anathesiRevokeAnService;
            this.anathesiModifyAnService = anathesiModifyAnService;
            this.anathesiModifyAnAKService = anathesiModifyAnAKService;

            this.apofasiInitialAnService = apofasiInitialAnService;
            this.apofasiSupplementAnService = apofasiSupplementAnService;
            this.apofasiCancelAnService = apofasiCancelAnService;
            this.apofasiRevokeAnService = apofasiRevokeAnService;
            this.apofasiCorrectAnService = apofasiCorrectAnService;
            this.apofasiModifyAnService = apofasiModifyAnService;
            this.apofasiModifyAnAKService = apofasiModifyAnAKService;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        // ----------------
        // ΑΝΑΠΛΗΡΩΤΕΣ ΕΠΑΣ
        // ----------------


        #region ΑΡΧΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult ApofaseisInitialAnapl(string notify = null)
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

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(EPAS);
            PopulateSchools(EPAS);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiInitialAnapl_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiInitialAnService.Read(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiInitialAnapl_Create([DataSourceRequest] DataSourceRequest request, ApofasiInitialAnaplirotesGridViewModel data)
        {
            ApofasiInitialAnaplirotesGridViewModel newdata = new ApofasiInitialAnaplirotesGridViewModel();

            if (!Kerberos.CanCreateApofasiInitialAnapl((int)data.ΣΧΟΛΗ, (int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ))
            {
                ModelState.AddModelError("", "Υπάρχει ήδη αρχική απόφαση αναπληρωτών για αυτό το σχολείο και σχολικό έτος. Η δημιουργία της απόφασης ακυρώθηκε.");
            }
            if (data != null && ModelState.IsValid)
            {
                apofasiInitialAnService.Create(data);
                newdata = apofasiInitialAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiInitialAnapl_Update([DataSourceRequest] DataSourceRequest request, ApofasiInitialAnaplirotesGridViewModel data)
        {
            ApofasiInitialAnaplirotesGridViewModel newdata = new ApofasiInitialAnaplirotesGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiInitialAnService.Update(data);
                newdata = apofasiInitialAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiInitialAnapl_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiInitialAnaplirotesGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiInitialAnapl(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiInitialAnService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της απόφασης διότι έχει συνημμένες αναθέσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ANATHESEIS GRID

        public ActionResult AnatheseisInitialAnapl_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiInitialAnService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisInitialAnapl_Update([DataSourceRequest] DataSourceRequest request, AnathesiInitialAnaplirotesViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiInitialAnaplirotesViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiInitialAnService.Update(data, ap);
                    newdata = anathesiInitialAnService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisInitialAnapl_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiInitialAnaplirotesViewModel data)
        {
            if (data != null)
            {
                anathesiInitialAnService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΕΠΙΣΥΝΑΨΗ ΑΝΑΘΕΣΕΩΝ

        // --------------------------------------------------
        // Ουσιαστικά, η επισύναψη παίρνει μια ανάθεση ορφανή
        // και της δίνει τον κωδικό της επιλεγμένης απόφασης
        // --------------------------------------------------
        public ActionResult ApofasiInitialAnaplAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν αρχικές αναθέσεις αναπληρωτών για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiInitialAnaplAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΑΡΧΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ - ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult ApofasiInitialAnaplEdit(int apofasiId)
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

            ApofasiInitialAnaplirotesViewModel data = apofasiInitialAnService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "ApofaseisAn", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultInitialAnaplFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiInitialAnaplEdit(int apofasiId, ApofasiInitialAnaplirotesViewModel model)
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

            string ErrorMsg = ValidateApofasiInitialAnaplFields(model);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                apofasiInitialAnService.UpdateRecord(model, apofasiId, EPAS);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiInitialAnaplirotesViewModel newApofasi = apofasiInitialAnService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        public string ValidateApofasiInitialAnaplFields(ApofasiInitialAnaplirotesViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null)
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ))
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null)
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS))
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiInitialAnaplirotesViewModel SetDefaultInitialAnaplFields(ApofasiInitialAnaplirotesViewModel data)
        {
            var data1 = (from d in db.ΣΥΣ_ΕΠΙΤΡΟΠΕΣ_ΑΒ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ && d.ΠΕΡΙΦΕΡΕΙΑΚΗ == data.ΠΕΡΙΦΕΡΕΙΑΚΗ select d).FirstOrDefault();
            if (data1 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΠΡΩΤΟΒΑΘΜΙΑ = data1.ΠΡΩΤΟΒΑΘΜΙΑ;
                data.ΑΠΟΦΑΣΗ_ΔΕΥΤΕΡΟΒΑΘΜΙΑ = data1.ΔΕΥΤΕΡΟΒΑΘΜΙΑ;
            }

            var data2 = (from d in db.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ = data2.ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ;
                data.ΠΥΣ_ΑΡΘΡΟ = data2.ΠΥΣ_ΑΡΘΡΟ;
                data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data2.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data2.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
                data.ΛΕΚΤΙΚΟ_ΥΠΟΥΡΓΟΥ = data2.ΛΕΚΤΙΚΟ_ΥΠΟΥΡΓΟΥ;
            }

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΡΧΙΚΗ-ΑΝ";

            return data;
        }

        #endregion

        #endregion ΑΡΧΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult ApofaseisSupplementAnapl(string notify = null)
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

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(EPAS);
            PopulateSchools(EPAS);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiSupplementAnapl_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiSupplementAnService.Read(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSupplementAnapl_Create([DataSourceRequest] DataSourceRequest request, ApofasiSupplementAnaplirotesGridViewModel data)
        {
            ApofasiSupplementAnaplirotesGridViewModel newdata = new ApofasiSupplementAnaplirotesGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSupplementAnService.Create(data);
                newdata = apofasiSupplementAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSupplementAnapl_Update([DataSourceRequest] DataSourceRequest request, ApofasiSupplementAnaplirotesGridViewModel data)
        {
            ApofasiSupplementAnaplirotesGridViewModel newdata = new ApofasiSupplementAnaplirotesGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSupplementAnService.Update(data);
                newdata = apofasiSupplementAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSupplementAnapl_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiSupplementAnaplirotesGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiSupplementAnapl(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiSupplementAnService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της απόφασης διότι έχει συνημμένες αναθέσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ANATHESEIS GRID

        public ActionResult AnatheseisSupplementAnapl_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiSupplementAnService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisSupplementAnapl_Update([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAnaplirotesViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiSupplementAnaplirotesViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiSupplementAnService.Update(data, ap);
                    newdata = anathesiSupplementAnService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisSupplementAnapl_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAnaplirotesViewModel data)
        {
            if (data != null)
            {
                anathesiSupplementAnService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΕΠΙΣΥΝΑΨΗ ΑΝΑΘΕΣΕΩΝ

        // --------------------------------------------------
        // Ουσιαστικά, η επισύναψη παίρνει μια ανάθεση ορφανή
        // και της δίνει τον κωδικό της επιλεγμένης απόφασης
        // --------------------------------------------------
        public ActionResult ApofasiSupplementAnaplAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συμπληρωματικές αναθέσεις αναπληρωτών για αυτό το σχολείο και σχολ. έτος, για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiSupplementAnaplAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ - ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult ApofasiSupplementAnaplEdit(int apofasiId)
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

            ApofasiSupplementAnaplirotesViewModel data = apofasiSupplementAnService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "ApofaseisAn", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultSupplementAnaplFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiSupplementAnaplEdit(int apofasiId, ApofasiSupplementAnaplirotesViewModel model)
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

            string ErrorMsg = ValidateApofasiSupplementAnaplFields(model);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                apofasiSupplementAnService.UpdateRecord(model, apofasiId, EPAS);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiSupplementAnaplirotesViewModel newApofasi = apofasiSupplementAnService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        public string ValidateApofasiSupplementAnaplFields(ApofasiSupplementAnaplirotesViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null)
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ))
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null)
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS))
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiSupplementAnaplirotesViewModel SetDefaultSupplementAnaplFields(ApofasiSupplementAnaplirotesViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiAnaplProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            var data2 = (from d in db.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data2.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data2.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
            }

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ-ΑΝ";

            return data;
        }

        #endregion

        #endregion ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΑΚΥΡΩΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult ApofaseisCancelAnapl(string notify = null)
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

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(EPAS);
            PopulateSchools(EPAS);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiCancelAnapl_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiCancelAnService.Read(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCancelAnapl_Create([DataSourceRequest] DataSourceRequest request, ApofasiCancelAnaplirotesGridViewModel data)
        {
            ApofasiCancelAnaplirotesGridViewModel newdata = new ApofasiCancelAnaplirotesGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiCancelAnService.Create(data);
                newdata = apofasiCancelAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCancelAnapl_Update([DataSourceRequest] DataSourceRequest request, ApofasiCancelAnaplirotesGridViewModel data)
        {
            ApofasiCancelAnaplirotesGridViewModel newdata = new ApofasiCancelAnaplirotesGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiCancelAnService.Update(data);
                newdata = apofasiCancelAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCancelAnapl_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiCancelAnaplirotesGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiCancelAnapl(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiCancelAnService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της απόφασης διότι έχει συνημμένες αναθέσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ANATHESEIS GRID

        public ActionResult AnatheseisCancelAnapl_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiCancelAnService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisCancelAnapl_Update([DataSourceRequest] DataSourceRequest request, AnathesiCancelAnaplirotesViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiCancelAnaplirotesViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiCancelAnService.Update(data, ap);
                    newdata = anathesiCancelAnService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisCancelAnapl_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiCancelAnaplirotesViewModel data)
        {
            if (data != null)
            {
                anathesiCancelAnService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΕΠΙΣΥΝΑΨΗ ΑΝΑΘΕΣΕΩΝ

        // --------------------------------------------------
        // Ουσιαστικά, η επισύναψη παίρνει μια ανάθεση ορφανή
        // και της δίνει τον κωδικό της επιλεγμένης απόφασης
        // --------------------------------------------------
        public ActionResult ApofasiCancelAnaplAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν ακυρωτικές αναθέσεις αναπληρωτών για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiCancelAnaplAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΑΚΥΡΩΤΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult ApofasiCancelAnaplEdit(int apofasiId)
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

            ApofasiCancelAnaplirotesViewModel data = apofasiCancelAnService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "ApofaseisAn", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultCancelAnaplFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiCancelAnaplEdit(int apofasiId, ApofasiCancelAnaplirotesViewModel model)
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

            string ErrorMsg = ValidateApofasiCancelAnaplFields(model);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                apofasiCancelAnService.UpdateRecord(model, apofasiId, EPAS);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiCancelAnaplirotesViewModel newApofasi = apofasiCancelAnService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        public string ValidateApofasiCancelAnaplFields(ApofasiCancelAnaplirotesViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null)
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ))
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null)
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS))
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiCancelAnaplirotesViewModel SetDefaultCancelAnaplFields(ApofasiCancelAnaplirotesViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiAnaplProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            var data2 = (from d in db.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data2.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data2.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
            }

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΚΥΡΩΤΙΚΗ-ΑΝ";

            return data;
        }

        #endregion

        #endregion ΑΚΥΡΩΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΑΝΑΚΛΗΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult ApofaseisRevokeAnapl(string notify = null)
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

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(EPAS);
            PopulateSchools(EPAS);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiRevokeAnapl_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiRevokeAnService.Read(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiRevokeAnapl_Create([DataSourceRequest] DataSourceRequest request, ApofasiRevokeAnaplirotesGridViewModel data)
        {
            ApofasiRevokeAnaplirotesGridViewModel newdata = new ApofasiRevokeAnaplirotesGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiRevokeAnService.Create(data);
                newdata = apofasiRevokeAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiRevokeAnapl_Update([DataSourceRequest] DataSourceRequest request, ApofasiRevokeAnaplirotesGridViewModel data)
        {
            ApofasiRevokeAnaplirotesGridViewModel newdata = new ApofasiRevokeAnaplirotesGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiRevokeAnService.Update(data);
                newdata = apofasiRevokeAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiRevokeAnapl_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiRevokeAnaplirotesGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiRevokeAnapl(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiRevokeAnService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της απόφασης διότι έχει συνημμένες αναθέσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ANATHESEIS GRID

        public ActionResult AnatheseisRevokeAnapl_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiRevokeAnService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisRevokeAnapl_Update([DataSourceRequest] DataSourceRequest request, AnathesiRevokeAnaplirotesViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiRevokeAnaplirotesViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiRevokeAnService.Update(data, ap);
                    newdata = anathesiRevokeAnService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisRevokeAnapl_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiRevokeAnaplirotesViewModel data)
        {
            if (data != null)
            {
                anathesiRevokeAnService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΕΠΙΣΥΝΑΨΗ ΑΝΑΘΕΣΕΩΝ

        // --------------------------------------------------
        // Ουσιαστικά, η επισύναψη παίρνει μια ανάθεση ορφανή
        // και της δίνει τον κωδικό της επιλεγμένης απόφασης
        // --------------------------------------------------
        public ActionResult ApofasiRevokeAnaplAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν αναθέσεις ανάκλησης αναπληρωτών για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiRevokeAnaplAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΑΝΑΚΛΗΤΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult ApofasiRevokeAnaplEdit(int apofasiId)
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

            ApofasiRevokeAnaplirotesViewModel data = apofasiRevokeAnService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "ApofaseisAn", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultRevokeAnaplFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiRevokeAnaplEdit(int apofasiId, ApofasiRevokeAnaplirotesViewModel model)
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

            string ErrorMsg = ValidateApofasiRevokeAnaplFields(model);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                apofasiRevokeAnService.UpdateRecord(model, apofasiId, EPAS);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiRevokeAnaplirotesViewModel newApofasi = apofasiRevokeAnService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        public string ValidateApofasiRevokeAnaplFields(ApofasiRevokeAnaplirotesViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null)
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ))
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null)
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS))
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiRevokeAnaplirotesViewModel SetDefaultRevokeAnaplFields(ApofasiRevokeAnaplirotesViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiAnaplProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            var data2 = (from d in db.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data2.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data2.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
            }

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΝΑΚΛΗΣΗ-ΑΝ";

            return data;
        }


        #endregion

        #endregion ΑΝΑΚΛΗΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΔΙΟΡΘΩΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult ApofaseisCorrectAnapl(string notify = null)
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

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(EPAS);
            PopulateSchools(EPAS);
            //populateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiCorrectAnapl_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiCorrectAnService.Read(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCorrectAnapl_Create([DataSourceRequest] DataSourceRequest request, ApofasiCorrectAnaplirotesGridViewModel data)
        {
            ApofasiCorrectAnaplirotesGridViewModel newdata = new ApofasiCorrectAnaplirotesGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiCorrectAnService.Create(data);
                newdata = apofasiCorrectAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCorrectAnapl_Update([DataSourceRequest] DataSourceRequest request, ApofasiCorrectAnaplirotesGridViewModel data)
        {
            ApofasiCorrectAnaplirotesGridViewModel newdata = new ApofasiCorrectAnaplirotesGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiCorrectAnService.Update(data);
                newdata = apofasiCorrectAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCorrectAnapl_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiCorrectAnaplirotesGridViewModel data)
        {
            if (data != null)
            {
                apofasiCorrectAnService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ΔΙΟΡΘΩΤΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult ApofasiCorrectAnaplEdit(int apofasiId)
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

            ApofasiCorrectAnaplirotesViewModel data = apofasiCorrectAnService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "ApofaseisAn", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultCorrectAnaplFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiCorrectAnaplEdit(int apofasiId, ApofasiCorrectAnaplirotesViewModel model)
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

            string ErrorMsg = ValidateApofasiCorrectAnaplFields(model);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                apofasiCorrectAnService.UpdateRecord(model, apofasiId, EPAS);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiCorrectAnaplirotesViewModel newApofasi = apofasiCorrectAnService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        public string ValidateApofasiCorrectAnaplFields(ApofasiCorrectAnaplirotesViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null)
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ))
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null)
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS))
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiCorrectAnaplirotesViewModel SetDefaultCorrectAnaplFields(ApofasiCorrectAnaplirotesViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiAnaplProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            var data2 = (from d in db.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data2.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data2.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
            }

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΔΙΟΡΘΩΤΙΚΗ-ΑΝ";

            return data;
        }

        #endregion

        #endregion ΔΙΟΡΘΩΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult ApofaseisModifyAnapl(string notify = null)
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

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(EPAS);
            PopulateSchools(EPAS);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiModifyAN_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiModifyAnService.Read(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModifyAN_Create([DataSourceRequest] DataSourceRequest request, ApofasiModifyAnaplirotesGridViewModel data)
        {
            ApofasiModifyAnaplirotesGridViewModel newdata = new ApofasiModifyAnaplirotesGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiModifyAnService.Create(data);
                newdata = apofasiModifyAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModifyAN_Update([DataSourceRequest] DataSourceRequest request, ApofasiModifyAnaplirotesGridViewModel data)
        {
            ApofasiModifyAnaplirotesGridViewModel newdata = new ApofasiModifyAnaplirotesGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiModifyAnService.Update(data);
                newdata = apofasiModifyAnService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModifyAN_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiModifyAnaplirotesGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiModifyAnapl(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiModifyAnService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της απόφασης διότι έχει συνημμένες αναθέσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ANATHESEIS GRID

        public ActionResult AnatheseisModifyAN_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiModifyAnService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisModifyAN_Update([DataSourceRequest] DataSourceRequest request, AnathesiModifyAnaplirotesViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiModifyAnaplirotesViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyAnService.Update(data, ap);
                    newdata = anathesiModifyAnService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisModifyAN_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiModifyAnaplirotesViewModel data)
        {
            if (data != null)
            {
                anathesiModifyAnService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΕΠΙΣΥΝΑΨΗ ΑΝΑΘΕΣΕΩΝ

        // --------------------------------------------------
        // Ουσιαστικά, η επισύναψη παίρνει μια ανάθεση ορφανή
        // και της δίνει τον κωδικό της επιλεγμένης απόφασης
        // --------------------------------------------------
        public ActionResult ApofasiModifyAnaplAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν τροποποιητικές αναθέσεις για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiModifyAnaplAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΤΡΟΠΟΠΟΙΗΤΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult ApofasiModifyAnaplEdit(int apofasiId)
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

            ApofasiModifyAnaplirotesViewModel data = apofasiModifyAnService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "ApofaseisAn", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultModifyAnaplFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiModifyAnaplEdit(int apofasiId, ApofasiModifyAnaplirotesViewModel model)
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

            string ErrorMsg = ValidateApofasiModifyAnaplFields(model);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                apofasiModifyAnService.UpdateRecord(model, apofasiId, EPAS);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiModifyAnaplirotesViewModel newApofasi = apofasiModifyAnService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        public string ValidateApofasiModifyAnaplFields(ApofasiModifyAnaplirotesViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null)
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ))
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null)
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS))
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiModifyAnaplirotesViewModel SetDefaultModifyAnaplFields(ApofasiModifyAnaplirotesViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiAnaplProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            var data2 = (from d in db.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data2.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data2.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
            }

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ-ΑΝ";

            return data;
        }

        #endregion

        #endregion ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΗΣ - ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult ApofaseisModifyAnaplAK(string notify = null)
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

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(EPAS);
            PopulateSchools(EPAS);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiModifyANAK_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiModifyAnAKService.Read(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModifyANAK_Create([DataSourceRequest] DataSourceRequest request, ApofasiModifyAnaplirotesAKGridViewModel data)
        {
            ApofasiModifyAnaplirotesAKGridViewModel newdata = new ApofasiModifyAnaplirotesAKGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiModifyAnAKService.Create(data);
                newdata = apofasiModifyAnAKService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModifyANAK_Update([DataSourceRequest] DataSourceRequest request, ApofasiModifyAnaplirotesAKGridViewModel data)
        {
            ApofasiModifyAnaplirotesAKGridViewModel newdata = new ApofasiModifyAnaplirotesAKGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiModifyAnAKService.Update(data);
                newdata = apofasiModifyAnAKService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModifyANAK_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiModifyAnaplirotesAKGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiModifyAnaplAK(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiModifyAnAKService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της απόφασης διότι έχει συνημμένες αναθέσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ANATHESEIS GRID

        public ActionResult AnatheseisModifyANAK_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiModifyAnAKService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisModifyANAK_Update([DataSourceRequest] DataSourceRequest request, AnathesiModifyAnaplirotesAKViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiModifyAnaplirotesAKViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyAnAKService.Update(data, ap);
                    newdata = anathesiModifyAnAKService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisModifyANAK_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiModifyAnaplirotesAKViewModel data)
        {
            if (data != null)
            {
                anathesiModifyAnAKService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΕΠΙΣΥΝΑΨΗ ΑΝΑΘΕΣΕΩΝ

        // --------------------------------------------------
        // Ουσιαστικά, η επισύναψη παίρνει μια ανάθεση ορφανή
        // και της δίνει τον κωδικό της επιλεγμένης απόφασης
        // --------------------------------------------------
        public ActionResult ApofasiModifyAnaplAKAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν τροποποιητικές αναθέσεις Α.Κ. αναπληρωτών για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ target = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiModifyAnaplAKAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ target = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΤΡΟΠΟΠΟΙΗΤΙΚΗ ΑΚ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult ApofasiModifyAnaplAKEdit(int apofasiId)
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

            ApofasiModifyAnaplirotesAKViewModel data = apofasiModifyAnAKService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "ApofaseisAn", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultModifyAnaplAKFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiModifyAnaplAKEdit(int apofasiId, ApofasiModifyAnaplirotesAKViewModel model)
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

            string ErrorMsg = ValidateApofasiModifyAnaplAKFields(model);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                apofasiModifyAnAKService.UpdateRecord(model, apofasiId, EPAS);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiModifyAnaplirotesAKViewModel newApofasi = apofasiModifyAnAKService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        public string ValidateApofasiModifyAnaplAKFields(ApofasiModifyAnaplirotesAKViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null)
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ))
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null)
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS))
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiModifyAnaplirotesAKViewModel SetDefaultModifyAnaplAKFields(ApofasiModifyAnaplirotesAKViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiAnaplProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            var data2 = (from d in db.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data2.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data2.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
            }

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, EPAS);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ-ΑΝ-ΑΚ";

            return data;
        }

        #endregion

        #endregion ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΕΩΣ ΑΠΟΦΑΣΕΙΣ - ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΕΚΤΥΠΩΣΕΙΣ ΑΠΟΦΑΣΕΩΝ ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult ApofasiCancelAnaplPrint(int apofasiId)
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
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiCancelAnaplirotesViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult ApofasiCorrectAnaplPrint(int apofasiId)
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
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΔΙΟΡΘΩΤΙΚΕΣ_ΑΝ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiCorrectAnaplirotesViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult ApofasiInitialAnaplPrint(int apofasiId)
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
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiInitialAnaplirotesViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult ApofasiRevokeAnaplPrint(int apofasiId)
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
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiRevokeAnaplirotesViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult ApofasiSupplementAnaplPrint(int apofasiId)
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
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiSupplementAnaplirotesViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult ApofasiModifyAnaplPrint(int apofasiId)
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
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiModifyAnaplirotesViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult ApofasiModifyAnaplAKPrint(int apofasiId)
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
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiModifyAnaplirotesAKViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        #endregion

        public USER_ADMINS GetLoginAdmin()
        {
            loggedAdmin = db.USER_ADMINS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FULLNAME;
            return loggedAdmin;
        }

    }
}