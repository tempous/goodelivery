using System.Linq.Expressions;
using Goodelivery.DAL.Entities;

namespace Goodelivery.DAL.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> FindAllAsync(Expression<Func<Order, bool>>? filter = null, CancellationToken cancellationToken = default);
    Task<Order?> FindOneAsync(Expression<Func<Order, bool>> filter, CancellationToken cancellationToken = default);
    Task<bool> CheckAsync(Expression<Func<Order, bool>> filter, CancellationToken cancellationToken = default);
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
    Task RemoveAsync(Order order, CancellationToken cancellationToken = default);
}