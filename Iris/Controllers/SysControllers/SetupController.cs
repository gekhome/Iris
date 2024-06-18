using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Iris.DAL;
using Iris.Filters;
using Iris.Models;
using Iris.BPM;
using Iris.Notification;
using Iris.Services;

namespace Iris.Controllers.SysControllers
{
    [ErrorHandlerFilter]
    public class SetupController : Controller
    {
        private readonly IrisDBEntities db;
        private USER_ARIADNE loggedAdmin;

        private readonly ISchoolService schoolService;
        private readonly ISchoolAccountService schoolAccountService;
        private readonly ISchoolYearService schoolYearService;
        private readonly IProkirixiApofasiService prokirixiApofasiService;
        private readonly IParametersApofasiService parametersApofasiService;
        private readonly IEpitropesApofasiService epitropesApofasiService;

        private readonly IEidikotitesOldService eidikotitesOldService;
        private readonly IEidikotitesService eidikotitesService;
        private readonly IPeriferiakesService periferiakesService;
        private readonly IKladoiHoursService kladoiHoursService;
        private readonly IHourWagesService hourWagesService;

        private readonly IDiaxiristes2Service diaxiristes2Service;
        private readonly IProistamenoi2Service proistamenoi2Service;
        private readonly IDirectors2Service directors2Service;
        private readonly IGenikoi2Service genikoi2Service;
        private readonly IAntiproedroi2Service antiproedroi2Service;
        private readonly IDioikites2Service dioikites2Service;

        public SetupController(IrisDBEntities entities, ISchoolService schoolService,
            ISchoolAccountService schoolAccountService, ISchoolYearService schoolYearService,
            IProkirixiApofasiService prokirixiApofasiService, IParametersApofasiService parametersApofasiService,
            IEpitropesApofasiService epitropesApofasiService, IEidikotitesOldService eidikotitesOldService,
            IEidikotitesService eidikotitesService, IPeriferiakesService periferiakesService,
            IKladoiHoursService kladoiHoursService, IHourWagesService hourWagesService,
            IDiaxiristes2Service diaxiristes2Service, IProistamenoi2Service proistamenoi2Service,
            IDirectors2Service directors2Service, IGenikoi2Service genikoi2Service,
            IAntiproedroi2Service antiproedroi2Service, IDioikites2Service dioikites2Service)
        {
            db = entities;

            this.schoolService = schoolService;
            this.schoolAccountService = schoolAccountService;
            this.schoolYearService = schoolYearService;
            this.prokirixiApofasiService = prokirixiApofasiService;
            this.parametersApofasiService = parametersApofasiService;
            this.epitropesApofasiService = epitropesApofasiService;
            this.eidikotitesOldService = eidikotitesOldService;

            this.eidikotitesService = eidikotitesService;
            this.periferiakesService = periferiakesService;
            this.kladoiHoursService = kladoiHoursService;
            this.hourWagesService = hourWagesService;

            this.diaxiristes2Service = diaxiristes2Service;
            this.proistamenoi2Service = proistamenoi2Service;
            this.directors2Service = directors2Service;
            this.genikoi2Service = genikoi2Service;
            this.antiproedroi2Service = antiproedroi2Service;
            this.dioikites2Service = dioikites2Service;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        #region ΣΧΟΛΕΙΑ (ΣΤΟΙΧΕΙΑ)

        public ActionResult xSchoolsList()
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
            return View();
        }

