using SweetLemons.DTO;
using SweetLemons.Entities;
using SweetLemons.Infrastructure;

namespace SweetLemons.Api;

public class Mutation
{
    public async Task<ProductCreated> CreateProductAsync(SweetLemonsContext context, NewProduct product)
    {
        var domainProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            Price = product.Price
        };

        context.Products.Add(domainProduct);

        await context.SaveChangesAsync();

        return new ProductCreated
        {
            Id = domainProduct.Id
        };
    }
}