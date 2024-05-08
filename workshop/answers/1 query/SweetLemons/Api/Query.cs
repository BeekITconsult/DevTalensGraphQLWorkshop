using Microsoft.EntityFrameworkCore;
using SweetLemons.Entities;
using SweetLemons.Infrastructure;

namespace SweetLemons.Api;

public class Query
{
    [UseProjection]
    public IQueryable<Customer> Customers(SweetLemonsContext context) => context.Customers.AsNoTracking().AsQueryable();

    [UseProjection]
    public IQueryable<Order> Orders(SweetLemonsContext context) => context.Orders.AsNoTracking().AsQueryable();

    [UseProjection]
    public IQueryable<Product> Products(SweetLemonsContext context) => context.Products.AsNoTracking().AsQueryable();
}