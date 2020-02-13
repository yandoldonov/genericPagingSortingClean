using dbPersistance.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace businessLogic.interfaces
{
    public interface IFilterDataFormModel
    {
        string selectedProperty { get; set; }
        string selectedQuetyOption { get; set; }
        string queryString { get; set; }
        IEnumerable<SelectListItem> properties { get; set; }
        IEnumerable<SelectListItem> queryVariants { get; set; }
        string genericTypeName { get; set; }
        queryOptions getfromString();

        void setFormInitialParameters(Type T);
        void updateTakeCount(int takeCount);
        void updateParameters(string controller, string controllerAction, int takeCount, int currentPage, sortOrder currentSortOrder, string orderBy, string selectedProperty, string queryString, string selectedQuetyOption);

        string controller { get; set; }
        string controllerAction { get; set; }
        int takeCount { get; set; }
        int currentPage { get; set; }
        string orderBy { get; set; }
        sortOrder currentSortOrder { get; set; }
    }
}
