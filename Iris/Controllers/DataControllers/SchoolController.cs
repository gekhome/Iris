using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
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
    public class SchoolController : Controller
    {
        private readonly IrisDBEntities db;

        private USER_SCHOOLS loggedSchool;

        private readonly IAnathesiInitialService anathesiInitialService;
        private readonly IAnathesiDirectService anathesiDirectService;
        private readonly IAnathesiSupplementService anathesiSupplementService;
        private readonly IAnathesiSupplementAKService anathesiSupplementAKService;
        private readonly IAnathesiCancelService anathesiCancelService;
        private readonly IAnathesiModifyService anathesiModifyService;
        private readonly IAnathesiModifyAKService anathesiModifyAKService;

        private readonly IAnathesiInitialAnService anathesiInitialAnService;
        private readonly IAnathesiSupplementAnService anathesiSupplementAnService;
        private readonly IAnathesiCancelAnService anathesiCancelAnService;
        private readonly IAnathesiModifyAnService anathesiModifyAnService;
        private readonly IAnathesiModifyAnAKService anathesiModifyAnAKService;

        private readonly IRegAnathesiProslipsiService regAnathesiProslipsiService;
        private readonly IRegAnathesiProslipsiAnService regAnathesiProslipsiAnService;
        private readonly IRegAnathesiMetaboliService regAnathesiMetaboliService;
        private readonly IRegAnathesiMetaboliAnService regAnathesiMetaboliAnService;

        public SchoolController(IrisDBEntities entities,
            IAnathesiInitialService anathesiInitialService, IAnathesiDirectService anathesiDirectService,
            IAnathesiSupplementService anathesiSupplementService, IAnathesiSupplementAKService anathesiSupplementAKService,
            IAnathesiCancelService anathesiCancelService, IAnathesiModifyService anathesiModifyService,
            IAnathesiModifyAKService anathesiModifyAKService, IAnathesiInitialAnService anathesiInitialAnService,
            IAnathesiSupplementAnService anathesiSupplementAnService, IAnathesiCancelAnService anathesiCancelAnService,
            IAnathesiModifyAnService anathesiModifyAnService, IAnathesiModifyAnAKService anathesiModifyAnAKService,
            IRegAnathesiProslipsiService regAnathesiProslipsiService, IRegAnathesiProslipsiAnService regAnathesiProslipsiAnService,
            IRegAnathesiMetaboliService regAnathesiMetaboliService, IRegAnathesiMetaboliAnService regAnathesiMetaboliAnService)
        {
            db = entities;

            this.anathesiInitialService = anathesiInitialService;
            this.anathesiDirectService = anathesiDirectService;
            this.anathesiSupplementService = anathesiSupplementService;
            this.anathesiSupplementAKService = anathesiSupplementAKService;
            this.anathesiCancelService = anathesiCancelService;
            this.anathesiModifyService = anathesiModifyService;
            this.anathesiModifyAKService = anathesiModifyAKService;

            this.anathesiInitialAnService = anathesiInitialAnService;
            this.anathesiSupplementAnService = anathesiSupplementAnService;
            this.anathesiCancelAnService = anathesiCancelAnService;
            this.anathesiModifyAnService = anathesiModifyAnService;
            this.anathesiModifyAnAKService = anathesiModifyAnAKService;

            this.regAnathesiProslipsiService = regAnathesiProslipsiService;
            this.regAnathesiProslipsiAnService = regAnathesiProslipsiAnService;
            this.regAnathesiMetaboliService = regAnathesiMetaboliService;
            this.regAnathesiMetaboliAnService = regAnathesiMetaboliAnService;
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
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);
            return View();
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

        public ActionResult School_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetSchoolsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SchoolsGridViewModel> GetSchoolsFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ
                        orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ
                        select new SchoolsGridViewModel
                        {
                            ΣΧΟΛΗ_ΚΩΔ = d.ΣΧΟΛΗ_ΚΩΔ,
                            ΕΠΩΝΥΜΙΑ = d.ΕΠΩΝΥΜΙΑ,
                            ΔΟΜΗ = d.ΔΟΜΗ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ
                        }).ToList();

            return data;
        }


        #region ΠΕΡΙΦΕΡΕΙΕΣ-ΔΗΜΟΙ

        public ActionResult PeriferiesDimoi()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();          
            }
            return View();
        }

        public ActionResult Periferies([DataSourceRequest] DataSourceRequest request)
        {
            var periferies = db.SYS_PERIFERIES.Select(p => new PeriferiaViewModel()
            {
                PERIFERIA_ID = p.PERIFERIA_ID,
                PERIFERIA_NAME = p.PERIFERIA_NAME
            });
            return Json(periferies.ToDataSourceResult(request));
        }

        public ActionResult Dimoi([DataSourceRequest] DataSourceRequest request, int periferiaId)
        {
            var dimoi = db.SYS_DIMOS.Where(o => o.DIMOS_PERIFERIA == periferiaId).Select(p => new DimosViewModel()
            {
                DIMOS_ID = p.DIMOS_ID,
                DIMOS = p.DIMOS,
                DIMOS_PERIFERIA = p.DIMOS_PERIFERIA
            });
            return Json(dimoi.ToDataSourceResult(request));
        }

        public ActionResult PeriferiesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                return View();
            }
        }

        #endregion


        #region ΧΑΡΤΕΣ GOOGLE

        public ActionResult GoogleMaps()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            return View();
        }

        #endregion


        #region ΩΡΟΜΙΣΘΙΟΙ

        #region ΑΝΑΘΕΣΕΙΣ ΑΡΧΙΚΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisInitial()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();

            return View();
        }

        public ActionResult AnathesiInitial_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiInitialService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiInitial_Create([DataSourceRequest] DataSourceRequest request, AnathesiInitialViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiInitialViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateInitial(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη αρχική ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }

                if (data != null && ModelState.IsValid)
                {
                    anathesiInitialService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiInitialService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiInitial_Update([DataSourceRequest] DataSourceRequest request, AnathesiInitialViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiInitialViewModel();

            if (schoolyearId > 0)
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
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiInitial_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiInitialViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanEditDeleteInitial(data))
                {
                    anathesiInitialService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΑΡΧΙΚΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΑΠΕΥΘΕΙΑΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisDirect()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();

            return View();
        }

        public ActionResult AnathesiDirect_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiDirectService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiDirect_Create([DataSourceRequest] DataSourceRequest request, AnathesiDirectViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiDirectViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateDirect(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη απευθείας ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }

                if (data != null && ModelState.IsValid)
                {
                    anathesiDirectService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiDirectService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiDirect_Update([DataSourceRequest] DataSourceRequest request, AnathesiDirectViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiDirectViewModel();

            if (schoolyearId > 0)
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
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiDirect_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiDirectViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanEditDeleteDirect(data))
                {
                    anathesiDirectService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion


        #region ΑΝΑΘΕΣΕΙΣ ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisSupplement()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiSupplement_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiSupplementService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplement_Create([DataSourceRequest] DataSourceRequest request, AnathesiSupplementViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiSupplementViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateSupplement(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη συμπληρωματική ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiSupplementService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiSupplementService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplement_Update([DataSourceRequest] DataSourceRequest request, AnathesiSupplementViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiSupplementViewModel();

            if (schoolyearId > 0)
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
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplement_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiSupplementViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanEditDeleteSupplement(data))
                {
                    anathesiSupplementService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion


        #region ΑΝΑΘΕΣΕΙΣ ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΗΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisSupplementAK()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiSupplementAK_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiSupplementAKService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplementAK_Create([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAKViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiSupplementAKViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateSupplementAK(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη συμπληρωματική A.K. ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiSupplementAKService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiSupplementAKService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplementAK_Update([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAKViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiSupplementAKViewModel();

            if (schoolyearId > 0)
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
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplementAK_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAKViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanEditDeleteSupplementAK(data))
                {
                    anathesiSupplementAKService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion


        #region ΑΝΑΘΕΣΕΙΣ ΑΚΥΡΩΤΙΚΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisCancel()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiCancel_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiCancelService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiCancel_Create([DataSourceRequest] DataSourceRequest request, AnathesiCancelViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiCancelViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateCancel(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη ακυρωτική ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiCancelService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiCancelService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiCancel_Update([DataSourceRequest] DataSourceRequest request, AnathesiCancelViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiCancelViewModel();

            if (schoolyearId > 0)
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
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiCancel_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiCancelViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanEditDeleteCancel(data))
                {
                    anathesiCancelService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΑΚΥΡΩΤΙΚΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisModify()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiModify_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiModifyService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModify_Create([DataSourceRequest] DataSourceRequest request, AnathesiModifyViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiModifyViewModel();

            if (schoolyearId > 0)
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
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModify_Update([DataSourceRequest] DataSourceRequest request, AnathesiModifyViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiModifyViewModel();

            if (schoolyearId > 0)
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
                if (Kerberos.CanEditDeleteModify(data))
                {
                    anathesiModifyService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΗΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisModifyAK()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiModifyAK_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiModifyAKService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModifyAK_Create([DataSourceRequest] DataSourceRequest request, AnathesiModifyAKViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiModifyAKViewModel();

            if (schoolyearId > 0)
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
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModifyAK_Update([DataSourceRequest] DataSourceRequest request, AnathesiModifyAKViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiModifyAKViewModel();

            if (schoolyearId > 0)
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
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModifyAK_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiModifyAKViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanEditDeleteModifyAK(data))
                {
                    anathesiModifyAKService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ AK

        #endregion ΩΡΟΜΙΣΘΙΟΙ


        #region ΑΝΑΠΛΗΡΩΤΕΣ

        #region ΑΝΑΘΕΣΕΙΣ ΑΡΧΙΚΕΣ ΑΝΑΠΛΗΡΩΤΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisInitialAnaplirotes()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiInitialAN_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiInitialAnService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiInitialAN_Create([DataSourceRequest] DataSourceRequest request, AnathesiInitialAnaplirotesViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiInitialAnaplirotesViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateInitialAnaplirotes(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη αρχική ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiInitialAnService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiInitialAnService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiInitialAN_Update([DataSourceRequest] DataSourceRequest request, AnathesiInitialAnaplirotesViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiInitialAnaplirotesViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteInitialAnaplirotes(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiInitialAnService.Update(data, schoolyearId, schoolId);
                    newdata = anathesiInitialAnService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiInitialAN_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiInitialAnaplirotesViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanEditDeleteInitialAnaplirotes(data))
                {
                    anathesiInitialAnService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΑΡΧΙΚΕΣ ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ ΑΝΑΠΛΗΡΩΤΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisSupplementAnaplirotes()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiSupplementAN_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiSupplementAnService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplementAN_Create([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAnaplirotesViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiSupplementAnaplirotesViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateSupplementAnaplirotes(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη συμπληρωματική ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiSupplementAnService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiSupplementAnService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplementAN_Update([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAnaplirotesViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiSupplementAnaplirotesViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteSupplementAnaplirotes(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiSupplementAnService.Update(data, schoolyearId, schoolId);
                    newdata = anathesiSupplementAnService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiSupplementAN_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiSupplementAnaplirotesViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanEditDeleteSupplementAnaplirotes(data))
                {
                    anathesiSupplementAnService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΑΚΥΡΩΤΙΚΕΣ ΑΝΑΠΛΗΡΩΤΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisCancelAnaplirotes()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiCancelAN_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiCancelAnService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiCancelAN_Create([DataSourceRequest] DataSourceRequest request, AnathesiCancelAnaplirotesViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiCancelAnaplirotesViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanCreateCancelAnaplirotes(data.ΑΦΜ, schoolId, schoolyearId))
                {
                    ModelState.AddModelError("", "Υπάρχει ήδη ακυρωτική ανάθεση για τον εκπαιδευτικό γι' αυτό το σχολικό έτος. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiCancelAnService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiCancelAnService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiCancelAN_Update([DataSourceRequest] DataSourceRequest request, AnathesiCancelAnaplirotesViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiCancelAnaplirotesViewModel();

            if (schoolyearId > 0 && schoolId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteCancelAnaplirotes(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiCancelAnService.Update(data, schoolyearId, schoolId);
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
        public ActionResult AnathesiCancelAN_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiCancelAnaplirotesViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanEditDeleteCancelAnaplirotes(data))
                {
                    anathesiCancelAnService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΑΚΥΡΩΤΙΚΕΣ ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΝΑΠΛΗΡΩΤΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisModifyAnaplirotes()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiModifyAN_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiModifyAnService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModifyAN_Create([DataSourceRequest] DataSourceRequest request, AnathesiModifyAnaplirotesViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiModifyAnaplirotesViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyAnService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiModifyAnService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModifyAN_Update([DataSourceRequest] DataSourceRequest request, AnathesiModifyAnaplirotesViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiModifyAnaplirotesViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteModifyAnaplirotes(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyAnService.Update(data, schoolyearId, schoolId);
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
        public ActionResult AnathesiModifyAN_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiModifyAnaplirotesViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanEditDeleteModifyAnaplirotes(data))
                {
                    anathesiModifyAnService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ ΑΣΘΕΝΕΙΑΣ-ΚΥΗΣΗΣ ΑΝΑΠΛΗΡΩΤΕΣ

        #region GRID CRUD FUNCTIONS

        public ActionResult sAnatheseisModifyAnaplirotesAK()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateSchoolDomes();
            PopulatePeriferiakes();
            PopulateEidikotites();
            return View();
        }

        public ActionResult AnathesiModifyANAK_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = anathesiModifyAnAKService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModifyANAK_Create([DataSourceRequest] DataSourceRequest request, AnathesiModifyAnaplirotesAKViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiModifyAnaplirotesAKViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }

                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyAnAKService.Create(data, schoolyearId, schoolId);
                    newdata = anathesiModifyAnAKService.Refresh(data.ΑΝΑΘΕΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή σχολείου και σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AnathesiModifyANAK_Update([DataSourceRequest] DataSourceRequest request, AnathesiModifyAnaplirotesAKViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newdata = new AnathesiModifyAnaplirotesAKViewModel();

            if (schoolyearId > 0)
            {
                if (!Common.CheckAFM(data.ΑΦΜ))
                {
                    ModelState.AddModelError("", "Το ΑΦΜ που δόθηκε δεν είναι έγκυρο. Η ενημέρωση ακυρώθηκε.");
                }
                if (!Kerberos.CanEditDeleteModifyAnaplirotesAK(data))
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η ενημέρωση ακυρώθηκε.");
                }
                if (data != null && ModelState.IsValid)
                {
                    anathesiModifyAnAKService.Update(data, schoolyearId, schoolId);
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
        public ActionResult AnathesiModifyANAK_Destroy([DataSourceRequest] DataSourceRequest request, AnathesiModifyAnaplirotesAKViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanEditDeleteModifyAnaplirotesAK(data))
                {
                    anathesiModifyAnAKService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ανάθεση αυτή έχει εκδοθεί σε Απόφαση. Η διαγραφή ακυρώθηκε.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion GRID CRUD FUNCTIONS

        #endregion ΑΝΑΘΕΣΕΙΣ ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ AK

        #endregion ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΜΗΤΡΩΑ ΑΝΑΘΕΣΕΩΝ

        #region ΑΝΑΘΕΣΕΙΣ ΠΡΟΣΛΗΨΗΣ ΩΡΟΜΙΣΘΙΟΙ

        public ActionResult sRegAnatheseisProslipsi(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            var data = regAnathesiProslipsiService.ReadSchool(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αναθέσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
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
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = regAnathesiProslipsiService.ReadSchool(schoolId, schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAnathesiProslipsiRecord(int recordId)
        {
            var data = regAnathesiProslipsiService.GetRecord(recordId);

            return PartialView("sRegAnathesiProslipsiPartial", data);
        }

        #endregion ΑΝΑΘΕΣΕΙΣ ΠΡΟΣΛΗΨΗΣ ΩΡΟΜΙΣΘΙΟΙ


        #region ΑΝΑΘΕΣΕΙΣ ΠΡΟΣΛΗΨΗΣ ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult sRegAnatheseisProslipsiAn(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            var data = regAnathesiProslipsiAnService.ReadSchool(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αναθέσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
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
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = regAnathesiProslipsiAnService.ReadSchool(schoolId, schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAnathesiProslipsiAnRecord(int recordId)
        {
            var data = regAnathesiProslipsiAnService.GetRecord(recordId);

            return PartialView("sRegAnathesiProslipsiAnPartial", data);
        }

        #endregion ΑΝΑΘΕΣΕΙΣ ΠΡΟΣΛΗΨΗΣ ΑΝΑΠΛΗΡΩΤΕΣ


        #region ΑΝΑΘΕΣΕΙΣ ΜΕΤΑΒΟΛΩΝ ΩΡΟΜΙΣΘΙΟΙ

        public ActionResult sRegAnatheseisMetaboli(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            var data = regAnathesiMetaboliService.ReadSchool(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αναθέσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
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
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = regAnathesiMetaboliService.ReadSchool(schoolId, schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAnathesiMetaboliRecord(int recordId)
        {
            var data = regAnathesiMetaboliService.GetRecord(recordId);

            return PartialView("sRegAnathesiMetaboliPartial", data);
        }

        #endregion ΑΝΑΘΕΣΕΙΣ ΜΕΤΑΒΟΛΩΝ ΩΡΟΜΙΣΘΙΟΙ


        #region ΑΝΑΘΕΣΕΙΣ ΜΕΤΑΒΟΛΩΝ ΑΝΑΠΛΗΡΩΤΕΣ

        public ActionResult sRegAnatheseisMetaboliAn(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            var data = regAnathesiMetaboliAnService.ReadSchool(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αναθέσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
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
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = regAnathesiMetaboliAnService.ReadSchool(schoolId, schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAnathesiMetaboliAnRecord(int recordId)
        {
            var data = regAnathesiMetaboliAnService.GetRecord(recordId);

            return PartialView("sRegAnathesiMetaboliAnPartial", data);
        }

        #endregion ΑΝΑΘΕΣΕΙΣ ΜΕΤΑΒΟΛΩΝ ΑΝΑΠΛΗΡΩΤΕΣ

        #endregion


        #region PRINTOUTS

        public ActionResult AnatheseisCancelPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        // ανακλητικές δεν υπάρχουν για τα σχολεία
        public ActionResult AnatheseisRevokePrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        public ActionResult AnatheseisDirectPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        public ActionResult AnatheseisInitialPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        public ActionResult AnatheseisModifyPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        public ActionResult AnatheseisModifyAKPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        public ActionResult AnatheseisSupplementPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        public ActionResult AnatheseisSupplementAKPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        public ActionResult AnatheseisCancelAnPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        public ActionResult AnatheseisInitialAnPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        public ActionResult AnatheseisSupplementAnPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        public ActionResult AnatheseisModifyAnPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }

        public ActionResult AnatheseisModifyAnAKPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();

                AnatheseisParameters ap = new AnatheseisParameters();
                ap.schoolID = (int)loggedSchool.USER_SCHOOLID;
                ap.schoolYearID = 0;
                return View(ap);
            }
        }


        #endregion


        #region ΠΑΛΑΙΕΣ ΕΙΔΙΚΟΤΗΤΕΣ ΕΚΠΑΙΔΕΥΤΩΝ
        // ΚΩΔΙΚΟΛΟΓΙΟ ΕΙΔΙΚΟΤΗΤΩΝ

        public ActionResult EidikotitesOldList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            PopulateKladoi();
            List<SysEidikotitesOldViewModel> data = GetEidikotitesOldFromDB();
            return View(data);
        }

        [HttpPost]
        public ActionResult EidikotitaOld_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetEidikotitesOldFromDB();

            var result = new JsonResult();
            result.Data = data.ToDataSourceResult(request);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaOld_Create([DataSourceRequest] DataSourceRequest request, SysEidikotitesOldViewModel data)
        {
            var newdata = new SysEidikotitesOldViewModel();

            var existingData = db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ.Where(s => s.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ == data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ).ToList();
            if (existingData.Count > 0) ModelState.AddModelError("", "Η ειδικότητα αυτή υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ entity = new ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ()
                {
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = data.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ,
                    ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ
                };
                db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ.Add(entity);
                db.SaveChanges();
                data.ΚΩΔΙΚΟΣ = entity.ΚΩΔΙΚΟΣ;
                newdata = RefreshEidikotitaOldFromDB(data.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaOld_Update([DataSourceRequest] DataSourceRequest request, SysEidikotitesOldViewModel data)
        {
            var newdata = new SysEidikotitesOldViewModel();

            if (data != null)
            {
                ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ entity = db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ.Find(data.ΚΩΔΙΚΟΣ);

                entity.ΚΩΔΙΚΟΣ = data.ΚΩΔΙΚΟΣ;
                entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ;
                entity.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = data.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ;
                entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                newdata = RefreshEidikotitaOldFromDB(entity.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaOld_Destroy([DataSourceRequest] DataSourceRequest request, SysEidikotitesOldViewModel data)
        {
            if (data != null)
            {
                ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ entity = db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ.Find(data.ΚΩΔΙΚΟΣ);
                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public SysEidikotitesOldViewModel RefreshEidikotitaOldFromDB(int recordId)
        {
            var data = (from d in db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ
                        where d.ΚΩΔΙΚΟΣ == recordId
                        select new SysEidikotitesOldViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = d.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
                        }).FirstOrDefault();

            return data;
        }

        public List<SysEidikotitesOldViewModel> GetEidikotitesOldFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ
                        orderby d.ΚΛΑΔΟΣ, d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ
                        select new SysEidikotitesOldViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = d.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
                        }).ToList();

            return data;
        }

        public ActionResult EidikotitesOldPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                return View();
            }
        }

        #endregion


        #region ΝΕΕΣ ΕΙΔΙΚΟΤΗΤΕΣ ΕΚΠΑΙΔΕΥΤΩΝ (2018)

        public ActionResult EidikotitesList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            PopulateKladoi();
            PopulateKladoiUnified();
            List<SysEidikotitesViewModel> data = GetEidikotitesFromDB();
            return View(data);
        }

        public ActionResult Eidikotita_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetEidikotitesFromDB();

            var result = new JsonResult();
            result.Data = data.ToDataSourceResult(request);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Create([DataSourceRequest] DataSourceRequest request, SysEidikotitesViewModel data)
        {
            var newdata = new SysEidikotitesViewModel();

            var existingData = db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Where(s => s.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ == data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ).ToList();
            if (existingData.Count > 0) ModelState.AddModelError("", "Η ειδικότητα αυτή υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ entity = new ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ()
                {
                    ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = data.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ,
                    ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ = data.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ,
                    ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                    ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ
                };
                db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Add(entity);
                db.SaveChanges();
                data.ΚΩΔΙΚΟΣ = entity.ΚΩΔΙΚΟΣ;
                newdata = RefreshEidikotitaFromDB(data.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Update([DataSourceRequest] DataSourceRequest request, SysEidikotitesViewModel data)
        {
            var newdata = new SysEidikotitesViewModel();

            if (data != null)
            {
                ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ entity = db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.ΚΩΔΙΚΟΣ);

                entity.ΚΩΔΙΚΟΣ = data.ΚΩΔΙΚΟΣ;
                entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ;
                entity.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = data.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ;
                entity.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ = data.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ;
                entity.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ;
                entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                newdata = RefreshEidikotitaFromDB(entity.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Destroy([DataSourceRequest] DataSourceRequest request, SysEidikotitesViewModel data)
        {
            if (data != null)
            {
                ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ entity = db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.ΚΩΔΙΚΟΣ);
                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public SysEidikotitesViewModel RefreshEidikotitaFromDB(int recordId)
        {
            var data = (from d in db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ
                        where d.ΚΩΔΙΚΟΣ == recordId
                        select new SysEidikotitesViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = d.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ = d.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ,
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
                        }).FirstOrDefault();

            return data;
        }

        public List<SysEidikotitesViewModel> GetEidikotitesFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ
                        orderby d.ΚΛΑΔΟΣ, d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ
                        select new SysEidikotitesViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = d.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ = d.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ,
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
                        }).ToList();

            return data;
        }

        public ActionResult EidikotitesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                return View();
            }
        }


        #endregion ΝΕΕΣ ΕΙΔΙΚΟΤΗΤΕΣ ΕΚΠΑΙΔΕΥΤΩΝ


        #region ΣΥΣΧΕΤΙΣΗ ΠΑΛΑΙΩΝ-ΝΕΩΝ ΕΙΔΙΚΟΤΗΤΩΝ

        public ActionResult EidikotitesOldNewList()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            List<SysEidikotitesOldNewViewModel> data = GetEidikotitesOldNewFromDB();
            return View(data);
        }

        public ActionResult EidikotitaOldNew_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetEidikotitesOldNewFromDB();

            var result = new JsonResult();
            result.Data = data.ToDataSourceResult(request);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public List<SysEidikotitesOldNewViewModel> GetEidikotitesOldNewFromDB()
        {
            var data = (from d in db.sqlEIDIKOTITES_OLDNEW
                        orderby d.ΚΛΑΔΟΣ, d.ΕΙΔΙΚΟΤΗΤΑ_ΠΑΛΙΑ
                        select new SysEidikotitesOldNewViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΠΑΛΙΑ = d.ΕΙΔΙΚΟΤΗΤΑ_ΠΑΛΙΑ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΝΕΑ = d.ΕΙΔΙΚΟΤΗΤΑ_ΝΕΑ,
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
                        }).ToList();

            return data;
        }

        public ActionResult EidikotitesOldNewPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOLS");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                return View();
            }
        }

        #endregion ΣΥΣΧΕΤΙΣΗ ΠΑΛΑΙΩΝ-ΝΕΩΝ ΕΙΔΙΚΟΤΗΤΩΝ


        #region POPULATORS

        public void PopulateKladoiUnified()
        {
            var kladoiUnified = (from k in db.ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ select k).ToList();

            ViewData["kladoiUnified"] = kladoiUnified;
            ViewData["kladoiUnifiedDefault"] = kladoiUnified.First().ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ;
        }

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
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ select d).ToList();
            ViewData["schools"] = data;
        }


        #endregion


        #region GETTERS

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

        #endregion

    }
}
