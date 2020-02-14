using businessLogic.interfaces;
using dbPersistance.enums;
using dbPersistance.extentionHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace businessLogic.viewModels
{
    public class filterFormModel : IFilterDataFormModel
    {
        public filterFormModel()
        {

        }

        public filterFormModel(string _controller, string _controllerAction)
        {
            this.controller = _controller;
            this.controllerAction = _controllerAction;
        }


        [Display(Name = "Select Property")]
        public string selectedProperty { get; set; }
        [Display(Name = "Filter Type")]
        public string selectedQuetyOption { get; set; }
        [Display(Name = "Data To Match")]
        public string queryString { get; set; }
        public IEnumerable<SelectListItem> properties { get; set; }
        public IEnumerable<SelectListItem> queryVariants { get; set; }

        public string genericTypeName { get; set; }

        public queryOptions getfromString()
        {
            switch (this.selectedQuetyOption)
            {
                case "equals":
                    return queryOptions.equals;
                case "notEquals":
                    return queryOptions.notEquals;
                case "contains":
                    return queryOptions.contains;
                case "lessThan":
                    return queryOptions.lessThan;
                case "largerThan":
                    return queryOptions.largerThan;
                case "excludes":
                    return queryOptions.excludes;
                case "isTrue":
                    return queryOptions.isTrue;
                case "isFalse":
                    return queryOptions.isFalse;
                default:
                    return queryOptions.none;
            }
        }

        public void setFormInitialParameters(Type _type)
        {           
            this.properties = pagedListExtentionHelpers.searchablePropertyList(_type);

            if (this.properties.Any())
            {
                this.queryVariants = pagedListExtentionHelpers.queryVariants(_type, this.properties.FirstOrDefault().Value);
            }
            else
            {
                this.queryVariants = new List<SelectListItem>
                {
                    new SelectListItem { Text = "no-options", Value = "xxxxxxxxxxxxxxx" },
                };
            }

            this.orderBy = typeExtentions.getDefaultPagedListProperty(_type).Name;
            this.currentPage = 1;

            this.genericTypeName = _type.Name;
        }
        public void updateTakeCount(int _takeCount)
        {
            this.takeCount = _takeCount;
        }

        public string controller { get; set; }
        public string controllerAction { get; set; }
        public string orderBy { get; set; }
        public int takeCount { get; set; }
        public int currentPage { get; set; }
        public sortOrder currentSortOrder { get; set; }

        public void updateParameters
            (string _controller,
            string _controllerAction,
            int _takeCount,
            int _currentPage,
            sortOrder _currentSortOrder,
            string _orderBy,
            string _selectedProperty,
            string _queryString,
            string _selectedQuetyOption)
        {
            this.controller = _controller;
            this.controllerAction = _controllerAction;
            this.takeCount = _takeCount;
            this.currentPage = _currentPage;
            this.currentSortOrder = _currentSortOrder;
            this.orderBy = _orderBy;
            this.selectedProperty = _selectedProperty;
            this.queryString = _queryString;
            this.selectedQuetyOption = _selectedQuetyOption;
        }
    }
}
