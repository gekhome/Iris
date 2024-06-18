using Iris.DAL;
using Iris.Models;
using Iris.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Iris.Controllers
{
    [ErrorHandlerFilter]
    public class HomeController : Controller
    {
        private readonly IrisDBEntities db;

        public HomeController(IrisDBEntities entities)
        {
            db = entities;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            string userTxt = "(χωρίς σύνδεση)";
            try
            {
                bool AppStatusOn = GetApplicationStatus();
                if (AppStatusOn == false)
                {
                    return RedirectToAction("AppStatusOff", "Home");
                }
            }
            catch
            {
                return RedirectToAction("ErrorConnect", "Home");
            }

            ViewBag.loggedUser = userTxt;
            return View();
        }

        [AllowAnonymous]
        public ActionResult AppStatusOff()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ErrorConnect()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Σύντομη περιγραφή της εφαρμογής.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Στοιχεία επικοινωνίας.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public bool GetApplicationStatus()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.STATUS_VALUE ?? false;
            return status;
        }


        #region States of Grids

        [ValidateInput(false)]
        public ActionResult Save(string data)
        {
            Session["data"] = data;

            //int temp = 1;

            return new EmptyResult();
        }

        [AllowAnonymous]
        public ActionResult Load()
        {
            if (Session["data"] != null)
            {
                string data = Session["data"].ToString();
            }

            //int temp = 1;

            return Json(Session["data"], JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult SaveRow(string data)
        {
            Session["row"] = data;

            //int temp = 1;

            return new EmptyResult();
        }

        [AllowAnonymous]
        public ActionResult LoadRow()
        {
            if (Session["row"] != null)
            {
                string data = Session["row"].ToString();
            }

            //int temp = 1;

            return Json(Session["row"], JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}