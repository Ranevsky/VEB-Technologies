using Identity.Application.Features.Users.Queries.GetUserCatalog;
using Identity.Application.Models;
using Identity.Domain.Entities;

namespace Identity.Application.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> GetByEmailWithPasswordAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<string> CreateAsync(
        UserEntity userEntity,
        CancellationToken cancellationToken = default);

    Task SetPasswordAsync(
        string userId,
        PasswordEntity newPasswordEntity,
        CancellationToken cancellationToken = default);

    Task<PasswordEntity> GetPasswordAsync(
        string userId,
        CancellationToken cancellationToken = default);
    
    Task<UserInfo> GetUserInfoByIdAsync(
        string userId,
        CancellationToken cancellationToken = default);
    
    Task RemoveByIdAsync(
        string userId,
        CancellationToken cancellationToken = default);

    Task AddRoleAsync(
        string userId,
        string roleId,
        CancellationToken cancellationToken = default);

    Task<UserCatalogViewModel> GetUserCatalogAsync(
        UserCatalogCondition condition,
        CancellationToken cancellationToken = default);
    
    Task UpdateAsync(
        string userId,
        UserUpdateModel updateModel,
        CancellationToken cancellationToken = default);
}