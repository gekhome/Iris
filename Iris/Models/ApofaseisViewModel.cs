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
    public class ApofasiInitialViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Α'/θμιας")]
        public string ΑΠΟΦΑΣΗ_ΠΡΩΤΟΒΑΘΜΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Β'/θμιας")]
        public string ΑΠΟΦΑΣΗ_ΔΕΥΤΕΡΟΒΑΘΜΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εγκριτική απόφαση")]
        public string ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Άρθρο ΠΥΣ")]
        public string ΠΥΣ_ΑΡΘΡΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

    }

    public class ApofasiInitialGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }


    }

    public class ApofasiDirectViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Υπουργική απόφαση")]
        public string ΥΠΟΥΡΓΙΚΗ_ΑΠΟΦΑΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

    }

    public class ApofasiDirectGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiSupplementViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

    }

    public class ApofasiSupplementGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiSupplementAKViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [Display(Name = "Σχετικό κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΑΚ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

    }

    public class ApofasiSupplementAKGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiModifyViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

        [Display(Name = "Απόφαση μεταβολής")]
        public string ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ { get; set; }
    }

    public class ApofasiModifyGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiModifyAKViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [Display(Name = "Σχετικό κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΑΚ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

        [Display(Name = "Απόφαση μεταβολής")]
        public string ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ { get; set; }
    }

    public class ApofasiModifyAKGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiCorrectViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [Display(Name = "Σχετικό κείμενο")]
        public string ΚΕΙΜΕΝΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

    }

    public class ApofasiCorrectGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiCancelViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [Display(Name = "Σχετικό κείμενο")]
        public string ΚΕΙΜΕΝΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

        [Display(Name = "Απόφαση μεταβολής")]
        public string ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ { get; set; }
    }

    public class ApofasiCancelGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiRevokeViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [Display(Name = "Εγκύκλιος Α2")]
        public string ΕΓΚΥΚΛΙΟΣ_Α2 { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εκπαιδευτικός")]
        public string ΕΚΠΑΙΔΕΥΤΙΚΟΣ { get; set; }

        [Display(Name = "Σχετικό κείμενο")]
        public string ΚΕΙΜΕΝΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

        [Display(Name = "Απόφαση μεταβολής")]
        public string ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ { get; set; }
    }

    public class ApofasiRevokeGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    // ΑΝΑΠΛΗΡΩΤΕΣ

    public class ApofasiInitialAnaplirotesViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Α'/θμιας")]
        public string ΑΠΟΦΑΣΗ_ΠΡΩΤΟΒΑΘΜΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Β'/θμιας")]
        public string ΑΠΟΦΑΣΗ_ΔΕΥΤΕΡΟΒΑΘΜΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εγκριτική απόφαση")]
        public string ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Άρθρο ΠΥΣ")]
        public string ΠΥΣ_ΑΡΘΡΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Υπουργού (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Λεκτικό Υπουργού (για αναπληρωτές)")]
        public string ΛΕΚΤΙΚΟ_ΥΠΟΥΡΓΟΥ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Διοικητή (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ { get; set; }
    }

    public class ApofasiInitialAnaplirotesGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }


    }

    public class ApofasiSupplementAnaplirotesViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Υπουργού (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Διοικητή (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ { get; set; }

    }

    public class ApofasiSupplementAnaplirotesGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiCorrectAnaplirotesViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [Display(Name = "Σχετικό κείμενο")]
        public string ΚΕΙΜΕΝΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Υπουργού (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Διοικητή (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ { get; set; }

    }

    public class ApofasiCorrectAnaplirotesGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiCancelAnaplirotesViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [Display(Name = "Σχετικό κείμενο")]
        public string ΚΕΙΜΕΝΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Υπουργού (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Διοικητή (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Απόφαση μεταβολής")]
        public string ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ { get; set; }
    }

    public class ApofasiCancelAnaplirotesGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiRevokeAnaplirotesViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [Display(Name = "Εγκύκλιος Α2")]
        public string ΕΓΚΥΚΛΙΟΣ_Α2 { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εκπαιδευτικός")]
        public string ΕΚΠΑΙΔΕΥΤΙΚΟΣ { get; set; }

        [Display(Name = "Σχετικό κείμενο")]
        public string ΚΕΙΜΕΝΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Υπουργού (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Διοικητή (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Απόφαση μεταβολής")]
        public string ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ { get; set; }
    }

    public class ApofasiRevokeAnaplirotesGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiModifyAnaplirotesViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

        [Display(Name = "Απόφαση μεταβολής")]
        public string ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Υπουργού (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Διοικητή (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ { get; set; }
    }

    public class ApofasiModifyAnaplirotesGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiModifyAnaplirotesAKViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρχική απόφαση")]
        public string ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [Display(Name = "Σχετικό κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΑΚ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Ελεύθερο κείμενο")]
        public string ΚΕΙΜΕΝΟ_ΥΠΟΨΗ { get; set; }

        [Display(Name = "Απόφαση μεταβολής")]
        public string ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Υπουργού (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Διοικητή (για αναπληρωτές)")]
        public string ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ { get; set; }
    }

    public class ApofasiModifyAnaplirotesAKGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }


    public class ApofasiParameters
    {
        public int apofasiId { get; set; }
        public int schoolyearId { get; set; }
        public int schoolId { get; set; }
        public string apofasiType { get; set; }

    }

}