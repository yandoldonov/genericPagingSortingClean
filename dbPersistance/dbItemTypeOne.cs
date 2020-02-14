using dbPersistance.enums;
using dbPersistance.helperModels;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace dbPersistance
{
    public class dbItemTypeOne : IPocoEntity
    {
        public int Id { get; set; }
        public string guid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal decimalData { get; set; }
        public bool boolvalue { get; set; }

        public int listablePropertiesCount => 6;

        public static Expression<Func<dbItemTypeOne, string>> getStringSortOrder(string orderProperty)
        {
            Expression<Func<dbItemTypeOne, string>> filter;

            switch (orderProperty)
            {
                case "guid":
                    filter = x => x.guid;
                    break;
                case "description":
                    filter = x => x.description;
                    break;
                default:
                    filter = x => x.name;
                    break;
            }

            return filter;
        }
        public static Expression<Func<dbItemTypeOne, int>> getIntSortOrder(string orderProperty)
        {
            Expression<Func<dbItemTypeOne, int>> filter;

            switch (orderProperty)
            {
                default:
                    filter = x => x.Id;
                    break;
            }

            return filter;
        }
        public static Expression<Func<dbItemTypeOne, bool>> getBoolSortOrder(string orderProperty)
        {
            Expression<Func<dbItemTypeOne, bool>> filter;

            switch (orderProperty)
            {
                default:
                    filter = x => x.boolvalue;
                    break;
            }

            return filter;
        }
        public static Expression<Func<dbItemTypeOne, decimal>> getDecimalSortOrder(string orderProperty)
        {
            Expression<Func<dbItemTypeOne, decimal>> filter;

            switch (orderProperty)
            {
                default:
                    filter = x => x.decimalData;
                    break;
            }

            return filter;
        }

        public static filterType filterType(string orderProperty)
        {
            filterType _filterType;

            switch (orderProperty)
            {
                case "guid":
                    _filterType = enums.filterType.STRINGVALUE;
                    break;
                case "description":
                    _filterType = enums.filterType.STRINGVALUE;
                    break;
                case "name":
                    _filterType = enums.filterType.STRINGVALUE;
                    break;
                case "boolvalue":
                    _filterType = enums.filterType.BOOLVALUE;
                    break;
                case "decimalData":
                    _filterType = enums.filterType.DECIMALVALUE;
                    break;
                default:
                    _filterType = enums.filterType.INTVALUE;
                    break;
            }

            return _filterType;
        }

        public static string getDefaultSortBy(string orderProperty)
        {
            switch (orderProperty)
            {
                case "guid":
                    break;
                case "name":
                    break;
                case "description":
                    break;
                case "decimalData":
                    break;
                case "boolvalue":
                    break;
                default:
                    orderProperty = "Id";
                    break;
            }

            return orderProperty;
        }

        public static IEnumerable<SelectListItem> searchablePropertyList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Id", Value = "Id" },
                new SelectListItem { Text = "guid", Value = "guid" },
                new SelectListItem { Text = "name", Value = "name" },
                new SelectListItem { Text = "description", Value = "description" },
                new SelectListItem { Text = "decimalData", Value = "decimalData" },
                new SelectListItem { Text = "boolvalue", Value = "boolvalue" }
            };
        }

        public static IEnumerable<SelectListItem> searchableFilterTypes(string property)
        {
            // none, equals, contains, lessThan, largerThan, excludes, notEqual

            return new List<SelectListItem>
            {
                new SelectListItem { Text = "equals", Value = "equals" },
                new SelectListItem { Text = "lessThan", Value = "lessThan" },
                new SelectListItem { Text = "largerThan", Value = "largerThan" },
                new SelectListItem { Text = "notEqual", Value = "notEqual" }
            };
        }

        public static string getDefaultSortProperty()
        {
            return "Id";
        }

        public static Expression<Func<dbItemTypeOne, bool>> getFilter
        (string selectedProperty,
        string queryString,
        queryOptions _queryOptions)
        {
            Expression<Func<dbItemTypeOne, bool>> _expression;

            switch (selectedProperty)
            {
                case "Id":
                    if (int.TryParse(queryString, out int _IdValue))
                    {
                        switch (_queryOptions)
                        {
                            case queryOptions.equals:
                                _expression = x => x.Id == _IdValue;
                                break;
                            case queryOptions.notEquals:
                                _expression = x => x.Id != _IdValue;
                                break;
                            case queryOptions.lessThan:
                                _expression = x => x.Id < _IdValue;
                                break;
                            case queryOptions.largerThan:
                                _expression = x => x.Id > _IdValue;
                                break;
                            default:
                                _expression = null;
                                break;
                        }
                    }
                    else
                    {
                        throw new InvalidCastException("provIded argument is not of correct type");
                    }
                    break;
                case "guid":
                    switch (_queryOptions)
                    {
                        case queryOptions.contains:
                            _expression = x => x.guid.Contains(queryString);
                            break;
                        case queryOptions.notEquals:
                            _expression = x => x.guid != queryString;
                            break;
                        case queryOptions.equals:
                            _expression = x => x.guid == queryString;
                            break;
                        case queryOptions.excludes:
                            _expression = x => !x.guid.Contains(queryString);
                            break;
                        default:
                            _expression = null;
                            break;
                    }
                    break;

                case "name":
                    switch (_queryOptions)
                    {
                        case queryOptions.contains:
                            _expression = x => x.name.Contains(queryString);
                            break;
                        case queryOptions.notEquals:
                            _expression = x => x.name != queryString;
                            break;
                        case queryOptions.equals:
                            _expression = x => x.name == queryString;
                            break;
                        case queryOptions.excludes:
                            _expression = x => !x.name.Contains(queryString);
                            break;
                        default:
                            _expression = null;
                            break;
                    }
                    break;
                case "description":
                    switch (_queryOptions)
                    {
                        case queryOptions.contains:
                            _expression = x => x.description.Contains(queryString);
                            break;
                        case queryOptions.notEquals:
                            _expression = x => x.description != queryString;
                            break;
                        case queryOptions.equals:
                            _expression = x => x.description == queryString;
                            break;
                        case queryOptions.excludes:
                            _expression = x => !x.description.Contains(queryString);
                            break;
                        default:
                            _expression = null;
                            break;
                    }
                    break;
                case "decimalData":
                    if (decimal.TryParse(queryString, out decimal _decimalDataValue))
                    {
                        switch (_queryOptions)
                        {
                            case queryOptions.equals:
                                _expression = x => x.decimalData == _decimalDataValue;
                                break;
                            case queryOptions.notEquals:
                                _expression = x => x.decimalData != _decimalDataValue;
                                break;
                            case queryOptions.lessThan:
                                _expression = x => x.decimalData < _decimalDataValue;
                                break;
                            case queryOptions.largerThan:
                                _expression = x => x.decimalData > _decimalDataValue;
                                break;
                            default:
                                _expression = null;
                                break;
                        }
                    }
                    else
                    {
                        throw new InvalidCastException("provIded argument is not of correct type");
                    }
                    break;
                case "boolvalue":
                    if (queryString == "isTrue")
                    {
                        _expression = x => x.boolvalue == true;
                    }
                    else
                    {
                        _expression = x => x.boolvalue == false;
                    }
                    break;
                default:
                    _expression = null;
                    break;
            }

            return _expression;
        }

        public static SelectList getSelectList(string property)
        {
            List<selectlistItemHelper> emptyList = new List<selectlistItemHelper>();

            if (property == "Id" || property == "decimalData")
            {
                emptyList.Add(new selectlistItemHelper { name = "equals", guid = "equals" });
                emptyList.Add(new selectlistItemHelper { name = "lessThan", guid = "lessThan" });
                emptyList.Add(new selectlistItemHelper { name = "largerThan", guid = "largerThan" });
                emptyList.Add(new selectlistItemHelper { name = "notEqual", guid = "notEqual" });
            }
            if (property == "guid" || property == "name" || property == "description")
            {
                emptyList.Add(new selectlistItemHelper { name = "equals", guid = "equals" });
                emptyList.Add(new selectlistItemHelper { name = "contains", guid = "contains" });
                emptyList.Add(new selectlistItemHelper { name = "excludes", guid = "excludes" });
            }
            if (property == "boolvalue")
            {
                emptyList.Add(new selectlistItemHelper { name = "isTrue", guid = "isTrue" });
                emptyList.Add(new selectlistItemHelper { name = "isFalse", guid = "isFalse" });
            }

            return new SelectList(emptyList, "guid", "name");
        }

        public string getPropertyDisplay(int propertyIndex)
        {
            switch (propertyIndex)
            {
                case 0:
                    return "Id";
                case 1:
                    return "guid";
                case 2:
                    return "name";
                case 3:
                    return "description";
                case 4:
                    return "decimalData";
                case 5:
                    return "boolvalue";
                default:
                    return string.Empty;
            }
        }

        public string getPropertyValue(int propertyIndex)
        {
            switch (propertyIndex)
            {
                case 0:
                    return this.Id.ToString();
                case 1:
                    return this.guid;
                case 2:
                    return this.name;
                case 3:
                    return this.description;
                case 4:
                    return this.decimalData.ToString();
                case 5:
                    return this.boolvalue.ToString();
                default:
                    return string.Empty;
            }
        }

        public static IViewSortParameters getListSortParemeters()
        {
            sortParameters _sortParameters = new sortParameters()
            {
                defaultSortColumn = "Id",
                defaultSortOrder = sortOrder.ASC,
                pageNumber = 1,
                takeCount = 10
            };

            _sortParameters.addPropertyParameters(new sortParameterItem()
            {
                number = 1,
                colDisplay = "Id",
                colValue = "Id",
                sortOrder = sortOrder.ASC
            });

            _sortParameters.addPropertyParameters(new sortParameterItem()
            {
                number = 2,
                colDisplay = "guid",
                colValue = "guid",
                sortOrder = sortOrder.ASC
            });

            _sortParameters.addPropertyParameters(new sortParameterItem()
            {
                number = 3,
                colDisplay = "name",
                colValue = "name",
                sortOrder = sortOrder.ASC
            });

            _sortParameters.addPropertyParameters(new sortParameterItem()
            {
                number = 4,
                colDisplay = "description",
                colValue = "description",
                sortOrder = sortOrder.ASC
            });

            _sortParameters.addPropertyParameters(new sortParameterItem()
            {
                number = 5,
                colDisplay = "decimalData",
                colValue = "decimalData",
                sortOrder = sortOrder.ASC
            });

            _sortParameters.addPropertyParameters(new sortParameterItem()
            {
                number = 6,
                colDisplay = "boleanValue",
                colValue = "boleanValue",
                sortOrder = sortOrder.ASC
            });

            return _sortParameters;
        }
    }
}
