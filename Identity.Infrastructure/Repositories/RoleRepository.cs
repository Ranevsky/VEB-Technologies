using Identity.Application.Exceptions;
using Identity.Application.Models;
using Identity.Application.Repositories.Interfaces;
using Identity.Domain.Entities;
using Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

internal class RoleRepository : IRoleRepository
{
    private readonly ApplicationContext _db;

    public RoleRepository(ApplicationContext db)
    {
        _db = db;
    }
    
    public async Task<string> CreateAsync(RoleEntity roleEntity, CancellationToken cancellationToken = default)
    {
        var roleNameDb = await _db.Roles
            .AsNoTracking()
            .Select(entity => new { entity.Name })
            .FirstOrDefaultAsync(request => request.Name == roleEntity.Name, cancellationToken: cancellationToken);
        
        if (roleNameDb is not null)
        {
            throw new RoleModelExistException();
        }
        
        await _db.Roles.AddAsync(roleEntity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        
        return roleEntity.Id;
    }

    public async Task<ICollection<RoleInfoView>> GetAll(CancellationToken cancellationToken = default)
    {
        var roles = await _db.Roles
            .AsNoTracking()
            .Select(role => new RoleInfoView
            {
                Id = role.Id,
                Name = role.Name,
            }).ToArrayAsync(cancellationToken: cancellationToken);

        return roles;
    }

    public async Task<RoleEntity> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var role = await _db.Roles
                       .AsNoTracking()
                       .Select(role => new RoleEntity
                       {
                           Id = role.Id,
                           Name = role.Name,
                       })
                       .FirstOrDefaultAsync(role => role.Name == name, cancellationToken: cancellationToken)
                   ?? throw new RoleNotFoundByNameException(name);

        return role;
    }
}