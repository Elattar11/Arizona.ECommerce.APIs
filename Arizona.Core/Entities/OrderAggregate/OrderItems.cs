using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Core.Entities.OrderAggregate
{
    public class OrderItems : BaseEntity
    {
        private OrderItems()
        {
            //For EF Core
        }
        public OrderItems(ProductItemOrder product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder Product { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }


    }
}
