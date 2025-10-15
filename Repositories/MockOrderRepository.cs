using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

// mock data to simulate users, will be replaced by ODBC connection to King
public class MockOrderRepository : IOrderRepository
{
    private static readonly IReadOnlyList<Order> _orders =
        [
        new Order { Id = 1, Status = "In productie", Note = "Let op! speciale afwerking", Amount = 40, CreatedBy = "Frank", MadeBy = "Pietje" },
        new Order { Id = 2, Status = "Afgerond", Note = "Standaard order", Amount = 100, CreatedBy = "Marie-Louise", MadeBy = "Thomas" },
        new Order { Id = 3, Status = "In productie", Note = "Spoed order", Amount = 20, CreatedBy = "Frank", MadeBy = "Patrick" },
        new Order { Id = 4, Status = "Afgerond", Note = "Let op! speciale afwerking", Amount = 10, CreatedBy = "Marie-Louise", MadeBy = "Pietje" },
        new Order { Id = 5, Status = "In productie", Note = "Controle vereist", Amount = 75, CreatedBy = "Frank", MadeBy = "Thomas" },
        new Order { Id = 6, Status = "Afgerond", Note = "Standaard order", Amount = 50, CreatedBy = "Marie-Louise", MadeBy = "Patrick" },
        new Order { Id = 7, Status = "Geannuleerd", Note = "Wacht op materiaal", Amount = 30, CreatedBy = "Frank", MadeBy = "Pietje" },
        new Order { Id = 8, Status = "In productie", Note = "Order ingetrokken door klant", Amount = 15, CreatedBy = "Marie-Louise", MadeBy = "Thomas" },
        new Order { Id = 9, Status = "In productie", Note = "Extra kwaliteitscontrole vereist", Amount = 60, CreatedBy = "Frank", MadeBy = "Patrick" },
        new Order { Id = 10, Status = "Gereserveerd", Note = "Spoedorder op tijd afgerond", Amount = 25, CreatedBy = "Marie-Louise", MadeBy = "Pietje" }
    ];

    // get all orders
    public Task<IReadOnlyList<Order>> GetOrdersAsync(CancellationToken ct = default)
         => Task.FromResult(_orders);

}