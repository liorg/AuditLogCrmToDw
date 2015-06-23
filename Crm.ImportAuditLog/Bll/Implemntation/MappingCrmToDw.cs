using Microsoft.Crm.Sdk.Messages;
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
    //public class Changed
    //{
    //    public string SchemaName { get; set; }
    //    public string OldValue { get; set; }
    //    public string NewValue { get; set; }
    //}
    public class MappingCrmToDw : Imapping
    {

        Action<string> _log;

        public MappingCrmToDw(Action<string> log)
        {
            _log = log;
        }

        public void Map(Microsoft.Crm.Sdk.Messages.AuditDetail source, DataModel.AuditLogModel target)
        {
            var audtLog = source;
            var attr = (AttributeAuditDetail)source;
            Entity record = (Entity)source.AuditRecord;
            var modified = attr.AuditRecord.GetAttributeValue<EntityReference>("userid");
            DisplayAuditDetails(audtLog);
            target.AuditLogId = Guid.NewGuid();
            target.ChangeDateTime = attr.AuditRecord.GetAttributeValue<DateTime>("createdon");

            target.ChangeType = record.FormattedValues["operation"];
            target.CrmAuditId = audtLog.AuditRecord.Id;
            target.EntityType = record.GetAttributeValue<EntityReference>("objectid").LogicalName;
            target.EntityTypeDesc = record.FormattedValues["objecttypecode"]; // record.GetAttributeValue<EntityReference>("objectid").LogicalName;
            target.FieldDesc = "";
            target.FieldSchemaName = "";
            target.ModifiedByID = modified.Id;
            target.NewValue = "";
            target.OldValue = "";
            // target.FieldSchemaName=


        }

        private static void DisplayAuditDetails(AuditDetail detail)
        {
            // Write out some of the change history information in the audit record. 
            Entity record = (Entity)detail.AuditRecord;

            //     Console.WriteLine("\nAudit record created on: {0}", record.CreatedOn.Value.ToLocalTime());
            Console.WriteLine("Entity: {0}, Action: {1}, Operation: {2}",
                record.GetAttributeValue<EntityReference>("objectid").LogicalName, record.FormattedValues["action"],
                record.FormattedValues["operation"]);

            // Show additional details for certain AuditDetail sub-types.
            var detailType = detail.GetType();
            if (detailType == typeof(AttributeAuditDetail))
            {
                var attributeDetail = (AttributeAuditDetail)detail;
                if (attributeDetail.NewValue != null && attributeDetail.NewValue.Attributes.Any())
                {
                    // Display the old and new attribute values.
                    foreach (KeyValuePair<String, object> attribute in attributeDetail.NewValue.Attributes)
                    {
                        String oldValue = "(no value)", newValue = "(no value)";

                        //TODO Display the lookup values of those attributes that do not contain strings.
                        if (attributeDetail.OldValue.Contains(attribute.Key))
                        {
                            if (attributeDetail.OldValue[attribute.Key] is OptionSetValue)
                                oldValue = attributeDetail.OldValue.GetAttributeValue<OptionSetValue>(attribute.Key).Value.ToString();
                            else
                                oldValue = attributeDetail.OldValue[attribute.Key].ToString();
                        }
                        if (attributeDetail.NewValue[attribute.Key] is OptionSetValue)
                            newValue = attributeDetail.NewValue.GetAttributeValue<OptionSetValue>(attribute.Key).Value.ToString();
                        else
                            newValue = attributeDetail.NewValue[attribute.Key].ToString();

                        Console.WriteLine("Attribute: {0}, old value: {1}, new value: {2}",
                            attribute.Key, oldValue, newValue);
                    }
                } 
                // for data who clear (the old value was data and new value was empty)
                if (attributeDetail.OldValue != null && attributeDetail.OldValue.Attributes.Any())
                {
                    foreach (KeyValuePair<String, object> attribute in attributeDetail.OldValue.Attributes)
                    {
                        if (!attributeDetail.NewValue.Contains(attribute.Key))
                        {
                            String newValue = "(no value)";

                            //TODO Display the lookup values of those attributes that do not contain strings.
                            String oldValue = attributeDetail.OldValue[attribute.Key].ToString();

                            Console.WriteLine("Attribute: {0}, old value: {1}, new value: {2}",
                                attribute.Key, oldValue, newValue);
                        }
                    }
                }
            }
            Console.WriteLine();
        }

        public IEnumerable<DataModel.AuditLogModel> ToDw(IOrganizationService service,AuditDetail source)
        {
            List<DataModel.AuditLogModel> items = new List<DataModel.AuditLogModel>();
            var audtLog = source;
            var attr = (AttributeAuditDetail)source;
            Entity record = (Entity)source.AuditRecord;
            var modified = attr.AuditRecord.GetAttributeValue<EntityReference>("userid");
            DisplayAuditDetails(audtLog);
            DataModel.AuditLogModelBase baseEntity = new DataModel.AuditLogModelBase();
           // baseEntity.AuditLogId = Guid.NewGuid();
            baseEntity.ChangeDateTime = attr.AuditRecord.GetAttributeValue<DateTime>("createdon");

            baseEntity.ChangeType = record.FormattedValues["operation"];
            baseEntity.CrmAuditId = audtLog.AuditRecord.Id;
            baseEntity.EntityType = record.GetAttributeValue<EntityReference>("objectid").LogicalName;
            baseEntity.EntityTypeDesc = record.FormattedValues["objecttypecode"]; // record.GetAttributeValue<EntityReference>("objectid").LogicalName;
           // baseEntity.FieldDesc = "";
           // baseEntity.FieldSchemaName = "";
            baseEntity.ModifiedByID = modified.Id;
          //  baseEntity.NewValue = "";
         //   baseEntity.OldValue = "";

            RetrieveEntityRequest retrieveBankAccountEntityRequest = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.Attributes,
                LogicalName = baseEntity.EntityType 
            };
            RetrieveEntityResponse retrieveBankAccountEntityResponse = (RetrieveEntityResponse)service.Execute(retrieveBankAccountEntityRequest);
            foreach (var attEntity in retrieveBankAccountEntityResponse.EntityMetadata.Attributes)
            {
                
            }
            return items;
        }
    }
}
