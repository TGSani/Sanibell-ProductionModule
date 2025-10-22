using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

public class MockOrderDetailRepository : IOrderDetailRepository
{
    private static readonly IReadOnlyList<OrderDetail> _orderDetails = new List<OrderDetail>
    {
        new OrderDetail { Id = 1, Component="Test",  Locatie="B12-4", Aantal = 4  },
        new OrderDetail { Id = 1, Component="test2", Locatie="B125-1",Aantal = 2  },
        new OrderDetail { Id = 2, Component="Sample",Locatie="C10-3", Aantal = 5  },
        new OrderDetail { Id = 2, Component="Sample2",Locatie="C11-2",Aantal = 1 },
        new OrderDetail { Id = 2, Component="Example",Locatie="D14-5",Aantal = 3  },
        new OrderDetail { Id = 2, Component="Test",  Locatie="B12-4", Aantal = 4  },
        new OrderDetail { Id = 2, Component="test2", Locatie="B125-1",Aantal = 2  },
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