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
    
    public partial class USER_ADMINS
    {
        public int USER_ID { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string FULLNAME { get; set; }
        public Nullable<bool> ISACTIVE { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<int> ADMIN_LEVEL { get; set; }
    }
}
