using businessLogic.interfaces;
using dbPersistance.enums;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.viewModels
{
    public class genericModelCollection : IListModelCollection
    {
        public genericModelCollection()
        {
            items = new List<ICollectionItem>();
            filterFormModel = new filterFormModel();
        }

        public genericModelCollection(string _controller, string _controllerAction)
        {
            items = new List<ICollectionItem>();
            filterFormModel = new filterFormModel(_controller, _controllerAction);
            this.controller = _controller;
            this.controllerAction = _controllerAction;
        }

        public int listablePropertiesCount { get; set; }
        public IViewSortParameters viewSortParams { get; set; }
        public IFilterDataFormModel filterFormModel { get; set; }

        public IList<ICollectionItem> items { get; set; }

        public int currentPage { get; set; }
        public int currentViewCount { get; set; }
        public int totalPages { get; set; }
        public int totalItemCount { get; set; }

        public sortOrder currentSortOrder { get; set; }
        public string currentSort { get; set; }
        public string queryString { get; set; }
        public string selectedProperty { get; set; }
        public queryOptions queryOptions { get; set; }

        public string controller { get; set; }
        public string controllerAction { get; set; }

        public int previousPage
        {
            get
            {
                if (totalPages > 2)
                {
                    if (currentPage == 1) return currentPage;
                    else return this.currentPage - 1;
                }
                else return currentPage;
            }
        }

        public int nextPage
        {
            get
            {
                if (totalPages > 2)
                {
                    if (currentPage == totalPages) return currentPage;
                    else return this.currentPage + 1;
                }
                else return currentPage;
            }
        }

        public string currentSortColumn { get; set; }

        public void updateSortingOrder(string orderBy, sortOrder _sortOrder)
        {
            viewSortParams.defaultSortOrder = _sortOrder;
            viewSortParams.defaultSortColumn = orderBy;
        }
    }
}
