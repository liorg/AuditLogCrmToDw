using Crm.ImportAuditLog.DataModel;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public class LoopkupFieldValue : IFieldDesc
    {
        Action<string> _log;

        public LoopkupFieldValue(Action<string> log)
        {
            _log = log;
        }

        public CrmValueAttrbite GetValue(string key,Entity  entity)
        {
            return null;
        }
    }
}
