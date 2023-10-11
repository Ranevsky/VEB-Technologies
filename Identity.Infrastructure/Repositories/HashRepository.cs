using Identity.Application.Repositories.Interfaces;
using Identity.Domain.Entities;
using Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

internal class HashRepository : IHashRepository
{
    private readonly ApplicationContext _db;

    public HashRepository(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<HashSettingsEntity?> GetAsync(
        HashSettingsEntity settings,
        CancellationToken cancellationToken = default)
    {
        var argonSettingDb = await _db.HashSettings
            .AsTracking()
            .Where(setting =>
                setting.Iterations == settings.Iterations &&
                setting.MemorySize == settings.MemorySize &&
                setting.DegreeOfParallelism == settings.DegreeOfParallelism &&
                setting.HashLength == settings.HashLength)
            .FirstOrDefaultAsync(cancellationToken);

        return argonSettingDb;
    }
}