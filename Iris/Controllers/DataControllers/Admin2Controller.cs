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

namespace Iris.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class Admin2Controller : Controller
    {
        private readonly IrisDBEntities db;
        private USER_ARIADNE loggedAdmin;

        private readonly IAnathesiInitialService anathesiInitialService;
        private readonly IAnathesiDirectService anathesiDirectService;
        private readonly IAnathesiSupplementService anathesiSupplementService;
        private readonly IAnathesiSupplementAKService anathesiSupplementAKService;
        private readonly IAnathesiCancelService anathesiCancelService;
        private readonly IAnathesiRevokeService anathesiRevokeService;
        private readonly IAnathesiModifyService anathesiModifyService;
        private readonly IAnathesiModifyAKService anathesiModifyAKService;

        private readonly IRegAnathesiProslipsiService regAnathesiProslipsiService;
        private readonly IRegAnathesiProslipsiAnService regAnathesiProslipsiAnService;
        private readonly IRegAnathesiMetaboliService regAnathesiMetaboliService;
        private readonly IRegAnathesiMetaboliAnService regAnathesiMetaboliAnService;
        private readonly IRegAnathesiUniversalService regAnathesiUniversalService;

        public Admin2Controller(IrisDBEntities entities, IAnathesiInitialService anathesiInitialService,
            IAnathesiDirectService anathesiDirectService, IAnathesiSupplementService anathesiSupplementService,
            IAnathesiSupplementAKService anathesiSupplementAKService, IAnathesiCancelService anathesiCancelService,
            IAnathesiRevokeService anathesiRevokeService, IAnathesiModifyService anathesiModifyService,
            IAnathesiModifyAKService anathesiModifyAKService, IRegAnathesiProslipsiService regAnathesiProslipsiService,
            IRegAnathesiProslipsiAnService regAnathesiProslipsiAnService, IRegAnathesiMetaboliService regAnathesiMetaboliService,
            IRegAnathesiMetaboliAnService regAnathesiMetaboliAnService, IRegAnathesiUniversalService regAnathesiUniversalService)
        {
            db = entities;

            this.anathesiInitialService = anathesiInitialService;
            this.anathesiDirectService = anathesiDirectService;
            this.anathesiSupplementService = anathesiSupplementService;
            this.anathesiSupplementAKService = anathesiSupplementAKService;
            this.anathesiCancelService = anathesiCancelService;
            this.anathesiRevokeService = anathesiRevokeService;
            this.anathesiModifyService = anathesiModifyService;
            this.anathesiModifyAKService = anathesiModifyAKService;

            this.regAnathesiProslipsiService = regAnathesiProslipsiService;
            this.regAnathesiProslipsiAnService = regAnathesiProslipsiAnService;
            this.regAnathesiMetaboliService = regAnathesiMetaboliService;
            this.regAnathesiMetaboliAnService = regAnathesiMetaboliAnService;
            this.regAnathesiUniversalService = regAnathesiUniversalService;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index(string notify = null)
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
            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            return View();
        }

        #region ΑΝΑΘΕΣΕΙΣ (ΠΙΝΑΚΕΣ ΑΠΟ ΣΧΟΛΕΙΑ ΚΑΙ ΔΙΑΧΕΙΡΙΣΤΕΣ)

        public ActionResult School_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetSchoolsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SchoolsGridViewModel> GetSchoolsFromDB()
        {

            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ
                        where d.ΔΟΜΗ != 1 && d.ΔΟΜΗ != 4
                        orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ
                        select new SchoolsGridViewModel
                        {
                            ΣΧΟΛΗ_ΚΩΔ = d.ΣΧΟΛΗ_ΚΩΔ,
                            ΕΠΩΝΥΜΙΑ = d.ΕΠΩΝΥΜΙΑ,
                            ΔΟΜΗ = d.ΔΟΜΗ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ,
                            ΠΕΡΙΦΕΡΕΙΑ = d.ΠΕΡΙΦΕΡΕΙΑ
                        }).ToList();

            return data;
        }

        #region ΩΡΟΜΙΣΘΙΟΙ

        #region ΑΝΑΘΕΣΕΙΣ ΑΡΧΙΚΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult xAnatheseisInitial()
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
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiInitial_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int schoolId = 0)
        {
            var data = anathesiInitialService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiInitial_Create([DataSourceRequest] DataSourceRequest request, AnathesiInitialViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiInitialViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateInitial(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη αρχική ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολείο και σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiInitialService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiInitialService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiInitial_Update([DataSourceRequest] DataSourceRequest request, AnathesiInitialViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiInitialViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteInitial(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiInitialService.Update(data, schoolyearId, schoolId);
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
        public ActionResult AnathesiInitial_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiInitialViewModel data)
        {
            if (data != null)
            {
                anathesiInitialService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΑΡΧΙΚΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΑΠΕΥΘΕΙΑΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult xAnatheseisDirect()
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
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiDirect_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int schoolId = 0)
        {
            var data = anathesiDirectService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiDirect_Create([DataSourceRequest] DataSourceRequest request, AnathesiDirectViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiDirectViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateDirect(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη απευθείας ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολείο και σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiDirectService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiDirectService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiDirect_Update([DataSourceRequest] DataSourceRequest request, AnathesiDirectViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiDirectViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteDirect(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiDirectService.Update(data, schoolyearId, schoolId);
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
        public ActionResult AnathesiDirect_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiDirectViewModel data)
        {
            if (data != null)
            {
                anathesiDirectService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion


        #region ΑΝΑΘΕΣΕΙΣ ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult xAnatheseisSupplement()
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
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiSupplement_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int schoolId = 0)
        {
            var data = anathesiSupplementService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplement_Create([DataSourceRequest] DataSourceRequest request, AnathesiSupplementViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiSupplementViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateSupplement(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη συμπληρωματική ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολείο και σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiSupplementService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiSupplementService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplement_Update([DataSourceRequest] DataSourceRequest request, AnathesiSupplementViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiSupplementViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteSupplement(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiSupplementService.Update(data, schoolyearId, schoolId);
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
        public ActionResult AnathesiSupplement_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiSupplementViewModel data)
        {
            if (data != null)
            {
                anathesiSupplementService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion


        #region ΑΝΑΘΕΣΕΙΣ ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ ΑΚ

        #region GRID CRUD FUNCTIONS

        public ActionResult xAnatheseisSupplementAK()
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
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiSupplementAK_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int schoolId = 0)
        {
            var data = anathesiSupplementAKService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplementAK_Create([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAKViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiSupplementAKViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateSupplementAK(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη συμπληρωματική A.K. ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολείο και σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiSupplementAKService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiSupplementAKService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplementAK_Update([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAKViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiSupplementAKViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteSupplementAK(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiSupplementAKService.Update(data, schoolyearId, schoolId);
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
        public ActionResult AnathesiSupplementAK_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAKViewModel data)
        {
            if (data != null)
            {
                anathesiSupplementAKService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion


        #region ΑΝΑΘΕΣΕΙΣ ΑΚΥΡΩΤΙΚΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult xAnatheseisCancel()
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
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiCancel_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int schoolId = 0)
        {
            var data = anathesiCancelService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiCancel_Create([DataSourceRequest] DataSourceRequest request, AnathesiCancelViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiCancelViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateCancel(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη ακυρωτική ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολείο και σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiCancelService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiCancelService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiCancel_Update([DataSourceRequest] DataSourceRequest request, AnathesiCancelViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiCancelViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteCancel(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiCancelService.Update(data, schoolyearId, schoolId);
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
        public ActionResult AnathesiCancel_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiCancelViewModel data)
        {
            if (data != null)
            {
                anathesiCancelService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΑΚΥΡΩΤΙΚΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΑΝΑΚΛΗΣΗΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult xAnatheseisRevoke()
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
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiRevoke_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int schoolId = 0)
        {
            var data = anathesiRevokeService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiRevoke_Create([DataSourceRequest] DataSourceRequest request, AnathesiRevokeViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiRevokeViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateRevoke(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη ανάκληση ανάθεσης για τον εκπαιδευτικό γι' αυτό το σχολείο και σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiRevokeService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiRevokeService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiRevoke_Update([DataSourceRequest] DataSourceRequest request, AnathesiRevokeViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiRevokeViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteRevoke(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiRevokeService.Update(data, schoolyearId, schoolId);
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
        public ActionResult AnathesiRevoke_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiRevokeViewModel data)
        {
            if (data != null)
            {
                anathesiRevokeService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΑΝΑΚΛΗΣΗΣ


        #region ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult xAnatheseisModify()
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
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiModify_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int schoolId = 0)
        {
            var data = anathesiModifyService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModify_Create([DataSourceRequest] DataSourceRequest request, AnathesiModifyViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiModifyViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiModifyService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModify_Update([DataSourceRequest] DataSourceRequest request, AnathesiModifyViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiModifyViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteModify(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyService.Update(data, schoolyearId, schoolId);
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
        public ActionResult AnathesiModify_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiModifyViewModel data)
        {
            if (data != null)
            {
                anathesiModifyService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΗΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult xAnatheseisModifyAK()
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
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiModifyAK_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int schoolId = 0)
        {
            var data = anathesiModifyAKService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModifyAK_Create([DataSourceRequest] DataSourceRequest request, AnathesiModifyAKViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiModifyAKViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }

                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyAKService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiModifyAKService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModifyAK_Update([DataSourceRequest] DataSourceRequest request, AnathesiModifyAKViewModel data, int schoolyearId = 0, int schoolId = 0)
        {
            var newdata = new AnathesiModifyAKViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteModifyAK(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyAKService.Update(data, schoolyearId, schoolId);
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
        public ActionResult AnathesiModifyAK_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiModifyAKViewModel data)
        {
            if (data != null)
            {
                anathesiModifyAKService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ AK

        #endregion

        #endregion ΑΝΑΘΕΣΕΙΣ - ΠΙΝΑΚΕΣ ΑΠΟ ΣΧΟΛΕΙΑ


        #region ΜΕΤΑΚΙΝΗΣΗ (ΛΑΝΘΑΣΜΕΝΩΝ) ΑΝΑΘΕΣΕΩΝ

        // --------------
        // ΩΡΟΜΙΣΘΙΟΙ
        // --------------

        public ActionResult TransferAgent(int sourceAnathesiId, string ApofasiType, int targetAnathesiType)
        {
            string msg = "Η μετακίνηση της ανάθεσης ολοκληρώθηκε.";
            bool result = false;

            bool rule1 = ApofasiType == "ΑΚΥΡΩΤΙΚΗ" && (targetAnathesiType == 2 || targetAnathesiType == 7 || targetAnathesiType == 8);
            bool rule2 = ApofasiType == "ΑΝΑΚΛΗΣΗ" && (targetAnathesiType == 1 || targetAnathesiType == 7 || targetAnathesiType == 8);
            bool rule3 = ApofasiType == "ΑΠΕΥΘΕΙΑΣ" && (targetAnathesiType == 4 || targetAnathesiType == 5 || targetAnathesiType == 6);
            bool rule4 = ApofasiType == "ΑΡΧΙΚΗ" && (targetAnathesiType == 3 || targetAnathesiType == 5 || targetAnathesiType == 6);
            bool rule5 = ApofasiType == "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ" && (targetAnathesiType == 3 || targetAnathesiType == 4 || targetAnathesiType == 6);
            bool rule6 = ApofasiType == "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ-ΑΚ" && (targetAnathesiType == 3 || targetAnathesiType == 4 || targetAnathesiType == 5);
            bool rule7 = ApofasiType == "ΤΡΟΠΟΠΟΙΗΤΙΚΗ" && (targetAnathesiType == 1 || targetAnathesiType == 2 || targetAnathesiType == 8);
            bool rule8 = ApofasiType == "ΤΡΟΠΟΠΟΙΗΤΙΚΗ-ΑΚ" && (targetAnathesiType == 1 || targetAnathesiType == 2 || targetAnathesiType == 7);

            if (ApofasiType == "ΑΚΥΡΩΤΙΚΗ" && rule1) 
            {
                result = MoveAnathesiCancel(sourceAnathesiId, targetAnathesiType);
                if (result) DeleteAnathesiCancel(sourceAnathesiId);
            }
            else if (ApofasiType == "ΑΝΑΚΛΗΣΗ" && rule2)
            {
                result = MoveAnathesiRevoke(sourceAnathesiId, targetAnathesiType);
                if (result) DeleteAnathesiRevoke(sourceAnathesiId);
            }
            else if (ApofasiType == "ΑΠΕΥΘΕΙΑΣ" && rule3)
            {
                result = MoveAnathesiDirect(sourceAnathesiId, targetAnathesiType);
                if (result) DeleteAnathesiDirect(sourceAnathesiId);
            }
            else if (ApofasiType == "ΑΡΧΙΚΗ" && rule4)
            {
                result = MoveAnathesiInitial(sourceAnathesiId, targetAnathesiType);
                if (result) DeleteAnathesiInitial(sourceAnathesiId);
            }
            else if (ApofasiType == "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ" && rule5)
            {
                result = MoveAnathesiSupplement(sourceAnathesiId, targetAnathesiType);
                if (result) DeleteAnathesiSupplement(sourceAnathesiId);
            }
            else if (ApofasiType == "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ-ΑΚ" && rule6)
            {
                result = MoveAnathesiSupplementAK(sourceAnathesiId, targetAnathesiType);
                if (result) DeleteAnathesiSupplementAK(sourceAnathesiId);
            }
            else if (ApofasiType == "ΤΡΟΠΟΠΟΙΗΤΙΚΗ" && rule7)
            {
                result = MoveAnathesiModify(sourceAnathesiId, targetAnathesiType);
                if (result) DeleteAnathesiModify(sourceAnathesiId);
            }
            else if (ApofasiType == "ΤΡΟΠΟΠΟΙΗΤΙΚΗ-ΑΚ" && rule8)
            {
                result = MoveAnathesiModifyAK(sourceAnathesiId, targetAnathesiType);
                if (result) DeleteAnathesiModifyAK(sourceAnathesiId);
            }
            else
            {
                msg = "Αυτό το είδος μετακίνησης δεν επιτρέπεται λόγω ασυμβατότητας των αναθέσεων.";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public bool MoveAnathesiCancel(int sourceAnathesiId, int targetAnathesiType)
        {
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ where d.ΑΝΑΘΕΣΗ_ΚΩΔ == sourceAnathesiId select d).FirstOrDefault();
            if (source == null) return false;
            if (source.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;

            if (targetAnathesiType == 2)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΝΑΚΛΗΣΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 7)
            {
                ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 8)
            {
                ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ entity = new ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ-ΑΚ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ.Add(entity);
                db.SaveChanges();
            }
            return true;
        }

        public bool DeleteAnathesiCancel(int sourceAnathesiId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ entity = db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Find(sourceAnathesiId);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Remove(entity);
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool MoveAnathesiRevoke(int sourceAnathesiId, int targetAnathesiType)
        {
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ where d.ΑΝΑΘΕΣΗ_ΚΩΔ == sourceAnathesiId select d).FirstOrDefault();
            if (source == null) return false;
            if (source.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;

            if (targetAnathesiType == 1)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΚΥΡΩΤΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 7)
            {
                ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 8)
            {
                ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ entity = new ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ-ΑΚ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ.Add(entity);
                db.SaveChanges();
            }
            return true;
        }

        public bool DeleteAnathesiRevoke(int sourceAnathesiId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ entity = db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ.Find(sourceAnathesiId);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ.Remove(entity);
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool MoveAnathesiModify(int sourceAnathesiId, int targetAnathesiType)
        {
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ where d.ΑΝΑΘΕΣΗ_ΚΩΔ == sourceAnathesiId select d).FirstOrDefault();
            if (source == null) return false;
            if (source.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;

            if (targetAnathesiType == 1)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΚΥΡΩΤΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 2)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΝΑΚΛΗΣΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 8)
            {
                ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ entity = new ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ-ΑΚ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ.Add(entity);
                db.SaveChanges();
            }
            return true;
        }

        public bool DeleteAnathesiModify(int sourceAnathesiId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ entity = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Find(sourceAnathesiId);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Remove(entity);
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool MoveAnathesiModifyAK(int sourceAnathesiId, int targetAnathesiType)
        {
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ where d.ΑΝΑΘΕΣΗ_ΚΩΔ == sourceAnathesiId select d).FirstOrDefault();
            if (source == null) return false;
            if (source.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;

            if (targetAnathesiType == 1)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΚΥΡΩΤΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 2)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΝΑΚΛΗΣΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 7)
            {
                ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ_ΑΠΟ = source.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                    ΩΡΕΣ_ΕΒΔ_ΣΕ = source.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,                 // ωρομίσθιος
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            return true;
        }

        public bool DeleteAnathesiModifyAK(int sourceAnathesiId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ entity = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ.Find(sourceAnathesiId);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ.Remove(entity);
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool MoveAnathesiDirect(int sourceAnathesiId, int targetAnathesiType)
        {
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ where d.ΑΝΑΘΕΣΗ_ΚΩΔ == sourceAnathesiId select d).FirstOrDefault();
            if (source == null) return false;
            if (source.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;

            if (targetAnathesiType == 4)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΡΧΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 5)
            {
                ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,        
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 6)
            {
                ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ entity = new ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ-ΑΚ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ.Add(entity);
                db.SaveChanges();
            }
            return true;
        }

        public bool DeleteAnathesiDirect(int sourceAnathesiId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ entity = db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ.Find(sourceAnathesiId);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ.Remove(entity);
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool MoveAnathesiInitial(int sourceAnathesiId, int targetAnathesiType)
        {
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ where d.ΑΝΑΘΕΣΗ_ΚΩΔ == sourceAnathesiId select d).FirstOrDefault();
            if (source == null) return false;
            if (source.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;

            if (targetAnathesiType == 3)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΠΕΥΘΕΙΑΣ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 5)
            {
                ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 6)
            {
                ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ entity = new ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ-ΑΚ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ.Add(entity);
                db.SaveChanges();
            }
            return true;
        }

        public bool DeleteAnathesiInitial(int sourceAnathesiId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ entity = db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Find(sourceAnathesiId);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Remove(entity);
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool MoveAnathesiSupplement(int sourceAnathesiId, int targetAnathesiType)
        {
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ where d.ΑΝΑΘΕΣΗ_ΚΩΔ == sourceAnathesiId select d).FirstOrDefault();
            if (source == null) return false;
            if (source.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;

            if (targetAnathesiType == 3)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΠΕΥΘΕΙΑΣ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 4)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΡΧΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 6)
            {
                ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ entity = new ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ-ΑΚ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ.Add(entity);
                db.SaveChanges();
            }
            return true;
        }

        public bool DeleteAnathesiSupplement(int sourceAnathesiId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Find(sourceAnathesiId);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Remove(entity);
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool MoveAnathesiSupplementAK(int sourceAnathesiId, int targetAnathesiType)
        {
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ where d.ΑΝΑΘΕΣΗ_ΚΩΔ == sourceAnathesiId select d).FirstOrDefault();
            if (source == null) return false;
            if (source.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;

            if (targetAnathesiType == 3)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΠΕΥΘΕΙΑΣ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 4)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΡΧΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            else if (targetAnathesiType == 5)
            {
                ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ()
                {
                    ΑΦΜ = source.ΑΦΜ,
                    ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑ = source.ΟΝΟΜΑ,
                    ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = source.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                    ΩΡΕΣ_ΕΒΔ = source.ΩΡΕΣ_ΕΒΔ,
                    ΣΧΟΛΙΚΟ_ΕΤΟΣ = source.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                    ΣΧΟΛΗ = source.ΣΧΟΛΗ,
                    ΣΥΜΒΑΣΗ = source.ΣΥΜΒΑΣΗ,
                    ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ",
                    ΑΠΟΦΑΣΗ_ΚΩΔ = 0,
                    ΚΛΑΔΟΣ_ΚΩΔ = source.ΚΛΑΔΟΣ_ΚΩΔ,
                    ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = source.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                    ΦΥΛΟ = source.ΦΥΛΟ
                };
                db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Add(entity);
                db.SaveChanges();
            }
            return true;
        }

        public bool DeleteAnathesiSupplementAK(int sourceAnathesiId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ entity = db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ.Find(sourceAnathesiId);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ.Remove(entity);
                db.SaveChanges();
                return true;
            }
            else return false;
        }


        #endregion


        #region ΜΗΤΡΩΑ ΑΝΑΘΕΣΕΩΝ

        #region ΑΝΑΘΕΣΕΙΣ ΠΡΟΣΛΗΨΗΣ ΩΡΟΜΙΣΘΙΟΙ

        public ActionResult xRegAnatheseisProslipsi(string notify = null)
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

            var data = regAnathesiProslipsiService.ReadIek();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αναθέσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin2", new { notify = msg });
            }
            RegAnathesiProslipsiViewModel anathesi = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(anathesi);
        }

        public ActionResult RegAnathesiProslipsi_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            var data = regAnathesiProslipsiService.ReadIek(schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAnathesiProslipsiRecord(int recordId)
        {
            var data = regAnathesiProslipsiService.GetRecord(recordId);

            return PartialView("xRegAnathesiProslipsiPartial", data);
        }

        #endregion ΑΝΑΘΕΣΕΙΣ ΠΡΟΣΛΗΨΗΣ ΩΡΟΜΙΣΘΙΟΙ


        #region ΑΝΑΘΕΣΕΙΣ ΠΡΟΣΛΗΨΗΣ ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult xRegAnatheseisProslipsiAn(string notify = null)
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

            var data = regAnathesiProslipsiAnService.Read();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αναθέσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin2", new { notify = msg });
            }
            RegAnathesiProslipsiAnViewModel anathesi = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(anathesi);
        }

        public ActionResult RegAnathesiProslipsiAn_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            var data = regAnathesiProslipsiAnService.Read(schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAnathesiProslipsiAnRecord(int recordId)
        {
            var data = regAnathesiProslipsiAnService.GetRecord(recordId);

            return PartialView("xRegAnathesiProslipsiAnPartial", data);
        }

        #endregion ΑΝΑΘΕΣΕΙΣ ΠΡΟΣΛΗΨΗΣ ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΜΕΤΑΒΟΛΩΝ ΩΡΟΜΙΣΘΙΟΙ

        public ActionResult xRegAnatheseisMetaboli(string notify = null)
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

            var data = regAnathesiMetaboliService.ReadIek();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αναθέσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin2", new { notify = msg });
            }
            RegAnathesiMetaboliViewModel anathesi = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(anathesi);
        }

        public ActionResult RegAnathesiMetaboli_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            var data = regAnathesiMetaboliService.ReadIek(schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAnathesiMetaboliRecord(int recordId)
        {
            var data = regAnathesiMetaboliService.GetRecord(recordId);

            return PartialView("xRegAnathesiMetaboliPartial", data);
        }

        #endregion ΑΝΑΘΕΣΕΙΣ ΜΕΤΑΒΟΛΩΝ ΩΡΟΜΙΣΘΙΟΙ


        #region ΑΝΑΘΕΣΕΙΣ ΜΕΤΑΒΟΛΩΝ ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult xRegAnatheseisMetaboliAn(string notify = null)
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

            var data = regAnathesiMetaboliAnService.Read();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αναθέσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin2", new { notify = msg });
            }
            RegAnathesiMetaboliAnViewModel anathesi = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(anathesi);
        }

        public ActionResult RegAnathesiMetaboliAn_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            var data = regAnathesiMetaboliAnService.Read(schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAnathesiMetaboliAnRecord(int recordId)
        {
            var data = regAnathesiMetaboliAnService.GetRecord(recordId);

            return PartialView("xRegAnathesiMetaboliAnPartial", data);
        }

        #endregion ΑΝΑΘΕΣΕΙΣ ΜΕΤΑΒΟΛΩΝ ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΚΑΘΟΛΙΚΟ ΜΗΤΡΩΟ ΑΝΑΘΕΣΕΩΝ 1 (ΚΑΤΑ ΕΙΔΙΚΟΤΗΤΕΣ)

        public ActionResult xAnatheseisUniversal(string notify = null)
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

            var data = regAnathesiUniversalService.Read();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αναθέσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin2", new { notify = msg });
            }
            AnatheseisUniversalViewModel anathesi = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(anathesi);
        }

        public ActionResult AnatheseisUniversal_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            var data = regAnathesiUniversalService.Read(schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAnathesiUniversalRecord(int recordId)
        {
            var data = regAnathesiUniversalService.GetRecord(recordId);

            return PartialView("xAnatheseisUniversalPartial", data);
        }

        #endregion


        #region ΚΑΘΟΛΙΚΟ ΜΗΤΡΩΟ ΑΝΑΘΕΣΕΩΝ 2 (ΚΑΤΑ ΕΝΙΑΙΟΥΣ ΚΛΑΔΟΥΣ)

        public ActionResult xAnatheseisUniversal2(string notify = null)
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

            var data = regAnathesiUniversalService.Read();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αναθέσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin2", new { notify = msg });
            }
            AnatheseisUniversalViewModel anathesi = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(anathesi);
        }

        public PartialViewResult GetAnathesiUniversal2Record(int recordId)
        {
            var data = regAnathesiUniversalService.GetRecord(recordId);

            return PartialView("xAnatheseisUniversal2Partial", data);
        }

        #endregion


        #region ΕΚΤΥΠΩΣΕΙΣ ΜΗΤΡΩΩΝ

        public ActionResult xregAnatheseisProslipsiPrint()
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
            return View();
        }

        public ActionResult xregAnatheseisMetabolesPrint()
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
            return View();
        }

        public ActionResult xregAnatheseisProslipsiAnaplPrint()
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
            return View();
        }

        public ActionResult xregAnatheseisMetabolesAnaplPrint()
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
            return View();
        }


        #endregion ΕΚΥΠΩΣΕΙΣ ΜΗΤΡΩΩΝ

        #endregion


        #region ΣΤΑΤΙΣΤΙΚΕΣ ΕΚΘΕΣΕΙΣ

        #region ΑΝΑΛΥΤΙΚΑ ΣΤΟΙΧΕΙΑ

        public ActionResult xReportsDetailList()
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

            List<SysReportViewModel> reports = GetReportsDetailFromDB();
            return View(reports);
        }

        public ActionResult ReportSelectorDetail(int reportId = 0)
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
            // logic of report selection here
            if (reportId == 1)
            {
                return RedirectToAction("AnatheseisAnaEidikotitaPrint", "Admin2");
            }
            else if (reportId == 2)
            {
                return RedirectToAction("AnatheseisAno20HoursPrint", "Admin2");
            }
            else if (reportId == 3)
            {
                return RedirectToAction("AnatheseisPollaplesPrint", "Admin2");
            }
            else if (reportId == 4)
            {
                return RedirectToAction("AnatheseisDetailsPrint", "Admin2");
            }
            else if (reportId == 5)
            {
                return RedirectToAction("AnatheseisDetailSummaryPrint", "Admin2");
            }
            else if (reportId == 6)
            {
                return RedirectToAction("AnatheseisDetailsList1Print", "Admin2");
            }
            else if (reportId == 7)
            {
                return RedirectToAction("AnatheseisDetailsList2Print", "Admin2");
            }
            else if (reportId == 8)
            {
                return RedirectToAction("AnatheseisCancelListPrint", "Admin2");
            }
            else if (reportId == 17)
            {
                return RedirectToAction("AnatheseisPrimaryEidikotitaPrint", "Admin2");
            }
            else if (reportId == 23)
            {
                return RedirectToAction("AnatheseisDetailsList3Print", "Admin2");
            }
            else if (reportId == 24)
            {
                return RedirectToAction("AnatheseisDetailsList4Print", "Admin2");
            }
            else if (reportId == 27)
            {
                return RedirectToAction("BudgetAnatheseisDatesPrint", "Admin2");
            }
            else if (reportId == 28)
            {
                return RedirectToAction("AnatheseisHourLimitsPrint", "Admin2");
            }
            else
            {
                return RedirectToAction("ReportDemoPrint", "Admin2");
            }
        }

        public ActionResult ReportsDetail_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetReportsDetailFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsDetailFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ
                        where d.DOC_CLASS == "DETAIL" orderby d.DOC_DESCRIPTION
                        select new SysReportViewModel
                        {
                            DOC_ID = d.DOC_ID,
                            DOC_NAME = d.DOC_NAME,
                            DOC_DESCRIPTION = d.DOC_DESCRIPTION,
                            DOC_CLASS = d.DOC_CLASS
                        }).ToList();
            return data;
        }

        #endregion


        #region ΣΥΓΚΕΝΤΡΩΤΙΚΑ ΣΤΟΙΧΕΙΑ

        public ActionResult xReportsSummaryList()
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

            List<SysReportViewModel> reports = GetReportsSummaryFromDB();
            return View(reports);
        }

        public ActionResult ReportSelectorSummary(int reportId = 0)
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
            // logic of report selection here
            if (reportId == 9)
            {
                return RedirectToAction("smTeachersKladosHoursPrint", "Admin2");
            }
            else if (reportId == 10)
            {
                return RedirectToAction("smAnatheseisKladosSchoolPrint", "Admin2");
            }
            else if (reportId == 11)
            {
                return RedirectToAction("smSimbasiKladosHoursPrint", "Admin2");
            }
            else if (reportId == 12)
            {
                return RedirectToAction("smAnatheseisKladosPeriferiaPrint", "Admin2");
            }
            else if (reportId == 13)
            {
                return RedirectToAction("smAnatheseisKladosGenderPrint", "Admin2");
            }
            else if (reportId == 14)
            {
                return RedirectToAction("smAnatheseisKladosDomiPrint", "Admin2");
            }
            else if (reportId == 15)
            {
                return RedirectToAction("smAnatheseisHoursDomiPrint", "Admin2");
            }
            else if (reportId == 16)
            {
                return RedirectToAction("smTeachersMonesPollaplesPrint", "Admin2");
            }
            else if (reportId == 18)
            {
                return RedirectToAction("smAnatheseisEidikotitaPrint", "Admin2");
            }
            else if (reportId == 19)
            {
                return RedirectToAction("statOpekaTeacherSummaryPrint", "Admin2");
            }
            else if (reportId == 20)
            {
                return RedirectToAction("hrEidikotitesManpowerPrint", "Admin2");
            }
            else if (reportId == 21)
            {
                return RedirectToAction("hrEidikotitesUnifiedSchoolPrint", "Admin2");
            }
            else if (reportId == 22)
            {
                return RedirectToAction("hrEidikotitesUnifiedPeriferiaPrint", "Admin2");
            }
            else if (reportId == 25)
            {
                return RedirectToAction("sysApofaseisPersonPrint", "Admin2");
            }
            else if (reportId == 26)
            {
                return RedirectToAction("sysApofaseisTypesPrint", "Admin2");
            }
            else if (reportId == 29)
            {
                return RedirectToAction("smEidikotitesPeriferiaPrint", "Admin2");
            }
            else if (reportId == 30)
            {
                return RedirectToAction("smEidikotitesManpowerPrint", "Admin2");
            }
            else if (reportId == 31)
            {
                return RedirectToAction("statTeachersPeriferiaSchoolPrint", "Admin2");
            }
            else if (reportId == 34)
            {
                return RedirectToAction("TeachersEidikotitaCountPrint", "Admin2");
            }
            else
            {
                return RedirectToAction("ReportDemoPrint", "Admin2");
            }
        }

        public ActionResult ReportsSummary_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetReportsSummaryFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SysReportViewModel> GetReportsSummaryFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΝΤΥΠΑ
                        where d.DOC_CLASS == "SUMMARY" orderby d.DOC_DESCRIPTION
                        select new SysReportViewModel
                        {
                            DOC_ID = d.DOC_ID,
                            DOC_NAME = d.DOC_NAME,
                            DOC_DESCRIPTION = d.DOC_DESCRIPTION,
                            DOC_CLASS = d.DOC_CLASS
                        }).ToList();
            return data;
        }

        #endregion

        public ActionResult ReportDemoPrint()
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
            return View();
        }


        #region ΑΝΑΛΥΤΙΚΑ ΣΤΟΙΧΕΙΑ

        public ActionResult AnatheseisHourLimitsPrint()
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
            return View();
        }

        public ActionResult BudgetAnatheseisDatesPrint()
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
            return View();
        }

        public ActionResult AnatheseisAnaEidikotitaPrint()
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
            return View();
        }

        public ActionResult AnatheseisAno20HoursPrint()
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
            return View();
        }

        public ActionResult AnatheseisPollaplesPrint()
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
            return View();
        }

        public ActionResult AnatheseisDetailsPrint()
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
            return View();
        }

        public ActionResult AnatheseisDetailSummaryPrint()
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
            return View();
        }

        public ActionResult AnatheseisDetailsList1Print()
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
            return View();
        }

        public ActionResult AnatheseisDetailsList2Print()
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
            return View();
        }

        public ActionResult AnatheseisDetailsList3Print()
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
            return View();
        }

        public ActionResult AnatheseisDetailsList4Print()
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
            return View();
        }

        public ActionResult AnatheseisCancelListPrint()
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
            return View();
        }

        public ActionResult AnatheseisPrimaryEidikotitaPrint()
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
            return View();
        }


        #endregion


        #region ΣΥΓΚΕΝΤΡΩΤΙΚΑ ΣΤΟΙΧΕΙΑ

        public ActionResult TeachersEidikotitaCountPrint()
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
            return View();
        }

        public ActionResult sysApofaseisPersonPrint()
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
            return View();
        }

        public ActionResult sysApofaseisTypesPrint()
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
            return View();
        }

        public ActionResult hrEidikotitesManpowerPrint()
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
            return View();
        }

        public ActionResult hrEidikotitesUnifiedSchoolPrint()
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
            return View();
        }

        public ActionResult hrEidikotitesUnifiedPeriferiaPrint()
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
            return View();
        }

        public ActionResult smTeachersKladosHoursPrint()
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
            return View();
        }

        public ActionResult smTeachersMonesPollaplesPrint()
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
            return View();
        }

        public ActionResult smAnatheseisKladosSchoolPrint()
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
            return View();
        }

        public ActionResult smSimbasiKladosHoursPrint()
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
            return View();
        }

        public ActionResult smAnatheseisKladosPeriferiaPrint()
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
            return View();
        }

        public ActionResult smAnatheseisKladosGenderPrint()
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
            return View();
        }

        public ActionResult smAnatheseisKladosDomiPrint()
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
            return View();
        }

        public ActionResult smAnatheseisHoursDomiPrint()
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
            return View();
        }

        public ActionResult smAnatheseisEidikotitaPrint()
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
            return View();
        }

        public ActionResult smEidikotitesPeriferiaPrint()
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
            return View();
        }

        public ActionResult smEidikotitesManpowerPrint()
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
            return View();
        }

        public ActionResult statOpekaTeacherSummaryPrint()
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
            return View();
        }

        public ActionResult statTeachersPeriferiaSchoolPrint()
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
            return View();
        }

        #endregion

        #endregion ΣΤΑΤΙΣΤΙΚΕΣ ΕΚΘΕΣΕΙΣ


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

        public void PopulateShoolYears()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).ToList();
            ViewData["schoolYears"] = data;
        }

        public void PopulateSchools()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.ΔΟΜΗ != 1 && d.ΔΟΜΗ != 4 orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ select d).ToList();
            ViewData["schools"] = data;
        }


        #endregion


        #region GETTERS

        public JsonResult GetAnathesiTypes()
        {
            var data = db.SYS_ANATHESI_TYPES.Select(m => new SysAnathesiTypeViewModel
            {
                ANATHESI_ID = m.ANATHESI_ID,
                ANATHESI_TYPE = m.ANATHESI_TYPE
            });

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

        public JsonResult GetGenders()
        {
            var genders = db.ΣΥΣ_ΦΥΛΑ.Select(p => new SysGenderViewModel
            {
                ΦΥΛΟ_ΚΩΔ = p.ΦΥΛΟ_ΚΩΔ,
                ΦΥΛΟ = p.ΦΥΛΟ
            });

            return Json(genders, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDomes()
        {
            var data = db.ΣΥΣ_ΕΚΠΑΙΔΕΥΤΙΚΕΣ_ΔΟΜΕΣ.Select(p => new SysDomesViewModel
            {
                ΚΩΔΙΚΟΣ = p.ΚΩΔΙΚΟΣ,
                ΜΟΝΑΔΑ = p.ΜΟΝΑΔΑ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPeriferiakes()
        {
            var data = db.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ.Select(p => new SysPeriferiakiViewModel
            {
                ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ = p.ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ,
                ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ = p.ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public USER_ARIADNE GetLoginAdmin()
        {
            loggedAdmin = db.USER_ARIADNE.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FULLNAME;

            return loggedAdmin;
        }

        #endregion


        #region EXTRAS

        public ActionResult UpdateAnatheseis()
        {
            var data1 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ select d).ToList();
            if (data1.Count > 0)
            {
                foreach (var d in data1)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            var data2 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ select d).ToList();
            if (data2.Count > 0)
            {
                foreach (var d in data2)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            var data3 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ select d).ToList();
            if (data3.Count > 0)
            {
                foreach (var d in data3)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            var data4 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ select d).ToList();
            if (data4.Count > 0)
            {
                foreach (var d in data4)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ target = db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var data5 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ select d).ToList();
            if (data5.Count > 0)
            {
                foreach (var d in data5)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var data6 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ select d).ToList();
            if (data6.Count > 0)
            {
                foreach (var d in data6)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var data7 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ select d).ToList();
            if (data7.Count > 0)
            {
                foreach (var d in data7)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var data8 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ select d).ToList();
            if (data8.Count > 0)
            {
                foreach (var d in data8)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ target = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            string message = "Η ενημέρωση των αναθέσεων πρόσληψης και μεταβολών (κλάδος, ωρομίσθιο, φύλο) ολοκληρώθηκε.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAnatheseisAnaplirotes()
        {
            var data1 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ select d).ToList();
            if (data1.Count > 0)
            {
                foreach (var d in data1)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotitaAN((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var data2 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ select d).ToList();
            if (data2.Count > 0)
            {
                foreach (var d in data2)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotitaAN((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var data3 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ select d).ToList();
            if (data3.Count > 0)
            {
                foreach (var d in data3)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotitaAN((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var data4 = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ select d).ToList();
            if (data4.Count > 0)
            {
                foreach (var d in data4)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);

                    target.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    target.ΦΥΛΟ = Common.GetGenderFromName(d.ΟΝΟΜΑ);
                    target.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotitaAN((int)d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            string message = "Η ενημέρωση των αναθέσεων πρόσληψης και μεταβολών (φύλο, κλάδος, ωρομίσθιο) ολοκληρώθηκε.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}