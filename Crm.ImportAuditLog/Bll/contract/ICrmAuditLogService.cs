using Crm.ImportAuditLog.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
   public interface ICrmAuditLogService
    {

       void RetreiveAndSet(IConfiguration config,IJobTime job, Imapping mapping, IDwService dw);
    }
}
