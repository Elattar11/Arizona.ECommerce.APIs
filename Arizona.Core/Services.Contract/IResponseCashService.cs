using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Core.Services.Contract
{
    public interface IResponseCashService
    {
        Task CashResponseAsync(string key, object Response , TimeSpan timeToLife);

        Task<string?> GetCashedResponseAsync(string key); 
    }
}
