using System;
using System.Linq;
using System.Linq.Expressions;

namespace AeropuertoApp.Infrastructure.Queries
{
    public static class Translation<T> where T : class
    {
        public static Expression<Func<T, TKey>> LambdaExpression<TKey>(string property)
        {
            var argParam = Expression.Parameter(typeof(T), "s");
            var nameProperty = Expression.Property(argParam, property);
            var conversion = Expression.Convert(nameProperty, typeof(TKey));
            var lambda = Expression.Lambda<Func<T, TKey>>(conversion, argParam);
            return lambda;
        }

        public static Expression<Func<T, TKey>> LambdaExpressionChild<TKey>(string properties)
        {
            var propertiesToConvert = properties.Split(QueryFactory.PropertyAndChildSeparator);
            var argParam = Expression.Parameter(typeof(T), "s");
            var child = typeof(T).GetProperty(propertiesToConvert[0]);
            var childProperty = child.PropertyType.GetProperty(propertiesToConvert[1]);
            var inner = Expression.Property(argParam, child);
            var outer = Expression.Property(inner, childProperty);
            var conversion = Expression.Convert(outer, typeof(TKey));
            var lambda = Expression.Lambda<Func<T, TKey>>(conversion, argParam);
            return lambda;
        }

        public static IQueryable<T> SortByType<TKey>(string sort, string sortBy, IQueryable<T> query)
        {
            Expression<Func<T, TKey>> property;
            var isChild = sortBy.Contains(QueryFactory.PropertyAndChildSeparator);
            if (isChild)
            {
                property = LambdaExpressionChild<TKey>(sortBy);
            }
            else
            {
                property = LambdaExpression<TKey>(sortBy);
            }

            if (sort.Equals(QueryConstants.OrderByDescending))
                return query.OrderByDescending(property);

            if (sort.Equals(QueryConstants.OrderByAscending))
                return query.OrderBy(property);

            return null;
        }

        public static Expression<Func<T, bool>> MiddleEntityLambdaExpression(T item)
        {
            var firstPropertyName = typeof(T).GetProperties()[0].Name;
            var firstPropertyValue = item.GetType().GetProperty(firstPropertyName).GetValue(item, null);
            var secondPropertyName = typeof(T).GetProperties()[1].Name;
            var secondPropertyValue = item.GetType().GetProperty(secondPropertyName).GetValue(item, null);
            var paramExpression = Expression.Parameter(typeof(T), "s");
            
            var firstProperty = Expression.Property(paramExpression, firstPropertyName);
            var firstValue = Expression.Parameter(typeof(int), firstPropertyValue.ToString());
            var firstConditionEqual = Expression.Equal(firstProperty, firstValue);

            var secondProperty = Expression.Property(paramExpression, secondPropertyName);
            var secondValue = Expression.Parameter(typeof(int), secondPropertyValue.ToString());
            var secondConditionEqual = Expression.Equal(secondProperty, secondValue);

            var concatConditions = Expression.And(firstConditionEqual, secondConditionEqual);
            Expression conversion = Expression.Convert(concatConditions, typeof(bool));
            var lambda = Expression.Lambda<Func<T, bool>>(conversion, paramExpression);
            return lambda;
        }
    }
}