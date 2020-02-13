using dbPersistance.enums;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.helperModels
{
    public class sortParameters : IViewSortParameters
    {
        public IList<ISortParameterItem> columnParemeters { get; set; }
        public int listablePropertiesCount { get { return columnParemeters.Count(); } }
        public string dataItemTypeName { get; set; }
        public sortOrder defaultSortOrder { get; set; }
        public string defaultSortColumn { get; set; }

        public sortParameters()
        {
            columnParemeters = new List<ISortParameterItem>();
        }

        public string getPropertyDisplay(int propertyIndex)
        {
            if (columnParemeters.Count() == 0) return string.Empty;
            if (propertyIndex < 1 || propertyIndex > columnParemeters.Count()) return string.Empty;

            return this.columnParemeters[propertyIndex - 1].colDisplay;
        }

        public string getPropertyValue(int propertyIndex)
        {
            if (columnParemeters.Count() == 0) return string.Empty;
            if (propertyIndex < 1 || propertyIndex > columnParemeters.Count()) return string.Empty;

            return this.columnParemeters[propertyIndex - 1].colValue;
        }

        public sortOrder getPropertySortOrder(int propertyIndex)
        {
            if (columnParemeters.Count() == 0) return sortOrder.ASC;
            if (propertyIndex < 1 || propertyIndex > columnParemeters.Count()) return sortOrder.ASC;

            return this.columnParemeters[propertyIndex - 1].sortOrder;
        }

        public void addPropertyParameters(ISortParameterItem _sortParameterItem)
        {
            if (columnParemeters == null) columnParemeters = new List<ISortParameterItem>();
            columnParemeters.Add(_sortParameterItem);
        }

        public void reverseSortingOrderForCurrentProperty(string orderBy, sortOrder _sortOrder)
        {
            if (this.columnParemeters != null)
            {
                var entry = this.columnParemeters.Where(x => x.colValue == orderBy).FirstOrDefault();
                if (entry != null) entry.sortOrder = reverseOrder(_sortOrder);
            }
        }

        sortOrder reverseOrder(sortOrder _sortOrder)
        {
            if (_sortOrder == sortOrder.ASC) return sortOrder.DSC;
            else return sortOrder.ASC;
        }

        public int pageNumber { get; set; }
        public int takeCount { get; set; }

        public void updatePageNumber(int number)
        {
            this.pageNumber = number;
        }
        public void updateTakeCount(int _takeCount)
        {
            this.takeCount = _takeCount;
        }
    }
}
