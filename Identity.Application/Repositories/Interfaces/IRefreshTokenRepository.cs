using Identity.Domain.Entities;

namespace Identity.Application.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task<TokenEntity> GetAsync(
        string userId,
        string refreshToken,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        string tokenId,
        string refreshToken,
        DateTime expireTime,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        string userId,
        TokenEntity tokenEntity,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(
        string userId,
        string tokenId,
        CancellationToken cancellationToken = default);

    Task RemoveAllAsync(
        string userId,
        CancellationToken cancellationToken = default);
}