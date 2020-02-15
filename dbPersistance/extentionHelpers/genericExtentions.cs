using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.extentionHelpers
{
    public static class genericExtentions<TDataItem> where TDataItem : class, IPocoEntity
    {
        public static bool isThePropertyOfStandardType(string propertyName)
        {
            var prop = typeExtentions.getPageListProperties(typeof(TDataItem)).Where(x => x.Name == propertyName).FirstOrDefault();

            if (prop != null)
            {
                if (prop.PropertyType == typeof(string)
                    || prop.PropertyType == typeof(int)
                    || prop.PropertyType == typeof(decimal)
                    || prop.PropertyType == typeof(bool)
                    || prop.PropertyType == typeof(DateTime)
                    || prop.PropertyType == typeof(DateTime?)) return true;
            }

            return false;
        }
    }
}
