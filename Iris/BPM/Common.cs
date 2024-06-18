using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Iris.DAL;
using Iris.Models;
using Iris.BPM;
using Iris.Notification;


namespace Iris.BPM
{

    public static class Common
    {
        private const int EPAS = 1;
        private const int IEK = 2;


        #region String Functions (equivalent to VB)

        public static string Right(string text, int numberCharacters)
        {
            return text.Substring(numberCharacters > text.Length ? 0 : text.Length - numberCharacters);
        }

        public static string Left(string text, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "length must be > 0");
            else if (length == 0 || text.Length == 0)
                return "";
            else if (text.Length <= length)
                return text;
            else
                return text.Substring(0, length);
        }
        public static int Len(string text)
        {
            int _length;
            _length = text.Length;
            return _length;
        }
        public static byte Asc(string src)
        {
            return (System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(src + "")[0]);
        }
        public static char Chr(byte src)
        {
            return (System.Text.Encoding.GetEncoding("iso-8859-1").GetChars(new byte[] { src })[0]);
        }
        public static bool isNumber(string param)
        {
            Regex isNum = new Regex("[^0-9]");
            return !isNum.IsMatch(param);
        }

        #endregion

        #region DATE FUNCTIONS
        /// <summary>
        /// Μετατρέπει τον αριθμό του μήνα σε λεκτικό
        /// στη γενική πτώση.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string monthToGRstring(int m)
        {
            string stGRmonth = "";

            switch (m)
            {
                case 1: stGRmonth = "Ιανουαρίου"; break;
                case 2: stGRmonth = "Φεβρουαρίου"; break;
                case 3: stGRmonth = "Μαρτίου"; break;
                case 4: stGRmonth = "Απριλίου"; break;
                case 5: stGRmonth = "Μαϊου"; break;
                case 6: stGRmonth = "Ιουνίου"; break;
                case 7: stGRmonth = "Ιουλίου"; break;
                case 8: stGRmonth = "Αυγούστου"; break;
                case 9: stGRmonth = "Σεπτεμβρίου"; break;
                case 10: stGRmonth = "Οκτωβρίου"; break;
                case 11: stGRmonth = "Νοεμβρίου"; break;
                case 12: stGRmonth = "Δεκεμβρίου"; break;
                default: break;
            }
            return stGRmonth;
        }

        /// <summary>
        /// Ελέγχει αν η αρχική ημερομηνία είναι μικρότερη
        /// ή ίση με την τελική ημερομηνία.
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public static bool ValidStartEndDates(DateTime dateStart, DateTime dateEnd)
        {
            bool result;

            if (dateStart > dateEnd)
                result = false;
            else
                result = true;
            return result;
        }

        /// <summary>
        /// Ελέγχει αν δύο ημερομηνίες ανήκουν στο ίδιο έτος.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool DatesInSameYear(DateTime date1, DateTime date2)
        {
            bool result;

            if (date1.Year != date2.Year)
                result = false;
            else
                result = true;
            return result;
        }

        /// <summary>
        /// Ελέγχει αν δύο ημερομηνίες είναι μέσα στο ίδιο Σχ. Έτος
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="schoolYearID"></param>
        /// <returns></returns>
        public static bool DatesInSchoolYear(DateTime dateStart, DateTime dateEnd, int schoolYearID)
        {
            bool result = true;

            using (var db = new IrisDBEntities())
            {
                var schoolYear = (from s in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ
                                  where s.SCHOOLYEAR_ID == schoolYearID
                                  select new { s.ΗΜΝΙΑ_ΕΝΑΡΞΗ, s.ΗΜΝΙΑ_ΛΗΞΗ }).FirstOrDefault();

                if (dateStart < schoolYear.ΗΜΝΙΑ_ΕΝΑΡΞΗ || dateEnd > schoolYear.ΗΜΝΙΑ_ΛΗΞΗ)
                    result = false;

                return result;
            }
        }

