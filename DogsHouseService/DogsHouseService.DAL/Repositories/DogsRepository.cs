using DogsHouseService.DAL.Abstractions;
using DogsHouseService.DAL.Models;
using DogsHouseService.DAL.Models.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public Task<PagedList<Dog>> GetDogsByExpressionAsync(Expression<Func<Dog, bool>> predicate, PagingParams pagingParams)
        {
            IQueryable<Dog> dogs = _dbContext.Dogs.Where(predicate);

            return PagedList<Dog>.CreateAsync(dogs, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task InsertDogAsync(Dog dog)
        {
            _dbContext.Dogs.Add(dog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
