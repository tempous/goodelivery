using System.Linq.Expressions;
using Goodelivery.DAL.Entities;
using Goodelivery.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Goodelivery.DAL.Repositories;

internal class OrderRepository(DataContext context) : IOrderRepository
{
    public async Task<List<Order>> FindAllAsync(Expression<Func<Order, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        var query = context.Orders.AsNoTracking().AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        return await query
            .OrderBy(order => order.Region)
            .ThenBy(order => order.Time)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> FindOneAsync(Expression<Func<Order, bool>> filter, CancellationToken cancellationToken = default) => await context.Orders.AsNoTracking().FirstOrDefaultAsync(filter, cancellationToken);

    public async Task<bool> CheckAsync(Expression<Func<Order, bool>> filter, CancellationToken cancellationToken = default) => await context.Orders.AsNoTracking().AnyAsync(filter, cancellationToken);

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Order order, CancellationToken cancellationToken = default)
    {
        context.Orders.Remove(order);
        await context.SaveChangesAsync(cancellationToken);
    }
}