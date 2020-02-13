using businessLogic.interfaces;
using businessLogic.viewModels;
using dbPersistance.enums;
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

        public IListModelCollection getDataList(string _controller, string _controllerAction)
        {
            IListModelCollection _collectionModel = new genericModelCollection(_controller, _controllerAction);

            MethodInfo method2 = typeof(TDataItem).GetMethod("getListSortParemeters");
            _collectionModel.viewSortParams = method2.Invoke(null, new object[] { }) as IViewSortParameters;
            _collectionModel.currentSortOrder = _collectionModel.viewSortParams.defaultSortOrder;
            _collectionModel.currentSortColumn = _collectionModel.viewSortParams.defaultSortColumn;

            var dataCollection = unit.repository.GetAsNoTracking();

            MethodInfo method = typeof(TListModelItem).GetMethod("buildFromDbInstance");

            foreach (var item in dataCollection) _collectionModel.items.Add(method.Invoke(null, new object[] { item }) as ICollectionItem);

            _collectionModel.currentPage = 1;
            _collectionModel.currentViewCount = 10;
            int totalCount = unit.repository.Count();

            if (totalCount > _collectionModel.currentViewCount)
            {
                if (totalCount % _collectionModel.currentViewCount > 0)
                {
                    _collectionModel.totalPages = totalCount / _collectionModel.currentViewCount + 1;
                }
                else
                {
                    _collectionModel.totalPages = totalCount / _collectionModel.currentViewCount;
                }
            }
            else
            {
                _collectionModel.totalPages = 1;
            }


            return _collectionModel;
        }

        public IListModelCollection getDataList(string _controller, string _controllerAction, int takeCount)
        {
            IListModelCollection _collectionModel = new genericModelCollection(_controller, _controllerAction);

            MethodInfo method2 = typeof(TDataItem).GetMethod("getListSortParemeters");
            _collectionModel.viewSortParams = method2.Invoke(null, new object[] { null }) as IViewSortParameters;
            _collectionModel.currentSortOrder = _collectionModel.viewSortParams.defaultSortOrder;
            _collectionModel.currentSortColumn = _collectionModel.viewSortParams.defaultSortColumn;

            var dataCollection = unit.repository.GetChunksOf(0, takeCount);

            MethodInfo method = typeof(TListModelItem).GetMethod("buildFromDbInstance");
            foreach (var item in dataCollection) _collectionModel.items.Add(method.Invoke(null, new object[] { item }) as ICollectionItem);

            _collectionModel.currentPage = 1;
            _collectionModel.currentViewCount = takeCount;
            int totalCount = unit.repository.Count();

            if (totalCount > _collectionModel.currentViewCount)
            {
                if (totalCount % _collectionModel.currentViewCount > 0)
                {
                    _collectionModel.totalPages = totalCount / _collectionModel.currentViewCount + 1;
                }
                else
                {
                    _collectionModel.totalPages = totalCount / _collectionModel.currentViewCount;
                }
            }
            else
            {
                _collectionModel.totalPages = 1;
            }

            return _collectionModel;
        }

        public IListModelCollection getDataList(string _controller, string _controllerAction, int takeCount, int pageNumber)
        {
            IListModelCollection _collectionModel = new genericModelCollection(_controller, _controllerAction);

            MethodInfo method2 = typeof(TDataItem).GetMethod("getListSortParemeters");
            _collectionModel.viewSortParams = method2.Invoke(null, new object[] { }) as IViewSortParameters;
            _collectionModel.currentSortOrder = _collectionModel.viewSortParams.defaultSortOrder;
            _collectionModel.currentSortColumn = _collectionModel.viewSortParams.defaultSortColumn;

            _collectionModel.filterFormModel.setFormInitialParameters(typeof(TDataItem));

            _collectionModel.currentViewCount = takeCount;
            _collectionModel.totalItemCount = unit.repository.Count();

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

            if (pageNumber > _collectionModel.totalPages) pageNumber = _collectionModel.totalPages;

            int thisSkip = 0;

            if (_collectionModel.totalItemCount > takeCount && pageNumber > 1)
            {
                thisSkip = takeCount * pageNumber;
            }

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

            MethodInfo method2 = typeof(TDataItem).GetMethod("getListSortParemeters");
            _collectionModel.viewSortParams = method2.Invoke(null, new object[] { }) as IViewSortParameters;
            _collectionModel.currentSortOrder = _sortOrder;
            _collectionModel.currentSortColumn = orderBy;

            #region prepare paremeters

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


            _collectionModel.totalItemCount = unit.repository.Count();
            _collectionModel.updateSortingOrder(orderBy, _sortOrder);

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

            if (pageNumber > _collectionModel.totalPages) pageNumber = _collectionModel.totalPages;

            int thisSkip = 0;

            if (_collectionModel.totalItemCount > takeCount && pageNumber > 1)
            {
                thisSkip = takeCount * pageNumber;
            }
            #endregion

            IQueryable<TDataItem> dataCollection;
    
            filterType sortType = checkPropertyType(typeof(TDataItem), orderBy);

            switch (sortType)
            {
                case filterType.STRINGVALUE:

                    MethodInfo stringSortOrderMethod = typeof(TDataItem).GetMethod("getStringSortOrder");
                    dataCollection = unit.repository.GetChunksOfWithStringOrderBy(thisSkip, takeCount, _sortOrder, stringSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, string>>);
                    break;
                case filterType.BOOLVALUE:
                    MethodInfo boolSortOrderMethod = typeof(TDataItem).GetMethod("getBoolSortOrder");
                    dataCollection = unit.repository.GetChunksOfWithBoolOrderBy(thisSkip, takeCount, _sortOrder, boolSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, bool>>);
                    break;
                case filterType.DECIMALVALUE:
                    MethodInfo decimalSortOrderMethod = typeof(TDataItem).GetMethod("getDecimalSortOrder");
                    dataCollection = unit.repository.GetChunksOfWithDecimalOrderBy(thisSkip, takeCount, _sortOrder, decimalSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, decimal>>);
                    break;
                case filterType.DATETIME:
                    MethodInfo dateTimeSortOrderMethod = typeof(TDataItem).GetMethod("getDateTimeSortOrder");
                    dataCollection = unit.repository.GetChunksOfWithDateTimeOrderBy(thisSkip, takeCount, _sortOrder, dateTimeSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, DateTime>>);
                    break;
                default:
                    MethodInfo intSortOrderMethod = typeof(TDataItem).GetMethod("getIntSortOrder");
                    dataCollection = unit.repository.GetChunksOfWithIntOrderBy(thisSkip, takeCount, _sortOrder, intSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, int>>);
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
            queryOptions queryOptions)
        {
            IListModelCollection _collectionModel = new genericModelCollection(_controller, _controllerAction);

            // get list of properties, their display, value and sort order - to be used for column headings and 
            // as parameters for associated links

            MethodInfo method2 = typeof(TDataItem).GetMethod("getListSortParemeters");
            _collectionModel.viewSortParams = method2.Invoke(null, new object[] { }) as IViewSortParameters;
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

            MethodInfo getFilterMethod = typeof(TDataItem).GetMethod("getFilter");

            Expression<Func<TDataItem, bool>> _dataFilter = getFilterMethod.Invoke(null, new object[] { selectedProperty, queryString, queryOptions }) as Expression<Func<TDataItem, bool>>;

            _collectionModel.currentViewCount = takeCount;
            _collectionModel.totalItemCount = unit.repository.CountSelectively(_dataFilter);
            _collectionModel.updateSortingOrder(orderBy, _sortOrder);

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

            if (pageNumber > _collectionModel.totalPages) pageNumber = _collectionModel.totalPages;

            int thisSkip = 0;

            if (_collectionModel.totalItemCount > takeCount && pageNumber > 1)
            {
                thisSkip = takeCount * pageNumber;
            }

            IQueryable<TDataItem> dataCollection;

            filterType sortType = checkPropertyType(typeof(TDataItem), orderBy);

            switch (sortType)
            {
                case filterType.STRINGVALUE:

                    MethodInfo stringSortOrderMethod = typeof(TDataItem).GetMethod("getStringSortOrder");
                    dataCollection = unit.repository.GetChunksOfWithStringOrderBy(thisSkip, takeCount, _sortOrder, stringSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, string>>, _dataFilter);
                    break;
                case filterType.BOOLVALUE:
                    MethodInfo boolSortOrderMethod = typeof(TDataItem).GetMethod("getBoolSortOrder");
                    dataCollection = unit.repository.GetChunksOfWithBoolOrderBy(thisSkip, takeCount, _sortOrder, boolSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, bool>>, _dataFilter);
                    break;
                case filterType.DECIMALVALUE:
                    MethodInfo decimalSortOrderMethod = typeof(TDataItem).GetMethod("getDecimalSortOrder");
                    dataCollection = unit.repository.GetChunksOfWithDecimalOrderBy(thisSkip, takeCount, _sortOrder, decimalSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, decimal>>, _dataFilter);
                    break;
                case filterType.DATETIME:
                    MethodInfo dateTimeSortOrderMethod = typeof(TDataItem).GetMethod("getDateTimeSortOrder");
                    dataCollection = unit.repository.GetChunksOfWithDateTimeOrderBy(thisSkip, takeCount, _sortOrder, dateTimeSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, DateTime>>, _dataFilter);
                    break;
                default:
                    MethodInfo intSortOrderMethod = typeof(TDataItem).GetMethod("getIntSortOrder");
                    dataCollection = unit.repository.GetChunksOfWithIntOrderBy(thisSkip, takeCount, _sortOrder, intSortOrderMethod.Invoke(null, new object[] { orderBy }) as Expression<Func<TDataItem, int>>, _dataFilter);
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
            if(_type.GetProperties().Any(x => x.Name == propertyName))
            {
                var prop = _type.GetProperties().FirstOrDefault(x => x.Name == propertyName);

                if (prop.PropertyType == typeof(string)) return filterType.STRINGVALUE;
                if (prop.PropertyType == typeof(int)) return filterType.INTVALUE;
                if (prop.PropertyType == typeof(decimal)) return filterType.DECIMALVALUE;
                if (prop.PropertyType == typeof(bool)) return filterType.BOOLVALUE;
                if (prop.PropertyType == typeof(DateTime)) return filterType.DATETIME;
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
