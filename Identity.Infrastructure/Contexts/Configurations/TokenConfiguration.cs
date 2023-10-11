using EntityFrameworkCoreLibrary.Extensions;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Contexts.Configurations;

internal class TokenConfiguration : IEntityTypeConfiguration<TokenEntity>
{
    public void Configure(EntityTypeBuilder<TokenEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).AddSequentialId();
    }
}