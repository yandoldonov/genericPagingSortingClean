using dbPersistance.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.extentionHelpers
{
    public static class enumHelpers
    {
        public static queryOptions getfromString(string selectedQuetyOption)
        {
            switch (selectedQuetyOption)
            {
                case "equals":
                    return queryOptions.equals;
                case "notEquals":
                    return queryOptions.notEquals;
                case "contains":
                    return queryOptions.contains;
                case "lessThan":
                    return queryOptions.lessThan;
                case "largerThan":
                    return queryOptions.largerThan;
                case "excludes":
                    return queryOptions.excludes;
                case "isTrue":
                    return queryOptions.isTrue;
                case "isFalse":
                    return queryOptions.isFalse;
                default:
                    return queryOptions.none;
            }
        }
    }
}
