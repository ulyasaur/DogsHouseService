using DogsHouseService.DAL.Models;
using DogsHouseService.DAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.DAL.Abstractions
{
    public interface IDogsRepository
    {
        Task InsertDogAsync(Dog dog);

        Task<Dog?> FindDogByExpressionAsync(Expression<Func<Dog, bool>> predicate);

        Task<List<Dog>> GetDogsAsync(PagingParams pagingParams, SortingParams sortingParams);
    }
}
