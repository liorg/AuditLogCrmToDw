using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Help
{
    public class AppConfigConfiguration : IConfiguration
    {
        public object Get(string name)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(name))
            {
                return ConfigurationManager.AppSettings[name];
            }

            return null;
        }
    }
}
