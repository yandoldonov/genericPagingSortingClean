using businessLogic.interfaces;
using businessLogic.viewModels;
using dbPersistance.enums;
using dbPersistance.extentionHelpers;
using dbPersistance.Interfaces;
using dbPersistance.uOw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.viewModelFactories
{
    public class genericListModelFactory<TDataItem, TListModel, TListModelItem> : IDisposable, IListModelFactory<TDataItem, TListModel> where TDataItem : class, IPocoEntity where TListModel : class, IListModelCollection where TListModelItem : class, ICollectionItem
    {
        IUnitOfWork<TDataItem> unit;
        public genericListModelFactory(IUnitOfWork<TDataItem> _unit)
        {
            unit = _unit;
        }

        public genericListModelFactory()
        {
            unit = new unitOfWork<TDataItem>();
        }       

        public IListModelCollection getDataList(string _controller, string _controllerAction, int takeCount, int pageNumber)
        {
            IListModelCollection _collectionModel = new genericModelCollection(_controller, _controllerAction);

            // build list of sortable properties - these will be used for column headings
            _collectionModel.viewSortParams = pagedListExtentionHelpers.getListSortParemeters(typeof(TDataItem));

            // determine default sort column and sort order
            _collectionModel.currentSortOrder = _collectionModel.viewSortParams.defaultSortOrder;
            _collectionModel.currentSortColumn = _collectionModel.viewSortParams.defaultSortColumn;

            // construct form model for set page and set take-count forms
            _collectionModel.filterFormModel.setFormInitialParameters(typeof(TDataItem));

            // set default view parameters
            _collectionModel.currentViewCount = takeCount;
            _collectionModel.totalItemCount = unit.repository.Count();
            _collectionModel.currentPage = 1;

            // calculate total pages count
            if (_collectionModel.totalItemCount > _collectionModel.currentViewCount)
            {
                if (_collectionModel.totalItemCount % _collectionModel.currentViewCount > 0)
                {
                    _collectionModel.totalPages = _collectionModel.totalItemCount / _collectionModel.currentViewCount + 1;
                }
                else
                {
                    _collectionModel.totalPages = _collectionModel.totalItemCount / _collectionModel.currentViewCount;
                }
            }
            else
            {
                _collectionModel.totalPages = 1;
            }

            // calculate skip parameter for database query build
            if (pageNumber > _collectionModel.totalPages) pageNumber = _collectionModel.totalPages;
            int thisSkip = 0;
            if (_collectionModel.totalItemCount > takeCount && pageNumber > 1)
            {
                thisSkip = takeCount * pageNumber;
            }

            // get data from database
            var dataCollection = unit.repository.GetChunksOf(thisSkip, takeCount);

            MethodInfo method = typeof(TListModelItem).GetMethod("buildFromDbInstance");
            foreach (var item in dataCollection) _collectionModel.items.Add(method.Invoke(null, new object[] { item }) as ICollectionItem);


            return _collectionModel;
        }

        public IListModelCollection getDataList(string _controller, string _controllerAction, string orderBy, sortOrder _sortOrder, int takeCount, int pageNumber)
        {
            IListModelCollection _collectionModel = new genericModelCollection
            {
                controller = _controller
            };

            // build list of sortable properties - these will be used for column headings
            _collectionModel.viewSortParams = pagedListExtentionHelpers.getListSortParemeters(typeof(TDataItem));

            // determine default sort column and sort order
            _collectionModel.currentSortOrder = _sortOrder;
            _collectionModel.currentSortColumn = orderBy;

            #region prepare parameters

            // reverse sort order for current property
            _collectionModel.viewSortParams.reverseSortingOrderForCurrentProperty(orderBy, _sortOrder);

            // update default page count
            _collectionModel.currentViewCount = takeCount;
            _collectionModel.viewSortParams.updateTakeCount(takeCount);

            // set current takeCount for the form model
            _collectionModel.filterFormModel
                .updateParameters
                (_controller,
                _controllerAction, takeCount, pageNumber, _sortOrder, orderBy, string.Empty, string.Empty, string.Empty);

            // get total amount of item in the database
            _collectionModel.totalItemCount = unit.repository.Count();

            // update sorting order and default column for the model 
            _collectionModel.updateSortingOrder(orderBy, _sortOrder);

            // calculate total pages
            if (_collectionModel.totalItemCount > _collectionModel.currentViewCount)
            {
                if (_collectionModel.totalItemCount % _collectionModel.currentViewCount > 0)
                {
                    _collectionModel.totalPages = _collectionModel.totalItemCount / _collectionModel.currentViewCount + 1;
                }
                else
                {
                    _collectionModel.totalPages = _collectionModel.totalItemCount / _collectionModel.currentViewCount;
                }
            }
            else
            {
                _collectionModel.totalPages = 1;
            }

            // calculate skip
            if (pageNumber > _collectionModel.totalPages) pageNumber = _collectionModel.totalPages;
            int thisSkip = 0;
            if (_collectionModel.totalItemCount > takeCount && pageNumber > 1)
            {
                thisSkip = takeCount * pageNumber;
            }
            #endregion

            IQueryable<TDataItem> dataCollection;
    
            filterType sortType = checkPropertyType(typeof(TDataItem), orderBy);

            //switch (sortType)
            //{
            //    case filterType.STRINGVALUE:

            //        MethodInfo stringSortOrderMethod = typeof(TDataItem).GetMethod("getStringSortOrder");
            //        dataCollection = unit.repository.GetChunksOfWithStringOrderBy(thisSkip, takeCount, _sortOrder, stringSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, string>>);
            //        break;
            //    case filterType.BOOLVALUE:
            //        MethodInfo boolSortOrderMethod = typeof(TDataItem).GetMethod("getBoolSortOrder");
            //        dataCollection = unit.repository.GetChunksOfWithBoolOrderBy(thisSkip, takeCount, _sortOrder, boolSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, bool>>);
            //        break;
            //    case filterType.DECIMALVALUE:
            //        MethodInfo decimalSortOrderMethod = typeof(TDataItem).GetMethod("getDecimalSortOrder");
            //        dataCollection = unit.repository.GetChunksOfWithDecimalOrderBy(thisSkip, takeCount, _sortOrder, decimalSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, decimal>>);
            //        break;
            //    case filterType.DATETIME:
            //        MethodInfo dateTimeSortOrderMethod = typeof(TDataItem).GetMethod("getDateTimeSortOrder");
            //        dataCollection = unit.repository.GetChunksOfWithDateTimeOrderBy(thisSkip, takeCount, _sortOrder, dateTimeSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, DateTime>>);
            //        break;
            //    default:
            //        MethodInfo intSortOrderMethod = typeof(TDataItem).GetMethod("getIntSortOrder");
            //        dataCollection = unit.repository.GetChunksOfWithIntOrderBy(thisSkip, takeCount, _sortOrder, intSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, int>>);
            //        break;
            //}

            switch (sortType)
            {
                case filterType.STRINGVALUE:

                    dataCollection = unit.repository.GetChunksOfWithStringOrderBy(thisSkip, takeCount, _sortOrder, expressionTreeBuilder<TDataItem>.buildStringOrdeByExpression(orderBy));
                    break;
                case filterType.BOOLVALUE:

                    dataCollection = unit.repository.GetChunksOfWithBoolOrderBy(thisSkip, takeCount, _sortOrder, expressionTreeBuilder<TDataItem>.buildBoolOrdeByExpression(orderBy));
                    break;
                case filterType.DECIMALVALUE:

                    dataCollection = unit.repository.GetChunksOfWithDecimalOrderBy(thisSkip, takeCount, _sortOrder, expressionTreeBuilder<TDataItem>.buildDecimalOrdeByExpression(orderBy));
                    break;
                case filterType.DATETIME:

                    dataCollection = unit.repository.GetChunksOfWithDateTimeOrderBy(thisSkip, takeCount, _sortOrder, expressionTreeBuilder<TDataItem>.buildDateTimeOrdeByExpression(orderBy));
                    break;
                default:

                    dataCollection = unit.repository.GetChunksOfWithIntOrderBy(thisSkip, takeCount, _sortOrder, expressionTreeBuilder<TDataItem>.buildIntOrdeByExpression(orderBy));
                    break;
            }

            MethodInfo method = typeof(TListModelItem).GetMethod("buildFromDbInstance");
            foreach (var item in dataCollection) _collectionModel.items.Add(method.Invoke(null, new object[] { item }) as ICollectionItem);

            _collectionModel.currentPage = pageNumber;
            return _collectionModel;
        }

        public IListModelCollection getDataList
           (string _controller,
            string _controllerAction,
            string orderBy,
            sortOrder _sortOrder,
            int takeCount,
            int pageNumber,
            string selectedProperty,
            string queryString,
            string queryOptions)
        {
            IListModelCollection _collectionModel = new genericModelCollection(_controller, _controllerAction);

            // get list of properties, their display, value and sort order - to be used for column headings and 
            // as parameters for associated links

            _collectionModel.viewSortParams = pagedListExtentionHelpers.getListSortParemeters(typeof(TDataItem));
            _collectionModel.currentSortOrder = _sortOrder;
            _collectionModel.currentSortColumn = orderBy;

            // reverse sort order for current property
            _collectionModel.viewSortParams.reverseSortingOrderForCurrentProperty(orderBy, _sortOrder);

            // update default page count
            _collectionModel.currentViewCount = takeCount;
            _collectionModel.viewSortParams.updateTakeCount(takeCount);

            // set current takeCount for the form model
            _collectionModel.filterFormModel
                .updateParameters
                (_controller,
                _controllerAction, takeCount, pageNumber, _sortOrder, orderBy, selectedProperty, queryString, queryOptions.ToString());

            //MethodInfo getFilterMethod = typeof(TDataItem).GetMethod("getFilter");
            //Expression<Func<TDataItem, bool>> _dataFilter 
            //    = getFilterMethod.Invoke(null, new object[] { selectedProperty, queryString, queryOptions }) as Expression<Func<TDataItem, bool>>;

            // construct expression to filter data
            Expression<Func<TDataItem, bool>> _dataFilter 
                = dbPersistance.extentionHelpers.expressionTreeBuilder<TDataItem>.buildQueryExpression(selectedProperty, queryString, queryOptions);

            // update model total parameters 
            _collectionModel.currentViewCount = takeCount;
            _collectionModel.totalItemCount = unit.repository.CountSelectively(_dataFilter);
            _collectionModel.updateSortingOrder(orderBy, _sortOrder);

            // calculate total pages count
            if (_collectionModel.totalItemCount > _collectionModel.currentViewCount)
            {
                if (_collectionModel.totalItemCount % _collectionModel.currentViewCount > 0)
                {
                    _collectionModel.totalPages = _collectionModel.totalItemCount / _collectionModel.currentViewCount + 1;
                }
                else
                {
                    _collectionModel.totalPages = _collectionModel.totalItemCount / _collectionModel.currentViewCount;
                }
            }
            else
            {
                _collectionModel.totalPages = 1;
            }

            // set page number
            if (pageNumber > _collectionModel.totalPages) pageNumber = _collectionModel.totalPages;

            // calculate skip
            int thisSkip = 0;
            if (_collectionModel.totalItemCount > takeCount && pageNumber > 1)
            {
                thisSkip = takeCount * (pageNumber - 1);
            }

            IQueryable<TDataItem> dataCollection;

            filterType sortType = checkPropertyType(typeof(TDataItem), orderBy);

            //switch (sortType)
            //{
            //    case filterType.STRINGVALUE:

            //        MethodInfo stringSortOrderMethod = typeof(TDataItem).GetMethod("getStringSortOrder");
            //        dataCollection = unit.repository.GetChunksOfWithStringOrderBy(thisSkip, takeCount, _sortOrder, stringSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, string>>, _dataFilter);
            //        break;
            //    case filterType.BOOLVALUE:
            //        MethodInfo boolSortOrderMethod = typeof(TDataItem).GetMethod("getBoolSortOrder");
            //        dataCollection = unit.repository.GetChunksOfWithBoolOrderBy(thisSkip, takeCount, _sortOrder, boolSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, bool>>, _dataFilter);
            //        break;
            //    case filterType.DECIMALVALUE:
            //        MethodInfo decimalSortOrderMethod = typeof(TDataItem).GetMethod("getDecimalSortOrder");
            //        dataCollection = unit.repository.GetChunksOfWithDecimalOrderBy(thisSkip, takeCount, _sortOrder, decimalSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, decimal>>, _dataFilter);
            //        break;
            //    case filterType.DATETIME:
            //        MethodInfo dateTimeSortOrderMethod = typeof(TDataItem).GetMethod("getDateTimeSortOrder");
            //        dataCollection = unit.repository.GetChunksOfWithDateTimeOrderBy(thisSkip, takeCount, _sortOrder, dateTimeSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, DateTime>>, _dataFilter);
            //        break;
            //    default:
            //        MethodInfo intSortOrderMethod = typeof(TDataItem).GetMethod("getIntSortOrder");
            //        dataCollection = unit.repository.GetChunksOfWithIntOrderBy(thisSkip, takeCount, _sortOrder, intSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, int>>, _dataFilter);
            //        break;
            //}

            switch (sortType)
            {
                case filterType.STRINGVALUE:

                    dataCollection = unit.repository.GetChunksOfWithStringOrderBy(thisSkip, takeCount, _sortOrder, expressionTreeBuilder<TDataItem>.buildStringOrdeByExpression(orderBy), _dataFilter);
                    break;
                case filterType.BOOLVALUE:
         
                    dataCollection = unit.repository.GetChunksOfWithBoolOrderBy(thisSkip, takeCount, _sortOrder, expressionTreeBuilder<TDataItem>.buildBoolOrdeByExpression(orderBy), _dataFilter);
                    break;
                case filterType.DECIMALVALUE:
             
                    dataCollection = unit.repository.GetChunksOfWithDecimalOrderBy(thisSkip, takeCount, _sortOrder, expressionTreeBuilder<TDataItem>.buildDecimalOrdeByExpression(orderBy), _dataFilter);
                    break;
                case filterType.DATETIME:
              
                    dataCollection = unit.repository.GetChunksOfWithDateTimeOrderBy(thisSkip, takeCount, _sortOrder, expressionTreeBuilder<TDataItem>.buildDateTimeOrdeByExpression(orderBy), _dataFilter);
                    break;
                default:
                  
                    dataCollection = unit.repository.GetChunksOfWithIntOrderBy(thisSkip, takeCount, _sortOrder, expressionTreeBuilder<TDataItem>.buildIntOrdeByExpression(orderBy), _dataFilter);
                    break;
            }

            MethodInfo method = typeof(TListModelItem).GetMethod("buildFromDbInstance");
            foreach (var item in dataCollection) _collectionModel.items.Add(method.Invoke(null, new object[] { item }) as ICollectionItem);

            _collectionModel.currentPage = pageNumber;
            
            _collectionModel.selectedProperty = selectedProperty;
            _collectionModel.queryString = queryString;
            _collectionModel.queryOptions = queryOptions;

            return _collectionModel;
        }


        public filterType checkPropertyType(Type _type, string propertyName)
        {
            if (_type.GetProperties().Any(x => x.Name == propertyName))
            {
                var prop = _type.GetProperties().FirstOrDefault(x => x.Name == propertyName);

                if (prop.PropertyType == typeof(string)) return filterType.STRINGVALUE;
                if (prop.PropertyType == typeof(int)) return filterType.INTVALUE;
                if (prop.PropertyType == typeof(decimal)) return filterType.DECIMALVALUE;
                if (prop.PropertyType == typeof(bool)) return filterType.BOOLVALUE;
                if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?)) return filterType.DATETIME;
            }

            return filterType.NONE;
        }

        


        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    unit.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
