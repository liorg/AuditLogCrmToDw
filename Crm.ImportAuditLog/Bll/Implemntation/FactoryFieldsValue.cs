using Crm.ImportAuditLog.DataModel;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.ImportAuditLog.Bll
{
    public class FactoryFieldsValue
    {
        //const string BigIntType;
        //const string BooleanType;
        //const string CalendarRulesType;//unsopported
        const string CustomerType = "CustomerType";
        const string DateTimeType = "DateTimeType";
        //const string DecimalType;
        //const string DoubleType;
        //const string EntityNameType;
        readonly string ImageType = "ImageType"; //unsopported
        //const static readonly string IntegerType;
        const string LookupType = "LookupType";
        //const string ManagedPropertyType;
        //const string MemoType;
        const string MoneyType = "MoneyType";
        const string OwnerType = "OwnerType";
        //const string PartyListType;
        const string PicklistType = "PicklistType";
        const string StateType = "StateType";
        const string StatusType = "StatusType";
        //const string StringType;
        const string UniqueidentifierType = "UniqueidentifierType";
        //const string VirtualType;


        Action<string> _log;

        public FactoryFieldsValue(Action<string> log)
        {
            _log = log;

        }

        public IFieldDesc GetFieldDesc(string key,  CrmAttrbite attr,Entity entity)
        {
           
            switch (attr.AttributeTypeName)
            {
                case LookupType:
                    return new LoopkupFieldValue(_log);
                case PicklistType:
                case StateType:
                case StatusType:
                    return new PicklistFieldValue(_log);
                case MoneyType:
                    return new PicklistFieldValue(_log);
                default:
                    return new SimpleFieldValue(_log);

            }
        }
    }
}
