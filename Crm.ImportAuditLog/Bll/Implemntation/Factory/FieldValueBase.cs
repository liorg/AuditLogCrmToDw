using Crm.ImportAuditLog.DataModel;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public class FieldValueBase : IFieldDesc
    {
       protected Action<string> _log;

        public FieldValueBase(Action<string> log)
        {
            _log = log;
        }

        public virtual CrmValueAttrbite GetValue(string key, CrmAttrbite attr, Entity entity)
        {
            return new CrmValueAttrbite { AttributeTypeName = attr.AttributeTypeName, DisplayName = attr.DisplayName, FieldName = attr.FieldName ,FieldValue=""};

        }
    }
}
