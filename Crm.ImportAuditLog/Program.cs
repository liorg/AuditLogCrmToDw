using Crm.ImportAuditLog.Bll;
using Crm.ImportAuditLog.Dal;
using Crm.ImportAuditLog.DataModel;
using Crm.ImportAuditLog.Help;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog
{
    class Program
    {
        static void Log(string m)
        {
            Console.WriteLine(m);
            Logger.Write(m);
        }

        private static bool AlreadyRunning()
        {
            Process[] processes = Process.GetProcesses();
            Process currentProc = Process.GetCurrentProcess();

            foreach (Process process in processes)
            {
                try
                {
                    if (process.Modules[0].FileName == System.Reflection.Assembly.GetExecutingAssembly().Location
                                && currentProc.Id != process.Id)
                        return true;
                }
                catch (Exception)
                {

                }
            }

            return false;
        }

        static void Main(string[] args)
        {
            if (AlreadyRunning())
            {
                Console.WriteLine("running already");
                return;
            }

            Log("begin running 1.0.0.0");

            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create());

            var config = new AppConfigConfiguration();
            IOrganizationService service = null;
            try
            {
                service = HelperCrm.CreateOrganizationService(config);
                IDwService dw = new DWService(Log);
                IJobTime job = new JobService(Log);
                IRemoveDuplicatesService removeDup = new RemoveDuplicateAuditLog(Log);
                Imapping mapping = new MappingCrmToDw(Log);

                ICrmAuditLogService crmToDw = new CrmAuditLogService(service, Log);
                crmToDw.RetreiveAndSet(config, job, mapping, dw);


            }
            catch (Exception e)
            {

                Log(e.ToString());
            }

            Log("end running");
        }
    }
}
