using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iris.DAL;
using Iris.Models;
using Iris.BPM;

namespace Iris.BPM
{
    public static class Kerberos
    {
        public const int TICKET_TIMEOUT_MINUTES = 280;

        /// <summary>
        /// Υπολογίζει τις εργάσιμες ημέρες μεταξύ δύο ημερομηνιών,
        /// δηλ. χωρίς τα Σαββατοκύριακα.
        /// </summary>
        /// <param name="initial_date"></param>
        /// <param name="final_date"></param>
        /// <returns name="daycount"></returns>
        public static int WorkingDays(DateTime initial_date, DateTime final_date)
        {
            int daycount = 0;

            DateTime date1 = initial_date;
            DateTime date2 = final_date;

            while (date1 <= date2)
            {
                switch (date1.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                    case DayOfWeek.Saturday:
                        break;
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        daycount++;
                        break;
                    default:
                        break;
                }
                date1 = date1.AddDays(1);
            }
            return daycount;
        }


        #region ΚΑΝΟΝΕΣ ΑΝΑΘΕΣΕΩΝ

        #region CREATE RULES

        public static bool CanCreateInitial(string afm, int schoolId, int schoolyearId)
        {
            using (var db = new IrisDBEntities()) 
            {
                var data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ
                            where d.ΣΧΟΛΗ == schoolId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΑΦΜ == afm
                            select d).ToList();

                if (data.Count > 0)
                    return false;
                else
                    return true;
            }
        }

        public static bool CanCreateDirect(string afm, int schoolId, int schoolyearId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ
                            where d.ΣΧΟΛΗ == schoolId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΑΦΜ == afm
                            select d).ToList();

