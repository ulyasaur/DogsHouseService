using DogsHouseService.DAL.Abstractions;
using DogsHouseService.DAL.Extensions;
using DogsHouseService.DAL.Models;
using DogsHouseService.DAL.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DogsHouseService.DAL.Repositories
{
    public class DogsRepository : IDogsRepository
    {
        private readonly ApplicationContext _dbContext;

        public DogsRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dog?> FindDogByExpressionAsync(Expression<Func<Dog, bool>> predicate)
        {
            return await _dbContext.Dogs.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Dog>> GetDogsAsync(PagingParams pagingParams, SortingParams sortingParams)
        {
            IQueryable<Dog> dogs = _dbContext.Dogs;

            if (pagingParams is { PageNumber: not null, PageSize: not null})
            {
                dogs = dogs
                    .Skip((pagingParams.PageNumber!.Value - 1) * pagingParams.PageSize!.Value)
                    .Take(pagingParams.PageSize!.Value);
            }

            if (sortingParams is { Attribute: not null, SortingOrder: not null })
            {
                dogs = dogs.OrderByParams(sortingParams);
            }

            return await dogs.ToListAsync();
        }

        public async Task InsertDogAsync(Dog dog)
        {
            _dbContext.Dogs.Add(dog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
