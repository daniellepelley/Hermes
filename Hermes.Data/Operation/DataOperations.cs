using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Hermes.Data.Operation
{
    public static class DataOperations
    {
        public static IQueryable<T> GetData<T>(this IQueryable<T> source, DataOperator dataOperator)
        {
            foreach (var dataFilter in dataOperator.Filters)
            {
                if (!string.IsNullOrEmpty(dataFilter.FilterProperty))
                {
                    var result = DynamicEquals<T>(dataFilter.FilterProperty, dataFilter.FilterOperator, dataFilter.FilterValue);
                    source = source.Where(result).AsQueryable();
                }                
            }

            var orderBy = dataOperator.OrderBys.FirstOrDefault();

            if (orderBy != null &&
                !string.IsNullOrEmpty(orderBy.SortProperty))
            {
                source = orderBy.SortDirection == "DESC"
                    ? source.OrderByDescending(orderBy.SortProperty)
                    : source.OrderBy(orderBy.SortProperty);
            } 

            foreach (var dataSorter in dataOperator.OrderBys.Skip(1))
            {
                if (!string.IsNullOrEmpty(dataSorter.SortProperty))
                {
                    source = dataSorter.SortDirection == "DESC"
                        ? ((IOrderedQueryable<T>)source).ThenByDescending(dataSorter.SortProperty)
                        : ((IOrderedQueryable<T>)source).ThenBy(dataSorter.SortProperty);
                }                
            }

            if (dataOperator.Pager.NumberPerPage > 0)
            {
                if (dataOperator.Pager.PageNumber > 1)
                {
                    source = source.Skip(dataOperator.Pager.NumberPerPage * (dataOperator.Pager.PageNumber - 1));
                }

                source = source.Take(dataOperator.Pager.NumberPerPage);
            }

            return source;
        }

        private static Func<T, bool> DynamicEquals<T>(string propertyName, string filterOperator, object value)
        {
            Type type = typeof (T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            PropertyInfo pi = type.GetProperty(propertyName);
            expr = Expression.Property(expr, pi);
            type = pi.PropertyType;

            Expression expression = null;

            if (filterOperator == "eq")
            {
                expression = Expression.Equal(expr, Expression.Constant(value));
            }
            else if (filterOperator == "gr")
            {
                expression = Expression.GreaterThan(expr, Expression.Constant(value));
            }

            Type delegateType = typeof (Func<,>).MakeGenericType(typeof (T), typeof (bool));
            LambdaExpression lambda = Expression.Lambda(delegateType, expression, arg);

            return (Func<T, bool>) lambda.Compile();
        }

        private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }
        private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        private static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        private static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                          && method.IsGenericMethodDefinition
                          && method.GetGenericArguments().Length == 2
                          && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        } 
    }
}