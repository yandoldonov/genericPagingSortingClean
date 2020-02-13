using dbPersistance.enums;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.interfaces
{
    public interface IListModelCollection
    {
        int listablePropertiesCount { get; }
        IFilterDataFormModel filterFormModel { get; }
        IList<ICollectionItem> items { get; }
        int currentPage { get; set; }
        int currentViewCount { get; set; }
        int totalPages { get; set; }
        int totalItemCount { get; set; }

        IViewSortParameters viewSortParams { get; set; }
        sortOrder currentSortOrder { get; set; }
        string currentSortColumn { get; set; }

        #region query properties
        string queryString { get; set; }
        string selectedProperty { get; set; }
        queryOptions queryOptions { get; set; }
        #endregion

        int previousPage { get; }
        int nextPage { get; }

        void updateSortingOrder(string orderBy, sortOrder _sortOrder);

        string controller { get; set; }
        string controllerAction { get; set; }
    }
}
