using Arizona.Core.Entities;
using Arizona.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Core.Services.Contract
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);

        Task<Order? > UpdateOrderStatus(string paymentIntentId , bool isPaid);

    }
}
