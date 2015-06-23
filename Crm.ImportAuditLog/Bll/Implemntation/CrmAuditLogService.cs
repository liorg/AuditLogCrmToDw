using Crm.ImportAuditLog.DataModel;
using Crm.ImportAuditLog.Help;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public class CrmAuditLogService : ICrmAuditLogService
    {
        Action<string> _log; IOrganizationService _service;

        public CrmAuditLogService(IOrganizationService service, Action<string> log)
        {
            _log = log; _service = service;
        }


        public void RetreiveAndSet(IConfiguration config, IJobTime job, Imapping mapping, IDwService dw)
        {
            //   var month=config.Get("LastXMonth").ToString();
            var maxRecordsPerExcution = int.Parse(config.Get("MaxRecordsPerExcution").ToString());
            var languageCode = int.Parse(config.Get("LanguageCode").ToString());

            int pageNumber = 1;int gCount=0;
          
            List<AuditLogModel> audtLogs;
            bool moreRecords = true;

            var first = job.RetrieveLastDateJob(config);
            var last = DateTime.Now;

            do
            {
                audtLogs = new List<AuditLogModel>();
                audtLogs.Clear();
                var rmr = new RetrieveMultipleRequest();
                var resp = new RetrieveMultipleResponse();
              
                QueryExpression query = new QueryExpression()
                {
                    EntityName = "audit",
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression
                    {
                        FilterOperator = LogicalOperator.And,
                        Conditions = {
                              new ConditionExpression
                              {
                                  AttributeName = "createdon",
                                  Operator = ConditionOperator.Between,
                                  Values = { first,last }  //access
                              }
                          },
                    },
                    Orders = {
                                new OrderExpression
                                {
                                    AttributeName = "createdon",
                                    OrderType = OrderType.Descending// must change asc
                                }
                            }
                };

                query.PageInfo = new PagingInfo
                {
                    PageNumber = pageNumber,
                    Count = maxRecordsPerExcution
                };
                rmr.Query = query;
                resp = (RetrieveMultipleResponse)_service.Execute(rmr);
                foreach (Entity entiyAuditLog in resp.EntityCollection.Entities)
                {
                    gCount++;
                    Console.WriteLine(entiyAuditLog.Id);
                    // Retrieve the audit details and display them.
                    var auditDetailsRequest = new RetrieveAuditDetailsRequest
                    {
                        AuditId = entiyAuditLog.Id
                    };

                    var auditDetailsResponse = (RetrieveAuditDetailsResponse)_service.Execute(auditDetailsRequest);
                   // var audtLog = auditDetailsResponse.AuditDetail;
                   // var xx = (AttributeAuditDetail)auditDetailsResponse.AuditDetail;
                    //Debug.WriteLine(xx.NewValue.Attributes);
                   // AuditLogModel auditLogDw = new AuditLogModel();
               //     mapping.Map(auditDetailsResponse.AuditDetail, auditLogDw);
                    var changes = mapping.ToDwItems(_service, auditDetailsResponse.AuditDetail, languageCode);
                    audtLogs.AddRange(changes);

                }
                moreRecords = resp.EntityCollection.MoreRecords;
                if (moreRecords)
                {
                    pageNumber++;
                }
                dw.BulkAdd(audtLogs);

            }
            while (moreRecords);
            job.UpdateEndDateOnComplete(gCount, last);

        }
    }
}
