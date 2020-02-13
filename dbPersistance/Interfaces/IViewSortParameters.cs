using dbPersistance.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.Interfaces
{
    public interface IViewSortParameters
    {
        IList<ISortParameterItem> columnParemeters { get; set; }
        int listablePropertiesCount { get; }
        string dataItemTypeName { get; }
        string getPropertyDisplay(int propertyIndex);
        string getPropertyValue(int propertyIndex);
        sortOrder getPropertySortOrder(int propertyIndex);
        sortOrder defaultSortOrder { get; set; }
        string defaultSortColumn { get; set; }

        int pageNumber { get; set; }
        int takeCount { get; set; }

        void addPropertyParameters(ISortParameterItem _sortParameterItem);
        void reverseSortingOrderForCurrentProperty(string orderBy, sortOrder _sortOrder);

        void updatePageNumber(int number);
        void updateTakeCount(int takeCount);
    }
}
