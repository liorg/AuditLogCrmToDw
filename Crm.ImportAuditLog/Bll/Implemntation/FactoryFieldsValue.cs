using Crm.ImportAuditLog.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public class FactoryFieldsValue
    {
        //public static readonly string BigIntType;
        //public static readonly string BooleanType;
        //public static readonly string CalendarRulesType;//unsopported
        public static readonly string CustomerType="CustomerType";
       public static readonly string DateTimeType="DateTimeType";
        //public static readonly string DecimalType;
        //public static readonly string DoubleType;
        //public static readonly string EntityNameType;
        public static readonly string ImageType="ImageType"; //unsopported
        //public static readonly string IntegerType;
        public static readonly string LookupType = "LookupType";
        //public static readonly string ManagedPropertyType;
        //public static readonly string MemoType;
        public static readonly string MoneyType = "MoneyType";
        public static readonly string OwnerType = "OwnerType";
        //public static readonly string PartyListType;
        public static readonly string PicklistType = "PicklistType";
        public static readonly string StateType = "StateType";
        public static readonly string StatusType = "StatusType";
        //public static readonly string StringType;
        public static readonly string UniqueidentifierType= "UniqueidentifierType";
        //public static readonly string VirtualType;


        Action<string> _log;

        public FactoryFieldsValue(Action<string> log)
        {
            _log = log;
          
        }

        IFieldDesc Get(CrmAttrbite attr,object val)
        {
            return null;
        }
    }
}
