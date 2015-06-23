using Crm.ImportAuditLog.DataModel;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public interface Imapping
    {
        IEnumerable<AuditLogModel> ToDwItems(IOrganizationService service,AuditDetail source,int langCode,IJobTime job);


    }
}
