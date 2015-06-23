using Crm.ImportAuditLog.DataModel;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public class PartyListFieldValue : FieldValueBase
    {

        public PartyListFieldValue(Action<string> log)
            : base(log)
        {
         
        }

        public override CrmValueAttrbite GetValue(string key, Entity entity)
        {
            var val = base.GetValue(key, entity);
            if (entity.Contains(key) && entity[key] != null && entity[key] is EntityCollection)
            {
                EntityCollection ents = entity[key] as EntityCollection;
                if (ents != null && ents.Entities != null && ents.Entities.Any())
                {
                    var sb = new StringBuilder();
                    foreach (var item in  ents.Entities)
                    {
                        sb.AppendFormat("{0}", item.Id);
                    }
                    val.FieldValue = sb.ToString();
                }
                //val.FieldValue = entity.GetAttributeValue<DateTime>(key).ToString("yyyy-MM-dd hh:mm:ss");
            }
            return val;
        }
    }
}
