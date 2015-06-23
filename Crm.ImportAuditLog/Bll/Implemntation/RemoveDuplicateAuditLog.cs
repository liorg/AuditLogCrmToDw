using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public class RemoveDuplicateAuditLog : IRemoveDuplicatesService
    {
        
        Action<string> _log;

        public RemoveDuplicateAuditLog(Action<string> log)
        {
            _log = log;
        }

        public void ExcuteDuplicate()
        {
        }
    }
}
