using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

// mock data to simulate users, will be replaced by ODBC connection to King
public class MockOrderRepository : IOrderRepository
{
    private static readonly IReadOnlyList<Order> _orders =
        [
        new Order { Id = 1, RcptCode = "53B2-3", Status = "In productie", Note = "Let op! speciale afwerking", Amount = 40, CreatedBy = "Frank", ProduceBefore = new DateTime(2025,10,22) },
        new Order { Id = 2, RcptCode = "53B2-3", Status = "Afgerond", Note = "Standaard order", Amount = 100, CreatedBy = "Marie-Louise", ProduceBefore = new DateTime(2025,10,22) },
        new Order { Id = 3, RcptCode = "53B2-3",Status = "In productie", Note = "Spoed order", Amount = 20, CreatedBy = "Frank", ProduceBefore = new DateTime(2025,10,22) },
        new Order { Id = 4, RcptCode = "53B2-3",Status = "Afgerond", Note = "Let op! speciale afwerking", Amount = 10, CreatedBy = "Marie-Louise", ProduceBefore = new DateTime(2025,10,22) },
        new Order { Id = 5, RcptCode = "53B2-3",Status = "In productie", Note = "Controle vereist", Amount = 75, CreatedBy = "Frank", ProduceBefore = new DateTime(2025,10,22) },
        new Order { Id = 6, RcptCode = "53B2-3",Status = "Afgerond", Note = "Standaard order", Amount = 50, CreatedBy = "Marie-Louise", ProduceBefore = new DateTime(2025,10,22) },
        new Order { Id = 7, RcptCode = "53B2-3",Status = "Geannuleerd", Note = "Wacht op materiaal", Amount = 30, CreatedBy = "Frank", ProduceBefore = new DateTime(2025,10,22) },
        new Order { Id = 8, RcptCode = "53B2-3",Status = "In productie", Note = "Order ingetrokken door klant", Amount = 15, CreatedBy = "Marie-Louise", ProduceBefore = new DateTime(2025,10,22) },
        new Order { Id = 9, RcptCode = "53B2-3",Status = "In productie", Note = "Extra kwaliteitscontrole vereist", Amount = 60, CreatedBy = "Frank", ProduceBefore = new DateTime(2025,10,22) },
        new Order { Id = 10, RcptCode = "53B2-3",Status = "Gereserveerd", Note = "Spoedorder op tijd afgerond", Amount = 25, CreatedBy = "Marie-Louise", ProduceBefore = new DateTime(2025,10,22) }
    ];

    // get all orders
    public Task<IReadOnlyList<Order>> GetOrdersAsync(CancellationToken ct = default)
         => Task.FromResult(_orders);

}