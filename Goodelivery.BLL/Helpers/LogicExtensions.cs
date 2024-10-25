using Goodelivery.BLL.Services;
using Goodelivery.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Goodelivery.BLL.Helpers;

public static class LogicExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        
        return services;
    }
}