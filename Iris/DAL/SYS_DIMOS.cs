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
    
    public partial class SYS_DIMOS
    {
        public int DIMOS_ID { get; set; }
        public string DIMOS { get; set; }
        public Nullable<int> DIMOS_PERIFERIA { get; set; }
    
        public virtual SYS_PERIFERIES SYS_PERIFERIES { get; set; }
    }
}
