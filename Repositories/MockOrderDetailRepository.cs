using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

public class MockOrderDetailRepository : IOrderDetailRepository
{
    private static readonly IReadOnlyList<OrderDetail> _orderDetails =
        [new OrderDetail
        {
            OrderNummer = 1,
            ReceptCode = "RCP1001",
            Omschrijving = "Product A",
            Aantal = 20,
            EANCode = 8718835105419,
            Components = new List<Component>
            {
                new Component {Naam = "Component X", Aantal = 20, LocatieCode = "LB-01"},
                new Component {Naam = "Compnent Y", Aantal= 10, LocatieCode= "LB-05"}
            }
        },
        new OrderDetail
        {
            OrderNummer = 2,
            ReceptCode = "RCP1002",
            Omschrijving = "Product B",
            Aantal = 50,
            EANCode = 8718835105419,
            Components = new List<Component>
            {
                new Component { Naam = "Component Z", Aantal = 100, LocatieCode = "L003" }
            }
        },
        new OrderDetail
        {
            OrderNummer = 3,
            ReceptCode = "RCP1003",
            Omschrijving = "Product C",
            Aantal = 12,
            EANCode = 8718835105419,
            Components = new List<Component>
            {
                new Component { Naam = "Component W", Aantal = 24, LocatieCode = "L004" },
                new Component { Naam = "Component X", Aantal = 12, LocatieCode = "L001" },
                new Component { Naam = "Component Y", Aantal = 6, LocatieCode = "L002" }
            }
        },
        new OrderDetail
        {
            OrderNummer = 4,
            ReceptCode = "RCP1004",
            Omschrijving = "Product D",
            Aantal = 20,
            EANCode = 8718835105417,
            Components = new List<Component>
            {
                new Component { Naam = "Component Y", Aantal = 23, LocatieCode = "L005"},
                new Component { Naam = "Component X", Aantal = 20, LocatieCode = "L004"},
                new Component { Naam = "Component Z", Aantal = 5, LocatieCode = "L006"}
            }
        }
    ];

    public Task<IReadOnlyList<OrderDetail>> GetDetailByIdAsync(int id, CancellationToken ct = default)
    {
        IReadOnlyList<OrderDetail> Components = _orderDetails
            .Where(od => od.OrderNummer == id)
            .ToList()
            .AsReadOnly();
        return Task.FromResult(Components);
    }
}