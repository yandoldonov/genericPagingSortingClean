using dbPersistance.enums;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.interfaces
{
    public interface IListModelFactory<TDataItem, TListModel> : IDisposable where TDataItem : IPocoEntity where TListModel : IListModelCollection
    {
        IListModelCollection getDataList(string controller, string controllerAction, int takeCount, int pageNumber);
        IListModelCollection getDataList(string controller, string controllerAction, string orderBy, sortOrder _sortOrder, int takeCount, int pageNumber);

        IListModelCollection getDataList
        (string controller,
        string controllerAction,
        string orderBy,
        sortOrder _sortOrder,
        int takeCount,
        int pageNumber,
        string selectedProperty,
        string queryString,
        string queryOptions);
    }
}
