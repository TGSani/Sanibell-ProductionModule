using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

// mock data to simulate users, will be replaced by ODBC connection to King
public class MockOrderRepository : IOrderRepository
{
    private static readonly IReadOnlyList<Order> _orders =
        [
        new Order { Id = 1, RcptCode = "PC0001", Status = "In productie", Note = "naam en/of omschrijving eindproduct", Amount = 40, CreatedBy = "Frank", Urgency=false, ProduceBefore = new DateTime(2025,10,22).Date },
        new Order { Id = 2, RcptCode = "PC0003", Status = "Gereserveerd", Note = "naam en/of omschrijving eindproduct", Amount = 100, CreatedBy = "Marie-Louise", Urgency=true, ProduceBefore = new DateTime(2025,10,22).Date },
        new Order { Id = 3, RcptCode = "COMBITOETSB + MUIS", Status = "In productie", Note = "naam en/of omschrijving eindproduct", Amount = 20, CreatedBy = "Frank", Urgency=false, ProduceBefore = new DateTime(2025,10,22).Date },
        new Order { Id = 4, RcptCode = "53B2-3", Status = "Gereserveerd", Note = "naam en/of omschrijving eindproduct", Amount = 10, CreatedBy = "Marie-Louise", Urgency=false, ProduceBefore = new DateTime(2025,10,22).Date },
        new Order { Id = 5, RcptCode = "53B2-3", Status = "In productie", Note = "naam en/of omschrijving eindproduct", Amount = 75, CreatedBy = "Frank", Urgency=false, ProduceBefore = new DateTime(2025,10,22).Date },
        new Order { Id = 6, RcptCode = "53B2-3", Status = "Gereserveerd", Note = "naam en/of omschrijving eindproduct", Amount = 50, CreatedBy = "Marie-Louise", Urgency=true, ProduceBefore = new DateTime(2025,10,22).Date },
        new Order { Id = 7, RcptCode = "53B2-3", Status = "Gereserveerd", Note = "naam en/of omschrijving eindproduct", Amount = 30, CreatedBy = "Frank", Urgency=true, ProduceBefore = new DateTime(2025,10,22).Date },
        new Order { Id = 8, RcptCode = "53B2-3", Status = "In productie", Note = "naam en/of omschrijving eindproduct", Amount = 15, CreatedBy = "Marie-Louise", Urgency=true, ProduceBefore = new DateTime(2025,10,22).Date },
        new Order { Id = 9, RcptCode = "53B2-3", Status = "In productie", Note = "naam en/of omschrijving eindproduct", Amount = 60, CreatedBy = "Frank", Urgency=true, ProduceBefore = new DateTime(2025,10,22).Date },
        new Order { Id = 10, RcptCode = "53B2-3", Status = "Gereserveerd", Note = "naam en/of omschrijving eindproduct", Amount = 25, CreatedBy = "Marie-Louise", Urgency=true, ProduceBefore = new DateTime(2025,10,22).Date }
    ];

    // get all orders
    public Task<IReadOnlyList<Order>> GetOrdersAsync(CancellationToken ct = default)
         => Task.FromResult(_orders);

    public Task<Order?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        return Task.FromResult(order);
    }

}