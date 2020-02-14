using dbPersistance.helperModels;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace dbPersistance.extentionHelpers
{
    public static class pagedListExtentionHelpers
    {

        public static IViewSortParameters getListSortParemeters(this Type T)
        {
            sortParameters _sortParameters = new sortParameters()
            {
                defaultSortColumn = typeExtentions.getDefaultPagedListProperty(T).Name,
                defaultSortOrder = typeExtentions.getPagedListPropertyAttribute(typeExtentions.getDefaultPagedListProperty(T)).getSortOrder(),
                pageNumber = 1,
                takeCount = 10
            };

            foreach(var propItem in typeExtentions.getPageListProperties(T))
            {
                _sortParameters.addPropertyParameters(new sortParameterItem()
                {
                    number = typeExtentions.getPagedListPropertyAttribute(propItem).getPosition(),
                    colDisplay = typeExtentions.getPagedListPropertyAttribute(propItem).getDisplay(),
                    colValue = propItem.Name,
                    sortOrder = typeExtentions.getPagedListPropertyAttribute(propItem).getSortOrder()
                });
            }

            return _sortParameters;
        }

        public static IEnumerable<SelectListItem> searchablePropertyList(this Type T)
        {
            bool selectedEncountered = false;
            List<SelectListItem> _selectList = new List<SelectListItem>();

            foreach (var propItem in typeExtentions.getPageListProperties(T))
            {
                if(!selectedEncountered)
                {
                    if(typeExtentions.getPagedListPropertyAttribute(propItem).isThisDefault())
                    {
                        _selectList.Add(new SelectListItem { Text = typeExtentions.getPagedListPropertyAttribute(propItem).getDisplay(), Value = propItem.Name, Selected = true });
                        selectedEncountered = true;
                    }
                    else
                    {
                        _selectList.Add(new SelectListItem { Text = typeExtentions.getPagedListPropertyAttribute(propItem).getDisplay(), Value = propItem.Name });
                    }
                }
                else
                {
                    _selectList.Add(new SelectListItem { Text = typeExtentions.getPagedListPropertyAttribute(propItem).getDisplay(), Value = propItem.Name });
                }
                
            }

            return _selectList;
        }

        public static IEnumerable<SelectListItem> queryVariants(this Type T, string propertyName)
        {
           var prop = typeExtentions.getPageListProperties(T).Where(x => x.Name == propertyName).FirstOrDefault();

            if (prop.PropertyType == typeof(string))
            {
                return new List<SelectListItem>
                    {
                        new SelectListItem { Text = "equals", Value = "equals" },
                        new SelectListItem { Text = "contains", Value = "contains" },                      
                        new SelectListItem { Text = "excludes", Value = "excludes" }
                    };
            }
            if (prop.PropertyType == typeof(int))
            {
                return new List<SelectListItem>
                    {
                        new SelectListItem { Text = "equals", Value = "equals" },
                        new SelectListItem { Text = "lessThan", Value = "lessThan" },
                        new SelectListItem { Text = "largerThan", Value = "largerThan" },
                        new SelectListItem { Text = "notEqual", Value = "notEqual" }
                    };
            }
            if (prop.PropertyType == typeof(decimal))
            {
                return new List<SelectListItem>
                    {
                        new SelectListItem { Text = "equals", Value = "equals" },
                        new SelectListItem { Text = "lessThan", Value = "lessThan" },
                        new SelectListItem { Text = "largerThan", Value = "largerThan" },
                        new SelectListItem { Text = "notEqual", Value = "notEqual" }
                    };
            }
            if (prop.PropertyType == typeof(bool))
            {
                return new List<SelectListItem>
                    {
                        new SelectListItem { Text = "true", Value = "true" },
                        new SelectListItem { Text = "false", Value = "false" }
                    };
            }
            if (prop.PropertyType == typeof(DateTime))
            {
                return new List<SelectListItem>
                    {
                        new SelectListItem { Text = "none", Value = "none" }
                    };
            }

            return new List<SelectListItem>
                    {
                        new SelectListItem { Text = "none", Value = "none" }
                    };
        }

        public static SelectList queryVariantsSelectList(this Type T, string propertyName)
        {
            List<selectlistItemHelper> emptyList = new List<selectlistItemHelper>();

            var prop = typeExtentions.getPageListProperties(T).Where(x => x.Name == propertyName).FirstOrDefault();

            if (prop.PropertyType == typeof(string))
            {
                emptyList.Add(new selectlistItemHelper { name = "equals", guid = "equals" });
                emptyList.Add(new selectlistItemHelper { name = "contains", guid = "contains" });
                emptyList.Add(new selectlistItemHelper { name = "excludes", guid = "excludes" });
            }
            if (prop.PropertyType == typeof(int))
            {
                emptyList.Add(new selectlistItemHelper { name = "equals", guid = "equals" });
                emptyList.Add(new selectlistItemHelper { name = "lessThan", guid = "lessThan" });
                emptyList.Add(new selectlistItemHelper { name = "largerThan", guid = "largerThan" });
                emptyList.Add(new selectlistItemHelper { name = "notEqual", guid = "notEqual" });
            }
            if (prop.PropertyType == typeof(decimal))
            {
                emptyList.Add(new selectlistItemHelper { name = "equals", guid = "equals" });
                emptyList.Add(new selectlistItemHelper { name = "lessThan", guid = "lessThan" });
                emptyList.Add(new selectlistItemHelper { name = "largerThan", guid = "largerThan" });
                emptyList.Add(new selectlistItemHelper { name = "notEqual", guid = "notEqual" });
            }
            if (prop.PropertyType == typeof(bool))
            {
                emptyList.Add(new selectlistItemHelper { name = "isTrue", guid = "isTrue" });
                emptyList.Add(new selectlistItemHelper { name = "isFalse", guid = "isFalse" });
            }
            if (prop.PropertyType == typeof(DateTime))
            {
                emptyList.Add(new selectlistItemHelper { name = "none", guid = "none" });
            }

            return new SelectList(emptyList, "guid", "name");
        }
    }
}
