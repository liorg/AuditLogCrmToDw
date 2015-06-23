using Crm.ImportAuditLog.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public interface IDwService
    {
        void BulkAdd(IEnumerable<AuditLogModel> entitiesLog);

        int RemoveDuplicate();
    }
}
