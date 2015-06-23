using System;
namespace Crm.ImportAuditLog.DataModel
{
    public interface IAuditLogModel
    {
        DateTime ChangeDateTime { get; set; }
        string ChangeType { get; set; }
        Guid CrmAuditId { get; set; }
        string EntityType { get; set; }
        string EntityTypeDesc { get; set; }
        Guid ModifiedByID { get; set; }
        string ModifiedByName { get; set; }
        DateTime ModifiedOn { get; set; }
        Guid JobId { get; set; }
    }
}
