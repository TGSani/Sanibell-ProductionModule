using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

// mock data to simulate users, will be replaced by ODBC connection to King
public class MockOrderRepository : IOrderRepository
{
    private static readonly IReadOnlyList<Order> _orders =
        [
        new Order { Id = 1, Status = "In productie", Note = "Let op! speciale afwerking", Amount = 40, CreatedBy = "Frank", MadeBy = "Pietje" },
        new Order { Id = 2, Status = "Gereed", Note = "Standaard order", Amount = 100, CreatedBy = "Marie-Louise", MadeBy = "Thomas" },
        new Order { Id = 3, Status = "In productie", Note = "Spoed order", Amount = 20, CreatedBy = "Frank", MadeBy = "Patrick" },
        new Order { Id = 4, Status = "Gereed", Note = "Let op! speciale afwerking", Amount = 10, CreatedBy = "Marie-Louise", MadeBy = "Pietje" }

    ];

    // get all orders
    public Task<IReadOnlyList<Order>> GetOrdersAsync(CancellationToken ct = default)
         => Task.FromResult(_orders);

    // get order by id
    public Task<Order?> GetOrderByIdAsync(int id, CancellationToken ct = default)
    {
        var order = _orders.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(order);
    }
}