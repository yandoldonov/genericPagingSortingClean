using dbPersistance.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.atributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class pagedListPropertyAttribute : System.Attribute
    {
        readonly string display;
        readonly int position; 
        readonly bool isDefault;    
        readonly sortOrder sortOrder;
        readonly pagedPropertyType pagedPropertyType;

        public pagedListPropertyAttribute(string _display, int _position, bool _isDefault, sortOrder _sortOrder, pagedPropertyType _pagedPropertyType)
        {
            this.display = _display;
            this.position = _position;
            this.isDefault = _isDefault;
            this.sortOrder = _sortOrder;
            this.pagedPropertyType = _pagedPropertyType;
        }

        public string getDisplay()
        {
            return this.display;
        }

        public int getPosition()
        {
            return this.position;
        }

        public bool isThisDefault()
        {
            return this.isDefault;
        } 

        public sortOrder getSortOrder()
        {
            return this.sortOrder;
        }

        public pagedPropertyType getPagedPropertyType()
        {
            return this.pagedPropertyType;
        }
    }
}
