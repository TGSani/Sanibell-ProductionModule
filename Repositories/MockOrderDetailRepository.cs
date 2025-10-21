using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

public class MockOrderDetailRepository : IOrderDetailRepository
{
    private static readonly IReadOnlyList<OrderDetail> _orderDetails = new List<OrderDetail>
    {
        new OrderDetail { Id = 1, Recept = "Wastafel, Interessante woorden", Date = DateTime.Parse("12-02-2015"), Aantal = 4, Urgent = true },
        new OrderDetail { Id = 2, Recept = "Kraan, Speciaal", Date = DateTime.Parse("15-03-2015"), Aantal = 2, Urgent = false }
    };

    public Task<IReadOnlyList<OrderDetail>> GetDetailByIdAsync(int id, CancellationToken ct = default)
    {
        IReadOnlyList<OrderDetail> details = _orderDetails
            .Where(od => od.Id == id)
            .ToList()
            .AsReadOnly();

        return Task.FromResult(details);
    }
}
