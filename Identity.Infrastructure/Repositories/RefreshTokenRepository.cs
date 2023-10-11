using Identity.Application.Exceptions;
using Identity.Application.Repositories.Interfaces;
using Identity.Domain.Entities;
using Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

internal class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationContext _db;

    public RefreshTokenRepository(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<TokenEntity> GetAsync(
        string userId,
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var user = await _db.Users
                       .AsNoTracking()
                       .Include(user => user.Tokens.Where(token => token.RefreshToken == refreshToken).Take(1))
                       .Select(user => new { user.Id, Token = user.Tokens.FirstOrDefault() })
                       .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken)
                   ?? throw new UserNotFoundByIdException(userId);

        var token = user.Token ?? throw new TokenNotValidException();

        return token;
    }

    public async Task UpdateAsync(
        string tokenId,
        string refreshToken,
        DateTime expireTime,
        CancellationToken cancellationToken = default)
    {
        var token = await _db.Tokens
                        .AsNoTracking()
                        .Select(token => new TokenEntity
                        {
                            Id = token.Id,
                            RefreshToken = token.RefreshToken,
                            ExpireTime = token.ExpireTime,
                        })
                        .FirstOrDefaultAsync(token => token.Id == tokenId, cancellationToken)
                    ?? throw new TokenNotValidException();

        token.RefreshToken = refreshToken;
        token.ExpireTime = expireTime;

        var attachedEntity = _db.Attach(token);
        attachedEntity.Property(entity => entity.RefreshToken).IsModified = true;
        attachedEntity.Property(entity => entity.ExpireTime).IsModified = true;

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(
        string userId,
        TokenEntity tokenEntity,
        CancellationToken cancellationToken = default)
    {
        var user = await _db.Users
                       .AsNoTracking()
                       .Where(user => user.Id == userId)
                       .Select(entity => new UserEntity { Id = entity.Id, Tokens = new List<TokenEntity>()})
                       .FirstOrDefaultAsync(cancellationToken)
                   ?? throw new UserNotFoundByIdException(userId);
        
        user.Tokens.Add(tokenEntity);

        _db.Attach(user).Collection(entity => entity.Tokens).IsModified = true;
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(
        string userId,
        string tokenId,
        CancellationToken cancellationToken = default)
    {
        var request = await _db.Users
                          .AsNoTracking()
                          .Include(x => x.Tokens.Where(token => token.Id == tokenId).Take(1))
                          .Select(user => new
                          {
                              user.Id,
                              Token = user.Tokens
                                  .Select(token => new TokenEntity { Id = token.Id })
                                  .FirstOrDefault(),
                          })
                          .FirstAsync(user => user.Id == userId, cancellationToken)
                      ?? throw new UserNotFoundByIdException(userId);

        if (request.Token is null)
        {
            throw new TokenNotValidException();
        }

        _db.Tokens.Attach(request.Token).State = EntityState.Deleted;
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAllAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        var request = await _db.Users
                          .AsTracking()
                          .Include(user => user.Tokens)
                          .Select(user => new
                          {
                              user.Id,
                              Tokens = user.Tokens.Select(token => new TokenEntity { Id = token.Id }),
                          })
                          .FirstAsync(user => user.Id == userId, cancellationToken)
                      ?? throw new UserNotFoundByIdException(userId);

        _db.Tokens.RemoveRange(request.Tokens);
        await _db.SaveChangesAsync(cancellationToken);
    }
}