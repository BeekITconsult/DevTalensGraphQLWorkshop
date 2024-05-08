using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SweetLemons.Entities;

namespace SweetLemons.Infrastructure.EntityTypeConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.Orders);

        builder
            .OwnsMany(x => x.OrderLineItems, oli =>
            {
                oli.HasOne(l => l.Item);
            });
    }
}