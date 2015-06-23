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
        DateTime RetrieveLastDateJob(IConfiguration config);

        void UpdateEndDateOnComplete(int count, DateTime endDate);
    }
}
