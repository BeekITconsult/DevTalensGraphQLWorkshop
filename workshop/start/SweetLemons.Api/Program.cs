using System.Text.Json.Serialization;
using Books;
using Microsoft.EntityFrameworkCore;
using SweetLemons.Api;
using SweetLemons.Api.Entities;
using SweetLemons.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/orders", async(SweetLemonsContext context) =>
        await context
            .Orders
            .Include(x => x.Customer)
            .Include(o => o.OrderLineItems)
            .ThenInclude(x => x.Item)
            .ToListAsync())
    .WithName("Orders")
    .WithOpenApi();

app.MapPost("/johnNeedsMoreLemons", async (SweetLemonsContext context) =>
{
    var lemon = new Product()
    {
        Id = Guid.NewGuid(),
        Name = "Sweet Lemon",
        Price = 42
    };

    var john = new Customer()
    {
        Id = Guid.NewGuid(),
        Name = "John van Lieshout",
        Orders = new List<Order>(),
    };

    var order = new Order
    {
        Id = Guid.NewGuid(),
        Customer = john,
        OrderLineItems = new List<OrderLineItem>
        {
            new()
            {
                Item = lemon,
                Quantity = 342
            },
        },
    };
    john.Orders.Add(order);

    context.Customers.Add(john);
    context.Products.Add(lemon);
    context.Orders.Add(order);

    await context.SaveChangesAsync();
});

app.Run();