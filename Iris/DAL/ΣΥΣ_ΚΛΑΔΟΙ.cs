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
    
    public partial class ΣΥΣ_ΚΛΑΔΟΙ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ΣΥΣ_ΚΛΑΔΟΙ()
        {
            this.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ = new HashSet<ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ>();
        }
    
        public int ΚΛΑΔΟΣ_ΚΩΔ { get; set; }
        public string ΚΛΑΔΟΣ { get; set; }
        public Nullable<int> ΩΡΑΡΙΟ { get; set; }
        public Nullable<decimal> ΜΙΣΘΟΣ { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ> ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ { get; set; }
    }
}
