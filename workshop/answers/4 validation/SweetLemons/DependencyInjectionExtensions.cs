using Microsoft.EntityFrameworkCore;
using SweetLemons.Infrastructure;

namespace SweetLemons;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .AddDbContext<SweetLemonsContext>(options => options.UseSqlServer(configuration.GetConnectionString("SweetLemons")));
    }
}