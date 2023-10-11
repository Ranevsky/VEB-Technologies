using EntityFrameworkCoreLibrary.Extensions;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Contexts.Configurations;

internal class PasswordConfiguration : IEntityTypeConfiguration<PasswordEntity>
{
    public void Configure(EntityTypeBuilder<PasswordEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).AddSequentialId();
        
        builder.HasOne(password => password.HashSettings)
            .WithMany(setting => setting.Passwords);
    }
}