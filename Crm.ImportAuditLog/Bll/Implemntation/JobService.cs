using Crm.ImportAuditLog.Dal;
using Crm.ImportAuditLog.DataModel;
using Crm.ImportAuditLog.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public class JobService : IJobTime
    {
        Action<string> _log;

        public JobService(Action<string> log)
        {
            _log = log;
        }

        public DateTime RetrieveLastDateJob(IConfiguration config)
        {
            DateTime dt = DateTime.MinValue;
            _log("retrieve last date");
            var roundEndDateMinutesValue = int.Parse(config.Get("RoundEndDateMintuesValue").ToString());
            var lastXMonth = int.Parse(config.Get("LastXMonth").ToString());

            using (DWAuditLog context = new DWAuditLog())
            {
                var dateLast = context.Jobs.Take(1).OrderByDescending(j => j.EndDate).FirstOrDefault();
                if (dateLast == null)
                {
                    dt = DateTime.Now.AddMonths(lastXMonth);
                    _log("dateLast is null then" + dt.ToString("yyyyMMdd hh:mm:ss"));
                }
                else
                {
                    dt = dateLast.EndDate;
                }
                roundEndDateMinutesValue = roundEndDateMinutesValue * -1;
                dt = dt.AddMinutes(roundEndDateMinutesValue);
                _log(" last date on db is " + dt.ToString("yyyyMMdd hh:mm:ss"));


            }
            _log("end last date" + dt.ToString("yyyyMMdd hh:mm:ss"));
            return dt;

        }

        public void UpdateEndDateOnComplete(int count, DateTime endDate)
        {
            _log("UpdateEndDateOnComplete" +count.ToString()+" enddate" +endDate.ToString("yyyyMMdd hh:mm:ss"));

            using (DWAuditLog context = new DWAuditLog())
            {
                Job job = new Job();
                job.JobId = Guid.NewGuid();
                job.CountRecs = count;
                job.EndDate = endDate;
                context.Jobs.Add(job);
                context.SaveChanges();
            }
            _log("End UpdateEndDateOnComplete");


        }
    }
}
