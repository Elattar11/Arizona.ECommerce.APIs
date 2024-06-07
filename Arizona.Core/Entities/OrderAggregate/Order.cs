using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        private Order()
        {
            //For EF Core
        }
        public Order(string buyerEmail, OrderAddress shippingAddress, DeliveryMethod? deliveryMethod, ICollection<OrderItems> items, decimal subTotal , string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentInternId = paymentIntentId;
        }

        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress ShippingAddress { get; set; } = null!;

        public DeliveryMethod? DeliveryMethod { get; set; } = null!; //Navigational Property [ONE]

        public ICollection<OrderItems> Items { get; set; } = new HashSet<OrderItems>(); //Navigational Property [MANY]

        public decimal SubTotal { get; set; }

        //[NotMapped]
        //public decimal Total => SubTotal + DeliveryMethod.Cost;

        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;

        public string PaymentInternId { get; set; } 


    }
}
