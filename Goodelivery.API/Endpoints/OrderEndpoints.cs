using Goodelivery.API.Requests;
using Goodelivery.BLL.Services.Interfaces;
using MiniValidation;

namespace Goodelivery.API.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("orders").WithOpenApi();

        api.MapGet("", async (IOrderService orderService) => Results.Ok(await orderService.GetAllAsync()));

        api.MapGet("{id:guid}", async (Guid id, IOrderService orderService) =>
        {
            var order = await orderService.GetByIdAsync(id);
            return order != null
                ? Results.Ok(order)
                : Results.NotFound("Такой заказ не существует!");
        });

        api.MapGet("{region}", async (string region, IOrderService orderService) => await orderService.CheckRegionAsync(region)
            ? Results.Ok()
            : Results.NotFound("Район не входит в зону доставки!"));

        api.MapGet("{region}/{firstDelivery}", async (string region, DateTime firstDelivery, IOrderService orderService) =>
        {
            var filteredOrders = await orderService.GetFilteredAsync(region, firstDelivery);
            if (filteredOrders != null)
                return filteredOrders.Any()
                    ? Results.Ok(filteredOrders)
                    : Results.NotFound("В районе пока нет ожидаемых доставок!");

            return Results.BadRequest("Район не входит в зону доставки!");
        });

        api.MapPost("", async (OrderRequest order, IOrderService orderService) =>
        {
            if (!MiniValidator.TryValidate(order, out var errors))
                return Results.ValidationProblem(errors);

            await orderService.CreateAsync(order.Weight, order.Region, order.Time);
            return Results.NoContent();
        });

        api.MapPut("{id:guid}",
            async (Guid id, OrderRequest order, IOrderService orderService) =>
            {
                if (!MiniValidator.TryValidate(order, out var errors))
                    return Results.ValidationProblem(errors);

                return await orderService.ChangeAsync(id, order.Weight, order.Region, order.Time)
                    ? Results.NoContent()
                    : Results.NotFound("Такой заказ не существует!");
            });

        api.MapDelete("{id:guid}", async (Guid id, IOrderService orderService) => await orderService.DeleteAsync(id)
            ? Results.NoContent()
            : Results.NotFound("Такой заказ не существует!"));
    }
}