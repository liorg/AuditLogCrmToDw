using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.DataModel
{
    public class AuditLogModelBase
    {
        public DateTime ChangeDateTime { get; set; }
        public string ModifiedByName { get; set; }

        public Guid ModifiedByID { get; set; }
        public string ChangeType { get; set; }
        public string EntityType { get; set; }
        public string EntityTypeDesc { get; set; }

        public Guid CrmAuditId { get; set; }

    }
    [Table("tblAuditLog")]
    public class AuditLogModel : AuditLogModelBase
    {
        [Key]
        public Guid AuditLogId { get; set; }
      
        public string FieldSchemaName { get; set; }
        public string FieldDesc { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        



    }
}
