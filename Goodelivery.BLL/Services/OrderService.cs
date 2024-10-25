using System.Linq.Expressions;
using Goodelivery.BLL.Services.Interfaces;
using Goodelivery.DAL.Entities;
using Goodelivery.DAL.Repositories.Interfaces;

namespace Goodelivery.BLL.Services;

internal class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public Task<List<Order>> GetAllAsync(CancellationToken cancellationToken = default) => orderRepository.FindAllAsync(cancellationToken: cancellationToken);

    public Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => orderRepository.FindOneAsync(order => order.Id == id, cancellationToken);

    public Task<bool> CheckRegionAsync(string region, CancellationToken cancellationToken = default) => orderRepository.CheckAsync(order => order.Region.Equals(region), cancellationToken);

    public async Task<List<Order>?> GetFilteredAsync(string region, DateTime firstDelivery,
        CancellationToken cancellationToken = default)
    {
        if (await CheckRegionAsync(region, cancellationToken))
        {
            Expression<Func<Order, bool>> filter = order => order.Region.Equals(region)
                                                            && order.Time > firstDelivery
                                                            && order.Time <= firstDelivery.AddMinutes(30);

            return await orderRepository.FindAllAsync(filter, cancellationToken);
        }

        return null;
    }

    public async Task CreateAsync(double weight, string region, DateTime time,
        CancellationToken cancellationToken = default) =>
        await orderRepository.AddAsync(new Order { Weight = weight, Region = region, Time = time }, cancellationToken);

    public async Task<bool> ChangeAsync(Guid id, double weight, string region, DateTime time,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var order = await GetByIdAsync(id, cancellationToken);
            if (order == null)
                throw new ArgumentNullException(nameof(id));

            order.Weight = weight;
            order.Region = region;
            order.Time = time;

            await orderRepository.UpdateAsync(order, cancellationToken);
            return true;
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var order = await GetByIdAsync(id, cancellationToken);
            if (order == null)
                throw new ArgumentNullException(nameof(id));

            await orderRepository.RemoveAsync(order, cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}