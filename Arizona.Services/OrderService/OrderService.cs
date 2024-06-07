using Arizona.Core;
using Arizona.Core.Entities;
using Arizona.Core.Entities.OrderAggregate;
using Arizona.Core.Repositories.Contract;
using Arizona.Core.Services.Contract;
using Arizona.Core.Specifications.OrderSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Application.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(
                IBasketRepository basketRepo, 
                IUnitOfWork unitOfWork,
                IPaymentService paymentService
            )
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, OrderAddress shippingAddress)
        {
            // 1.Get Basket From Baskets Repo

            var basket = await _basketRepo.GetBasketAsync(basketId);

            // 2. Get Selected Items at Basket From Products Repo

            var orderItems = new List<OrderItems>();

            if (basket?.Items?.Count > 0 )
            {
                var productRepository = _unitOfWork.Repository<Product>();
                foreach (var item in basket.Items)
                {
                    //Get Order by id
                    var product = await productRepository.GetAsync(item.Id);

                    //set product data as item from product was ordered
                    var productItemOrdered = new ProductItemOrder(
                            item.Id,
                            product.Name,
                            product.PictureUrl
                        );

                    //set order item using productItemOrdered
                    var orderItem = new OrderItems(
                            productItemOrdered,
                            product.Price,
                            item.Quantity
                        );


                    //Add the order item to order items list
                    orderItems.Add( orderItem );

                }
            }

            // 3. Calculate SubTotal

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 4. Get Delivery Method From DeliveryMethods Repo

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);

            // 5. Create Order

            var orderRepo = _unitOfWork.Repository<Order>();
            var spec = new OrderWithPaymentIntentSpecifications(basket?.PaymentIntentId);

            var existingOrder = await orderRepo.GetWithSpecAsync(spec);

            if (existingOrder is not null) 
            {
                orderRepo.Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }

            var order = new Order(
                    buyerEmail: buyerEmail,
                    shippingAddress: shippingAddress, 
                    deliveryMethod: deliveryMethod,
                    items: orderItems,
                    subTotal: subTotal,
                    paymentIntentId: basket?.PaymentIntentId ?? ""
                );

            orderRepo.Add(order);

            // 6. Save To Database [TODO]

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;

            return order;

        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var ordersRepo = _unitOfWork.Repository<Order>();

            var spec = new OrderSpecifications(buyerEmail);

            var orders = await ordersRepo.GetAllWithSpecAsync(spec);

            return orders;
        }

        public Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            var orderRepo = _unitOfWork.Repository<Order>();

            var orderSpec = new OrderSpecifications(buyerEmail , orderId);

            var order = orderRepo.GetWithSpecAsync(orderSpec);

            return order;
        }

        
    }
}
