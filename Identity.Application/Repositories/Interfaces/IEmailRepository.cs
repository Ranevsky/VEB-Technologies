namespace Identity.Application.Repositories.Interfaces;

public interface IEmailRepository
{
    Task ConfirmEmailAsync(string id, string confirmCode, CancellationToken cancellationToken = default);
}