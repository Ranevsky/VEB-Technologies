using Identity.Application.Exceptions;
using Identity.Application.Repositories.Interfaces;
using Identity.Domain.Entities;
using Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

internal class EmailRepository : IEmailRepository
{
    private readonly ApplicationContext _db;

    public EmailRepository(ApplicationContext db)
    {
        _db = db;
    }

    public async Task ConfirmEmailAsync(string id, string confirmCode, CancellationToken cancellationToken = default)
    {
        var email = await _db.Emails
                        .AsNoTracking()
                        .Select(email => new EmailEntity { Id = email.Id, ConfirmCode = email.ConfirmCode })
                        .FirstOrDefaultAsync(email => email.Id == id, cancellationToken)
                    ?? throw new EmailNotFoundByIdException(id);

        if (email.ConfirmCode != confirmCode)
        {
            throw new EmailConfirmationCodeIncorrectException();
        }

        email.ConfirmCode = null;
        _db.Attach(email).Property(entity => entity.ConfirmCode).IsModified = true;

        await _db.SaveChangesAsync(cancellationToken);
    }
}