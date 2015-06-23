using Crm.ImportAuditLog.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public class DWService : IDwService
    {
        Action<string> _log;

        public DWService(Action<string> log)
        {
            _log = log;
        }

        public void BulkAdd(IEnumerable<DataModel.AuditLogModel> entitiesLog)
        {
            _log("begin bulkadd" + entitiesLog.Count());
            using (DWAuditLog context = new DWAuditLog())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;
                context.Audits.AddRange(entitiesLog);
                context.SaveChanges();
            }
            _log("end bulkadd" + entitiesLog.Count());
        }

    }
}

//DWAuditLog context = new DWAuditLog();
//context.Configuration.AutoDetectChangesEnabled = false;
//context.Configuration.ValidateOnSaveEnabled = false;
//int i = 0;
//List<AuditLogModel> items = new List<AuditLogModel>();
//while (i < 5000)
//{
//    AuditLogModel aa = new AuditLogModel();
//    aa.AuditLogId = Guid.NewGuid();
//    aa.ChangeDateTime = DateTime.Now;
//    aa.ModifiedByName = "";
//    aa.ChangeType = "s";

//    aa.CrmAuditId = Guid.NewGuid();
//    aa.EntityType = "s";
//    aa.EntityTypeDesc = "d";

//    aa.FieldDesc = "s";
//    aa.FieldSchemaName = "s";
//    aa.ModifiedByID = Guid.NewGuid();

//    aa.NewValue = "s";
//    aa.OldValue = "x";
//    i++;
//    items.Add(aa);

//}

//context.Audits.AddRange(items);
//context.SaveChanges();
