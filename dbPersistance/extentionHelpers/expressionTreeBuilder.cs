using dbPersistance.enums;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.extentionHelpers
{
    public static class expressionTreeBuilder<TDataItem> where TDataItem : class, IPocoEntity
    {
        public static Expression<Func<TDataItem, bool>> buildQueryExpression(string propertyName, string value, queryOptions _queryOptions)
        {
            try
            {
                switch (_queryOptions)
                {
                    case queryOptions.contains:

                        var parameterExp = Expression.Parameter(typeof(TDataItem), "type");
                        var propertyExp = Expression.Property(parameterExp, propertyName);
                        MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var someValue = Expression.Constant(value, typeof(string));
                        var containsMethodExp = Expression.Call(propertyExp, method, someValue);
                        return Expression.Lambda<Func<TDataItem, bool>>(containsMethodExp, parameterExp);

                    case queryOptions.excludes:

                        var excludeParameterExp = Expression.Parameter(typeof(TDataItem), "type");
                        var excludePropertyExp = Expression.Property(excludeParameterExp, propertyName);
                        MethodInfo excludeMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var excludeValue = Expression.Constant(value, typeof(string));
                        var ecludesMethodExp = Expression.Call(excludePropertyExp, excludeMethod, excludeValue);
                        var rzlt = Expression.Lambda<Func<TDataItem, bool>>(ecludesMethodExp, excludeParameterExp);

                        return Expression.Lambda<Func<TDataItem, bool>>(
                                Expression.Not(rzlt.Body),
                                rzlt.Parameters);

                    case queryOptions.equals:

                        var equalsParameter = Expression.Parameter(typeof(TDataItem), "w");
                        var equalsProperty = Expression.Property(equalsParameter, propertyName);
                        var equalsPropertyType = typeof(TDataItem).GetProperty(propertyName).PropertyType;

                        BinaryExpression equalsBody;

                        if (equalsPropertyType == typeof(string)) 
                        {
                            equalsBody = Expression.Equal(equalsProperty,
                             Expression.Convert(Expression.Constant(value), equalsPropertyType));
                            return Expression.Lambda<Func<TDataItem, bool>>(equalsBody, equalsParameter);
                        }
                        else if (equalsPropertyType == typeof(int))
                        {
                            if (int.TryParse(value, out int _intValue))
                            {
                                equalsBody = Expression.Equal(equalsProperty,
                                Expression.Convert(Expression.Constant(_intValue), equalsPropertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(equalsBody, equalsParameter);
                            }
                        }
                        else if (equalsPropertyType == typeof(decimal))
                        {
                            if (decimal.TryParse(value, out decimal _decimalValue))
                            {
                                equalsBody = Expression.Equal(equalsProperty,
                                Expression.Convert(Expression.Constant(_decimalValue), equalsPropertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(equalsBody, equalsParameter);
                            }
                        }
                        else if (equalsPropertyType == typeof(bool))
                        {
                            if (value.ToLower() == "isTrue")
                            {
                                equalsBody = Expression.Equal(equalsProperty,
                                Expression.Convert(Expression.Constant(true), equalsPropertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(equalsBody, equalsParameter);
                            }
                            else
                            {
                                equalsBody = Expression.Equal(equalsProperty,
                               Expression.Convert(Expression.Constant(false), equalsPropertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(equalsBody, equalsParameter);
                            }

                        }
                        else if (equalsPropertyType == typeof(DateTime))
                        {
                            if (DateTime.TryParse(value, out DateTime _dateTimeValue))
                            {
                                equalsBody = Expression.Equal(equalsProperty,
                                Expression.Convert(Expression.Constant(_dateTimeValue), equalsPropertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(equalsBody, equalsParameter);
                            }
                        }
                        throw new InvalidOperationException("provided expression can not be processes as it is of incorrect type...");                     

                    case queryOptions.largerThan:

                        var param = Expression.Parameter(typeof(TDataItem), "w");
                        var property = Expression.Property(param, propertyName);
                        var propertyType = typeof(TDataItem).GetProperty(propertyName).PropertyType;

                        BinaryExpression body;

                        if (propertyType == typeof(string))
                        {
                            body = Expression.GreaterThan(property,
                            Expression.Convert(Expression.Constant(value), propertyType));
                            return Expression.Lambda<Func<TDataItem, bool>>(body, param);
                        }
                        else if (propertyType == typeof(int))
                        {
                            if(int.TryParse(value, out int _intValue))
                            {
                                body = Expression.GreaterThan(property,
                                Expression.Convert(Expression.Constant(_intValue), propertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(body, param);
                            }
                        }
                        else if (propertyType == typeof(decimal))
                        {
                            if (decimal.TryParse(value, out decimal _decimalValue))
                            {
                                body = Expression.GreaterThan(property,
                                Expression.Convert(Expression.Constant(_decimalValue), propertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(body, param);
                            }
                        }
                        else if (propertyType == typeof(bool))
                        {
                            if(value.ToLower() == "isTrue")
                            {
                                body = Expression.GreaterThan(property,
                                Expression.Convert(Expression.Constant(true), propertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(body, param);
                            }
                            else
                            {
                                body = Expression.GreaterThan(property,
                               Expression.Convert(Expression.Constant(false), propertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(body, param);
                            }

                        }
                        else if (propertyType == typeof(DateTime))
                        {
                            if (DateTime.TryParse(value, out DateTime _dateTimeValue))
                            {
                                body = Expression.GreaterThan(property,
                                Expression.Convert(Expression.Constant(_dateTimeValue), propertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(body, param);
                            }
                        }
                        throw new InvalidOperationException("provided expression can not be processes as it is of incorrect type...");        

                    case queryOptions.lessThan:

                        var lessThanParameter = Expression.Parameter(typeof(TDataItem), "w");
                        var lessThanProperty = Expression.Property(lessThanParameter, propertyName);
                        var lessThanPropertyType = typeof(TDataItem).GetProperty(propertyName).PropertyType; 


                        BinaryExpression lessThanBody;

                        if (lessThanPropertyType == typeof(string))
                        {
                            lessThanBody = Expression.LessThan(lessThanProperty,
                            Expression.Convert(Expression.Constant(value), lessThanPropertyType));
                            return Expression.Lambda<Func<TDataItem, bool>>(lessThanBody, lessThanParameter);
                        }
                        else if (lessThanPropertyType == typeof(int))
                        {
                            if (int.TryParse(value, out int _intValue))
                            {
                                lessThanBody = Expression.LessThan(lessThanProperty,
                                Expression.Convert(Expression.Constant(_intValue), lessThanPropertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(lessThanBody, lessThanParameter);
                            }
                        }
                        else if (lessThanPropertyType == typeof(decimal))
                        {
                            if (decimal.TryParse(value, out decimal _decimalValue))
                            {
                                lessThanBody = Expression.LessThan(lessThanProperty,
                                 Expression.Convert(Expression.Constant(_decimalValue), lessThanPropertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(lessThanBody, lessThanParameter);
                            }
                        }
                        else if (lessThanPropertyType == typeof(bool))
                        {
                            if (value.ToLower() == "isTrue")
                            {
                                lessThanBody = Expression.LessThan(lessThanProperty,
                                Expression.Convert(Expression.Constant(true), lessThanPropertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(lessThanBody, lessThanParameter);
                            }
                            else
                            {
                                lessThanBody = Expression.LessThan(lessThanProperty,
                                Expression.Convert(Expression.Constant(false), lessThanPropertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(lessThanBody, lessThanParameter);
                            }

                        }
                        else if (lessThanPropertyType == typeof(DateTime))
                        {
                            if (DateTime.TryParse(value, out DateTime _dateTimeValue))
                            {
                                lessThanBody = Expression.LessThan(lessThanProperty,
                                Expression.Convert(Expression.Constant(_dateTimeValue), lessThanPropertyType));
                                return Expression.Lambda<Func<TDataItem, bool>>(lessThanBody, lessThanParameter);
                            }
                        }
                        throw new InvalidOperationException("provided expression can not be processes as it is of incorrect type...");


                       

                    default:
                        return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }    
        }


        // STRING
        public static Expression<Func<TDataItem, string>> buildStringOrdeByExpression(string propertyName)
        {
            var arg = Expression.Parameter(typeof(TDataItem), "x");
            var property = Expression.Property(arg, propertyName);
            //return the property as object
            var conv = Expression.Convert(property, typeof(string));
            var exp = Expression.Lambda<Func<TDataItem, string>>(conv, new ParameterExpression[] { arg });
            return exp;
        }

        // BOOL
        public static Expression<Func<TDataItem, bool>> buildBoolOrdeByExpression(string propertyName)
        {
            var arg = Expression.Parameter(typeof(TDataItem), "x");
            var property = Expression.Property(arg, propertyName);
            //return the property as object
            var conv = Expression.Convert(property, typeof(bool));
            var exp = Expression.Lambda<Func<TDataItem, bool>>(conv, new ParameterExpression[] { arg });
            return exp;
        }

        // DateTime
        public static Expression<Func<TDataItem, DateTime>> buildDateTimeOrdeByExpression(string propertyName)
        {
            var arg = Expression.Parameter(typeof(TDataItem), "x");
            var property = Expression.Property(arg, propertyName);
            //return the property as object
            var conv = Expression.Convert(property, typeof(DateTime));
            var exp = Expression.Lambda<Func<TDataItem, DateTime>>(conv, new ParameterExpression[] { arg });
            return exp;
        } 

        // INT
        public static Expression<Func<TDataItem, int>> buildIntOrdeByExpression(string propertyName)
        {
            var arg = Expression.Parameter(typeof(TDataItem), "x");
            var property = Expression.Property(arg, propertyName);
            //return the property as object
            var conv = Expression.Convert(property, typeof(int));
            var exp = Expression.Lambda<Func<TDataItem, int>>(conv, new ParameterExpression[] { arg });
            return exp;
        }

        // DECIMAL
        public static Expression<Func<TDataItem, decimal>> buildDecimalOrdeByExpression(string propertyName)
        {
            var arg = Expression.Parameter(typeof(TDataItem), "x");
            var property = Expression.Property(arg, propertyName);
            //return the property as object
            var conv = Expression.Convert(property, typeof(decimal));
            var exp = Expression.Lambda<Func<TDataItem, decimal>>(conv, new ParameterExpression[] { arg });
            return exp;
        }
    }
}
