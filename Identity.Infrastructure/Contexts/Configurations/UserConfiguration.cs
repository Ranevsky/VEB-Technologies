using EntityFrameworkCoreLibrary.Extensions;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Contexts.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).AddSequentialId();
        
        builder.HasMany(user => user.Tokens).WithOne(token => token.UserEntity);
        builder.HasOne(user => user.PasswordEntity)
            .WithOne()
            .HasForeignKey<PasswordEntity>(password => password.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(user => user.Email)
            .WithOne()
            .HasForeignKey<EmailEntity>(email => email.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.Roles)
            .WithMany(role => role.Users);
    }
}