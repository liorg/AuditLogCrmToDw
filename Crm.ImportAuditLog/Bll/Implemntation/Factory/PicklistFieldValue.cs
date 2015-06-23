using Crm.ImportAuditLog.DataModel;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public class MoneyFieldValue : FieldValueBase
    {
        Action<string> _log;

        public MoneyFieldValue(Action<string> log)
            : base(log)
        {
            _log = log;
        }

        public override CrmValueAttrbite GetValue(string key, CrmAttrbite attr, Entity entity)
        {
            var val = base.GetValue(key, attr, entity);
            if (entity.Contains(key) && entity[key] != null && entity[key] is OptionSetValue)
            {
                val.FieldValue = entity.GetAttributeValue<OptionSetValue>(key).Value.ToString();
            }
            return val;
        }
    }
}
