using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace Crm.ImportAuditLog.Help
{
    public interface IConfiguration
    {
        object Get(string name);
    }
}
