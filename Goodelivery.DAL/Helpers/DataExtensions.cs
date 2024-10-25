using Goodelivery.DAL.Repositories;
using Goodelivery.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goodelivery.DAL.Helpers;

public static class DataExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(nameof(DataContext))));
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        return services;
    }
}