using System.Linq.Expressions;
using ExceptionLibrary;
using Identity.Domain.Entities;

namespace Identity.Application.Features.Users.Queries.GetUserCatalog.Models;

public class UserCatalogSort
{
    public enum Field
    {
        Age,
        Name,
        Email,
    }

    public bool Ascending { get; set; } = true;
    public Field SortField { get; set; } = Field.Name;
    
    private static Expression<Func<UserEntity, object>> GetDefaultField()
    {
        return product => product.Name;
    }

    public Expression<Func<UserEntity, object>> GetField()
    {
        ArgumentValidator.ThrowIfOutOfRange(SortField);
    
        var result = SortField switch
        {
            Field.Age => user => user.Age,
            Field.Email => user => user.Email.Email,
            Field.Name => user => user.Name,
            _ => GetDefaultField(),
        };

        return result;
    }
}