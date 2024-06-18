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
using Iris.Models;
using Iris.BPM;
using Iris.Notification;
using Iris.ServicesApofaseis;

namespace Iris.Controllers.DataControllers
{
    public class ControllerUnit : Controller
    {
        private readonly IrisDBEntities db;

        public const int EPAS = 1;
        public const int IEK = 2;

        public ControllerUnit(IrisDBEntities entities)
        {
            db = entities;
        }

        #region ΕΠΙΣΥΝΑΨΗ ΑΝΑΘΕΣΕΩΝ

        // --------------------------------------------------
        // Ουσιαστικά, η επισύναψη παίρνει μια ανάθεση ορφανή
        // και της δίνει τον κωδικό της επιλεγμένης απόφασης
        // --------------------------------------------------

        public ActionResult ApofasiInitialAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΡΧΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν αρχικές αναθέσεις για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiInitialAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiDirectAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν απευθείας αναθέσεις για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiDirectAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiSupplementAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συμπληρωματικές αναθέσεις για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiSupplementAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiSupplementAKAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συμπληρωματικές A.K. αναθέσεις για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ target = db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiSupplementAKAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ target = db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiModifyAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν τροποποιητικές αναθέσεις για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiModifyAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiModifyAKAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν τροποποιητικές A.K. αναθέσεις για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ target = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiModifyAKAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ target = db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiCancelAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν ακυρωτικές αναθέσεις για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiCancelAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiRevokeAttach(ApofasiParameters ap)
        {
            string message = "";

            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ap.schoolyearId && d.ΣΧΟΛΗ == ap.schoolId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν αναθέσεις ανάκλησης για αυτό το σχολείο και σχολ. έτος για επισύναψη.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη ανάθεση προς αποφυγή διπλοεγγραφών.
                var anathesi = (from e in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ where e.ΑΝΑΘΕΣΗ_ΚΩΔ == d.ΑΝΑΘΕΣΗ_ΚΩΔ && e.ΑΠΟΦΑΣΗ_ΚΩΔ != 0 select e).ToList();
                if (anathesi.Count == 0)
                {
                    ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                    target.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = ap.apofasiType;
                    target.ΑΠΟΦΑΣΗ_ΚΩΔ = ap.apofasiId;
                    target.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date;

                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApofasiRevokeAttachCancel(ApofasiParameters ap)
        {
            string message = "";
            var source = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId select d).ToList();

            if (source.Count == 0)
            {
                message = "Δεν βρέθηκαν συνημμένες αναθέσεις για ακύρωση επισύναψης.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var d in source)
            {
                ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ target = db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ.Find(d.ΑΝΑΘΕΣΗ_ΚΩΔ);
                target.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region APOFASEIS FORM GETTERS and COMMON FUNCTIONS

        public JsonResult GetEgrafoTypes()
        {
            var data = db.ΣΥΣ_ΕΓΓΡΑΦΟ_ΕΙΔΟΣ.Select(m => new SysEgrafoTypeViewModel
            {
                ΚΩΔΙΚΟΣ = m.ΚΩΔΙΚΟΣ,
                ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = m.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDiaxiristes(int departement)
        {
            var data = new List<DiaxiristisViewModel>();

            if (departement == EPAS)
            {
                data = db.Δ_ΔΙΑΧΕΙΡΙΣΤΕΣ.Select(m => new DiaxiristisViewModel
                {
                    ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ = m.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ,
                    ΟΝΟΜΑΤΕΠΩΝΥΜΟ = m.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ = m.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ
                }).OrderBy(d => d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ).ToList();
            }
            else
            {
                data = db.Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ.Select(m => new DiaxiristisViewModel
                {
                    ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ = m.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ,
                    ΟΝΟΜΑΤΕΠΩΝΥΜΟ = m.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                    ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ = m.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ
                }).OrderBy(d => d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ).ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProistamenos(int schoolyear, int departement)
        {
            var data = new List<ProistamenosViewModel>();

            if (departement == EPAS)
            {
                data = (from d in db.Δ_ΠΡΟΙΣΤΑΜΕΝΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear
                        select new ProistamenosViewModel
                        {
                            ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ = d.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ,
                            ΠΡΟΙΣΤΑΜΕΝΟΣ = d.ΠΡΟΙΣΤΑΜΕΝΟΣ
                        }).ToList();
            }
            else
            {
                data = (from d in db.Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear
                        select new ProistamenosViewModel
                        {
                            ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ = d.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ,
                            ΠΡΟΙΣΤΑΜΕΝΟΣ = d.ΠΡΟΙΣΤΑΜΕΝΟΣ
                        }).ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDirectors(int schoolyear, int departement)
        {
            var data = new List<DirectorViewModel>();

            if (departement == EPAS)
            {
                data = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear
                        select new DirectorViewModel
                        {
                            ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ = d.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ,
                            ΔΙΕΥΘΥΝΤΗΣ = d.ΔΙΕΥΘΥΝΤΗΣ
                        }).ToList();
            }
            else
            {
                data = (from d in db.Δ2_ΔΙΕΥΘΥΝΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear
                        select new DirectorViewModel
                        {
                            ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ = d.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ,
                            ΔΙΕΥΘΥΝΤΗΣ = d.ΔΙΕΥΘΥΝΤΗΣ
                        }).ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGenikos(int schoolyear, int departement)
        {
            var data = new List<DirectorGeneralViewModel>();

            if (departement == EPAS)
            {
                data = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear
                        select new DirectorGeneralViewModel
                        {
                            ΓΕΝΙΚΟΣ_ΚΩΔ = d.ΓΕΝΙΚΟΣ_ΚΩΔ,
                            ΓΕΝΙΚΟΣ = d.ΓΕΝΙΚΟΣ
                        }).ToList();
            }
            else
            {
                data = (from d in db.Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear
                        select new DirectorGeneralViewModel
                        {
                            ΓΕΝΙΚΟΣ_ΚΩΔ = d.ΓΕΝΙΚΟΣ_ΚΩΔ,
                            ΓΕΝΙΚΟΣ = d.ΓΕΝΙΚΟΣ
                        }).ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDioikitis(int schoolyear, int departement)
        {
            var data = new List<DioikitisViewModel>();

            if (departement == EPAS)
            {
                data = (from d in db.Δ_ΔΙΟΙΚΗΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear
                        select new DioikitisViewModel
                        {
                            ΔΙΟΙΚΗΤΗΣ_ΚΩΔ = d.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ,
                            ΔΙΟΙΚΗΤΗΣ = d.ΔΙΟΙΚΗΤΗΣ
                        }).ToList();
            }
            else
            {
                data = (from d in db.Δ2_ΔΙΟΙΚΗΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear
                        select new DioikitisViewModel
                        {
                            ΔΙΟΙΚΗΤΗΣ_ΚΩΔ = d.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ,
                            ΔΙΟΙΚΗΤΗΣ = d.ΔΙΟΙΚΗΤΗΣ
                        }).ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAntiproedros(int schoolyear, int departement)
        {
            var data = new List<AntiproedrosViewModel>();

            if (departement == EPAS)
            {
                data = (from d in db.Δ_ΑΝΤΙΠΡΟΕΔΡΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear
                        select new AntiproedrosViewModel
                        {
                            ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ = d.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ,
                            ΑΝΤΙΠΡΟΕΔΡΟΣ = d.ΑΝΤΙΠΡΟΕΔΡΟΣ
                        }).ToList();
            }
            else
            {
                data = (from d in db.Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear
                        select new AntiproedrosViewModel
                        {
                            ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ = d.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ,
                            ΑΝΤΙΠΡΟΕΔΡΟΣ = d.ΑΝΤΙΠΡΟΕΔΡΟΣ
                        }).ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region POPULATORS

        public void PopulateDiaxiristes(int departement)
        {
            if (departement == EPAS)
            {
                var data = (from d in db.Δ_ΔΙΑΧΕΙΡΙΣΤΕΣ orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ select d).ToList();
                ViewData["diaxiristes"] = data;
                ViewData["defaultDiaxiristis"] = data.First().ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ;
            }
            else
            {
                var data = (from d in db.Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ select d).ToList();
                ViewData["diaxiristes"] = data;
                ViewData["defaultDiaxiristis"] = data.First().ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ;
            }
        }

        public void PopulateSchools(int departement)
        {
            if (departement == EPAS)
            {
                var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.ΔΟΜΗ == 1 || d.ΔΟΜΗ == 4 orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ select d).ToList();
                ViewData["schools"] = data;
                ViewData["defaultSchool"] = data.FirstOrDefault().ΣΧΟΛΗ_ΚΩΔ;
            }
            else
            {
                var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.ΔΟΜΗ != 1 && d.ΔΟΜΗ != 4 orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ select d).ToList();
                ViewData["schools"] = data;
                ViewData["defaultSchool"] = data.FirstOrDefault().ΣΧΟΛΗ_ΚΩΔ;
            }
        }

        public void PopulateDocTypes()
        {
            var data = (from d in db.ΣΥΣ_ΕΓΓΡΑΦΟ_ΕΙΔΟΣ select d).ToList();

            ViewData["doctypes"] = data;
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

        public void PopulateSchoolYears()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).ToList();
            ViewData["schoolYears"] = data;
            ViewData["defaultSchoolYear"] = data.First().SCHOOLYEAR_ID;
        }

        #endregion


        #region GETTERS

        public JsonResult GetDiaxiristes2(int departement)
        {
            var data = new List<DiaxiristisViewModel>();

            if (departement == EPAS)
            {
                data = db.Δ_ΔΙΑΧΕΙΡΙΣΤΕΣ.Select(m => new DiaxiristisViewModel
                {
                    ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ = m.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ,
                    ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ = m.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ
                }).OrderBy(d => d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ).ToList();
            }
            else
            {
                data = db.Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ.Select(m => new DiaxiristisViewModel
                {
                    ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ = m.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ,
                    ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ = m.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ
                }).OrderBy(d => d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ).ToList();
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchools(int departement)
        {
            var data = new List<SchoolsViewModel>();

            if (departement == EPAS)
            {
                data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.ΔΟΜΗ == 1 || d.ΔΟΜΗ == 4 orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ
                        select new SchoolsViewModel
                        {
                            ΣΧΟΛΗ_ΚΩΔ = d.ΣΧΟΛΗ_ΚΩΔ,
                            ΕΠΩΝΥΜΙΑ = d.ΕΠΩΝΥΜΙΑ
                        }).ToList();
            }
            else
            {
                data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.ΔΟΜΗ != 1 && d.ΔΟΜΗ != 4 orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ
                        select new SchoolsViewModel
                        {
                            ΣΧΟΛΗ_ΚΩΔ = d.ΣΧΟΛΗ_ΚΩΔ,
                            ΕΠΩΝΥΜΙΑ = d.ΕΠΩΝΥΜΙΑ
                        }).ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchoolTypes()
        {
            var data = db.ΣΥΣ_ΕΚΠΑΙΔΕΥΤΙΚΕΣ_ΔΟΜΕΣ.Select(p => new SysDomesViewModel
            {
                ΚΩΔΙΚΟΣ = p.ΚΩΔΙΚΟΣ,
                ΜΟΝΑΔΑ = p.ΜΟΝΑΔΑ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEidikotites()
        {
            var data = db.sqlEIDIKOTITES.Select(m => new sqlEidikotitesViewModel
            {
                ΚΩΔΙΚΟΣ = m.ΚΩΔΙΚΟΣ,
                ΕΙΔΙΚΟΤΗΤΑ = m.ΕΙΔΙΚΟΤΗΤΑ
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