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
            _log("begin bulkadd " + entitiesLog.Count());
            using (DWAuditLog context = new DWAuditLog())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;
                context.Audits.AddRange(entitiesLog);
                context.SaveChanges();
            }
            _log("end bulkadd " + entitiesLog.Count());
        }
        
        public int RemoveDuplicate()
        {
            _log("begin RemoveDuplicate ");
            using (DWAuditLog context = new DWAuditLog())
            {
                int countsDelsDup = context.RemoveDuplicate();
                _log("end RemoveDuplicate " + countsDelsDup.ToString());
                return countsDelsDup;
            }
        }

    }
}