        /// <summary>
        /// Ελέγχει αν το σχολικό έτος έχει τη μορφή ΝΝΝΝ-ΝΝΝΝ
        /// και αν τα έτη είναι συμβατά με τις ημερομηνίες
        /// έναρξης και λήξης.
        /// </summary>
        /// <param name="syear"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool VerifySchoolYear(string syear, DateTime d1, DateTime d2)
        {

            if (syear.IndexOf('-') == -1)
            {
                //ShowAdminMessage("Το σχολικό έτος πρέπει να είναι στη μορφή έτος1-έτος2.");
                return false;
            }

            string[] split = syear.Split(new Char[] { '-' });
            string sy1 = Convert.ToString(split[0]);
            string sy2 = Convert.ToString(split[1]);

            if (!isNumber(sy1) || !isNumber(sy2))
            {
                //ShowAdminMessage("Τα έτη δεν είναι αριθμοί.");
                return false;
            }
            else
            {
                int y1 = Convert.ToInt32(sy1);
                int y2 = Convert.ToInt32(sy2);

                if (y2 - y1 > 1 || y2 - y1 <= 0)
                {
                    //UserFunctions.ShowAdminMessage("Τα έτη δεν είναι σωστά.");
                    return false;
                }
                if (d1.Year != y1 || d2.Year != y2)
                {
                    //UserFunctions.ShowAdminMessage("Οι ημερομηνίες δεν συμφωνούν με τα έτη.");
                    return false;
                }
            }
            // at this point everything is ok
            return true;
        }

