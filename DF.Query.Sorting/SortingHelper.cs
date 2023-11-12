using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DF.Query.Sorting
{
    public static class SortingHelper
    {
        public static IOrderedQueryable<T> AddSorting<T>(this IQueryable<T> query, Models.Requests.Sorting? sorting)
        {
            if (sorting == null)
                return (IOrderedQueryable<T>)query;

            return query.OrderByName(sorting.OrderBy, sorting.Descending);
        }

        public static List<T> AddSorting<T>(this List<T> list, Models.Requests.Sorting? sorting)
        {
            if (sorting == null)
                return list;

            return list.OrderByName(sorting.OrderBy, sorting.Descending);
        }

        private static List<T> OrderByName<T>(this List<T> query, string propertyName, bool desc)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            ParameterExpression x = Expression.Parameter(type, "x");
            MemberExpression propertyExpresion = Expression.Property(x, propertyName);
            var lambda = Expression.Lambda(propertyExpresion, new ParameterExpression[] { x });

            var methodName = desc ? "OrderByDescending" : "OrderBy";
            var queryType = typeof(Queryable);
            var method = queryType.GetMethods()
                .Single(x =>
                {
                    if (x.Name != methodName)
                        return false;

                    if (!x.IsGenericMethodDefinition)
                        return false;

                    var parameters = x.GetParameters().ToList();
                    return parameters.Count == 2;
                });

            MethodInfo methodInfo = method.MakeGenericMethod(type, property.PropertyType);

            return (List<T>)methodInfo.Invoke(methodInfo, new object[] { query, lambda });
        }

        private static IOrderedQueryable<T> OrderByName<T>(this IQueryable<T> query, string propertyName, bool desc)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            ParameterExpression x = Expression.Parameter(type, "x");
            MemberExpression propertyExpresion = Expression.Property(x, propertyName);
            var lambda = Expression.Lambda(propertyExpresion, new ParameterExpression[] { x });

            var methodName = desc ? "OrderByDescending" : "OrderBy";
            var queryType = typeof(Queryable);
            var method = queryType.GetMethods()
                .Single(x =>
                {
                    if (x.Name != methodName)
                        return false;

                    if (!x.IsGenericMethodDefinition) 
                        return false;

                    var parameters = x.GetParameters().ToList();
                    return parameters.Count == 2;
                });

            MethodInfo methodInfo = method.MakeGenericMethod(type, property.PropertyType);

            return (IOrderedQueryable<T>) methodInfo.Invoke(methodInfo, new object[] { query, lambda });
        }
    }
}