        public ActionResult School_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = schoolService.Read("D3");

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult School_Create([DataSourceRequest] DataSourceRequest request, SchoolsGridViewModel data)
        {
            var newData = new SchoolsGridViewModel();

            var existingSchool = db.ΣΥΣ_ΣΧΟΛΕΣ.Where(s => s.ΔΟΜΗ == data.ΔΟΜΗ && s.ΕΠΩΝΥΜΙΑ == data.ΕΠΩΝΥΜΙΑ).Count();

            if (existingSchool > 0) 
                ModelState.AddModelError("", "Το σχολείο αυτό υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                schoolService.Create(data);
                newData = schoolService.Refresh(data.ΣΧΟΛΗ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult School_Update([DataSourceRequest] DataSourceRequest request, SchoolsGridViewModel data)
        {
            var newData = new SchoolsGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                schoolService.Update(data);
                newData = schoolService.Refresh(data.ΣΧΟΛΗ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult School_Destroy([DataSourceRequest] DataSourceRequest request, SchoolsGridViewModel data)
        {
            if (data != null)
            {
                schoolService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #region SCHOOL DATA FORM

        public ActionResult xSchoolEdit(int schoolId)
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

            SchoolsViewModel school = GetSchoolViewModelFromDB(schoolId);
            if (school == null)
            {
                return HttpNotFound();
            }

            SchoolsViewModel schoolData = GetSchoolViewModelFromDB(schoolId);
            return View(schoolData);
        }

        [HttpPost]
        public ActionResult xSchoolEdit(int schoolId, SchoolsViewModel data)
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

            if (ModelState.IsValid)
            {
                ΣΥΣ_ΣΧΟΛΕΣ entity = db.ΣΥΣ_ΣΧΟΛΕΣ.Find(schoolId);

                entity.ΕΠΩΝΥΜΙΑ = data.ΕΠΩΝΥΜΙΑ.Trim();
                entity.ΔΟΜΗ = data.ΔΟΜΗ;
                entity.ΓΡΑΜΜΑΤΕΙΑ = data.ΓΡΑΜΜΑΤΕΙΑ.Trim();
                entity.ΔΙΕΥΘΥΝΤΗΣ = data.ΔΙΕΥΘΥΝΤΗΣ.Trim();
                entity.ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = data.ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ;
                entity.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ = data.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ.Trim();
                entity.ΤΗΛΕΦΩΝΑ = data.ΤΗΛΕΦΩΝΑ.Trim();
                entity.ΦΑΞ = data.ΦΑΞ.Trim();
                entity.EMAIL = data.EMAIL.Trim();
                entity.ΚΙΝΗΤΟ = data.ΚΙΝΗΤΟ;
                entity.ΥΠΟΔΙΕΥΘΥΝΤΗΣ = data.ΥΠΟΔΙΕΥΘΥΝΤΗΣ.HasValue() ? data.ΥΠΟΔΙΕΥΘΥΝΤΗΣ.Trim() : data.ΥΠΟΔΙΕΥΘΥΝΤΗΣ;
                entity.ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = data.ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ;
                entity.ΠΕΡΙΦΕΡΕΙΑΚΗ = data.ΠΕΡΙΦΕΡΕΙΑΚΗ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                // Notify here
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                return View(data);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public SchoolsViewModel GetSchoolViewModelFromDB(int schoolId)
        {
            SchoolsViewModel schoolData;

            schoolData = (from s in db.ΣΥΣ_ΣΧΟΛΕΣ
                          where s.ΣΧΟΛΗ_ΚΩΔ == schoolId
                          select new SchoolsViewModel
                          {
                              ΣΧΟΛΗ_ΚΩΔ = s.ΣΧΟΛΗ_ΚΩΔ,
                              ΕΠΩΝΥΜΙΑ = s.ΕΠΩΝΥΜΙΑ,
                              ΔΟΜΗ = s.ΔΟΜΗ,
                              ΔΙΕΥΘΥΝΤΗΣ = s.ΔΙΕΥΘΥΝΤΗΣ,
                              ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = s.ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ,
                              ΤΑΧ_ΔΙΕΥΘΥΝΣΗ = s.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ,
                              ΤΗΛΕΦΩΝΑ = s.ΤΗΛΕΦΩΝΑ,
                              ΓΡΑΜΜΑΤΕΙΑ = s.ΓΡΑΜΜΑΤΕΙΑ,
                              ΦΑΞ = s.ΦΑΞ,
                              EMAIL = s.EMAIL,
                              ΚΙΝΗΤΟ = s.ΚΙΝΗΤΟ,
                              ΥΠΟΔΙΕΥΘΥΝΤΗΣ = s.ΥΠΟΔΙΕΥΘΥΝΤΗΣ,
                              ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = s.ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ,
                              ΠΕΡΙΦΕΡΕΙΑΚΗ = s.ΠΕΡΙΦΕΡΕΙΑΚΗ
                          }).FirstOrDefault();

            return schoolData;
        }

        #endregion

        public ActionResult xSchoolDataPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
                return View();
            }
        }

        public ActionResult xSchoolData2Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
                return View();
            }
        }

        #endregion


        #region ΔΙΟΙΚΗΣΗ - ΥΠΟΓΡΑΦΟΝΤΕΣ ΑΠΟΦΑΣΕΙΣ

        public ActionResult xAdministrators()
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
            PopulateShoolYears();
            PopulateGenders();
            return View();
        }

        #region ΔΙΑΧΕΙΡΙΣΤΕΣ

        public ActionResult Diaxiristis_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = diaxiristes2Service.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Diaxiristis_Create([DataSourceRequest] DataSourceRequest request, DiaxiristisViewModel data)
        {
            var newData = new DiaxiristisViewModel();

            var existingdata = db.Δ_ΔΙΑΧΕΙΡΙΣΤΕΣ.Where(s => s.ΟΝΟΜΑΤΕΠΩΝΥΜΟ == data.ΟΝΟΜΑΤΕΠΩΝΥΜΟ).Count();
            if (existingdata > 0)
                ModelState.AddModelError("", "Ο διαχειριστής αυτός υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                diaxiristes2Service.Create(data);
                newData = diaxiristes2Service.Refresh(data.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Diaxiristis_Update([DataSourceRequest] DataSourceRequest request, DiaxiristisViewModel data)
        {
            var newData = new DiaxiristisViewModel();

            if (data != null & ModelState.IsValid)
            {
                diaxiristes2Service.Update(data);
                newData = diaxiristes2Service.Refresh(data.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Diaxiristis_Destroy([DataSourceRequest] DataSourceRequest request, DiaxiristisViewModel data)
        {
            if (data != null)
            {
                diaxiristes2Service.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΔΙΑΧΕΙΡΙΣΤΕΣ

        #region ΠΡΟΪΣΤΑΜΕΝΟΙ

        public ActionResult Proistamenos_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = proistamenoi2Service.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Proistamenos_Create([DataSourceRequest] DataSourceRequest request, ProistamenosViewModel data)
        {
            var newData = new ProistamenosViewModel();

            //var existingdata = db.Δ_ΠΡΟΙΣΤΑΜΕΝΟΙ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            //if (existingdata > 0)
            //    ModelState.AddModelError("", "Υπάρχουν ήδη στοιχεία γι' αυτό το σχολικό έτος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                proistamenoi2Service.Create(data);
                newData = proistamenoi2Service.Refresh(data.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Proistamenos_Update([DataSourceRequest] DataSourceRequest request, ProistamenosViewModel data)
        {
            var newData = new ProistamenosViewModel();

            if (data != null & ModelState.IsValid)
            {
                proistamenoi2Service.Update(data);
                newData = proistamenoi2Service.Refresh(data.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Proistamenos_Destroy([DataSourceRequest] DataSourceRequest request, ProistamenosViewModel data)
        {
            if (data != null)
            {
                proistamenoi2Service.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #endregion ΠΡΟΪΣΤΑΜΕΝΟΙ

        #region ΔΙΕΥΘΥΝΤΕΣ

        public ActionResult Director_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = directors2Service.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Director_Create([DataSourceRequest] DataSourceRequest request, DirectorViewModel data)
        {
            var newData = new DirectorViewModel();

            //var existingdata = db.Δ_ΔΙΕΥΘΥΝΤΕΣ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            //if (existingdata > 0)
            //    ModelState.AddModelError("", "Υπάρχουν ήδη στοιχεία γι' αυτό το σχολικό έτος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                directors2Service.Create(data);
                newData = directors2Service.Refresh(data.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Director_Update([DataSourceRequest] DataSourceRequest request, DirectorViewModel data)
        {
            var newData = new DirectorViewModel();

            if (data != null & ModelState.IsValid)
            {
                directors2Service.Update(data);
                newData = directors2Service.Refresh(data.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Director_Destroy([DataSourceRequest] DataSourceRequest request, DirectorViewModel data)
        {
            if (data != null)
            {
                directors2Service.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #endregion ΔΙΕΥΘΥΝΤΕΣ

        #region ΓΕΝΙΚΟΙ ΔΙΕΥΘΥΝΤΕΣ

        public ActionResult Genikos_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = genikoi2Service.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Genikos_Create([DataSourceRequest] DataSourceRequest request, DirectorGeneralViewModel data)
        {
            var newData = new DirectorGeneralViewModel();

            //var existingdata = db.Δ_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            //if (existingdata > 0)
            //    ModelState.AddModelError("", "Υπάρχουν ήδη στοιχεία γι' αυτό το σχολικό έτος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                genikoi2Service.Create(data);
                newData = genikoi2Service.Refresh(data.ΓΕΝΙΚΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Genikos_Update([DataSourceRequest] DataSourceRequest request, DirectorGeneralViewModel data)
        {
            var newData = new DirectorGeneralViewModel();

            if (data != null & ModelState.IsValid)
            {
                genikoi2Service.Update(data);
                newData = genikoi2Service.Refresh(data.ΓΕΝΙΚΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Genikos_Destroy([DataSourceRequest] DataSourceRequest request, DirectorGeneralViewModel data)
        {
            if (data != null)
            {
                genikoi2Service.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #endregion ΓΕΝΙΚΟΙ ΔΙΕΥΘΥΝΤΕΣ

        #region ΑΝΤΙΠΡΟΕΔΡΟΙ

        public ActionResult Antiproedros_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = antiproedroi2Service.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Antiproedros_Create([DataSourceRequest] DataSourceRequest request, AntiproedrosViewModel data)
        {
            var newData = new AntiproedrosViewModel();

            //var existingdata = db.Δ_ΑΝΤΙΠΡΟΕΔΡΟΙ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            //if (existingdata > 0)
            //    ModelState.AddModelError("", "Υπάρχουν ήδη στοιχεία γι' αυτό το σχολικό έτος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                antiproedroi2Service.Create(data);
                newData = antiproedroi2Service.Refresh(data.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Antiproedros_Update([DataSourceRequest] DataSourceRequest request, AntiproedrosViewModel data)
        {
            var newData = new AntiproedrosViewModel();

            if (data != null & ModelState.IsValid)
            {
                antiproedroi2Service.Update(data);
                newData = antiproedroi2Service.Refresh(data.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Antiproedros_Destroy([DataSourceRequest] DataSourceRequest request, AntiproedrosViewModel data)
        {
            if (data != null)
            {
                antiproedroi2Service.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΑΝΤΙΠΡΟΕΔΡΟΙ

        #region ΔΙΟΙΚΗΤΕΣ

        public ActionResult Dioikitis_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = dioikites2Service.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Dioikitis_Create([DataSourceRequest] DataSourceRequest request, DioikitisViewModel data)
        {
            var newData = new DioikitisViewModel();

            //var existingdata = db.Δ_ΔΙΟΙΚΗΤΕΣ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            //if (existingdata > 0)
            //    ModelState.AddModelError("", "Υπάρχουν ήδη στοιχεία γι' αυτό το σχολικό έτος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                dioikites2Service.Create(data);
                newData = dioikites2Service.Refresh(data.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Dioikitis_Update([DataSourceRequest] DataSourceRequest request, DioikitisViewModel data)
        {
            var newData = new DioikitisViewModel();

            if (data != null & ModelState.IsValid)
            {
                dioikites2Service.Update(data);
                newData = dioikites2Service.Refresh(data.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Dioikitis_Destroy([DataSourceRequest] DataSourceRequest request, DioikitisViewModel data)
        {
            if (data != null)
            {
                dioikites2Service.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΔΙΟΙΚΗΤΕΣ

        #endregion


        #region ΣΧΟΛΙΚΑ ΕΤΗ

        public ActionResult xSchoolYearsList()
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

        public ActionResult SchoolYear_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = schoolYearService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Create([DataSourceRequest] DataSourceRequest request, SysSchoolYearViewModel data)
        {
            var newData = new SysSchoolYearViewModel();

            var existingdata = db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            if (existingdata > 0) 
                ModelState.AddModelError("", "Αυτό το σχολικό έτος υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                schoolYearService.Create(data);
                newData = schoolYearService.Refresh(data.SCHOOLYEAR_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Update([DataSourceRequest] DataSourceRequest request, SysSchoolYearViewModel data)
        {
            var newData = new SysSchoolYearViewModel();

            if (data != null && ModelState.IsValid)
            {
                schoolYearService.Update(data);
                newData = schoolYearService.Refresh(data.SCHOOLYEAR_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Destroy([DataSourceRequest] DataSourceRequest request, SysSchoolYearViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteSchoolYear(data.SCHOOLYEAR_ID))
                {
                    schoolYearService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί το σχολικό έτος διότι είναι σε χρήση.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΑΠΟΦΑΣΕΙΣ ΠΡΟΚΗΡΥΞΕΩΝ

        public ActionResult xProkirixiApofaseis()
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

            var data = prokirixiApofasiService.Read();

            PopulateShoolYears();
            return View(data);
        }

        public ActionResult ProkirixiApofasi_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = prokirixiApofasiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProkirixiApofasi_Create([DataSourceRequest] DataSourceRequest request, ProkirixiApofasiGridViewModel data)
        {
            var newData = new ProkirixiApofasiGridViewModel();

            var existingdata = db.ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();

            if (existingdata > 0) ModelState.AddModelError("", "Υπάρχουν δεδομένα για το έτος αυτό. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                prokirixiApofasiService.Create(data);
                newData = prokirixiApofasiService.Refresh(data.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProkirixiApofasi_Update([DataSourceRequest] DataSourceRequest request, ProkirixiApofasiGridViewModel data)
        {
            var newData = new ProkirixiApofasiGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                prokirixiApofasiService.Update(data);
                newData = prokirixiApofasiService.Refresh(data.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProkirixiApofasi_Destroy([DataSourceRequest] DataSourceRequest request, ProkirixiApofasiGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiParameters(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ))
                {
                    prokirixiApofasiService.Destroy(data);
                }
                else 
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή διότι υπάρχουν αποφάσεις γι' αυτό το σχολικό έτος.");
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #region ΚΑΡΤΕΛΑ ΥΠΟΥΡΓΙΚΩΝ ΣΤΟΙΧΕΙΩΝ

        public SysProkirixiApofasiViewModel GetProkirixiApofasiFromDB(int recordId)
        {
            var data = (from d in db.ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ
                        where d.ΚΩΔΙΚΟΣ == recordId
                        select new SysProkirixiApofasiViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ = d.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ,
                            ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ = d.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ,
                            ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ = d.ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ,
                            ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΙΕΚ = d.ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΙΕΚ,
                            ΥΠΟΥΡΓΟΣ_ΛΕΚΤΙΚΟ = d.ΥΠΟΥΡΓΟΣ_ΛΕΚΤΙΚΟ
                        }).FirstOrDefault();

            return data;
        }

        public ActionResult xProkirixiApofaseisEdit(int recordID)
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

            if (recordID > 0)
            {
                SysProkirixiApofasiViewModel data = GetProkirixiApofasiFromDB(recordID);
                return View(data);
            }
            else
            {
                SysProkirixiApofasiViewModel data = new SysProkirixiApofasiViewModel();
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult xProkirixiApofaseisEdit(int recordID, SysProkirixiApofasiViewModel data)
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

            if (recordID > 0 && ModelState.IsValid)
            {
                ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ entity = db.ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ.Find(recordID);

                entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
                entity.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ = data.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ;
                entity.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ = data.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ;
                entity.ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ = data.ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ;
                entity.ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΙΕΚ = data.ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΙΕΚ;
                entity.ΥΠΟΥΡΓΟΣ_ΛΕΚΤΙΚΟ = data.ΥΠΟΥΡΓΟΣ_ΛΕΚΤΙΚΟ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                // Notify here
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                return View(data);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης ή άκυρου κωδικού.");
            return View(data);
        }

        #endregion

        #endregion


        #region ΠΑΡΑΜΕΤΡΟΙ ΑΠΟΦΑΣΕΩΝ (ΑΠΟΦΑΣΕΙΣ ΠΥΣ - ΔΙΟΙΚΗΤΗ - ΥΠΟΥΡΓΟΥ ΚΑΙ ΛΕΚΤΙΚΟΥ)

        public ActionResult xApofasiParameters()
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

            var data = parametersApofasiService.Read();

            PopulateShoolYears();
            return View(data);
        }

        public ActionResult ApofasiParameters_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = parametersApofasiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiParameters_Create([DataSourceRequest] DataSourceRequest request, ApofasiParametersGridViewModel data)
        {
            var newData = new ApofasiParametersGridViewModel();

            var existingdata = db.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            if (existingdata > 0) 
                ModelState.AddModelError("", "Υπάρχουν δεδομένα για το έτος αυτό. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                parametersApofasiService.Create(data);
                newData = parametersApofasiService.Refresh(data.ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiParameters_Update([DataSourceRequest] DataSourceRequest request, ApofasiParametersGridViewModel data)
        {
            var newData = new ApofasiParametersGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                parametersApofasiService.Update(data);
                newData = parametersApofasiService.Refresh(data.ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiParameters_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiParametersGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiParameters((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ))
                {
                    parametersApofasiService.Destroy(data);
                }
                else 
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή διότι υπάρχουν αποφάσεις γι' αυτό το σχολικό έτος.");
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #region ΚΑΡΤΕΛΑ ΠΑΡΑΜΕΤΡΩΝ ΑΠΟΦΑΣΕΩΝ (ΑΠΟΦΑΣΕΙΣ ΠΥΣ - ΔΙΟΙΚΗΤΗ - ΥΠΟΥΡΓΟΥ ΚΑΙ ΛΕΚΤΙΚΟΥ)

        public SysApofasiParametersViewModel GetApofasiParametersFromDB(int recordId)
        {
            var data = (from d in db.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ
                        where d.ID == recordId
                        select new SysApofasiParametersViewModel
                        {
                            ID = d.ID,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = d.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ,
                            ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ = d.ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ,
                            ΠΥΣ_ΑΡΘΡΟ = d.ΠΥΣ_ΑΡΘΡΟ,
                            ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = d.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ,
                            ΛΕΚΤΙΚΟ_ΥΠΟΥΡΓΟΥ = d.ΛΕΚΤΙΚΟ_ΥΠΟΥΡΓΟΥ
                        }).FirstOrDefault();

            return data;
        }

        public ActionResult xApofasiParametersEdit(int recordId)
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

            if (recordId > 0)
            {
                SysApofasiParametersViewModel data = GetApofasiParametersFromDB(recordId);
                return View(data);
            }
            else
            {
                SysApofasiParametersViewModel data = new SysApofasiParametersViewModel();
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult xApofasiParametersEdit(int recordId, SysApofasiParametersViewModel data)
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

            if (recordId > 0 && ModelState.IsValid)
            {
                ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ entity = db.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ.Find(recordId);

                entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
                entity.ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ = data.ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ;
                entity.ΠΥΣ_ΑΡΘΡΟ = data.ΠΥΣ_ΑΡΘΡΟ;
                entity.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
                entity.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
                entity.ΛΕΚΤΙΚΟ_ΥΠΟΥΡΓΟΥ = data.ΛΕΚΤΙΚΟ_ΥΠΟΥΡΓΟΥ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                // Notify here
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                return View(data);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης ή άκυρου κωδικού.");
            return View(data);
        }

        #endregion

        #endregion


        #region ΑΠΟΦΑΣΕΙΣ ΣΥΣΤΑΣΗΣ ΕΠΙΤΡΟΠΩΝ

        public ActionResult xEpitropesApofaseis()
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

        public ActionResult ApofasiEpitropes_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0, int periferiakiId = 0)
        {
            var data = epitropesApofasiService.Read(schoolyearId, periferiakiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiEpitropes_Create([DataSourceRequest] DataSourceRequest request, SysEpitropesViewModel data, int schoolyearId = 0, int periferiakiId = 0)
        {
            var newData = new SysEpitropesViewModel();

            if (schoolyearId > 0 && periferiakiId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    epitropesApofasiService.Create(data, schoolyearId, periferiakiId);
                    newData = epitropesApofasiService.Refresh(data.ΕΠΙΤΡΟΠΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή περιφερειακής και σχολικού έτους. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiEpitropes_Update([DataSourceRequest] DataSourceRequest request, SysEpitropesViewModel data, int schoolyearId = 0, int periferiakiId = 0)
        {
            var newData = new SysEpitropesViewModel();

            if (schoolyearId > 0 && periferiakiId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    epitropesApofasiService.Update(data, schoolyearId, periferiakiId);
                    newData = epitropesApofasiService.Refresh(data.ΕΠΙΤΡΟΠΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει να προηγηθεί επιλογή περιφερειακής και σχολικού έτους. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiEpitropes_Destroy([DataSourceRequest] DataSourceRequest request, SysEpitropesViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiParameters((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ))
                {
                    epitropesApofasiService.Destroy(data);
                }
                else 
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή διότι υπάρχουν αποφάσεις γι' αυτό το σχολικό έτος.");
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΑΛΑΙΕΣ ΕΙΔΙΚΟΤΗΤΕΣ ΕΚΠΑΙΔΕΥΤΩΝ

        public ActionResult xEidikotitesOldList()
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
            var data = eidikotitesOldService.Read();

            PopulateKladoi();
            return View(data);
        }

        [HttpPost]
        public ActionResult EidikotitaOld_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = eidikotitesOldService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaOld_Create([DataSourceRequest] DataSourceRequest request, SysEidikotitesOldViewModel data)
        {
            var newdata = new SysEidikotitesOldViewModel();

            var existingData = db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ.Where(s => s.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ == data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Η ειδικότητα αυτή υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                eidikotitesOldService.Create(data);
                newdata = eidikotitesOldService.Refresh(data.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaOld_Update([DataSourceRequest] DataSourceRequest request, SysEidikotitesOldViewModel data)
        {
            var newdata = new SysEidikotitesOldViewModel();

            if (data != null)
            {
                eidikotitesOldService.Update(data);
                newdata = eidikotitesOldService.Refresh(data.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EidikotitaOld_Destroy([DataSourceRequest] DataSourceRequest request, SysEidikotitesOldViewModel data)
        {
            if (data != null)
            {
                eidikotitesOldService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult xEidikotitesOldPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                return View();
            }
        }

        #endregion


        #region ΝΕΕΣ ΕΙΔΙΚΟΤΗΤΕΣ ΕΚΠΑΙΔΕΥΤΩΝ (2018)

        public ActionResult xEidikotitesList()
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
            var data = eidikotitesService.Read();

            PopulateKladoi();
            PopulateKladoiUnified();
            return View(data);
        }

        public ActionResult Eidikotita_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = eidikotitesService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Create([DataSourceRequest] DataSourceRequest request, SysEidikotitesViewModel data)
        {
            var newdata = new SysEidikotitesViewModel();

            var existingData = db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Where(s => s.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ == data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ && s.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ == data.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ).Count();
            if (existingData > 0) 
                ModelState.AddModelError("", "Η ειδικότητα αυτή υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                eidikotitesService.Create(data);
                newdata = eidikotitesService.Refresh(data.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Update([DataSourceRequest] DataSourceRequest request, SysEidikotitesViewModel data)
        {
            var newdata = new SysEidikotitesViewModel();

            if (data != null)
            {
                eidikotitesService.Update(data);
                newdata = eidikotitesService.Refresh(data.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Destroy([DataSourceRequest] DataSourceRequest request, SysEidikotitesViewModel data)
        {
            if (data != null)
            {
                eidikotitesService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult xEidikotitesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                return View();
            }
        }


        #endregion ΝΕΕΣ ΕΙΔΙΚΟΤΗΤΕΣ ΕΚΠΑΙΔΕΥΤΩΝ


        #region ΝΕΑ ΟΘΟΝΗ ΜΕ ΔΥΝΑΤΟΤΗΤΑ ΔΗΜΙΟΥΡΓΙΑΣ ΕΝΙΑΙΩΝ ΚΛΑΔΩΝ (15-12-2018)

        public ActionResult xEidikotitesKladoi()
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
            PopulateKladoi();
            PopulateSqlEidikotites();
            return View();
        }

        #region ΠΛΕΓΜΑ ΕΝΙΑΙΩΝ ΚΛΑΔΩΝ

        public ActionResult KladosUnified_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetKladoiEniaioiFromDB();

            var result = new JsonResult();
            result.Data = data.ToDataSourceResult(request);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KladosUnified_Create([DataSourceRequest] DataSourceRequest request, SysKladosUnifiedViewModel data)
        {
            var newdata = new SysKladosUnifiedViewModel();

            if (data != null && ModelState.IsValid)
            {
                ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ entity = new ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ()
                {
                    ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                    KLADOS_LOWERCASE = data.KLADOS_LOWERCASE,
                    ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ
                };
                db.ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ.Add(entity);
                db.SaveChanges();
                data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ = entity.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ;
                newdata = RefreshKladosEniaiosFromDB(data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KladosUnified_Update([DataSourceRequest] DataSourceRequest request, SysKladosUnifiedViewModel data)
        {
            var newdata = new SysKladosUnifiedViewModel();

            if (data != null)
            {
                ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ entity = db.ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ.Find(data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ);

                entity.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ;
                entity.KLADOS_LOWERCASE = data.KLADOS_LOWERCASE;
                entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                newdata = RefreshKladosEniaiosFromDB(entity.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KladosUnified_Destroy([DataSourceRequest] DataSourceRequest request, SysKladosUnifiedViewModel data)
        {
            if (data != null)
            {
                ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ entity = db.ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ.Find(data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ);
                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ΕΙΔΙΚΟ ΠΛΕΓΜΑ ΕΙΔΙΚΟΤΗΤΩΝ (SET, RESET)

        public ActionResult sqlEidikotites_Read([DataSourceRequest] DataSourceRequest request, int eniaiosKladosId = 0)
        {
            var data = GetSqlEidikotitesFromDB(eniaiosKladosId);

            var result = new JsonResult();
            result.Data = data.ToDataSourceResult(request);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult sqlEidikotites_Set([DataSourceRequest] DataSourceRequest request, sqlEidikotitesViewModel data, int eniaiosKladosId = 0)
        {
            var newdata = new sqlEidikotitesViewModel();

            if (eniaiosKladosId > 0)
            {
                if (data != null)
                {
                    ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ entity = db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.ΚΩΔΙΚΟΣ);
                    entity.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = eniaiosKladosId;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    newdata = RefreshSqlEdikotitesFromDB(entity.ΚΩΔΙΚΟΣ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε έναν ενιαίο κλάδο. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult sqlEidikotites_Remove([DataSourceRequest] DataSourceRequest request, sqlEidikotitesViewModel data, int eniaiosKladosId = 0)
        {
            var newdata = new sqlEidikotitesViewModel();

            if (eniaiosKladosId > 0)
            {
                if (data != null)
                {
                    ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ entity = db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.ΚΩΔΙΚΟΣ);
                    entity.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = null;

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    newdata = RefreshSqlEdikotitesFromDB(entity.ΚΩΔΙΚΟΣ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε ένα ενιαίο κλάδο. Η ενημέρωση ακυρώθηκε.");
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public sqlEidikotitesViewModel RefreshSqlEdikotitesFromDB(int recordId)
        {
            var data = (from d in db.sqlEIDIKOTITES
                        where d.ΚΩΔΙΚΟΣ == recordId
                        orderby d.ΚΛΑΔΟΣ, d.ΕΙΔΙΚΟΤΗΤΑ
                        select new sqlEidikotitesViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ
                        }).FirstOrDefault();

            return (data);
        }

        public List<sqlEidikotitesViewModel> GetSqlEidikotitesFromDB(int eniaiosKladosId = 0)
        {
            var data = (from d in db.sqlEIDIKOTITES
                        where d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ == eniaiosKladosId
                        orderby d.ΚΛΑΔΟΣ, d.ΕΙΔΙΚΟΤΗΤΑ
                        select new sqlEidikotitesViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ
                        }).ToList();

            return (data);
        }

        #endregion

        public SysKladosUnifiedViewModel RefreshKladosEniaiosFromDB(int recordId)
        {
            var data = (from d in db.ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ
                        where d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ == recordId
                        select new SysKladosUnifiedViewModel
                        {
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ,
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                            KLADOS_LOWERCASE = d.KLADOS_LOWERCASE,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
                        }).FirstOrDefault();

            return (data);
        }

        public List<SysKladosUnifiedViewModel> GetKladoiEniaioiFromDB()
        {
            var data = (from d in db.ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ
                        orderby d.ΚΛΑΔΟΣ, d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ
                        select new SysKladosUnifiedViewModel
                        {
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ,
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                            KLADOS_LOWERCASE = d.KLADOS_LOWERCASE,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
                        }).ToList();

            return (data);
        }

        #endregion


        #region ΣΥΣΧΕΤΙΣΗ ΠΑΛΑΙΩΝ-ΝΕΩΝ ΕΙΔΙΚΟΤΗΤΩΝ

        public ActionResult xEidikotitesOldNewList()
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

        public ActionResult xEidikotitesOldNewPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                return View();
            }
        }

        #endregion ΣΥΣΧΕΤΙΣΗ ΠΑΛΑΙΩΝ-ΝΕΩΝ ΕΙΔΙΚΟΤΗΤΩΝ


        #region ΠΕΡΙΦΕΡΕΙΑΚΕΣ ΔΙΕΥΘΥΝΣΕΙΣ

        public ActionResult xPeriferiakesList()
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

            var data = periferiakesService.Read();
            return View(data);
        }

        public ActionResult Periferiaki_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = periferiakesService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Perifariaki_Create([DataSourceRequest] DataSourceRequest request, SysPeriferiakiViewModel data)
        {
            var newData = new SysPeriferiakiViewModel();

            var existingdata = db.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ.Where(s => s.ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ == data.ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ).Count();
            if (existingdata > 0) 
                ModelState.AddModelError("", "Αυτή η περιφερειακή υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                periferiakesService.Create(data);
                newData = periferiakesService.Refresh(data.ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Periferiaki_Update([DataSourceRequest] DataSourceRequest request, SysPeriferiakiViewModel data)
        {
            var newData = new SysPeriferiakiViewModel();

            if (data != null && ModelState.IsValid)
            {
                periferiakesService.Update(data);
                newData = periferiakesService.Refresh(data.ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Periferiaki_Destroy([DataSourceRequest] DataSourceRequest request, SysPeriferiakiViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeletePeriferiaki())
                {
                    periferiakesService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η διαγραφή Περιφερειακών είναι απενεργοποιημένη.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΚΛΑΔΟΙ - ΩΡΑΡΙΑ - ΜΙΣΘΟΣ

        public ActionResult xKladoiHours()
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

            var data = kladoiHoursService.Read();
            return View(data);
        }

        public ActionResult Klados_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = kladoiHoursService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Klados_Create([DataSourceRequest] DataSourceRequest request, SysKladosViewModel data)
        {
            var newData = new SysKladosViewModel();

            var existingdata = db.ΣΥΣ_ΚΛΑΔΟΙ.Where(s => s.ΚΛΑΔΟΣ == data.ΚΛΑΔΟΣ).Count();
            if (existingdata > 0) 
                ModelState.AddModelError("", "Αυτός ο κλάδος υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                kladoiHoursService.Create(data);
                newData = kladoiHoursService.Refresh(data.ΚΛΑΔΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Klados_Update([DataSourceRequest] DataSourceRequest request, SysKladosViewModel data)
        {
            var newData = new SysKladosViewModel();

            if (data != null && ModelState.IsValid)
            {
                kladoiHoursService.Update(data);
                newData = kladoiHoursService.Refresh(data.ΚΛΑΔΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Klados_Destroy([DataSourceRequest] DataSourceRequest request, SysKladosViewModel data)
        {
            if (data != null)
            {
                kladoiHoursService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΩΡΟΜΙΣΘΙΕΣ ΑΜΟΙΒΕΣ

        public ActionResult xHourWages()
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

            var data = hourWagesService.Read();
            return View(data);
        }

        public ActionResult Wages_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = hourWagesService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Wages_Create([DataSourceRequest] DataSourceRequest request, SysHourWagesViewModel data)
        {
            var newData = new SysHourWagesViewModel();

            var existingdata = db.ΣΥΣ_ΩΡΟΜΙΣΘΙΑ.Where(s => s.ΚΛΑΔΟΣ == data.ΚΛΑΔΟΣ).Count();
            if (existingdata > 0) 
                ModelState.AddModelError("", "Αυτός ο κλάδος υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                hourWagesService.Create(data);
                newData = hourWagesService.Refresh(data.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Wages_Update([DataSourceRequest] DataSourceRequest request, SysHourWagesViewModel data)
        {
            var newData = new SysHourWagesViewModel();

            if (data != null && ModelState.IsValid)
            {
                hourWagesService.Update(data);
                newData = hourWagesService.Refresh(data.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Wages_Destroy([DataSourceRequest] DataSourceRequest request, SysHourWagesViewModel data)
        {
            if (data != null)
            {
                hourWagesService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΕΡΙΦΕΡΕΙΕΣ-ΔΗΜΟΙ

        public ActionResult xPeriferiesDimoi()
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

        public ActionResult Periferies([DataSourceRequest] DataSourceRequest request)
        {
            var periferies = db.SYS_PERIFERIES.Select(p => new PeriferiaViewModel()
            {
                PERIFERIA_ID = p.PERIFERIA_ID,
                PERIFERIA_NAME = p.PERIFERIA_NAME
            });
            return Json(periferies.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dimoi([DataSourceRequest] DataSourceRequest request, int periferiaId)
        {
            var dimoi = db.SYS_DIMOS.Where(o => o.DIMOS_PERIFERIA == periferiaId).Select(p => new DimosViewModel()
            {
                DIMOS_ID = p.DIMOS_ID,
                DIMOS = p.DIMOS,
                DIMOS_PERIFERIA = p.DIMOS_PERIFERIA
            });
            return Json(dimoi.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult xPeriferiesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
                return View();
            }
        }

        #endregion


        #region ΛΟΓΑΡΙΑΣΜΟΙ ΣΧΟΛΕΙΩΝ

        public ActionResult xUserSchools(string notify = null)
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
            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            PopulateAllSchools();

            return View();
        }

        public ActionResult CreatePasswords()
        {
            var schools = (from s in db.USER_SCHOOLS select s).ToList();
            Random rnd = new Random();

            foreach (var school in schools)
            {
                school.PASSWORD = Common.GeneratePassword(rnd) + string.Format("{0:000}", school.USER_SCHOOLID);
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
            }

            string notify = "Η δημιουργία νέων κωδικών σχολείων ολοκληρώθηκε.";
            return RedirectToAction("UserSchools", "Setup", new { notify });
        }

        #region Grid CRUD Functions

        public ActionResult UserSchool_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = schoolAccountService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserSchool_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserSchoolViewModel> data)
        {
            var results = new List<UserSchoolViewModel>();
            foreach (var item in data)
            {
                if (item != null && ModelState.IsValid)
                {
                    schoolAccountService.Create(item);
                    results.Add(item);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserSchool_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserSchoolViewModel> data)
        {
            if (data != null)
            {
                foreach (var item in data)
                {
                    schoolAccountService.Update(item);
                }
            }

            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserSchool_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserSchoolViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    schoolAccountService.Destroy(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult xSchoolAccountsPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ARIADNE");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
                return View();
            }
        }

        #endregion


        #region ΕΙΣΟΔΟΙ ΣΧΟΛΕΙΩΝ

        public ActionResult xSchoolLogins()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            return View();
        }

        public ActionResult Logins_Read([DataSourceRequest] DataSourceRequest request)
        {
            var vms = GetSchoolLoginsFromDB();

            var result = new JsonResult();
            result.Data = vms.ToDataSourceResult(request);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public List<SchoolLoginsViewModel> GetSchoolLoginsFromDB()
        {
            var data = (from d in db.sqlSCHOOL_LOGINS
                        orderby d.SCHOOL_NAME
                        orderby d.LOGIN_DATETIME descending, d.SCHOOL_NAME
                        select new SchoolLoginsViewModel
                        {
                            LOGIN_ID = d.LOGIN_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            LOGIN_DATETIME = d.LOGIN_DATETIME
                        }).ToList();

            return data;
        }

        #endregion


        #region ΧΑΡΤΕΣ GOOGLE

        public ActionResult xGoogleMaps()
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


        #region POPULATORS

        public void PopulateAllSchools()
        {
            var schools = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ select d).ToList();

            ViewData["schools"] = schools;
        }

        public void PopulateKladoiUnified()
        {
            var kladoiUnified = (from k in db.ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ select k).ToList();

            ViewData["kladoiUnified"] = kladoiUnified;
            ViewData["kladoiUnifiedDefault"] = kladoiUnified.First().ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ;
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
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.ΔΟΜΗ !=1 && d.ΔΟΜΗ != 4 orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ select d).ToList();
            ViewData["schools"] = data;
        }

        public void PopulateSqlEidikotites()
        {
            var data = (from d in db.sqlEIDIKOTITES
                        orderby d.ΚΛΑΔΟΣ, d.ΕΙΔΙΚΟΤΗΤΑ
                        select new sqlEidikotitesViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ
                        }).ToList();

            ViewData["sqlEidikotites"] = data;
            ViewData["sqlDefaultEidikotita"] = data.First().ΚΩΔΙΚΟΣ;
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

        public USER_ARIADNE GetLoginAdmin()
        {
            loggedAdmin = db.USER_ARIADNE.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FULLNAME;

            return loggedAdmin;
        }

        #endregion

    }
}
