using System.Reflection;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Contexts;

internal class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<PasswordEntity> Passwords => Set<PasswordEntity>();
    public DbSet<EmailEntity> Emails => Set<EmailEntity>();
    public DbSet<HashSettingsEntity> HashSettings => Set<HashSettingsEntity>();
    public DbSet<TokenEntity> Tokens => Set<TokenEntity>();
    public DbSet<RoleEntity> Roles => Set<RoleEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.Entity<RoleEntity>().HasData(
            new RoleEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = RoleEntity.DefaultRole,
            },
            new RoleEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
            },
            new RoleEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Support",
            },
            new RoleEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = RoleEntity.SuperAdmin,
            });
    }
}