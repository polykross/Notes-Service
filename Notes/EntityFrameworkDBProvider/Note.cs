//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Notes.EntityFrameworkDBProvider
{
    using System;
    using System.Collections.Generic;
    
    public partial class Note
    {
        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime LastEditDate { get; set; }
        public System.Guid CustomerId { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
