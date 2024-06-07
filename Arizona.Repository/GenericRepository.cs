using Arizona.Core.Entities;
using Arizona.Core.Repositories.Contract;
using Arizona.Core.Specifications;
using Arizona.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _dbContext.Set<T>().ToListAsync();

        
        public async Task<T?> GetAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecifications(spec).ToListAsync();


        public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecifications(spec).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
            => SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);

        public async Task<int> GetCountAsync(ISpecification<T> spec)
            => await ApplySpecifications(spec).CountAsync();

        public void Add(T Entity)
            => _dbContext.Add(Entity);

        public void Update(T Entity)
            => _dbContext.Update(Entity);

        public void Delete(T Entity)
            => _dbContext.Remove(Entity);
    }
}
