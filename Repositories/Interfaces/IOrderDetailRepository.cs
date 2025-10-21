using Sanibell_ProductionModule.Models;

namespace Sanibell_ProductionModule.Repositories.Interfaces;

public interface IOrderDetailRepository
{
    Task<IReadOnlyList<OrderDetail>> GetDetailByIdAsync(int id, CancellationToken ct = default);
}
