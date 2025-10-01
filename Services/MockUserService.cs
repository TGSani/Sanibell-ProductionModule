using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services.Interfaces;

namespace Sanibell_ProductionModule.Services;

public class MockUsersService : IUsersService
{
    private readonly List<Users> _Users = new()
    {
        new Users { Id = 1, Naam = "Admin", Rol = "Admin"},
        new Users { Id = 2, Naam = "Pietje", Rol = "User"},
        new Users { Id = 3, Naam = "Frank", Rol = "Planner"},
        new Users { Id = 4, Naam = "Thomas", Rol = "User"},
        new Users { Id = 5, Naam = "Sebastiaan", Rol = "Planner"}
    };

    public List<Users> GetAllUsers() => _Users;
}