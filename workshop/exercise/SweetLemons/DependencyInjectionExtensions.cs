using Microsoft.EntityFrameworkCore;
using SweetLemons.Api.Infrastructure;

namespace SweetLemons.Api;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .AddDbContext<SweetLemonsContext>(options => options.UseSqlServer(configuration.GetConnectionString("SweetLemons")));
    }
}