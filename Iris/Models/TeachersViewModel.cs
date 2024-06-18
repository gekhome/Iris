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
    public class AnathesiInitialViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες/εβδ")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    public class AnathesiDirectViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες/εβδ")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }
    }

    public class AnathesiSupplementViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες/εβδ")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    public class AnathesiSupplementAKViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες/εβδ")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    public class AnathesiCancelViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες από")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΑΠΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες σε")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΣΕ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    public class AnathesiRevokeViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες από")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΑΠΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες σε")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΣΕ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    public class AnathesiModifyViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες από")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΑΠΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες σε")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΣΕ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    public class AnathesiModifyAKViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες από")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΑΠΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες σε")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΣΕ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    // ΑΝΑΠΛΗΡΩΤΕΣ

    public class AnathesiInitialAnaplirotesViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες/εβδ")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    public class AnathesiSupplementAnaplirotesViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες/εβδ")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    public class AnathesiCancelAnaplirotesViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες από")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΑΠΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες σε")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΣΕ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    public class AnathesiRevokeAnaplirotesViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες από")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΑΠΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες σε")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΣΕ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    public class AnathesiModifyAnaplirotesViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες από")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΑΠΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες σε")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΣΕ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    public class AnathesiModifyAnaplirotesAKViewModel
    {
        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες από")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΑΠΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(0, 40, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 40")]
        [Display(Name = "Ώρες σε")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΣΕ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ωρομίσθιο κωδ.")]
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

    }

    // ΜΗΤΡΩΑ ΑΝΑΘΕΣΕΩΝ

    public class RegAnathesiProslipsiViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string ΣΧΟΛ_ΕΤΟΣ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Δομή")]
        public string ΜΟΝΑΔΑ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Κλάδος")]
        public string ΚΛΑΔΟΣ { get; set; }

        [Display(Name = "Ώρες/εβδ")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Φύλο")]
        public string ΦΥΛΟ_ΛΕΚΤΙΚΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Ωρομίσθιο")]
        public Nullable<decimal> ΩΡΟΜΙΣΘΙΟ { get; set; }

        [Display(Name = "Σύμβαση")]
        public string ΣΥΜΒΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Display(Name = "Διαχειριστής")]
        public string ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΑΠΟΦΑΣΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "Ενιαίος κλάδος")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string ΠΕΡΙΦ_ΣΥΝΤΟΜΟΓΡΑΦΙΑ { get; set; }

        // ΚΩΔΙΚΟΙ

        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        public Nullable<int> ΣΧΟΛΗ { get; set; }

        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        public Nullable<int> ΦΥΛΟ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

        public int ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

    }

    public class RegAnathesiProslipsiAnViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string ΣΧΟΛ_ΕΤΟΣ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Εκπ.Δομή")]
        public string ΜΟΝΑΔΑ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Κλάδος")]
        public string ΚΛΑΔΟΣ { get; set; }

        [Display(Name = "Ώρες/εβδ")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Φύλο")]
        public string ΦΥΛΟ_ΛΕΚΤΙΚΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Ωρομίσθιο")]
        public Nullable<decimal> ΩΡΟΜΙΣΘΙΟ { get; set; }

        [Display(Name = "Σύμβαση")]
        public string ΣΥΜΒΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Display(Name = "Διαχειριστής")]
        public string ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΑΠΟΦΑΣΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "Ενιαίος κλάδος")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string ΠΕΡΙΦ_ΣΥΝΤΟΜΟΓΡΑΦΙΑ { get; set; }

        // ΚΩΔΙΚΟΙ

        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        public Nullable<int> ΣΧΟΛΗ { get; set; }

        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }

        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }

        public Nullable<int> ΦΥΛΟ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

        public int ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

    }

    public class RegAnathesiMetaboliViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string ΣΧΟΛ_ΕΤΟΣ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Εκπ.Δομή")]
        public string ΜΟΝΑΔΑ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Κλάδος")]
        public string ΚΛΑΔΟΣ { get; set; }

        [Display(Name = "Ώρες από")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΑΠΟ { get; set; }

        [Display(Name = "Ώρες σε")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΣΕ { get; set; }
      
        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public string ΣΥΜΒΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Display(Name = "Διαχειριστής")]
        public string ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΑΠΟΦΑΣΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "Ενιαίος κλάδος")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string ΠΕΡΙΦ_ΣΥΝΤΟΜΟΓΡΑΦΙΑ { get; set; }

        // ΚΩΔΙΚΟΙ

        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        public Nullable<int> ΣΧΟΛΗ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

        public int ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

    }

    public class RegAnathesiMetaboliAnViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string ΣΧΟΛ_ΕΤΟΣ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Εκπ.Δομή")]
        public string ΜΟΝΑΔΑ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Κλάδος")]
        public string ΚΛΑΔΟΣ { get; set; }

        [Display(Name = "Ώρες από")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΑΠΟ { get; set; }

        [Display(Name = "Ώρες σε")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ_ΣΕ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Σύμβαση")]
        public string ΣΥΜΒΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Display(Name = "Διαχειριστής")]
        public string ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΑΠΟΦΑΣΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "Ενιαίος κλάδος")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string ΠΕΡΙΦ_ΣΥΝΤΟΜΟΓΡΑΦΙΑ { get; set; }

        // ΚΩΔΙΚΟΙ

        public int ΑΝΑΘΕΣΗ_ΚΩΔ { get; set; }

        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        public Nullable<int> ΣΧΟΛΗ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }

        public int ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

    }

    // ΚΑΘΟΛΙΚΟ ΜΗΤΡΩΟ

    public class AnatheseisUniversalViewModel
    {
        public int ID { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Κλάδος")]
        public string ΚΛΑΔΟΣ { get; set; }

        [Display(Name = "Ώρ/εβ")]
        public Nullable<short> ΩΡΕΣ_ΕΒΔ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Ωρομίσθιο")]
        public Nullable<decimal> ΩΡΟΜΙΣΘΙΟ { get; set; }

        [Display(Name = "Ώρες 34εβδ")]
        public Nullable<int> ΩΡΕΣ_34ΕΒΔ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Ποσό 34εβδ")]
        public Nullable<decimal> ΠΟΣΟ_34ΕΒΔ { get; set; }

        [Display(Name = "Φύλο")]
        public string ΦΥΛΟ { get; set; }

        [Display(Name = "Σύμβαση")]
        public string ΣΥΜΒΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Δομή")]
        public string ΜΟΝΑΔΑ { get; set; }

        [Display(Name = "Σχολ.έτος")]
        public string ΣΧΟΛ_ΕΤΟΣ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Display(Name = "Σχολ. Μονάδα")]
        public string ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Απόφαση")]
        public string ΑΠΟΦΑΣΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "Διαχειριστής")]
        public string ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Ενιαίος κλάδος")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string ΠΕΡΙΦ_ΣΥΝΤΟΜΟΓΡΑΦΙΑ { get; set; }

        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        // ΚΩΔΙΚΟΙ
        public Nullable<int> ΣΧΟΛΗ { get; set; }
        public Nullable<int> ΚΛΑΔΟΣ_ΚΩΔ { get; set; }
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }
        public Nullable<int> ΩΡΟΜΙΣΘΙΟ_ΚΩΔ { get; set; }
        public Nullable<int> ΦΥΛΟ_ΚΩΔ { get; set; }
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }
        public Nullable<int> ΣΥΜΒΑΣΗ { get; set; }
        public int ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }
        public Nullable<int> ΔΟΜΗ { get; set; }

    }

}