using Arizona.Core.Entities;
using Arizona.Core.Entities.OrderAggregate;
using Arizona.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Core
{
    public interface IUnitOfWork: IAsyncDisposable 
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;

        Task<int> CompleteAsync();


    }
}
