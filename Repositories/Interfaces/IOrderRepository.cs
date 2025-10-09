using Sanibell_ProductionModule.Models;

namespace Sanibell_ProductionModule.Repositories.Interfaces;


public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetOrdersAsync(CancellationToken ct = default);

    Task<Order?> GetOrderByIdAsync(int id, CancellationToken ct = default);
}