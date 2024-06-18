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
using Iris.Filters;
using Iris.Models;
using Iris.BPM;
using Iris.DAL;
using Iris.DAL.Security;
using Iris.Notification;
using Iris.Services;

namespace Iris.Controllers.UserControllers
{
    [ErrorHandlerFilter]
    public class USER_ADMINSController : Controller
    {
        private readonly IrisDBEntities db;
        private USER_ADMINS loggedAdmin;

        private readonly IAdminAccountService adminAccountService;

        public USER_ADMINSController(IrisDBEntities entities, IAdminAccountService adminAccountService)
        {
            db = entities;
            this.adminAccountService = adminAccountService;
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
                loggedAdmin = db.USER_ADMINS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                if (loggedAdmin != null)
                {
                    ViewBag.loggedUser = loggedAdmin.FULLNAME;
                    //SetLoginStatus(loggedAdmin, true);
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "USERNAME,PASSWORD")]  UserAdminViewModel model)
        {
            var user = db.USER_ADMINS.Where(u => u.USERNAME == model.USERNAME && u.PASSWORD == model.PASSWORD).FirstOrDefault();

            if (user != null)
            {
                WriteUserCookie(model);
                SetLoginStatus(user, true);

                return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError("", "Το όνομα χρήστη ή/και ο κωδ.πρόσβασης δεν είναι σωστά");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut([Bind(Include = "ISACTIVE")] USER_ADMINS userAdmin)
        {
            var user = db.USER_ADMINS.Where(u => u.USERNAME == userAdmin.USERNAME && u.PASSWORD == userAdmin.PASSWORD).FirstOrDefault();

            FormsAuthentication.SignOut();
            SetLoginStatus(user, false);

            return RedirectToAction("Index", "Home");
        }

        public void WriteUserCookie(UserAdminViewModel user)
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

        public void SetLoginStatus(USER_ADMINS user, bool value)
        {
            user.ISACTIVE = value;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
         }

        public USER_ADMINS GetLoginAdmin()
        {
            loggedAdmin = db.USER_ADMINS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            //ViewBag.loggedUser = loggedAdmin.USERNAME;
            //ViewBag.loggedAdmin = db.USER_ADMINS.Find(loggedAdmin.USERNAME);
            ViewBag.loggedUser = loggedAdmin.FULLNAME;
            return loggedAdmin;
        }


        #region ADMIN ACCOUNTS CRUD FUNCTIONS

        public ActionResult ListAdmin()
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

            var data = adminAccountService.Read();
            return View(data);
        }

        public ActionResult ListAdmin_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = adminAccountService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListAdmin_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserAdminViewModel> data)
        {
            var results = new List<UserAdminViewModel>();
            foreach (var item in data)
            {
                if (item != null && ModelState.IsValid)
                {
                    adminAccountService.Create(item);
                    results.Add(item);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ListAdmin_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserAdminViewModel> data)
        {
            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    adminAccountService.Update(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ListAdmin_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserAdminViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    adminAccountService.Destroy(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}
