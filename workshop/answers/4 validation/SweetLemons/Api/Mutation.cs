using Microsoft.EntityFrameworkCore;
using SweetLemons.DTO;
using SweetLemons.Entities;
using SweetLemons.Exceptions;
using SweetLemons.Infrastructure;

namespace SweetLemons.Api;

public class Mutation
{
    [Error<EntityNotFoundException>]
    public async Task<Customer> UpdateCustomerAsync(SweetLemonsContext context, UpdateCustomer customer)
    {
        var current = context
            .Customers
            .Include(x => x.Orders)
            .SingleOrDefault(x => x.Id == customer.Id );

        if (current == null)
        {
            throw new EntityNotFoundException("");
        }

        current.Name = customer.Name;

        await context.SaveChangesAsync();

        return current;
    }


    [Error<ValidationException>]
    public async Task<ProductCreated> CreateProductAsync(SweetLemonsContext context, NewProduct product)
    {
        if (product.Price <= 0)
        {
            throw new ValidationException("Price must be above 0", nameof(product.Price));
        }


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