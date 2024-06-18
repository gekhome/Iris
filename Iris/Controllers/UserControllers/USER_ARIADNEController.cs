using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Iris.Models;
using Iris.BPM;
using Iris.DAL;
using Iris.DAL.Security;
using Iris.Filters;
using Iris.Notification;
using Iris.Services;

namespace Iris.Controllers.UserControllers
{
    [ErrorHandlerFilter]
    public class USER_ARIADNEController : Controller
    {
        private readonly IrisDBEntities db;
        private USER_ARIADNE loggedAdmin;

        private readonly IAdmin2AccountService admin2AccountService;

        public USER_ARIADNEController(IrisDBEntities entities, IAdmin2AccountService admin2AccountService)
        {
            db = entities;
            this.admin2AccountService = admin2AccountService;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Login()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
            }
            else
            {
                loggedAdmin = db.USER_ARIADNE.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                if (loggedAdmin != null)
                {
                    ViewBag.loggedUser = loggedAdmin.FULLNAME;
                    //SetLoginStatus(loggedAdmin2, true);
                    return RedirectToAction("Index", "Admin2");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "USERNAME,PASSWORD")]  UserAdmin2ViewModel model)
        {
            var user = db.USER_ARIADNE.Where(u => u.USERNAME == model.USERNAME && u.PASSWORD == model.PASSWORD).FirstOrDefault();

            if (user != null)
            {
                WriteUserCookie(model);
                SetLoginStatus(user, true);

                return RedirectToAction("Index", "Admin2");
            }
            ModelState.AddModelError("", "Το όνομα χρήστη ή/και ο κωδ.πρόσβασης δεν είναι σωστά");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut([Bind(Include = "ISACTIVE")] USER_ARIADNE userAriadne)
        {
            var user = db.USER_ARIADNE.Where(u => u.USERNAME == userAriadne.USERNAME && u.PASSWORD == userAriadne.PASSWORD).FirstOrDefault();

            FormsAuthentication.SignOut();
            SetLoginStatus(user, false);

            return RedirectToAction("Index", "Home");
        }

        public void WriteUserCookie(UserAdmin2ViewModel user)
        {
            AdminPrincipalSerializeModel serializeModel = new AdminPrincipalSerializeModel();
            serializeModel.UserId = user.USER_ID;
            serializeModel.Username = user.USERNAME;
            serializeModel.FullName = user.FULLNAME;

            string userData = JsonConvert.SerializeObject(serializeModel);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, user.USERNAME, DateTime.Now, DateTime.Now.AddMinutes(Kerberos.TICKET_TIMEOUT_MINUTES), false, userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }

        public void SetLoginStatus(USER_ARIADNE user, bool value)
        {
            user.ISACTIVE = value;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
         }

        public USER_ARIADNE GetLoginAdmin()
        {
            loggedAdmin = db.USER_ARIADNE.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            ViewBag.loggedUser = loggedAdmin.FULLNAME;
            return loggedAdmin;
        }

        #region ADMIN2 ACCOUNTS CRUD FUNCTIONS

        public ActionResult ListAdmin2()
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

            var data = admin2AccountService.Read();
            return View(data);
        }

        [HttpPost]
        public ActionResult ListAdmin2_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = admin2AccountService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListAdmin2_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserAdmin2ViewModel> data)
        {
            var results = new List<UserAdmin2ViewModel>();
            foreach (var item in data)
            {
                if (item != null && ModelState.IsValid)
                {
                    admin2AccountService.Create(item);
                    results.Add(item);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ListAdmin2_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserAdmin2ViewModel> data)
        {
            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    admin2AccountService.Update(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ListAdmin2_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserAdmin2ViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    admin2AccountService.Destroy(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}
