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
    public class Apofaseis2Controller : ControllerUnit
    {
        private USER_ARIADNE loggedAdmin;
        private readonly IrisDBEntities db;

        private readonly IApofasiInitialService apofasiInitialService;
        private readonly IApofasiDirectService apofasiDirectService;
        private readonly IApofasiSupplementService apofasiSupplementService;
        private readonly IApofasiSupplementAKService apofasiSupplementAKService;
        private readonly IApofasiModifyService apofasiModifyService;
        private readonly IApofasiModifyAKService apofasiModifyAKService;
        private readonly IApofasiCancelService apofasiCancelService;
        private readonly IApofasiRevokeService apofasiRevokeService;
        private readonly IApofasiCorrectService apofasiCorrectService;

        private readonly IAnathesiInitialService anathesiInitialService;
        private readonly IAnathesiDirectService anathesiDirectService;
        private readonly IAnathesiSupplementService anathesiSupplementService;
        private readonly IAnathesiSupplementAKService anathesiSupplementAKService;
        private readonly IAnathesiModifyService anathesiModifyService;
        private readonly IAnathesiModifyAKService anathesiModifyAKService;
        private readonly IAnathesiCancelService anathesiCancelService;
        private readonly IAnathesiRevokeService anathesiRevokeService;

        public Apofaseis2Controller(IrisDBEntities entities, IApofasiInitialService apofasiInitialService,
            IApofasiDirectService apofasiDirectService, IApofasiSupplementService apofasiSupplementService,
            IApofasiSupplementAKService apofasiSupplementAKService, IApofasiModifyService apofasiModifyService,
            IApofasiModifyAKService apofasiModifyAKService, IApofasiCancelService apofasiCancelService,
            IApofasiRevokeService apofasiRevokeService, IApofasiCorrectService apofasiCorrectService,
            IAnathesiInitialService anathesiInitialService, IAnathesiDirectService anathesiDirectService,
            IAnathesiSupplementService anathesiSupplementService, IAnathesiSupplementAKService anathesiSupplementAKService,
            IAnathesiModifyService anathesiModifyService, IAnathesiModifyAKService anathesiModifyAKService,
            IAnathesiCancelService anathesiCancelService, IAnathesiRevokeService anathesiRevokeService) : base(entities)
        {
            db = entities;

            this.apofasiInitialService = apofasiInitialService;
            this.apofasiDirectService = apofasiDirectService;
            this.apofasiSupplementService = apofasiSupplementService;
            this.apofasiSupplementAKService = apofasiSupplementAKService;
            this.apofasiModifyService = apofasiModifyService;
            this.apofasiModifyAKService = apofasiModifyAKService;
            this.apofasiCancelService = apofasiCancelService;
            this.apofasiRevokeService = apofasiRevokeService;
            this.apofasiCorrectService = apofasiCorrectService;

            this.anathesiInitialService = anathesiInitialService;
            this.anathesiDirectService = anathesiDirectService;
            this.anathesiSupplementService = anathesiSupplementService;
            this.anathesiSupplementAKService = anathesiSupplementAKService;
            this.anathesiModifyService = anathesiModifyService;
            this.anathesiModifyAKService = anathesiModifyAKService;
            this.anathesiCancelService = anathesiCancelService;
            this.anathesiRevokeService = anathesiRevokeService;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        // ---------------
        // ΩΡΟΜΙΣΘΙΟΙ ΙΕΚ
        // ---------------

        #region ΑΡΧΙΚΕΣ ΑΠΟΦΑΣΕΙΣ

        public ActionResult xApofaseisInitial(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(IEK);
            PopulateSchools(IEK);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiInitial_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiInitialService.Read2(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiInitial_Create([DataSourceRequest] DataSourceRequest request, ApofasiInitialGridViewModel data)
        {
            ApofasiInitialGridViewModel newdata = new ApofasiInitialGridViewModel();

            if (!Kerberos.CanCreateApofasiInitial((int)data.ΣΧΟΛΗ, (int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ))
            {
                ModelState.AddModelError("", "Υπάρχει ήδη αρχική απόφαση ωρομισθίων για αυτό το σχολείο και σχολικό έτος. Η δημιουργία της απόφασης ακυρώθηκε.");
            }
            if (data != null && ModelState.IsValid)
            {
                apofasiInitialService.Create(data);
                newdata = apofasiInitialService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiInitial_Update([DataSourceRequest] DataSourceRequest request, ApofasiInitialGridViewModel data)
        {
            ApofasiInitialGridViewModel newdata = new ApofasiInitialGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiInitialService.Update(data);
                newdata = apofasiInitialService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiInitial_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiInitialGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiInitial(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiInitialService.Destroy(data);
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

        public ActionResult AnatheseisInitial_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiInitialService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisInitial_Update([DataSourceRequest] DataSourceRequest request, AnathesiInitialViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiInitialViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiInitialService.Update(data, ap);
                    newdata = anathesiInitialService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisInitial_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiInitialViewModel data)
        {
            if (data != null)
            {
                anathesiInitialService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΑΡΧΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult xApofasiInitialEdit(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            ApofasiInitialViewModel data = apofasiInitialService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "Apofaseis2", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultInitialFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult xApofasiInitialEdit(int apofasiId, ApofasiInitialViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            string ErrorMsg = ValidateApofasiInitialFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiInitialService.UpdateRecord(data, apofasiId, IEK);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiInitialViewModel newApofasi = apofasiInitialService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiInitialFields(ApofasiInitialViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiInitialViewModel SetDefaultInitialFields(ApofasiInitialViewModel data)
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
            }

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΡΧΙΚΗ";

            return data;
        }

        #endregion

        #endregion ΑΡΧΙΚΕΣ ΑΠΟΦΑΣΕΙΣ


        #region ΑΠΕΥΘΕΙΑΣ ΑΠΟΦΑΣΕΙΣ

        public ActionResult xApofaseisDirect(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(IEK);
            PopulateSchools(IEK);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiDirect_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiDirectService.Read2(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiDirect_Create([DataSourceRequest] DataSourceRequest request, ApofasiDirectGridViewModel data)
        {
            ApofasiDirectGridViewModel newdata = new ApofasiDirectGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiDirectService.Create(data);
                newdata = apofasiDirectService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiDirect_Update([DataSourceRequest] DataSourceRequest request, ApofasiDirectGridViewModel data)
        {
            ApofasiDirectGridViewModel newdata = new ApofasiDirectGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiDirectService.Update(data);
                newdata = apofasiDirectService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiDirect_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiDirectGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiDirect(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiDirectService.Destroy(data);
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

        public ActionResult AnatheseisDirect_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiDirectService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisDirect_Update([DataSourceRequest] DataSourceRequest request, AnathesiDirectViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiDirectViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiDirectService.Update(data, ap);
                    newdata = anathesiDirectService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisDirect_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiDirectViewModel data)
        {
            if (data != null)
            {
                anathesiDirectService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΑΠΕΥΘΕΙΑΣ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult xApofasiDirectEdit(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            ApofasiDirectViewModel data = apofasiDirectService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "Apofaseis2", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultDirectFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult xApofasiDirectEdit(int apofasiId, ApofasiDirectViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            string ErrorMsg = ValidateApofasiDirectFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiDirectService.UpdateRecord(data, apofasiId, IEK);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiDirectViewModel newApofasi = apofasiDirectService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiDirectFields(ApofasiDirectViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && String.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiDirectViewModel SetDefaultDirectFields(ApofasiDirectViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            var data2 = (from d in db.ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                if (data.ΣΧΟΛΗ_ΤΥΠΟΣ == 1) data.ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ = data2.ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ;
                else data.ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ = data2.ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΙΕΚ;
            }

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΠΕΥΘΕΙΑΣ";

            return data;
        }

        #endregion

        #endregion ΑΠΕΥΘΕΙΑΣ ΑΠΟΦΑΣΕΙΣ


        #region ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ

        public ActionResult xApofaseisSupplement(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(IEK);
            PopulateSchools(IEK);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiSupplement_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiSupplementService.Read2(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSupplement_Create([DataSourceRequest] DataSourceRequest request, ApofasiSupplementGridViewModel data)
        {
            ApofasiSupplementGridViewModel newdata = new ApofasiSupplementGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSupplementService.Create(data);
                newdata = apofasiSupplementService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSupplement_Update([DataSourceRequest] DataSourceRequest request, ApofasiSupplementGridViewModel data)
        {
            ApofasiSupplementGridViewModel newdata = new ApofasiSupplementGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSupplementService.Update(data);
                newdata = apofasiSupplementService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSupplement_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiSupplementGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiSupplement(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiSupplementService.Destroy(data);
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

        public ActionResult AnatheseisSupplement_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiSupplementService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisSupplement_Update([DataSourceRequest] DataSourceRequest request, AnathesiSupplementViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiSupplementViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiSupplementService.Update(data, ap);
                    newdata = anathesiSupplementService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisSupplement_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiSupplementViewModel data)
        {
            if (data != null)
            {
                anathesiSupplementService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult xApofasiSupplementEdit(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            ApofasiSupplementViewModel data = apofasiSupplementService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "Apofaseis2", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultSupplementFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult xApofasiSupplementEdit(int apofasiId, ApofasiSupplementViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            string ErrorMsg = ValidateApofasiSupplementFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiSupplementService.UpdateRecord(data, apofasiId, IEK);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiSupplementViewModel newApofasi = apofasiSupplementService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiSupplementFields(ApofasiSupplementViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiSupplementViewModel SetDefaultSupplementFields(ApofasiSupplementViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ";

            return data;
        }

        #endregion

        #endregion ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ


        #region ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΗΣ

        public ActionResult xApofaseisSupplementAK(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(IEK);
            PopulateSchools(IEK);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiSupplementAK_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiSupplementAKService.Read2(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSupplementAK_Create([DataSourceRequest] DataSourceRequest request, ApofasiSupplementAKGridViewModel data)
        {
            ApofasiSupplementAKGridViewModel newdata = new ApofasiSupplementAKGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSupplementAKService.Create(data);
                newdata = apofasiSupplementAKService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSupplementAK_Update([DataSourceRequest] DataSourceRequest request, ApofasiSupplementAKGridViewModel data)
        {
            ApofasiSupplementAKGridViewModel newdata = new ApofasiSupplementAKGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSupplementAKService.Update(data);
                newdata = apofasiSupplementAKService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSupplementAK_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiSupplementAKGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiSupplementAK(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiSupplementAKService.Destroy(data);
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

        public ActionResult AnatheseisSupplementAK_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiSupplementAKService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisSupplementAK_Update([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAKViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiSupplementAKViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiSupplementAKService.Update(data, ap);
                    newdata = anathesiSupplementAKService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisSupplementAK_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAKViewModel data)
        {
            if (data != null)
            {
                anathesiSupplementAKService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΗΣ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult xApofasiSupplementAKEdit(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            ApofasiSupplementAKViewModel data = apofasiSupplementAKService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "Apofaseis2", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultSupplementAKFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult xApofasiSupplementAKEdit(int apofasiId, ApofasiSupplementAKViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            string ErrorMsg = ValidateApofasiSupplementAKFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiSupplementAKService.UpdateRecord(data, apofasiId, IEK);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiSupplementAKViewModel newApofasi = apofasiSupplementAKService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiSupplementAKFields(ApofasiSupplementAKViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && String.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiSupplementAKViewModel SetDefaultSupplementAKFields(ApofasiSupplementAKViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ-ΑΚ";

            return data;
        }

        #endregion

        #endregion ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΗΣ


        #region ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ

        public ActionResult xApofaseisModify(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(IEK);
            PopulateSchools(IEK);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiModify_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiModifyService.Read2(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModify_Create([DataSourceRequest] DataSourceRequest request, ApofasiModifyGridViewModel data)
        {
            ApofasiModifyGridViewModel newdata = new ApofasiModifyGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiModifyService.Create(data);
                newdata = apofasiModifyService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModify_Update([DataSourceRequest] DataSourceRequest request, ApofasiModifyGridViewModel data)
        {
            ApofasiModifyGridViewModel newdata = new ApofasiModifyGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiModifyService.Update(data);
                newdata = apofasiModifyService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModify_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiModifyGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiModify(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiModifyService.Destroy(data);
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

        public ActionResult AnatheseisModify_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiModifyService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisModify_Update([DataSourceRequest] DataSourceRequest request, AnathesiModifyViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiModifyViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyService.Update(data, ap);
                    newdata = anathesiModifyService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisModify_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiModifyViewModel data)
        {
            if (data != null)
            {
                anathesiModifyService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΤΡΟΠΟΠΟΙΗΤΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult xApofasiModifyEdit(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            ApofasiModifyViewModel data = apofasiModifyService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "Apofaseis2", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultModifyFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult xApofasiModifyEdit(int apofasiId, ApofasiModifyViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            string ErrorMsg = ValidateApofasiModifyFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiModifyService.UpdateRecord(data, apofasiId, IEK);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiModifyViewModel newApofasi = apofasiModifyService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiModifyFields(ApofasiModifyViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && String.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiModifyViewModel SetDefaultModifyFields(ApofasiModifyViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ";

            return data;
        }


        #endregion

        #endregion ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ


        #region ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΗΣ

        public ActionResult xApofaseisModifyAK(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(IEK);
            PopulateSchools(IEK);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiModifyAK_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiModifyAKService.Read2(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModifyAK_Create([DataSourceRequest] DataSourceRequest request, ApofasiModifyAKGridViewModel data)
        {
            ApofasiModifyAKGridViewModel newdata = new ApofasiModifyAKGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiModifyAKService.Create(data);
                newdata = apofasiModifyAKService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModifyAK_Update([DataSourceRequest] DataSourceRequest request, ApofasiModifyAKGridViewModel data)
        {
            ApofasiModifyAKGridViewModel newdata = new ApofasiModifyAKGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiModifyAKService.Update(data);
                newdata = apofasiModifyAKService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiModifyAK_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiModifyAKGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiModifyAK(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiModifyAKService.Destroy(data);
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

        public ActionResult AnatheseisModifyAK_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiModifyAKService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisModifyAK_Update([DataSourceRequest] DataSourceRequest request, AnathesiModifyAKViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiModifyAKViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyAKService.Update(data, ap);
                    newdata = anathesiModifyAKService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisModifyAK_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiModifyAKViewModel data)
        {
            if (data != null)
            {
                anathesiModifyAKService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΤΡΟΠΟΠΟΙΗΤΙΚΗ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΗΣ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult xApofasiModifyAKEdit(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            ApofasiModifyAKViewModel data = apofasiModifyAKService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "Apofaseis2", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultModifyAKFields(data, apofasiId);

            return View(data);
        }

        [HttpPost]
        public ActionResult xApofasiModifyAKEdit(int apofasiId, ApofasiModifyAKViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            string ErrorMsg = ValidateApofasiModifyAKFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiModifyAKService.UpdateRecord(data, apofasiId, IEK);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiModifyAKViewModel newApofasi = apofasiModifyAKService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiModifyAKFields(ApofasiModifyAKViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && String.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiModifyAKViewModel SetDefaultModifyAKFields(ApofasiModifyAKViewModel data, int apofasiId)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ-ΑΚ";

            return data;
        }


        #endregion

        #endregion ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΗΣ


        #region ΑΚΥΡΩΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ

        public ActionResult xApofaseisCancel(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(IEK);
            PopulateSchools(IEK);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiCancel_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiCancelService.Read2(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCancel_Create([DataSourceRequest] DataSourceRequest request, ApofasiCancelGridViewModel data)
        {
            ApofasiCancelGridViewModel newdata = new ApofasiCancelGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiCancelService.Create(data);
                newdata = apofasiCancelService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCancel_Update([DataSourceRequest] DataSourceRequest request, ApofasiCancelGridViewModel data)
        {
            ApofasiCancelGridViewModel newdata = new ApofasiCancelGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiCancelService.Update(data);
                newdata = apofasiCancelService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCancel_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiCancelGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiCancel(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiCancelService.Destroy(data);
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

        public ActionResult AnatheseisCancel_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiCancelService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisCancel_Update([DataSourceRequest] DataSourceRequest request, AnathesiCancelViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiCancelViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiCancelService.Update(data, ap);
                    newdata = anathesiCancelService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisCancel_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiCancelViewModel data)
        {
            if (data != null)
            {
                anathesiCancelService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΑΚΥΡΩΤΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult xApofasiCancelEdit(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            ApofasiCancelViewModel data = apofasiCancelService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "Apofaseis2", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultCancelFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult xApofasiCancelEdit(int apofasiId, ApofasiCancelViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            string ErrorMsg = ValidateApofasiCancelFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiCancelService.UpdateRecord(data, apofasiId, IEK);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiCancelViewModel newApofasi = apofasiCancelService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiCancelFields(ApofasiCancelViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiCancelViewModel SetDefaultCancelFields(ApofasiCancelViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΚΥΡΩΤΙΚΗ";

            return data;
        }

        #endregion

        #endregion ΑΚΥΡΩΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ


        #region ΑΝΑΚΛΗΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ

        public ActionResult xApofaseisRevoke(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(IEK);
            PopulateSchools(IEK);
            PopulateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiRevoke_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiRevokeService.Read2(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiRevoke_Create([DataSourceRequest] DataSourceRequest request, ApofasiRevokeGridViewModel data)
        {
            ApofasiRevokeGridViewModel newdata = new ApofasiRevokeGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiRevokeService.Create(data);
                newdata = apofasiRevokeService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiRevoke_Update([DataSourceRequest] DataSourceRequest request, ApofasiRevokeGridViewModel data)
        {
            ApofasiRevokeGridViewModel newdata = new ApofasiRevokeGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiRevokeService.Update(data);
                newdata = apofasiRevokeService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiRevoke_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiRevokeGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiRevoke(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiRevokeService.Destroy(data);
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

        public ActionResult AnatheseisRevoke_Read([DataSourceRequest] DataSourceRequest request, ApofasiParameters ap)
        {
            var data = anathesiRevokeService.Read(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnatheseisRevoke_Update([DataSourceRequest] DataSourceRequest request, AnathesiRevokeViewModel data, ApofasiParameters ap)
        {
            var newdata = new AnathesiRevokeViewModel();

            if (ap.schoolyearId > 0 && ap.schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiRevokeService.Update(data, ap);
                    newdata = anathesiRevokeService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnatheseisRevoke_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiRevokeViewModel data)
        {
            if (data != null)
            {
                anathesiRevokeService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΑΝΑΚΛΗΤΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult xApofasiRevokeEdit(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            ApofasiRevokeViewModel data = apofasiRevokeService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "Apofaseis2", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultRevokeFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult xApofasiRevokeEdit(int apofasiId, ApofasiRevokeViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            string ErrorMsg = ValidateApofasiRevokeFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiRevokeService.UpdateRecord(data, apofasiId, IEK);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiRevokeViewModel newApofasi = apofasiRevokeService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiRevokeFields(ApofasiRevokeViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiRevokeViewModel SetDefaultRevokeFields(ApofasiRevokeViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΝΑΚΛΗΣΗ";

            return data;
        }


        #endregion

        #endregion ΑΝΑΚΛΗΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ


        #region ΔΙΟΡΘΩΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ

        public ActionResult xApofaseisCorrect(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes(IEK);
            PopulateSchools(IEK);
            //populateEidikotites();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiCorrect_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int adminId = 0)
        {
            var data = apofasiCorrectService.Read2(schoolyearId, adminId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCorrect_Create([DataSourceRequest] DataSourceRequest request, ApofasiCorrectGridViewModel data)
        {
            ApofasiCorrectGridViewModel newdata = new ApofasiCorrectGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiCorrectService.Create(data);
                newdata = apofasiCorrectService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCorrect_Update([DataSourceRequest] DataSourceRequest request, ApofasiCorrectGridViewModel data)
        {
            ApofasiCorrectGridViewModel newdata = new ApofasiCorrectGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiCorrectService.Update(data);
                newdata = apofasiCorrectService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiCorrect_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiCorrectGridViewModel data)
        {
            if (data != null)
            {
                apofasiCorrectService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ΔΙΟΡΘΩΤΙΚΗ ΑΠΟΦΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult xApofasiCorrectEdit(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            ApofasiCorrectViewModel data = apofasiCorrectService.GetRecord(apofasiId);
            if (data == null)
            {
                string msg = "Δεν βρέθηκε η απόφαση. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή και δοκιμάστε πάλι.";
                return RedirectToAction("ErrorData", "Apofaseis2", new { notify = msg });
            }
            // Set default field values
            data = SetDefaultCorrectFields(data);

            return View(data);
        }

        [HttpPost]
        public ActionResult xApofasiCorrectEdit(int apofasiId, ApofasiCorrectViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            string ErrorMsg = ValidateApofasiCorrectFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiCorrectService.UpdateRecord(data, apofasiId, IEK);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiCorrectViewModel newApofasi = apofasiCorrectService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiCorrectFields(ApofasiCorrectViewModel apofasi)
        {
            string errMsg = "";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiCorrectViewModel SetDefaultCorrectFields(ApofasiCorrectViewModel data)
        {
            data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GetArxikiProtocol(data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ, data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, data.ΣΧΟΛΗ);

            if (data.ΠΡΟΙΣΤΑΜΕΝΟΣ == null)
            {
                data.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΕΥΘΥΝΤΗΣ == null)
            {
                data.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΓΕΝΙΚΟΣ == null)
            {
                data.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΔΙΟΙΚΗΤΗΣ == null)
            {
                data.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            if (data.ΑΝΤΙΠΡΟΕΔΡΟΣ == null)
            {
                data.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, IEK);
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΔΙΟΡΘΩΤΙΚΗ";

            return data;
        }


        #endregion

        #endregion ΔΙΟΡΘΩΤΙΚΕΣ ΑΠΟΦΑΣΕΙΣ


        #region ΕΚΤΥΠΩΣΕΙΣ ΑΠΟΦΑΣΕΩΝ

        public ActionResult xApofasiCancelPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiCancelViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult xApofasiCorrectPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΔΙΟΡΘΩΤΙΚΕΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiCorrectViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult xApofasiDirectPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiDirectViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult xApofasiInitialPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΡΧΙΚΕΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiInitialViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult xApofasiModifyPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiModifyViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult xApofasiModifyAKPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiModifyAKViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult xApofasiRevokePrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiRevokeViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult xApofasiSupplementPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiSupplementViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        public ActionResult xApofasiSupplementAKPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            var data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiSupplementAKViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).FirstOrDefault();

            return View(data);
        }

        #endregion


        public USER_ARIADNE GetLoginAdmin()
        {
            loggedAdmin = db.USER_ARIADNE.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FULLNAME;

            return loggedAdmin;
        }
    }
}