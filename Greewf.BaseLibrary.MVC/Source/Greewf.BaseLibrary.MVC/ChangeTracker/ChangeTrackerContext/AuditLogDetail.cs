//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Greewf.BaseLibrary.MVC.ChangeTracker.ChangeTrackerContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuditLogDetail
    {
        public System.Guid Id { get; set; }
        public System.Guid AuditLogId { get; set; }
        public string PropertyName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
    
        public virtual AuditLog AuditLog { get; set; }
    }
}
