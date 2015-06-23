using Crm.ImportAuditLog.DataModel;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public class SimpleFieldValue : FieldValueBase
    {
        

        public SimpleFieldValue(Action<string> log)
            : base(log)
        {
           
        }

        public override CrmValueAttrbite GetValue(string key, CrmAttrbite attr, Entity entity)
        {
            var val = base.GetValue(key, attr, entity);
            if (entity.Contains(key) && entity[key] != null)
            {
                val.FieldValue = entity[key].ToString();
            }
            return val;
        }
    }
}
