using GeekNotes.Modules.Idp.Domain.ExternalLogins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekNotes.Modules.Idp.Infrastructure.Persistence.Configurations;

internal sealed class ExternalLoginConfiguration
    : IEntityTypeConfiguration<ExternalLogin>
{
    public void Configure(EntityTypeBuilder<ExternalLogin> builder)
    {
        builder.ToTable("ExternalLogins");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                value => ExternalLoginId.Create(value));

        builder.Property(x => x.UserId);

        builder.Property(x => x.Provider);

        builder.Property(x => x.ProviderUserId)
            .HasMaxLength(200);

        builder.Property(x => x.Email)
            .HasMaxLength(320);

        builder.Property(x => x.LinkedOnUtc);

        builder.HasIndex(x => new
        {
            x.Provider,
            x.ProviderUserId
        }).IsUnique();
    }
}