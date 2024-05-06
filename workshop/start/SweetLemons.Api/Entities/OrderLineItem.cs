namespace SweetLemons.Api.Entities;

public class OrderLineItem
{
    public required Product Item { get; set; }

    public int Quantity { get; set; }
}