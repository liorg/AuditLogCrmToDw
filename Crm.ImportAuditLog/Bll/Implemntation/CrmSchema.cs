using Crm.ImportAuditLog.DataModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
   
    
    public class CrmSchema
    {
        static Dictionary<string, IEnumerable<CrmAttrbite>> crmMetaData = new Dictionary<string, IEnumerable<CrmAttrbite>>();

        
        public IEnumerable<CrmAttrbite> GetEntityFields(IOrganizationService service, Action<string> log, string entity, int languageCode)
        {
            if (crmMetaData.ContainsKey(entity))
                return crmMetaData[entity];
            List<CrmAttrbite> temp = new List<CrmAttrbite>();
            RetrieveEntityRequest retrieveBankAccountEntityRequest = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.Attributes,
                LogicalName = entity
            };
            RetrieveEntityResponse retrieveBankAccountEntityResponse = (RetrieveEntityResponse)service.Execute(retrieveBankAccountEntityRequest);
            foreach (var attEntity in retrieveBankAccountEntityResponse.EntityMetadata.Attributes)
            {
                CrmAttrbite newAttr = new CrmAttrbite();
                newAttr.AttributeTypeName = attEntity.AttributeTypeName.Value;
                newAttr.FieldName = attEntity.LogicalName;
                newAttr.DisplayName = attEntity.DisplayName.LocalizedLabels.Where(d => d.LanguageCode == languageCode).Select(s => s.Label).FirstOrDefault();
                temp.Add(newAttr);
            }
            crmMetaData.Add(entity, temp);
            return crmMetaData[entity];
        }
    }
}
