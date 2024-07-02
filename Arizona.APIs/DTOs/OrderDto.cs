using Arizona.Core.Entities.OrderAggregate;
using System.ComponentModel.DataAnnotations;

namespace Arizona.APIs.DTOs
{
    public class OrderDto
    {
        
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        public OrderAddressDto ShippingAddress { get; set; }
    }
}
