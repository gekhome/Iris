//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Iris.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ΣΥΣ_ΚΛΑΔΟΙ_ΕΝΙΑΙΟΙ()
        {
            this.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ = new HashSet<ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ>();
        }
    
        public int ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ { get; set; }
        public string ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ { get; set; }
        public string KLADOS_LOWERCASE { get; set; }
        public Nullable<int> ΚΛΑΔΟΣ { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ> ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ { get; set; }
    }
}
