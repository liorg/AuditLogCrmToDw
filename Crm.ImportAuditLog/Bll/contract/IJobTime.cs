using Crm.ImportAuditLog.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public interface IJobTime
    {
        Guid JobId { get; }
        DateTime RetrieveLastDateJob(IConfiguration config);

        void UpdateEndDateOnComplete(int fieldsChangeCount, int countCrm,  int? countCountDups ,DateTime endDate);
    }
}
