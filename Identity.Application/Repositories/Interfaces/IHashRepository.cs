using Identity.Domain.Entities;

namespace Identity.Application.Repositories.Interfaces;

public interface IHashRepository
{
    Task<HashSettingsEntity?> GetAsync(
        HashSettingsEntity settings,
        CancellationToken cancellationToken = default);
}