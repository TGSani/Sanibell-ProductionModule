using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services.Interfaces;

namespace Sanibell_ProductionModule.Services;

public class MockUsersService : IUsersService
{
    // mock data om users te simuleren, dit komt later uit het ERP systeem
    private readonly List<Users> _Users = new()
    {
        new Users { Id = 1, Name = "Admin", Role = "Admin", QRcode = "111"},
        new Users { Id = 2, Name = "Pietje", Role = "User", QRcode = "222"},
        new Users { Id = 3, Name = "Frank", Role = "Planner", QRcode = "333"},
        new Users { Id = 4, Name = "Thomas", Role = "User", QRcode = "444"},
        new Users { Id = 5, Name = "Sebastiaan", Role = "Planner", QRcode = "555"},
        new Users { Id = 6, Name = "Gerrit", Role = "Admin", QRcode = "666"},
        new Users { Id = 7, Name = "Patrick", Role = "User", QRcode = "777"},
        new Users { Id = 8, Name = "Koen", Role = "Planner", QRcode = "888"},
        new Users { Id = 9, Name = "Tim", Role = "User", QRcode = "999"},
        new Users { Id = 11, Name = "Hendrick-jan", Role = "Tester", QRcode = "112"},
        new Users { Id = 10, Name = "Marie-Louise", Role = "Planner", QRcode = "223"},
        new Users { Id = 12, Name = "QR-Test", Role = "Admin", QRcode = /* QR code opvragen*/"1"}
    };

    public List<Users> GetAllUsers() => _Users;

    public Users? GetById(int id)
    {
        return _Users.FirstOrDefault(u => u.Id == id);
    }
}