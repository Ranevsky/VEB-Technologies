using Identity.Application.Models;
using Identity.Domain.Entities;

namespace Identity.Application.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<string> CreateAsync(
        RoleEntity roleEntity, 
        CancellationToken cancellationToken = default);

    Task<ICollection<RoleInfoView>> GetAll(
        CancellationToken cancellationToken = default);

    Task<RoleEntity> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default);
}