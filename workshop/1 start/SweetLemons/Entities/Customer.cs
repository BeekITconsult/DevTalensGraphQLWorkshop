using System.Text.Json.Serialization;

namespace SweetLemons.Api.Entities;

public class Customer
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    [JsonIgnore]
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}