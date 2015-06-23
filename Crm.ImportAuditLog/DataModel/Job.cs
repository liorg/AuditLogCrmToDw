using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.DataModel
{
    [Table("tblJob")]
    public class Job
    {
        [Key]
        public Guid JobId { get; set; }
        public DateTime EndDate { get; set; }
        public int CountRecs { get; set; }
    }
}