                if (data.Count > 0) return false;
                else return true;
            }
        }

        public static bool CanCreateSupplement(string afm, int schoolId, int schoolyearId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ
                            where d.ΣΧΟΛΗ == schoolId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΑΦΜ == afm
                            select d).ToList();

                if (data.Count > 0) return false;
                else return true;
            }
        }

        public static bool CanCreateSupplementAK(string afm, int schoolId, int schoolyearId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ
                            where d.ΣΧΟΛΗ == schoolId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΑΦΜ == afm
                            select d).ToList();

                if (data.Count > 0) return false;
                else return true;
            }
        }

        public static bool CanCreateCancel(string afm, int schoolId, int schoolyearId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ
                            where d.ΣΧΟΛΗ == schoolId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΑΦΜ == afm
                            select d).ToList();

                if (data.Count > 0) return false;
                else return true;
            }
        }

        public static bool CanCreateRevoke(string afm, int schoolId, int schoolyearId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ
                            where d.ΣΧΟΛΗ == schoolId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΑΦΜ == afm
                            select d).ToList();

                if (data.Count > 0) return false;
                else return true;
            }
        }

        public static bool CanCreateInitialAnaplirotes(string afm, int schoolId, int schoolyearId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ
                            where d.ΣΧΟΛΗ == schoolId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΑΦΜ == afm
                            select d).ToList();

                if (data.Count > 0) return false;
                else return true;
            }
        }

        public static bool CanCreateSupplementAnaplirotes(string afm, int schoolId, int schoolyearId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ
                            where d.ΣΧΟΛΗ == schoolId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΑΦΜ == afm
                            select d).ToList();

                if (data.Count > 0) return false;
                else return true;
            }
        }

        public static bool CanCreateCancelAnaplirotes(string afm, int schoolId, int schoolyearId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ
                            where d.ΣΧΟΛΗ == schoolId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΑΦΜ == afm
                            select d).ToList();

                if (data.Count > 0) return false;
                else return true;
            }
        }

        public static bool CanCreateRevokeAnaplirotes(string afm, int schoolId, int schoolyearId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ
                            where d.ΣΧΟΛΗ == schoolId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΑΦΜ == afm
                            select d).ToList();

                if (data.Count > 0) return false;
                else return true;
            }
        }

        #endregion CREATE RULES


        #region EDIT-DELETE RULES

        public static bool CanEditDeleteInitial(AnathesiInitialViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteDirect(AnathesiDirectViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteSupplement(AnathesiSupplementViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteSupplementAK(AnathesiSupplementAKViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteCancel(AnathesiCancelViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteRevoke(AnathesiRevokeViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteModify(AnathesiModifyViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteModifyAK(AnathesiModifyAKViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteInitialAnaplirotes(AnathesiInitialAnaplirotesViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteSupplementAnaplirotes(AnathesiSupplementAnaplirotesViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteCancelAnaplirotes(AnathesiCancelAnaplirotesViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteRevokeAnaplirotes(AnathesiRevokeAnaplirotesViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteModifyAnaplirotes(AnathesiModifyAnaplirotesViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        public static bool CanEditDeleteModifyAnaplirotesAK(AnathesiModifyAnaplirotesAKViewModel anathesi)
        {
            if (anathesi.ΑΠΟΦΑΣΗ_ΚΩΔ > 0) return false;
            else return true;
        }

        #endregion EDIT-DELETE RULES

        #endregion ΚΑΝΟΝΕΣ ΑΝΑΘΕΣΕΩΝ


        #region ΚΑΝΟΝΕΣ ΕΠΙΣΥΝΑΨΗΣ ΑΝΑΘΕΣΕΩΝ

        public static bool ApofasiInitialHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiDirectHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiSupplementHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiSupplementAKHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiModifyHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiModifyAKHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiCancelHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiRevokeHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        // ΑΝΑΠΛΗΡΩΤΕΣ

        public static bool ApofasiInitialAnaplirotesHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiSupplementAnaplirotesHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiCancelAnaplirotesHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiRevokeAnaplirotesHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiModifyAnaplHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        public static bool ApofasiModifyAnaplAKHasChildren(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return true;
                else return false;
            }
        }

        #endregion ΚΑΝΟΝΕΣ ΕΠΙΣΥΝΑΨΗΣ ΑΝΑΘΕΣΕΩΝ


        #region ΚΑΝΟΝΕΣ ΔΙΑΓΡΑΦΗΣ ΠΑΡΑΜΕΤΡΙΚΩΝ ΣΤΟΙΧΕΙΩΝ

        public static bool CanDeleteSchoolYear(int syearId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΚΑΘΟΛΙΚΟ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == syearId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeletePeriferiaki()
        {
            return false;
        }

        public static bool CanDeleteDiaxiristis(int personId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.SYS_APOFASEIS_ALL where d.ΔΙΑΧΕΙΡΙΣΤΗΣ == personId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteProistamenos(int personId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.SYS_APOFASEIS_ALL where d.ΠΡΟΙΣΤΑΜΕΝΟΣ == personId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteDirector(int personId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.SYS_APOFASEIS_ALL where d.ΔΙΕΥΘΥΝΤΗΣ == personId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteGeneralDirector(int personId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.SYS_APOFASEIS_ALL where d.ΓΕΝΙΚΟΣ == personId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteDioikitis(int personId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.SYS_APOFASEIS_ALL where d.ΔΙΟΙΚΗΤΗΣ == personId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteAntiproedros(int personId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.SYS_APOFASEIS_ALL where d.ΑΝΤΙΠΡΟΕΔΡΟΣ == personId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiParameters(int schoolyearID)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΡΧΙΚΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearID select d).Count();

                if (data > 0) 
                    return false;
                else 
                    return true;
            }
        }


        #endregion  ΚΑΝΟΝΕΣ ΔΙΑΓΡΑΦΗΣ ΠΑΡΑΜΕΤΡΙΚΩΝ ΣΤΟΙΧΕΙΩΝ


        #region ΚΑΝΟΝΕΣ ΑΠΟΦΑΣΕΩΝ

        public static bool CanDeleteApofasiInitial(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();
                if (data > 0)
                    return false;
                else
                    return true;
            }
        }

        public static bool CanCreateApofasiInitial(int schoolID, int schoolyearID)
        {
            bool ok = true;
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΡΧΙΚΕΣ where d.ΣΧΟΛΗ == schoolID && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearID select d).Count();
                if (data > 0)
                    ok = false;
                return (ok);
            }
        }

        public static bool CanDeleteApofasiDirect(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΠΕΥΘΕΙΑΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiSupplement(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiSupplementAK(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiModify(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiModifyAK(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiCancel(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiRevoke(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiInitialAnapl(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0)
                    return false;
                else
                    return true;
            }
        }

        public static bool CanCreateApofasiInitialAnapl(int schoolID, int schoolyearID)
        {
            bool ok = true;
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ where d.ΣΧΟΛΗ == schoolID && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearID select d).Count();
                if (data > 0)
                    ok = false;
                return (ok);
            }
        }

        public static bool CanDeleteApofasiSupplementAnapl(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiCancelAnapl(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiRevokeAnapl(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΑΝΑΚΛΗΣΗΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiModifyAnapl(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiModifyAnaplAK(int apofasiId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (data > 0) return false;
                else return true;
            }
        }

        #endregion


        #region ΓΕΝΙΚΟΙ ΕΛΕΓΧΟΙ

        public static bool existsUsername(string username)
        {
            using (var db = new IrisDBEntities())
            {
                var userAdmins = db.USER_ADMINS.Where(u => u.USERNAME == username).FirstOrDefault();
                var userSchools = db.USER_SCHOOLS.Where(u => u.USERNAME == username).FirstOrDefault();

                return (userAdmins != null || userSchools != null);
            }
        }

        public static bool CanDeleteUpload(int uploadId)
        {
            using (var db = new IrisDBEntities())
            {
                int data = (from d in db.UPLOADS_FILES where d.UPLOAD_ID == uploadId select d).Count();

                if (data > 0) 
                    return false;
                else 
                    return true;
            }
        }

        #endregion


    }
}