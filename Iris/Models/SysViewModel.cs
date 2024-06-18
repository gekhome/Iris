using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Iris.Models;
using Iris.DAL;
using System.Web.Mvc;

namespace Iris.Models
{
    public class SysAnathesiTypeViewModel
    {
        public int ANATHESI_ID { get; set; }

        [Display(Name = "Ανάθεση είδος")]
        public string ANATHESI_TYPE { get; set; }
    }

    public class SysGenderViewModel
    {
        public int ΦΥΛΟ_ΚΩΔ { get; set; }
        public string ΦΥΛΟ { get; set; }

    }

    public class SysSchoolYearViewModel
    {
        public int SCHOOLYEAR_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(9, ErrorMessage = "Πρέπει να είναι μέχρι 9 χαρακτήρες (π.χ.2015-2016).")]
        [Display(Name = "Σχολικό Έτος")]
        public string ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία Έναρξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΕΝΑΡΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία Λήξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΛΗΞΗ { get; set; }
    }

    public class SysPeriferiakiViewModel
    {
        public int ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Περιφερειακή επωνυμία")]
        public string ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Ταχ. Διεύθυνση")]
        public string ΤΑΧ_ΔΙΕΥΘΥΝΣΗ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Φαξ")]
        public string FAX { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "E-Mail")]
        public string EMAIL { get; set; }

    }

    public class SysPeriferiaViewModel
    {
        public int PERIFERIA_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Περιφερειακή ενότητα")]
        public string PERIFERIA_NAME { get; set; }
    }

    public class SysKladosViewModel
    {
        public int ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Κλάδος")]
        public string ΚΛΑΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ωράριο")]
        public Nullable<int> ΩΡΑΡΙΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Βασικός μισθός")]
        public Nullable<decimal> ΜΙΣΘΟΣ { get; set; }

    }

    public class SysHourWagesViewModel
    {
        public int ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Ωρομίσθιο")]
        public Nullable<decimal> ΩΡΟΜΙΣΘΙΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Κλάδοι")]
        public string ΚΛΑΔΟΣ { get; set; }

    }

    public class SysEidikotitesOldViewModel
    {
        public int ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Κωδικός")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Ειδικότητα")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ { get; set; }
    }

    public class SysEidikotitesViewModel
    {
        public int ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Κωδικός")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Ειδικότητα ενιαία")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ1 { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Ενιαίος κλάδος")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Ειδικότητα πτυχίο")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος ενιαίος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ { get; set; }

        public virtual ΣΥΣ_ΚΛΑΔΟΙ ΣΥΣ_ΚΛΑΔΟΙ { get; set; }
        public virtual ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ { get; set; }
    }

    public class SysKladosUnifiedViewModel
    {
        public int ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(100, ErrorMessage = "Πρέπει να είναι μέχρι 100 χαρακτήρες.")]
        [Display(Name = "Κλάδος ενιαίος (κεφαλαία)")]
        public string ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(100, ErrorMessage = "Πρέπει να είναι μέχρι 100 χαρακτήρες.")]
        [Display(Name = "Κλάδος ενιαίος (πεζά)")]
        public string KLADOS_LOWERCASE { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ { get; set; }

    }

    public class SysEidikotitesOldNewViewModel
    {
        public int ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Ειδικότητα παλαιά")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΠΑΛΙΑ { get; set; }

        [Display(Name = "Ειδικότητα νέα")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΝΕΑ { get; set; }

        [Display(Name = "Κλάδος ενοποιημένος")]
        public string ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ { get; set; }

    }

    public class SysDomesViewModel
    {
        public int ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Σχολ. Δομή")]
        public string ΜΟΝΑΔΑ { get; set; }

    }

    public class SysProkirixiApofasiViewModel
    {
        public int ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολικό έτος")]
        public int ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Προκήρυξη ΕΠΑΣ")]
        public string ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Προκήρυξη ΙΕΚ")]
        public string ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Υπουργική ΕΠΑΣ")]
        public string ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Υπουργική ΙΕΚ")]
        public string ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ_ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Λεκτικό υπουργού")]
        public string ΥΠΟΥΡΓΟΣ_ΛΕΚΤΙΚΟ { get; set; }

    }

    public class ProkirixiApofasiGridViewModel
    {
        public int ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολικό έτος")]
        public int ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Προκήρυξη ΕΠΑΣ")]
        public string ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Προκήρυξη ΙΕΚ")]
        public string ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ { get; set; }

    }

    public class ApofasiParametersGridViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολικό έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Εγκριτική απόφαση ΠΥΣ")]
        public string ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Άρθρο ΠΥΣ")]
        public string ΠΥΣ_ΑΡΘΡΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Απόφαση Διοικητή (αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ { get; set; }
    }


    public class SysApofasiParametersViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολικό έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Εγκριτική απόφαση ΠΥΣ")]
        public string ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Άρθρο ΠΥΣ")]
        public string ΠΥΣ_ΑΡΘΡΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Απόφαση Διοικητή (αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Υπουργική απόφαση (αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Λεκτικό υπουργού (αναπλ.)")]
        public string ΛΕΚΤΙΚΟ_ΥΠΟΥΡΓΟΥ { get; set; }

    }

    public class SysEpitropesViewModel
    {
        public int ΕΠΙΤΡΟΠΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Απόφαση σύστασης Α'/θμιας")]
        public string ΠΡΩΤΟΒΑΘΜΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Απόφαση σύστασης Β'/θμιας")]
        public string ΔΕΥΤΕΡΟΒΑΘΜΙΑ { get; set; }

        [Display(Name = "Σχολικό έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

    }

    public class sqlEidikotitesViewModel
    {
        public int ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Ειδικότητα (πτυχίο)")]
        public string ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ { get; set; }

        public Nullable<int> ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ { get; set; }

    }

    public class SysEgrafoTypeViewModel
    {
        public int ΚΩΔΙΚΟΣ { get; set; }
        public string ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

    }

    public class SysReportViewModel
    {
        public int DOC_ID { get; set; }

        [Display(Name = "Ονομασία")]
        public string DOC_NAME { get; set; }

        [Display(Name = "Περιγραφή")]
        public string DOC_DESCRIPTION { get; set; }

        [Display(Name = "Κατηγορία")]
        public string DOC_CLASS { get; set; }

    }

    #region TOOLS

    //----------------------------------------------------
    // new addition 30-07-2016 for MasterChild grids
    public class PeriferiaViewModel
    {
        public int PERIFERIA_ID { get; set; }

        [Display(Name = "Περιφερειακή Ενότητα")]
        public string PERIFERIA_NAME { get; set; }
    }

    public class DimosViewModel
    {
        public int DIMOS_ID { get; set; }

        [Display(Name = "Δήμος")]
        public string DIMOS { get; set; }
        public Nullable<int> DIMOS_PERIFERIA { get; set; }
    }
    public class DimoiParameters
    {
        public int PERIFERIA_ID { get; set; }
    }

    public class AnatheseisParameters
    {
        public int schoolYearID { get; set; }

        public int schoolID { get; set; }

    }

    #endregion

    public class SchoolLoginsViewModel
    {
        public int LOGIN_ID { get; set; }

        [Display(Name = "Εκπαιδευτική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Τελευταία είσοδος")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public Nullable<System.DateTime> LOGIN_DATETIME { get; set; }

    }
}

