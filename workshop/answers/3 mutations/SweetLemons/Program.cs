using System.Text.Json.Serialization;
using Books;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using SweetLemons;
using SweetLemons.Api;
using SweetLemons.Entities;
using SweetLemons.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services
    .AddGraphQLServer()
    .AddProjections()
    .AddQueryType<Query>()
    .RegisterDbContext<SweetLemonsContext>()
    .AddFiltering()
    .AddSorting()
    .SetPagingOptions(new PagingOptions
        { MaxPageSize = int.MaxValue - 1, DefaultPageSize = int.MaxValue - 1, IncludeTotalCount = true })
    .AddMutationType<Mutation>()
    .AddMutationConventions();
    ;


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

app.MapPost("/seed", async (SweetLemonsContext context) =>
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

app.MapPost("/johnNeedsMoreLemons", async (SweetLemonsContext context) =>
{
    var order = new Order
    {
        Id = Guid.NewGuid(),
        Customer = context.Customers.First(c => c.Name == "John van Lieshout"),
        OrderLineItems = new List<OrderLineItem>
        {
            new()
            {
                Item = context.Products.First(c => c.Name == "Sweet Lemon"),
                Quantity = 342
            },
        },
    };

    context.Orders.Add(order);

    await context.SaveChangesAsync();
});

app.MapGraphQL();

app.Run();