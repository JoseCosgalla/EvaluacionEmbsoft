using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AeropuertoApp.Infrastructure.Integers;
using AeropuertoApp.Infrastructure.Objects;
using AeropuertoApp.Infrastructure.Strings;
using AeropuertoApp.Infrastructure.Validators;

namespace AeropuertoApp.Infrastructure.Queries
{
    public static class QueryFactory
    {
        private const int NumberOfPagesToSubtract = 1;
        public const char PropertyAndChildSeparator = '.';

        public static string SortResolver(this string sort)
        {
            if (sort.IsNotNullOrEmpty())
                return sort.ToUpper();
            return QueryConstants.OrderByAscending;
        }

        public static string SortByResolver<T>(this string sortBy)
        {
            var checkProperty = Validate<T>.Property(sortBy);
            if (checkProperty)
                return sortBy;
            return typeof(T).GetProperties().First().Name;
        }

        private static string PropertyNameResolver<T>(string property)
        {
            if (property.IsNullOrEmpty())
                return typeof(T).GetProperties().First().Name;

            var propertyToValidate = property.Split(PropertyAndChildSeparator);
            var checkProperty = Validate<T>.Property(propertyToValidate[0]);
            if (checkProperty.Equals(false))
                return typeof(T).GetProperties().First().Name;
            
            var isChild = property.Contains(PropertyAndChildSeparator);
            if (isChild)
            {
                var parentClass = typeof(T);
                var childClass = parentClass.GetProperties().First(prop => prop.Name.Equals(propertyToValidate[0], StringComparison.OrdinalIgnoreCase));
                var propChild = childClass.PropertyType;
                var propChilds = propChild.GetProperties();
                var childClassProperty = propChilds.FirstOrDefault(prop => prop.Name.Equals(propertyToValidate[1], StringComparison.OrdinalIgnoreCase));
                if (childClassProperty.IsNull())
                    return typeof(T).GetProperties().First().Name;
            }

            return property;
        }

        private static Type PropertyTypeResolver<T>(string property)
        {
            var propertyToRetrieve = property.Split(PropertyAndChildSeparator);
            var parentClass = typeof(T);
            var parentProperty = parentClass.GetProperties().First(prop => prop.Name.Equals(propertyToRetrieve[0], StringComparison.OrdinalIgnoreCase));
            var propertyType = parentProperty.PropertyType;

            var isChild = property.Contains(PropertyAndChildSeparator);
            if (isChild)
            {
                var childClassProperty = propertyType.GetProperties().First(prop => prop.Name.Equals(propertyToRetrieve[1], StringComparison.OrdinalIgnoreCase));
                return childClassProperty.PropertyType;
            }

            return propertyType;
        }

        public static void RetrieveALanguage<T>(this IEnumerable<T> collection, string property, string language)
        {
            if (!property.IsNullOrEmpty())
            {
                var checkProperty = Validate<T>.Property(property);
                if (checkProperty)
                {
                    collection.ToList().ForEach(action => 
                    {
                        PropertyInfo prop = action.GetType().GetProperty(property);
                        prop.SetValue(action, RetrieveOnlyWord(prop.GetValue(action).ToString(), language));
                    });
                }
            }
        }

        public static string RetrieveOnlyPropertyValue(this string name, string language)
        {
            return RetrieveOnlyWord(name, language);
        }
        private static string RetrieveOnlyWord(string name, string language)
        {
            string pattern = "|";
            int startPosition, endPosition;
            if (name.IndexOf(pattern) < 1)
                return name;
            switch (language)
            {
                case "en":
                    startPosition = name.IndexOf(pattern) + 1;
                    endPosition = (name.Length - 1) - startPosition + 1;
                    name = name.Substring(startPosition, endPosition);
                    break;
                case "es":
                    endPosition = name.IndexOf(pattern);
                    name = name.Substring(0, endPosition);
                    break;
            }
            return name;

        }

        public static IQueryable<T> SortByPropertyResolver<T>(string sort, string sortBy, IQueryable<T> query) where T : class
        {
            sort = sort.SortResolver();
            sortBy = PropertyNameResolver<T>(sortBy);
            var propertyType = PropertyTypeResolver<T>(sortBy);

            if (propertyType == typeof(string))
                return Translation<T>.SortByType<string>(sort, sortBy, query);

            if (propertyType == typeof(int))
                return Translation<T>.SortByType<int>(sort, sortBy, query);

            if (propertyType == typeof(DateTime))
                return Translation<T>.SortByType<DateTime>(sort, sortBy, query);

            if (propertyType == typeof(decimal))
                return Translation<T>.SortByType<decimal>(sort, sortBy, query);

            if (propertyType == typeof(bool))
                return Translation<T>.SortByType<bool>(sort, sortBy, query);

            if (propertyType.BaseType == typeof(Enum))
                return Translation<T>.SortByType<Enum>(sort, sortBy, query);

            return null;
        }

        public static IQueryable<T> PaginationResolver<T>(int itemsToShow, int page, IQueryable<T> query)
        {
            if (itemsToShow.IsNotZero() && page.IsNotZero())
                return query.Skip(itemsToShow * (page - NumberOfPagesToSubtract)).Take(itemsToShow);
            return query;
        }

        public static Expression<Func<T, bool>> DeleteMiddleLambdaResolver<T>(this T entityToDelete) where T : class
        {
            return Translation<T>.MiddleEntityLambdaExpression(entityToDelete);
        }
    }
}