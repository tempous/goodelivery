using Goodelivery.DAL.Entities;

namespace Goodelivery.BLL.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> CheckRegionAsync(string region, CancellationToken cancellationToken = default);

    Task<List<Order>?> GetFilteredAsync(string region, DateTime firstDelivery,
        CancellationToken cancellationToken = default);

    Task CreateAsync(double weight, string region, DateTime time, CancellationToken cancellationToken = default);

    Task<bool> ChangeAsync(Guid id, double weight, string region, DateTime time,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}