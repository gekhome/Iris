﻿using System;
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


namespace Iris.Controllers.UserControllers
{
    [ErrorHandlerFilter]
    public class USER_SCHOOLSController : Controller
    {
        private readonly IrisDBEntities db;
        private USER_SCHOOLS loggedSchool;

        public USER_SCHOOLSController(IrisDBEntities entities)
        {
            db = entities;
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
                loggedSchool = db.USER_SCHOOLS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                if (loggedSchool != null)
                {
                    ViewBag.loggedUser = GetLoginSchool();
                    //SetLoginStatus(loggedAdmin, true);
                    return RedirectToAction("Index", "School");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "USERNAME,PASSWORD")]  UserSchoolViewModel model)
        {
            var user = db.USER_SCHOOLS.Where(u => u.USERNAME == model.USERNAME && u.PASSWORD == model.PASSWORD).FirstOrDefault();

            if (user != null)
            {
                WriteUserCookie(model);
                SetLoginStatus(user, true);
                LoginRecord(user.USERNAME);

                return RedirectToAction("Index", "School");
            }
            ModelState.AddModelError("", "Το όνομα χρήστη ή/και ο κωδ.πρόσβασης δεν είναι σωστά");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut([Bind(Include = "ISACTIVE")] USER_SCHOOLS userSchool)
        {
            var user = db.USER_SCHOOLS.Where(u => u.USERNAME == userSchool.USERNAME && u.PASSWORD == userSchool.PASSWORD).FirstOrDefault();

            FormsAuthentication.SignOut();
            SetLoginStatus(user, false);

            return RedirectToAction("Index", "Home");
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

        public void WriteUserCookie(UserSchoolViewModel user)
        {
            SchoolPrincipalSerializeModel serializeModel = new SchoolPrincipalSerializeModel();
            serializeModel.UserId = user.USER_ID;
            serializeModel.Username = user.USERNAME;
            serializeModel.SchoolId = user.USER_SCHOOLID ?? 0;

            string userData = JsonConvert.SerializeObject(serializeModel);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, user.USERNAME, DateTime.Now, DateTime.Now.AddMinutes(Kerberos.TICKET_TIMEOUT_MINUTES), false, userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }

        public void SetLoginStatus(USER_SCHOOLS user, bool value)
        {
            db.Entry(user).State = EntityState.Modified;
            user.ISACTIVE = value;
            db.SaveChanges();
        }

        public void LoginRecord(string username)
        {
            var loginData = (from d in db.SYS_LOGINS where d.LOGIN_USERNAME == username select d).FirstOrDefault();

            if (loginData == null)
            {
                SYS_LOGINS entity = new SYS_LOGINS()
                {
                    LOGIN_USERNAME = username,
                    LOGIN_DATETIME = DateTime.Now
                };
                db.SYS_LOGINS.Add(entity);
                db.SaveChanges();
            }
            else
            {
                SYS_LOGINS entity = db.SYS_LOGINS.Find(loginData.LOGIN_ID);
                entity.LOGIN_DATETIME = DateTime.Now;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}
