using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace  Crm.ImportAuditLog.Help
{
    public static class ConstVar
    {
        public const string FinishFolder = "finish";
        public const string MoveFolder = "move";
        //public const string ExstenstionExcel = ".xlsx";
    }
    public static class HelperCrm
    {
        public static IOrganizationService CreateOrganizationService(IConfiguration config)
        {
            var organizationServiceUrl = (string)config.Get("OrganizationServiceURL");
            var useDefaultCredentials = (string)config.Get("UseDefaultCredentials");
            var credentials = new ClientCredentials();

            if (!String.Equals(useDefaultCredentials, Boolean.TrueString, StringComparison.InvariantCultureIgnoreCase))
            {
                var userName = (string)config.Get("UserName");
                var password = (string)config.Get("Password");
                var domain = (string)config.Get("Domain");

                credentials.Windows.ClientCredential = new System.Net.NetworkCredential(userName, password, domain);
            }

            var proxy = new OrganizationServiceProxy(new Uri(organizationServiceUrl), null, credentials, null);
            proxy.EnableProxyTypes();
            return proxy;
        }

        public static Dictionary<string, int> GetOptionsSet(IOrganizationService service, string entitySchemaName, string attributeSchemaName)
        {
            RetrieveAttributeRequest retrieveAttributeRequest = new RetrieveAttributeRequest
            {
                EntityLogicalName = entitySchemaName,
                LogicalName = attributeSchemaName,
                RetrieveAsIfPublished = true
            };
            RetrieveAttributeResponse retrieveAttributeResponse = (RetrieveAttributeResponse)service.Execute(retrieveAttributeRequest);
            PicklistAttributeMetadata retrievedPicklistAttributeMetadata = (PicklistAttributeMetadata)retrieveAttributeResponse.AttributeMetadata;
            OptionMetadata[] optionList = retrievedPicklistAttributeMetadata.OptionSet.Options.ToArray();
            Dictionary<string, int> metadata = new Dictionary<string, int>();


            if (optionList.Length > 0)
            {
                foreach (var p in optionList)
                {
                    if (!metadata.ContainsKey(p.Label.UserLocalizedLabel.Label))
                        metadata.Add(p.Label.UserLocalizedLabel.Label, p.Value.Value);
                }
            }
            return metadata;
        }

        public static string GetOptionsSetTextOnValue(IOrganizationService service, string entitySchemaName, string attributeSchemaName, int optionsetValue)
        {
            RetrieveAttributeRequest retrieveAttributeRequest = new RetrieveAttributeRequest
            {
                EntityLogicalName = entitySchemaName,
                LogicalName = attributeSchemaName,
                RetrieveAsIfPublished = true
            };
            RetrieveAttributeResponse retrieveAttributeResponse = (RetrieveAttributeResponse)service.Execute(retrieveAttributeRequest);
            PicklistAttributeMetadata retrievedPicklistAttributeMetadata = (PicklistAttributeMetadata)retrieveAttributeResponse.AttributeMetadata;
            OptionMetadata[] optionList = retrievedPicklistAttributeMetadata.OptionSet.Options.ToArray();
            string metadata = string.Empty;
            if (optionList.Length > 0)
            {
                metadata = (from a in optionList
                            where a.Value == optionsetValue
                            select a.Label.UserLocalizedLabel.Label).First();
            }
            return metadata;
        }


    }
}
