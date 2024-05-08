namespace SweetLemons.Api.Entities;

public class Product
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public double Price { get; set; }
}