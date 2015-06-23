
using Crm.ImportAuditLog.DataModel;
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

    public class MappingCrmToDw : Imapping
    {

        Action<string> _log;
        readonly CrmSchema _schema;
        readonly FactoryFieldsValue _factoryFieldsValue;
        public MappingCrmToDw(Action<string> log)
        {
            _log = log;
            _factoryFieldsValue = new FactoryFieldsValue(_log);
            _schema = new CrmSchema();
        }

        public IEnumerable<DataModel.AuditLogModel> ToDwItems(IOrganizationService service, AuditDetail source, int langCode, IJobTime job)
        {
            List<DataModel.AuditLogModel> items = new List<DataModel.AuditLogModel>();
            var audtLog = source;

            if (source.GetType() != typeof(AttributeAuditDetail))
                return items;

            var attr = (AttributeAuditDetail)source;
            Entity record = (Entity)source.AuditRecord;
            AuditLogModelBase baseEntity = new AuditLogModelBase();
            var modified = attr.AuditRecord.GetAttributeValue<EntityReference>("userid");
            baseEntity.EntityType = record.GetAttributeValue<EntityReference>("objectid").LogicalName;
            baseEntity.EntityTypeDesc = record.FormattedValues["objecttypecode"]; 
            baseEntity.ChangeDateTime = attr.AuditRecord.GetAttributeValue<DateTime>("createdon");
            baseEntity.ChangeType = record.FormattedValues["operation"];
            baseEntity.CrmAuditId = audtLog.AuditRecord.Id;
            baseEntity.ModifiedByName = modified.Name;
            baseEntity.ModifiedByID = modified.Id;
            baseEntity.JobId = job.JobId;
            baseEntity.ModifiedOn = DateTime.UtcNow;
            AddToListItems(service, baseEntity.EntityType, langCode, source, items, baseEntity);
            return items;
        }

        private void AddToListItems(IOrganizationService service, string entityName, int langCode, AuditDetail detail, List<DataModel.AuditLogModel> items, AuditLogModelBase auditLogModelBase)
        {
            var crmAttrbites = _schema.GetEntityFields(service, _log, entityName, langCode);
            AuditLogModel auditLogModel;
            IFieldDesc fieldDesc = null;
            CrmAttrbite attr = null;
            CrmValueAttrbite crmValueAttrbite = null;
            Entity record = (Entity)detail.AuditRecord;
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
                        fieldDesc = null;
                        crmValueAttrbite = null;
                        attr = crmAttrbites.Where(a => a.FieldName == attribute.Key).FirstOrDefault();
                        if (attr == null)
                            continue;
                        auditLogModel = new AuditLogModel();
                        auditLogModel.AuditLogId = Guid.NewGuid();
                        auditLogModelBase.CopyTo(auditLogModel);
                        auditLogModel.FieldDesc = attr.DisplayName;
                        auditLogModel.FieldSchemaName = attr.FieldName;
                        if (attributeDetail.OldValue.Contains(attribute.Key))
                        {
                            fieldDesc = _factoryFieldsValue.GetFieldDesc(attribute.Key, attr, attributeDetail.OldValue);
                            if (fieldDesc != null)
                            {
                                crmValueAttrbite = fieldDesc.GetValue(attribute.Key.ToString(), attributeDetail.OldValue);
                                auditLogModel.OldValue = crmValueAttrbite.FieldValue;
                            }
                        }
                        fieldDesc = _factoryFieldsValue.GetFieldDesc(attribute.Key, attr, attributeDetail.NewValue);
                        if (fieldDesc != null)
                        {
                            crmValueAttrbite = fieldDesc.GetValue(attribute.Key.ToString(), attributeDetail.NewValue);
                            auditLogModel.NewValue = crmValueAttrbite.FieldValue;
                        }
                        items.Add(auditLogModel);
                    }
                }
                // for data who clear (the old value was data and new value was empty)
                if (attributeDetail.OldValue != null && attributeDetail.OldValue.Attributes.Any())
                {
                    foreach (KeyValuePair<String, object> attribute in attributeDetail.OldValue.Attributes)
                    {
                        if (attributeDetail.NewValue != null && !attributeDetail.NewValue.Contains(attribute.Key))
                        {
                            fieldDesc = null;
                            crmValueAttrbite = null;
                            attr = crmAttrbites.Where(a => a.FieldName == attribute.Key).FirstOrDefault();
                            auditLogModel = new AuditLogModel();
                            auditLogModel.AuditLogId = Guid.NewGuid();
                            auditLogModelBase.CopyTo(auditLogModel);
                            auditLogModel.FieldDesc = attr.DisplayName;
                            auditLogModel.FieldSchemaName = attr.FieldName;

                            fieldDesc = _factoryFieldsValue.GetFieldDesc(attribute.Key, attr, attributeDetail.OldValue);
                            if (fieldDesc != null)
                            {
                                crmValueAttrbite = fieldDesc.GetValue(attribute.Key.ToString(), attributeDetail.OldValue);
                                auditLogModel.OldValue = crmValueAttrbite.FieldValue;
                                auditLogModel.NewValue = "";
                            }
                            items.Add(auditLogModel);
                        }
                    }
                }
            }
        }

    }
}
