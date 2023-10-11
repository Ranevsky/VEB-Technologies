using Identity.Domain.Entities;

namespace Identity.Application.Services.Interfaces;

public interface IPasswordManager
{
    Task<PasswordEntity> CreatePasswordAsync(string password, CancellationToken cancellationToken = default);
    Task<bool> IsEqualsAsync(PasswordEntity passwordEntityHash, string password, CancellationToken cancellationToken = default);
}