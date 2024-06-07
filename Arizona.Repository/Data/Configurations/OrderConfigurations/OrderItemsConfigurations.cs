using Arizona.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Infrastructure.Data.Configurations.OrderConfigurations
{
    internal class OrderItemsConfigurations : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.OwnsOne(orderItem => orderItem.Product, Product => Product.WithOwner());

            builder.Property(orderItem => orderItem.Price)
                .HasColumnType("decimal(12 , 2)");
        }
    }
}
