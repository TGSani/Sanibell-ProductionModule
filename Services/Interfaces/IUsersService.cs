using Sanibell_ProductionModule.Models;

namespace Sanibell_ProductionModule.Services.Interfaces;


public interface IUsersService
{
    List<Users> GetAllUsers();
}