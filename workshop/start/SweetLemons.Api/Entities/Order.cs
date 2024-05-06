namespace SweetLemons.Api.Entities;

public class Order
{
    public required Guid Id { get; set; }

    public required Customer Customer { get; set; }

    public ICollection<OrderLineItem> OrderLineItems { get; set; } = new List<OrderLineItem>();
}