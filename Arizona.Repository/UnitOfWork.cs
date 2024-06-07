using Arizona.Core;
using Arizona.Core.Entities;
using Arizona.Core.Entities.OrderAggregate;
using Arizona.Core.Repositories.Contract;
using Arizona.Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;

        //private Dictionary<string, GenericRepository<BaseEntity>> _repositories;

        private Hashtable _repositories;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _repositories = new Hashtable();
        }
        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var key = typeof(T).Name;

            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<T>(_storeContext);

                _repositories.Add(key, repository);
            }

            return _repositories[key] as IGenericRepository<T>;
            
        }
        public async Task<int> CompleteAsync()
            => await _storeContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _storeContext.DisposeAsync();

        
    }
}
