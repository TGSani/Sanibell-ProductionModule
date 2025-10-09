using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

// mock data to simulate users, will be replaced by ODBC connection to King
public class MockUserRepository : IUsersRepository
{
    private static readonly IReadOnlyList<User> _users =
        [
        new User { Id = 1, Name = "Admin", Role = "Administrator", QRcode = "111"},
        new User { Id = 2, Name = "Pietje", Role = "Productie medewerker", QRcode = "222"},
        new User { Id = 3, Name = "Frank", Role = "Planner", QRcode = "333"},
        new User { Id = 4, Name = "Thomas", Role = "Productie medewerker", QRcode = "444"},
        new User { Id = 5, Name = "Sebastiaan", Role = "Planner", QRcode = "555"},
        new User { Id = 6, Name = "Gerrit", Role = "Administrator", QRcode = "666"},
        new User { Id = 7, Name = "Patrick", Role = "Productie medewerker", QRcode = "777"},
        new User { Id = 8, Name = "Koen", Role = "Planner", QRcode = "888"},
        new User { Id = 9, Name = "Tim", Role = "Productie medewerker", QRcode = "999"},
        new User { Id = 11, Name = "Hendrick-jan", Role = "Productie medewerker", QRcode = "112"},
        new User { Id = 10, Name = "Marie-Louise", Role = "Planner", QRcode = "223"},
        new User { Id = 12, Name = "QR-Test", Role = "Administrator", QRcode = /* QR code opvragen*/"1"}
    ];
    
    // get all users
    public Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
         => Task.FromResult(_users);

    // get user by id
    public Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }
}