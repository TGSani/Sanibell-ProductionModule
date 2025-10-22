using Sanibell_ProductionModule.Models;

namespace Sanibell_ProductionModule.Repositories.Interfaces;

public interface IUsersRepository
{
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default);
    Task<User?> GetByIdAsync(int id, CancellationToken ct = default);
}