        /// <summary>
        /// Ελέγχει αν το χολικό έτος μορφής ΝΝΝΝ-ΝΝΝΝ υπάρχει ήδη.
        /// </summary>
        /// <param name="syear"></param>
        /// <returns></returns>
        public static bool SchoolYearExists(int syear)
        {
            using (var db = new IrisDBEntities())
            {
                var syear_recs = (from s in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ
                                  where s.SCHOOLYEAR_ID == syear
                                  select s).Count();

                if (syear_recs != 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Υπολογίζει τα έτη (στρογγυλοποιημένα) μεταξύ δύο ημερομηνιών
        /// </summary>
        /// <param name="sdate">αρχική ημερομηνία</param>
        /// <param name="edate">τελική ημερομηνία</param>
        /// <returns></returns>
        public static int YearsDiff(DateTime sdate, DateTime edate)
        {
            TimeSpan ts = edate - sdate;
            int days = ts.Days;

            double _years = days / 365;

            int years = Convert.ToInt32(Math.Ceiling(_years));

            return years;
        }

        public static int DaysDiff(DateTime sdate, DateTime edate)
        {
            TimeSpan ts = edate - sdate;
            int days = ts.Days;

            return days;
        }

        /// <summary>
        /// Get the calendar weeks between 2 dates
        /// </summary>
        /// <param name="d1">First day of date span</param>
        /// <param name="d2">Last day of date span</param>
        /// <returns></returns>
        public static int CalendarWeeks(string d1, string d2)
        {
            DateTime date1 = Convert.ToDateTime(d1);
            DateTime date2 = Convert.ToDateTime(d2);

            int weeksToRemove = 0;

            int year_date1 = date1.Year;
            int year_date2 = date2.Year;

            DateTime ChristmasStartDate = new DateTime(year_date1, 12, 24);
            DateTime ChristmasFinalDate = new DateTime(year_date1 + 1, 1, 6);

            DateTime EasterDate = GetOrthodoxEaster(year_date2);

            if (date1 < ChristmasStartDate && date2 > ChristmasFinalDate) weeksToRemove = 2;
            if (date1 < ChristmasStartDate && date2 > EasterDate.AddDays(7))
                weeksToRemove += 2;
            else if (date1 > ChristmasFinalDate && date2 > EasterDate.AddDays(7))
                weeksToRemove = 2;

            return 1 + (int)((date2 - date1).TotalDays / 7) - weeksToRemove;
        }

        public static DateTime GetOrthodoxEaster(int year)
        {
            var a = year % 19;
            var b = year % 7;
            var c = year % 4;

            var d = (19 * a + 16) % 30;
            var e = (2 * c + 4 * b + 6 * d) % 7;
            var f = (19 * a + 16) % 30;

            var key = f + e + 3;
            var month = (key > 30) ? 5 : 4;
            var day = (key > 30) ? key - 30 : key;

            return new DateTime(year, month, day);
        }

        public static void GetEasterDates(DateTime EasterDate, out DateTime EasterStartDate, out DateTime EasterFinalDate)
        {
            EasterStartDate = EasterDate.AddDays(-6);
            EasterFinalDate = EasterDate.AddDays(7);
        }

        public static void GetChristmasDates(int year, out DateTime ChristmasStartDate, out DateTime ChristmasFinalDate)
        {
            ChristmasStartDate = new DateTime(year, 12, 24);
            ChristmasFinalDate = new DateTime(year + 1, 1, 6);
        }

        #endregion

        #region AFM VALIDATION

        /// ------------------------------------------------------------------------
        /// CheckAFM: Ελέγχει αν ένα ΑΦΜ είναι σωστό
        /// Το ΑΦΜ που θα ελέγξουμε
        /// true = ΑΦΜ σωστό, false = ΑΦΜ Λάθος
        /// Αυτή είναι η χρησιμοποιούμενη μεθοδος.
        /// Προσθήκη: Αποκλεισμός όταν όλα τα ψηφία = 0 (ο αλγόριθμος τα δέχεται!)
        /// Ημ/νια: 12/3/2013
        /// ------------------------------------------------------------------------
        public static bool CheckAFM(string cAfm)
        {
            int nExp = 1;
            // Ελεγχος αν περιλαμβάνει μόνο γράμματα
            try { long nAfm = Convert.ToInt64(cAfm); }

            catch (Exception) { return false; }

            // Ελεγχος μήκους ΑΦΜ
            if (string.IsNullOrWhiteSpace(cAfm))
            {
                return false;
            }

            cAfm = cAfm.Trim();
            int nL = cAfm.Length;
            if (nL != 9) return false;

            // Έλεγχος αν όλα τα ψηφία είναι 0
            var count = cAfm.Count(x => x == '0');
            if (count == cAfm.Length) return false;

            //Υπολογισμός αν το ΑΦΜ είναι σωστό

            int nSum = 0;
            int xDigit = 0;
            int nT = 0;

            for (int i = nL - 2; i >= 0; i--)
            {
                xDigit = int.Parse(cAfm.Substring(i, 1));
                nT = xDigit * (int)(Math.Pow(2, nExp));
                nSum += nT;
                nExp++;
            }

            xDigit = int.Parse(cAfm.Substring(nL - 1, 1));

            nT = nSum / 11;
            int k = nT * 11;
            k = nSum - k;
            if (k == 10) k = 0;
            if (xDigit != k) return false;

            return true;
        }

        #endregion


        #region CUSTOM IRIS FUNCTIONS

        public static float Max(params float[] values)
        {
            return Enumerable.Max(values);
        }

        public static float Min(params float[] values)
        {
            return Enumerable.Min(values);
        }

        public static string GetSchoolYearText(int syearId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ
                            where d.SCHOOLYEAR_ID == syearId
                            select d).FirstOrDefault();

                string syearText = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
                return (syearText);
            }
        }

        public static string GeneratePassword(Random rnd)
        {
            int random = rnd.Next(1, 1000);
            return string.Format("{0:000}", random);
        }

        /// <summary>
        /// Υπολογίζει τις ημέρες λογιστικού έτους μεταξύ δύο ημερομηνιών,
        /// προσομειώνοντας τη συνάρτηση Days360 του Excel.
        /// </summary>
        /// <param name="initial_date"></param>
        /// <param name="final_date"></param>
        /// <returns name="meres"></returns>
        public static int Days360(DateTime initial_date, DateTime final_date)
        {
            DateTime date1 = initial_date;
            DateTime date2 = final_date;

            var y1 = date1.Year;
            var y2 = date2.Year;
            var m1 = date1.Month;
            var m2 = date2.Month;
            var d1 = date1.Day;
            var d2 = date2.Day;

            DateTime tempDate = date1.AddDays(1);
            if (tempDate.Day == 1 && date1.Month == 2)
            {
                d1 = 30;
            }
            if (d2 == 31 && d1 == 30)
            {
                d2 = 30;
            }

            double meres = (y2 - y1) * 360 + (m2 - m1) * 30 + (d2 - d1);
            meres = (meres / 30) * 25;
            meres = Math.Ceiling(meres);

            return Convert.ToInt32(meres);
        }

        #endregion


        #region GETTERS

        public static int GetSchoolType(int schoolId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.ΣΧΟΛΗ_ΚΩΔ == schoolId select d).FirstOrDefault();
                int school_type = (int)data.ΔΟΜΗ;

                return (school_type);
            }
        }

        public static int GetSchoolPeriferiaki(int schoolId)
        {
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.ΣΧΟΛΗ_ΚΩΔ == schoolId select d).FirstOrDefault();
                int periferiaki = (int)data.ΠΕΡΙΦΕΡΕΙΑΚΗ;

                return (periferiaki);
            }
        }

        public static string GetKladosEniaiosText(int klados_eniaios)
        {
            string klados_unified = "";

            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ where d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ == klados_eniaios select d).FirstOrDefault();
                if (data != null) klados_unified = data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ;

                return (klados_unified);
            }
        }

        public static int GetKladosFromEidikotita(int eidikotitaId)
        {
            int klados = 0;
            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ where d.ΚΩΔΙΚΟΣ == eidikotitaId select d).FirstOrDefault();

                if (data != null)
                {
                    klados = (int)data.ΚΛΑΔΟΣ;
                }
                return klados;
            }
        }

        // ΠΡΟΣΟΧΗ! - ΕΝΗΜΕΡΩΣΗ ΓΙΑ ΝΕΟΥΣ ΚΩΔΙΚΟΥΣ (2018)
        // ΚΑΙ ΣΤΟΝ ΠΙΝΑΚΑ ΤΗΣ ΒΑΣΗΣ ΓΙΑ ΤΑ ΩΡΟΜΙΣΘΙΑ
        // ΟΙ ΠΕ12, ΠΕ19 ΕΙΝΑΙ ΤΩΡΑ ΠΕ81-ΠΕ85, ΠΕ86 !
        public static int GetWagesCodeFromEidikotita(int eidikotitaId)
        {
            int wagesCode = 1;
            string eidikotitaCode = "ΠΕ";

            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ where d.ΚΩΔΙΚΟΣ == eidikotitaId select d).FirstOrDefault();

                if (data != null)
                {
                    eidikotitaCode = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ;
                }

                bool pe8186Predicate = eidikotitaCode.IndexOf("ΠΕ81") >= 0 ||
                           eidikotitaCode.IndexOf("ΠΕ82") >= 0 ||
                           eidikotitaCode.IndexOf("ΠΕ83") >= 0 ||
                           eidikotitaCode.IndexOf("ΠΕ84") >= 0 ||
                           eidikotitaCode.IndexOf("ΠΕ85") >= 0 ||
                           eidikotitaCode.IndexOf("ΠΕ86") >= 0;

                if (eidikotitaCode.IndexOf("ΠΕ") >= 0) wagesCode = 1;
                else if (eidikotitaCode.IndexOf("ΤΕ") >= 0) wagesCode = 4;
                else if (eidikotitaCode.IndexOf("ΔΕ") >= 0) wagesCode = 5;

                if (eidikotitaCode.IndexOf("ΠΕ08") >= 0 || pe8186Predicate) wagesCode = 2;
                else if (eidikotitaCode.IndexOf("ΠΕ87.01") >= 0) wagesCode = 3;

                return (wagesCode);
            }
        }

        public static int GetGenderFromName(string firstname)
        {
            int gender = 2;

            if (firstname[firstname.Length - 1] == 'Σ' || firstname[firstname.Length - 1] == 'Λ'
                || firstname[firstname.Length - 1] == 'Ν' || firstname[firstname.Length - 1] == 'Μ'
                || firstname[firstname.Length - 1] == 'Ξ' || firstname[firstname.Length - 1] == 'Ρ' || firstname[firstname.Length - 1] == 'Β') gender = 1;

            //int pos1 = firstname.IndexOf("Σ", firstname.Length-2, 1);
            //int pos2 = firstname.IndexOf("Λ", firstname.Length - 2, 1);
            //int pos3 = firstname.IndexOf("Ν", firstname.Length - 2, 1);

            //if (pos1 >= 0 || pos1 >= 0 || pos3 >= 0) gender = 1;
            return (gender);
        }

        public static int GetWagesCodeFromEidikotitaAN(int eidikotitaId)
        {
            int wagesCode = 1;

            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ where d.ΚΩΔΙΚΟΣ == eidikotitaId select d).FirstOrDefault();
                string eidikotitaCode = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ;

                if (eidikotitaCode.IndexOf("ΠΕ") >= 0) wagesCode = 6;
                else if (eidikotitaCode.IndexOf("ΤΕ") >= 0) wagesCode = 7;
                else if (eidikotitaCode.IndexOf("ΔΕ") >= 0) wagesCode = 8;

                return (wagesCode);
            }
        }

        public static string GetArxikiProtocol(string apofasiArxikiProtocol, int? schoolyear, int? school)
        {
            string date_protocol = "";
            string resultProtocol = "";

            using (var db = new IrisDBEntities())
            {
                var data1 = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΡΧΙΚΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear && d.ΣΧΟΛΗ == school select d).FirstOrDefault();
                if (data1 != null)
                {
                    DateTime? dt = data1.ΗΜΕΡΟΜΗΝΙΑ;
                    if (data1.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ != null)
                        dt = data1.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ;

                    date_protocol = dt.HasValue ? dt.Value.ToString("dd-MM-yyyy") : string.Empty;
                    if (apofasiArxikiProtocol == null)
                    {
                        resultProtocol = data1.ΠΡΩΤΟΚΟΛΛΟ + "/" + date_protocol;
                    }
                    else
                    {
                        resultProtocol = apofasiArxikiProtocol;
                    }
                }
                return (resultProtocol);
            }
        }

        public static string GetArxikiAnaplProtocol(string apofasiArxikiProtocol, int? schoolyear, int? school)
        {
            string date_protocol = "";
            string resultProtocol = "";

            using (var db = new IrisDBEntities())
            {
                var data1 = (from d in db.ΑΠΟΦΑΣΕΙΣ_ΑΡΧΙΚΕΣ_ΑΝ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyear && d.ΣΧΟΛΗ == school select d).FirstOrDefault();
                if (data1 != null)
                {
                    DateTime? dt = data1.ΗΜΕΡΟΜΗΝΙΑ;
                    if (data1.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ != null)
                        dt = data1.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ;

                    date_protocol = dt.HasValue ? dt.Value.ToString("dd-MM-yyyy") : string.Empty;
                    if (apofasiArxikiProtocol == null)
                    {
                        resultProtocol = data1.ΠΡΩΤΟΚΟΛΛΟ + "/" + date_protocol;
                    }
                    else
                    {
                        resultProtocol = apofasiArxikiProtocol;
                    }
                }
                return (resultProtocol);
            }
        }

        #endregion


        #region APOFASEIS COMMON FUNCTIONS

        public static int? LoadProistamenos(int? schoolyearId, int departement)
        {
            int? value = null;
            using (var db = new IrisDBEntities())
            {
                if (departement == EPAS)
                {
                    var data = (from d in db.Δ_ΠΡΟΙΣΤΑΜΕΝΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                    if (data != null) value = data.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ;
                }
                else
                {
                    var data = (from d in db.Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                    if (data != null) value = data.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ;
                }
                return value;
            }
        }

        public static int? LoadDirector(int? schoolyearId, int departement)
        {
            int? value = null;

            using (var db = new IrisDBEntities())
            {
                if (departement == EPAS)
                {
                    var data = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                    if (data != null) value = data.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ;
                }
                else
                {
                    var data = (from d in db.Δ2_ΔΙΕΥΘΥΝΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                    if (data != null) value = data.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ;
                }
                return value;
            }
        }

        public static int? LoadGenikos(int? schoolyearId, int departement)
        {
            int? value = null;
            using (var db = new IrisDBEntities())
            {
                if (departement == EPAS)
                {
                    var data = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                    if (data != null) value = data.ΓΕΝΙΚΟΣ_ΚΩΔ;
                }
                else
                {
                    var data = (from d in db.Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                    if (data != null) value = data.ΓΕΝΙΚΟΣ_ΚΩΔ;
                }
                return value;
            }
        }

        public static int? LoadDioikitis(int? schoolyearId, int departement)
        {
            int? value = null;
            using (var db = new IrisDBEntities())
            {
                if (departement == EPAS)
                {
                    var data = (from d in db.Δ_ΔΙΟΙΚΗΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                    if (data != null) value = data.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ;
                }
                else
                {
                    var data = (from d in db.Δ2_ΔΙΟΙΚΗΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                    if (data != null) value = data.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ;
                }
                return value;
            }
        }

        public static int? LoadAntiproedros(int? schoolyearId, int departement)
        {
            int? value = null;
            using (var db = new IrisDBEntities())
            {
                if (departement == EPAS)
                {
                    var data = (from d in db.Δ_ΑΝΤΙΠΡΟΕΔΡΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                    if (data != null) value = data.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ;
                }
                else
                {
                    var data = (from d in db.Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                    if (data != null) value = data.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ;
                }
                return value;
            }
        }

        public static bool ValidSignatures(int? schoolyearId, int departement)
        {
            bool ok = true;

            int? proistamenos = LoadProistamenos(schoolyearId, departement);
            int? director = LoadDirector(schoolyearId, departement);
            int? genikos = LoadGenikos(schoolyearId, departement);
            int? dioikitis = LoadDioikitis(schoolyearId, departement);
            int? aproedros = LoadAntiproedros(schoolyearId, departement);

            bool predicate = proistamenos != null && director != null && genikos != null && dioikitis != null && aproedros != null;
            if (predicate == false) ok = false;

            return (ok);
        }

        #endregion


        #region UPLOAD FUNCTIONS

        public static string GetSchoolUsername(int schoolId)
        {
            string username = "";

            using (var db = new IrisDBEntities())
            {
                var data = (from d in db.USER_SCHOOLS where d.USER_SCHOOLID == schoolId select d).FirstOrDefault();
                if (data != null) username = data.USERNAME;

                return (username);
            }
        }

        public static Guid GetFileGuidFromName(string filename, int uploadId)
        {
            Guid file_id = new Guid();

            using (var db = new IrisDBEntities())
            {
                var fileData = (from d in db.UPLOADS_FILES where d.FILENAME == filename && d.UPLOAD_ID == uploadId select d).FirstOrDefault();
                if (fileData != null) file_id = fileData.ID;

                return (file_id);
            }
        }

        public static Tuple<int, int> GetUploadInfo(int uploadId)
        {
            int school_id = 0;
            int syear_id = 0;

            using (var db = new IrisDBEntities())
            {
                var upload = (from d in db.UPLOADS where d.UPLOAD_ID == uploadId select d).FirstOrDefault();
                if (upload != null)
                {
                    school_id = (int)upload.SCHOOL_ID;
                    syear_id = (int)upload.SCHOOLYEAR_ID;
                }

                var data = Tuple.Create(school_id, syear_id);
                return (data);
            }
        }

        #endregion

    }   // class Common


    // View Engine extension of Primus
    public class IrisViewEngine : RazorViewEngine
    {
        public IrisViewEngine()
        {
            string[] locations = new string[] {  
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Document/{0}.cshtml",

                "~/Views/School/Teachers/{0}.cshtml",
                "~/Views/School/Printouts/{0}.cshtml",
                "~/Views/School/Statistika/{0}.cshtml",
                "~/Views/School/Misc/{0}.cshtml",          

                "~/Views/Admin/{1}/{0}.cshtml",
                "~/Views/Admin/Apofaseis/{0}.cshtml",
                "~/Views/Admin/ApofaseisAnaplirotes/{0}.cshtml",
                "~/Views/Admin/ApofaseisPrint/{0}.cshtml",
                "~/Views/Admin/Teachers/{0}.cshtml",
                "~/Views/Admin/Statistika/{0}.cshtml",
                "~/Views/Admin/Printouts/{0}.cshtml",

                "~/Views/Admin2/{1}/{0}.cshtml",
                "~/Views/Admin2/Apofaseis/{0}.cshtml",
                "~/Views/Admin2/ApofaseisPrint/{0}.cshtml",
                "~/Views/Admin2/Teachers/{0}.cshtml",
                "~/Views/Admin2/Statistika/{0}.cshtml",
                "~/Views/Admin2/Printouts/{0}.cshtml",

                "~/Views/Admin2/{1}/{0}.cshtml",
                "~/Views/Admin2/Anatheseis/{0}.cshtml",
                "~/Views/Admin2/Apofaseis/{0}.cshtml",
                "~/Views/Admin2/Teachers/{0}.cshtml",
                "~/Views/Admin2/Setup/{0}.cshtml",
                "~/Views/Admin2/Utilities/{0}.cshtml",

                "~/Views/Shared/PartialViews/{0}.cshtml",
                "~/Views/Shared/EditorTemplates/{0}.cshtml",
                "~/Views/Shared/Layouts/{0}.cshtml"
                };

            this.ViewLocationFormats = locations;
            this.PartialViewLocationFormats = locations;
            this.MasterLocationFormats = locations;
        }
    }

}   // namespace