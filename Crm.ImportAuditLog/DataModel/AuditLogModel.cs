using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.DataModel
{
    public class AuditLogModelBase :IAuditLogModel
    {
        public DateTime ChangeDateTime { get; set; }
        public string ModifiedByName { get; set; }
        public Guid ModifiedByID { get; set; }
        public string ChangeType { get; set; }
        public string EntityType { get; set; }
        public string EntityTypeDesc { get; set; }
        public Guid CrmAuditId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid JobId { get; set; }

        public void CopyTo(IAuditLogModel model)
        {
            model.ModifiedByID = this.ModifiedByID;
            model.ChangeDateTime = this.ChangeDateTime;
            model.ModifiedByName = this.ModifiedByName;

            model.ChangeType = this.ChangeType;
            model.EntityType = this.EntityType;
            model.EntityTypeDesc = this.EntityTypeDesc;

            model.CrmAuditId = this.CrmAuditId;

            model.ModifiedOn = this.ModifiedOn;
            model.JobId = this.JobId;
        }

    }
    [Table("tblAuditLog")]
    public class AuditLogModel : IAuditLogModel
    {
        [Key]
        public Guid AuditLogId { get; set; }
        public string FieldSchemaName { get; set; }
        public string FieldDesc { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangeDateTime { get; set; }
        public string ModifiedByName { get; set; }
        public Guid ModifiedByID { get; set; }
        public string ChangeType { get; set; }
        public string EntityType { get; set; }
        public string EntityTypeDesc { get; set; }
        public Guid CrmAuditId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid JobId { get; set; }
    }
}
