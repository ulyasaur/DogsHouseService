using DogsHouseService.DAL.Models.Common;
using System.Linq.Dynamic.Core;

namespace DogsHouseService.DAL.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByParams<T>(this IQueryable<T> query, SortingParams sortingParams)
        {
            if (string.IsNullOrEmpty(sortingParams.Attribute)
                || string.IsNullOrEmpty(sortingParams.SortingOrder))
            {
                return query;
            }

            if (sortingParams.SortingOrder != OrderNames.Descending
                && sortingParams.SortingOrder != OrderNames.Ascending)
            {
                return query;
            }

            if (!query.Any())
            {
                return query;
            }

            Type queryType = typeof(T);
            var propertyInfo = queryType
                .GetProperties()
                .FirstOrDefault(p => p.Name.ToLower() == sortingParams.Attribute.ToLower());

            if (propertyInfo is null)
            {
                return query;
            }

            return query.OrderBy($"{sortingParams.Attribute} {sortingParams.SortingOrder}");
        }
    }
}
