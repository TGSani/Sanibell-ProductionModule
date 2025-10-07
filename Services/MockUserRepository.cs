using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services.Interfaces;

namespace Sanibell_ProductionModule.Services;

// mock data om users te simuleren, dit komt later uit het ERP systeem
public class MockUserRepository : IUsersRepository
{
    private static readonly IReadOnlyList<User> _users =
        [
        new User { Id = 1, Name = "Admin", Role = "Admin", QRcode = "111"},
        new User { Id = 2, Name = "Pietje", Role = "User", QRcode = "222"},
        new User { Id = 3, Name = "Frank", Role = "Planner", QRcode = "333"},
        new User { Id = 4, Name = "Thomas", Role = "User", QRcode = "444"},
        new User { Id = 5, Name = "Sebastiaan", Role = "Planner", QRcode = "555"},
        new User { Id = 6, Name = "Gerrit", Role = "Admin", QRcode = "666"},
        new User { Id = 7, Name = "Patrick", Role = "User", QRcode = "777"},
        new User { Id = 8, Name = "Koen", Role = "Planner", QRcode = "888"},
        new User { Id = 9, Name = "Tim", Role = "User", QRcode = "999"},
        new User { Id = 11, Name = "Hendrick-jan", Role = "User", QRcode = "112"},
        new User { Id = 10, Name = "Marie-Louise", Role = "Planner", QRcode = "223"},
        new User { Id = 12, Name = "QR-Test", Role = "Admin", QRcode = /* QR code opvragen*/"1"}
    ];

    
    public Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
         => Task.FromResult(_users);

    public Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
    {
          var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }

}