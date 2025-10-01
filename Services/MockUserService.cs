using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services.Interfaces;

namespace Sanibell_ProductionModule.Services;

public class MockUsersService : IUsersService
{
    private readonly List<Users> _Users = new()
    {
        new Users { Id = 1, Naam = "Admin", Rol = "Admin", QRcode = "111"},
        new Users { Id = 2, Naam = "Pietje", Rol = "User", QRcode = "222"},
        new Users { Id = 3, Naam = "Frank", Rol = "Planner", QRcode = "333"},
        new Users { Id = 4, Naam = "Thomas", Rol = "User", QRcode = "444"},
        new Users { Id = 5, Naam = "Sebastiaan", Rol = "Planner", QRcode = "555"},
        new Users { Id = 6, Naam = "Gerrit", Rol = "Admin", QRcode = "666"},
        new Users { Id = 7, Naam = "Patrick", Rol = "User", QRcode = "777"},
        new Users { Id = 8, Naam = "Koen", Rol = "Planner", QRcode = "888"},
        new Users { Id = 9, Naam = "Tim", Rol = "User", QRcode = "999"},
        new Users { Id = 11, Naam = "Hendrick-jan", Rol = "Tester", QRcode = "112"},
        new Users { Id = 10, Naam = "Marie-Louise", Rol = "Planner", QRcode = "223"}
    };

    public List<Users> GetAllUsers() => _Users;

    public Users? GetById(int id)
    {
        return _Users.FirstOrDefault(u => u.Id == id);
    }
}