using Arizona.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, OrderAddress shippingAddress);

        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);

        Task<Order?> GetOrderByIdForUserAsync(string buyerEmail , int orderId);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
