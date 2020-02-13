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
    public class dbItemTypeTwo : IPocoEntity
    {
        public int Id { get; set; }
        public string guid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string stringValueOne { get; set; }
        public string stringValueTwo { get; set; }
        public decimal decValue { get; set; }
        public int intVlue { get; set; }
        public int invFieldTwo { get; set; }

        public int listablePropertiesCount => 9;

        public static Expression<Func<dbItemTypeTwo, string>> getStringSortOrder(string orderProperty)
        {
            Expression<Func<dbItemTypeTwo, string>> filter;

            switch (orderProperty)
            {
                case "guid":
                    filter = x => x.guid;
                    break;
                case "description":
                    filter = x => x.description;
                    break;
                case "stringValueOne":
                    filter = x => x.stringValueOne;
                    break;
                case "stringValueTwo":
                    filter = x => x.stringValueTwo;
                    break;
                default:
                    filter = x => x.name;
                    break;
            }

            return filter;
        }
        public static Expression<Func<dbItemTypeTwo, int>> getIntSortOrder(string orderProperty)
        {
            Expression<Func<dbItemTypeTwo, int>> filter;

            switch (orderProperty)
            {
                case "intVlue":
                    filter = x => x.intVlue;
                    break;
                case "invFieldTwo":
                    filter = x => x.invFieldTwo;
                    break;
                default:
                    filter = x => x.Id;
                    break;
            }

            return filter;
        }
        public static Expression<Func<dbItemTypeTwo, bool>> getBoolSortOrder(string orderProperty)
        {
            Expression<Func<dbItemTypeTwo, bool>> filter;

            switch (orderProperty)
            {
                default:
                    filter = x => true;
                    break;
            }

            return filter;
        }
        public static Expression<Func<dbItemTypeTwo, decimal>> getDecimalSortOrder(string orderProperty)
        {
            Expression<Func<dbItemTypeTwo, decimal>> filter;

            switch (orderProperty)
            {
                default:
                    filter = x => x.decValue;
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
                case "stringValueOne":
                    _filterType = enums.filterType.STRINGVALUE;
                    break;
                case "stringValueTwo":
                    _filterType = enums.filterType.STRINGVALUE;
                    break;
                case "boolvalue":
                    _filterType = enums.filterType.BOOLVALUE;
                    break;
                case "decValue":
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
                case "decValue":
                    break;
                case "stringValueOne":
                    break;
                case "stringValueTwo":
                    break;
                case "intVlue":
                    break;
                case "invFieldTwo":
                    break;
                default:
                    orderProperty = "id";
                    break;
            }

            return orderProperty;
        }

        public static IEnumerable<SelectListItem> searchablePropertyList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "id", Value = "id" },
                new SelectListItem { Text = "guid", Value = "guid" },
                new SelectListItem { Text = "name", Value = "name" },
                new SelectListItem { Text = "description", Value = "description" },
                new SelectListItem { Text = "Decimal Value", Value = "decValue" },
                new SelectListItem { Text = "String One", Value = "stringValueOne" },
                 new SelectListItem { Text = "String Two", Value = "stringValueTwo" },
                  new SelectListItem { Text = "Int Value #1", Value = "intVlue" },
                     new SelectListItem { Text = "Int Value #2", Value = "invFieldTwo" }
            };
        }

        public static IEnumerable<SelectListItem> searchableFilterTypes(string property)
        {
            // none, equals, contains, lessThan, largerThan, excludes, notEqual

            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Equal-To", Value = "equals" },
                new SelectListItem { Text = "Less-Than", Value = "lessThan" },
                new SelectListItem { Text = "Larger-Than", Value = "largerThan" },
                new SelectListItem { Text = "Not-Equal", Value = "notEqual" }
            };
        }

        public static string getDefaultSortProperty()
        {
            return "id";
        }

        public static Expression<Func<dbItemTypeTwo, bool>> getFilter
        (string selectedProperty,
        string queryString,
        queryOptions _queryOptions)
        {
            Expression<Func<dbItemTypeTwo, bool>> _expression;

            switch (selectedProperty)
            {
                case "id":
                    if (int.TryParse(queryString, out int _idValue))
                    {
                        switch (_queryOptions)
                        {
                            case queryOptions.equals:
                                _expression = x => x.Id == _idValue;
                                break;
                            case queryOptions.notEquals:
                                _expression = x => x.Id != _idValue;
                                break;
                            case queryOptions.lessThan:
                                _expression = x => x.Id < _idValue;
                                break;
                            case queryOptions.largerThan:
                                _expression = x => x.Id > _idValue;
                                break;
                            default:
                                _expression = null;
                                break;
                        }
                    }
                    else
                    {
                        throw new InvalidCastException("provided argument is not of correct type");
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
                case "decValue":
                    if (decimal.TryParse(queryString, out decimal _decimalDataValue))
                    {
                        switch (_queryOptions)
                        {
                            case queryOptions.equals:
                                _expression = x => x.decValue == _decimalDataValue;
                                break;
                            case queryOptions.notEquals:
                                _expression = x => x.decValue != _decimalDataValue;
                                break;
                            case queryOptions.lessThan:
                                _expression = x => x.decValue < _decimalDataValue;
                                break;
                            case queryOptions.largerThan:
                                _expression = x => x.decValue > _decimalDataValue;
                                break;
                            default:
                                _expression = null;
                                break;
                        }
                    }
                    else
                    {
                        throw new InvalidCastException("provided argument is not of correct type");
                    }
                    break;
                case "stringValueOne":
                    switch (_queryOptions)
                    {
                        case queryOptions.contains:
                            _expression = x => x.stringValueOne.Contains(queryString);
                            break;
                        case queryOptions.notEquals:
                            _expression = x => x.stringValueOne != queryString;
                            break;
                        case queryOptions.equals:
                            _expression = x => x.stringValueOne == queryString;
                            break;
                        case queryOptions.excludes:
                            _expression = x => !x.stringValueOne.Contains(queryString);
                            break;
                        default:
                            _expression = null;
                            break;
                    }
                    break;
                case "stringValueTwo":
                    switch (_queryOptions)
                    {
                        case queryOptions.contains:
                            _expression = x => x.stringValueTwo.Contains(queryString);
                            break;
                        case queryOptions.notEquals:
                            _expression = x => x.stringValueTwo != queryString;
                            break;
                        case queryOptions.equals:
                            _expression = x => x.stringValueTwo == queryString;
                            break;
                        case queryOptions.excludes:
                            _expression = x => !x.stringValueTwo.Contains(queryString);
                            break;
                        default:
                            _expression = null;
                            break;
                    }
                    break;
                case "intVlue":
                    if (int.TryParse(queryString, out int _intVlue))
                    {
                        switch (_queryOptions)
                        {
                            case queryOptions.equals:
                                _expression = x => x.intVlue == _intVlue;
                                break;
                            case queryOptions.notEquals:
                                _expression = x => x.intVlue != _intVlue;
                                break;
                            case queryOptions.lessThan:
                                _expression = x => x.intVlue < _intVlue;
                                break;
                            case queryOptions.largerThan:
                                _expression = x => x.intVlue > _intVlue;
                                break;
                            default:
                                _expression = null;
                                break;
                        }
                    }
                    else
                    {
                        throw new InvalidCastException("provided argument is not of correct type");
                    }
                    break;
                case "invFieldTwo":
                    if (int.TryParse(queryString, out int _invFieldTwo))
                    {
                        switch (_queryOptions)
                        {
                            case queryOptions.equals:
                                _expression = x => x.invFieldTwo == _invFieldTwo;
                                break;
                            case queryOptions.notEquals:
                                _expression = x => x.invFieldTwo != _invFieldTwo;
                                break;
                            case queryOptions.lessThan:
                                _expression = x => x.invFieldTwo < _invFieldTwo;
                                break;
                            case queryOptions.largerThan:
                                _expression = x => x.invFieldTwo > _invFieldTwo;
                                break;
                            default:
                                _expression = null;
                                break;
                        }
                    }
                    else
                    {
                        throw new InvalidCastException("provided argument is not of correct type");
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

            if (property == "id" || property == "decValue" || property == "intVlue" || property == "invFieldTwo")
            {
                emptyList.Add(new selectlistItemHelper { name = "equals", guid = "equals" });
                emptyList.Add(new selectlistItemHelper { name = "lessThan", guid = "lessThan" });
                emptyList.Add(new selectlistItemHelper { name = "largerThan", guid = "largerThan" });
                emptyList.Add(new selectlistItemHelper { name = "notEqual", guid = "notEqual" });
            }
            if (property == "guid" || property == "name" || property == "description" || property == "stringValueOne" || property == "stringValueTwo")
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
                    return "id";
                case 1:
                    return "guid";
                case 2:
                    return "name";
                case 3:
                    return "description";
                case 4:
                    return "stringValueOne";
                case 5:
                    return "stringValueTwo";
                case 6:
                    return "decValue";
                case 7:
                    return "intVlue";
                case 8:
                    return "invFieldTwo";
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
                    return this.stringValueOne;
                case 5:
                    return this.stringValueTwo;
                case 6:
                    return this.decValue.ToString();
                case 7:
                    return this.intVlue.ToString();
                case 8:
                    return this.invFieldTwo.ToString();
                default:
                    return string.Empty;
            }
        }

        public static IViewSortParameters getListSortParemeters()
        {
            sortParameters _sortParameters = new sortParameters()
            {
                defaultSortColumn = "id",
                defaultSortOrder = sortOrder.ASC,
                pageNumber = 1,
                takeCount = 10
            };

            _sortParameters.addPropertyParameters(new sortParameterItem()
            {
                number = 1,
                colDisplay = "id",
                colValue = "id",
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
                colDisplay = "stringValueOne",
                colValue = "stringValueOne",
                sortOrder = sortOrder.ASC
            });

            _sortParameters.addPropertyParameters(new sortParameterItem()
            {
                number = 6,
                colDisplay = "stringValueTwo",
                colValue = "stringValueTwo",
                sortOrder = sortOrder.ASC
            });
            _sortParameters.addPropertyParameters(new sortParameterItem()
            {
                number = 7,
                colDisplay = "decValue",
                colValue = "decValue",
                sortOrder = sortOrder.ASC
            });

            _sortParameters.addPropertyParameters(new sortParameterItem()
            {
                number = 8,
                colDisplay = "intVlue",
                colValue = "intVlue",
                sortOrder = sortOrder.ASC
            });
            _sortParameters.addPropertyParameters(new sortParameterItem()
            {
                number = 9,
                colDisplay = "invFieldTwo",
                colValue = "invFieldTwo",
                sortOrder = sortOrder.ASC
            });

            return _sortParameters;
        }
    }
}
