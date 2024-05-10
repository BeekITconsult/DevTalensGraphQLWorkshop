using Microsoft.EntityFrameworkCore;
using SweetLemons.Entities;
using SweetLemons.Infrastructure;

namespace SweetLemons.Api;

public class Query
{
    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Customer> Customers(SweetLemonsContext context) => context.Customers.AsNoTracking().AsQueryable();

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Order> Orders(SweetLemonsContext context) => context.Orders.AsNoTracking().AsQueryable();

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Product> Products(SweetLemonsContext context) => context.Products.AsNoTracking().AsQueryable();
}