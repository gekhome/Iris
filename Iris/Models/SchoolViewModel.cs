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
    public class SchoolsViewModel
    {
        public int ΣΧΟΛΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Επωνυμία")]
        public string ΕΠΩΝΥΜΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. Δομή")]
        public Nullable<int> ΔΟΜΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Ταχ. Διεύθυνση")]
        public string ΤΑΧ_ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλ. Διευθυντή")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλ. Γραμματείας")]
        public string ΓΡΑΜΜΑΤΕΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Φαξ")]
        public string ΦΑΞ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "E-Mail")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Διευθυντής")]
        public string ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φύλο Δ/ντή")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Κινητό Δ/ντη")]
        public string ΚΙΝΗΤΟ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Αναπληρωτής")]
        public string ΥΠΟΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Φύλο Αναπλ/τή")]
        public Nullable<int> ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ { get; set; }

        [Display(Name = "Περιφερειακή Ενότητα")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        public virtual ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ { get; set; }
        public virtual SYS_PERIFERIES SYS_PERIFERIES { get; set; }

    }

    public class SchoolsGridViewModel
    {
        public int ΣΧΟΛΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Επωνυμία")]
        public string ΕΠΩΝΥΜΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. Δομή")]
        public Nullable<int> ΔΟΜΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Περιφερειακή Ενότητα")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑ { get; set; }
    }
}