using dbPersistance.atributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.extentionHelpers
{
    public static class typeExtentions
    {
        public static bool isDecoratedWithAttributeExcludeChildren(this Type T, Type attributeType)
        {
            if (T.GetCustomAttributes(attributeType, false).FirstOrDefault() != null) return true;
            else return false;
        }

        public static bool isDecoratedWithAttributeIncludeChildren(this Type T, Type attributeType)
        {
            if (T.GetCustomAttributes(attributeType, true).FirstOrDefault() != null) return true;
            else return false;
        }

        public static pagedListPropertyAttribute getPagedListPropertyAttribute(this PropertyInfo T)
        {
            return T.GetCustomAttributes(
                typeof(pagedListPropertyAttribute), true
            ).FirstOrDefault() as pagedListPropertyAttribute;
        }

        public static IEnumerable<PropertyInfo> getPageListProperties(this Type T) 
        {
            return T.GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(pagedListPropertyAttribute)));
        }

        public static int countPageListProperties(this Type T)
        {
            return T.GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(pagedListPropertyAttribute))).Count();
        }

        public static PropertyInfo getDefaultPagedListProperty(this Type T)
        {
            var propertyList = getPageListProperties(T);

            foreach(var prop in propertyList)
            {
                if (getPagedListPropertyAttribute(prop).isThisDefault()) return prop;
            }

            return propertyList.FirstOrDefault();
        }


        // paged list attribute properties

    }
}
