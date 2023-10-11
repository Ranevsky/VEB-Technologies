using Identity.Application.Exceptions;
using Identity.Application.Features.Users.Queries.GetUserCatalog;
using Identity.Application.Features.Users.Queries.GetUserCatalog.Models;
using Identity.Application.Models;
using Identity.Application.Repositories.Interfaces;
using Identity.Domain.Entities;
using Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using ResultLibrary.Models.CatalogInfo;

namespace Identity.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationContext _db;
    private readonly IHashRepository _hashRepository;
    private readonly IRoleRepository _roleRepository;

    public UserRepository(
        ApplicationContext db,
        IHashRepository hashRepository,
        IRoleRepository roleRepository)
    {
        _db = db;
        _hashRepository = hashRepository;
        _roleRepository = roleRepository;
    }

    public async Task<UserEntity> GetByEmailWithPasswordAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var user = await _db.Users
                       .AsNoTracking()
                       .Include(user => user.Email)
                       .Include(user => user.PasswordEntity)
                       .ThenInclude(password => password.HashSettings)
                       .Include(user => user.Roles)
                       .FirstOrDefaultAsync(user => user.Email.Email == email, cancellationToken)
                   ?? throw new UserNotFoundByEmailException(email);

        return user;
    }
    
    public async Task<UserInfo> GetUserInfoByIdAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _db.Users
                       .AsNoTracking()
                       .Include(user => user.Roles)
                       .Select(user => new UserInfo
                       {
                           Id = user.Id,
                           Name = user.Name,
                           Email = user.Email.Email,
                           Age = user.Age,
                           Roles = user.Roles.Select(role => role.Name).ToArray(),
                       })
                       .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken)
                   ?? throw new UserNotFoundByIdException(userId);

        return user;
    }

    public async Task RemoveByIdAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _db.Users
                       .AsNoTracking()
                       .Select(user => new UserEntity
                       {
                           Id = user.Id
                       })
                       .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken: cancellationToken)
                   ?? throw new UserNotFoundByIdException(userId);

        _db.Entry(user).State = EntityState.Deleted;
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRoleAsync(
        string userId,
        string roleId,
        CancellationToken cancellationToken = default)
    {
        var user = await _db.Users
                       .AsTracking()
                       .Include(user => user.Roles.Where(role => role.Id == roleId).Take(1))
                       .AsTracking()
                       .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken: cancellationToken)
                   ?? throw new UserNotFoundByIdException(userId);

        if (user.Roles.Count > 0)
        {
            return;
        }

        var role = await _db.Roles
                       .AsTracking()
                       .FirstOrDefaultAsync(role => role.Id == roleId, cancellationToken: cancellationToken)
                   ?? throw new RoleNotFoundByIdException(roleId);
        
        user.Roles.Add(role);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserCatalogViewModel> GetUserCatalogAsync(
        UserCatalogCondition condition,
        CancellationToken cancellationToken = default)
    {
        var query = _db.Users.AsNoTracking();
        
        var filters = condition.Filters;
        if (filters is not null)
        {
            if (filters.Email is not null)
            {
                query = query.Where(user => user.Email.Email.Contains(filters.Email));
            }
            if (filters.Name is not null)
            {
                query = query.Where(user => user.Name.Contains(filters.Name));
            }
            if (filters.Age is not null)
            {
                if (filters.Age.From is not null)
                {
                    query = query.Where(user => user.Age >= filters.Age.From);
                }
                
                if (filters.Age.To is not null)
                {
                    query = query.Where(user => user.Age <= filters.Age.To);
                }
            }

            if (filters.Roles is not null)
            {
                query = filters.Roles.Aggregate(query, (current, role) => 
                    current.Where(user => user.Roles
                        .Select(roleEntity => roleEntity.Name)
                        .Any(roleName => roleName == role)));
            }
        }
        
        var paging = condition.Paging;
        
        var total = await query.LongCountAsync(cancellationToken: cancellationToken);
        ICollection<UserInfo> users;
        if (total == 0)
        {
            users = Array.Empty<UserInfo>();
        }
        else
        {
            var sort = condition.Sort ?? new UserCatalogSort();
            var field = sort.GetField();
            query = sort.Ascending
                ? query.OrderBy(field)
                : query.OrderByDescending(field);

            users = await query
                .Skip((paging.Page - 1) * paging.Take)
                .Take(paging.Take)
                .Select(user => new UserInfo
                {
                    Id = user.Id,
                    Email = user.Email.Email,
                    Age = user.Age,
                    Name = user.Name,
                    Roles = user.Roles.Select(role => role.Name).ToArray(),
                }).ToArrayAsync(cancellationToken: cancellationToken);
        }
        
        var page = new PageInfo
        {
            Limit = paging.Take,
            CurrentPage = paging.Page,
            LastPage = 1,
            Items = users.Count,
        };
        
        var result = new UserCatalogViewModel
        {
            Page = page,
            Total = total,
            Users = users,
        };

        return result;
        
    }

    public async Task UpdateAsync(
        string userId, 
        UserUpdateModel updateModel, 
        CancellationToken cancellationToken = default)
    {
        var user = await _db.Users
                       .AsTracking()
                       .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken: cancellationToken)
                   ?? throw new UserNotFoundByIdException(userId);

        if (updateModel.Age is not null)
        {
            user.Age = updateModel.Age.Value;
        }

        if (updateModel.Name is not null)
        {
            user.Name = updateModel.Name;
        }

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> CreateAsync(
        UserEntity userEntity,
        CancellationToken cancellationToken = default)
    {
        var isExists = await IsExistsAsync(userEntity.Email.Email, cancellationToken);
        if (isExists)
        {
            throw new RoleModelExistException();
        }

        var argonSetting = userEntity.PasswordEntity.HashSettings;

        var argonSettingDb = await _hashRepository.GetAsync(argonSetting, cancellationToken);
        if (argonSettingDb is not null)
        {
            userEntity.PasswordEntity.HashSettings = argonSettingDb;
        }

        var defaultRole = await _roleRepository.GetByNameAsync(RoleEntity.DefaultRole, cancellationToken);

        _db.Attach(defaultRole);
        userEntity.Roles.Add(defaultRole);
        
        await _db.Users.AddAsync(userEntity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return userEntity.Id;
    }

    public async Task SetPasswordAsync(
        string userId,
        PasswordEntity newPasswordEntity,
        CancellationToken cancellationToken = default)
    {
        var passwordDb = await _db.Passwords
                             .AsTracking()
                             .Include(x => x.HashSettings)
                             .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken)
                         ?? throw new UserNotFoundByIdException(userId);


        var hashSettingsDb = await _hashRepository.GetAsync(newPasswordEntity.HashSettings, cancellationToken);
        if (hashSettingsDb is not null)
        {
            newPasswordEntity.HashSettings = hashSettingsDb;
        }

        passwordDb.HashPassword = newPasswordEntity.HashPassword;
        passwordDb.Salt = newPasswordEntity.Salt;
        passwordDb.HashSettings = newPasswordEntity.HashSettings;

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<PasswordEntity> GetPasswordAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        var password = await _db.Passwords
                           .AsNoTracking()
                           .Include(password => password.HashSettings)
                           .Where(password => password.UserId == userId)
                           .Take(1)
                           .FirstOrDefaultAsync(cancellationToken)
                       ?? throw new UserNotFoundByIdException(userId);

        return password;
    }

    private async Task<bool> IsExistsAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var userDb = await _db.Users
            .AsNoTracking()
            .Select(x => x.Email.Email)
            .FirstOrDefaultAsync(emailDb => emailDb == email, cancellationToken);

        return userDb is not null;
    }
}